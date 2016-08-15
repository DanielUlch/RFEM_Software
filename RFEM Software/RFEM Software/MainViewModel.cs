using Microsoft.Win32;
using RFEMSoftware.Simulation.Desktop.CustomControls;
using RFEMSoftware.Simulation.Desktop.Forms;
using RFEMSoftware.Simulation.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Media;

namespace RFEMSoftware.Simulation.Desktop
{
    public class MainViewModel
    {
        
        private List<RFEMTabItem> _TabItems = new List<RFEMTabItem>();

        private MainWindow _MainWindow;

        private StateManager _StateManager;

        private SimManager _SimManager = new SimManager();

        private FormFactory _FormFactory;

        private FormRepository _Repository;

        public MainViewModel(MainWindow mainWindow)
        {
            _MainWindow = mainWindow;

            _StateManager = new StateManager(_SimManager);

            _FormFactory = new FormFactory(CloseTab,
                                           CmdCloseAllTabs,
                                           _MainWindow.CommandBindings,
                                           _MainWindow.Width - 10);

            _Repository = new FormRepository(_FormFactory);

            WireMainWindow();

            
        }


        private void WireMainWindow()
        {
            //Set Data Contexts
            _MainWindow.BottomStatusBar.DataContext = _SimManager;
            _MainWindow.mainRibbon.DataContext = _StateManager;
            _MainWindow.tabControl.DataContext = _TabItems;
            _MainWindow.BottomGrid.DataContext = _SimManager;

            //Wire MainWindow Events
            _MainWindow.Loaded += ReloadOpenTabs;
            _MainWindow.Closing += MainWindowClosing;
            _MainWindow.tabControl.SelectionChanged += ActiveTabChanged;

            //Wire Ribbon Events
            _MainWindow.mainRibbon.btn_RunSim.Click += RunSim;
            _MainWindow.mainRibbon.btnShowSummaryStats.Click += ShowSummaryStats;

            _MainWindow.mainRibbon.btnOpenExisting.Click += OpenExistingFile;
            _MainWindow.mainRibbon.NewDataFileRequested += OpenNewFile;
            
            _MainWindow.mainRibbon.btnSave.Click += Save;
            _MainWindow.mainRibbon.qaBtnSave.Click += Save;
            _MainWindow.mainRibbon.btnSaveAs.Click += SaveAs;
            _MainWindow.mainRibbon.qaBtnSaveAs.Click += SaveAs;

            _MainWindow.mainRibbon.btnSettings.Click += OpenSettingsTab;

            //RBear Events
            _MainWindow.mainRibbon.btnRBear2DShowMesh.Click += PopOutGhostViewWindow;
            _MainWindow.mainRibbon.btnRBear2DShowField.Click += PopOutGhostViewWindow;

            _MainWindow.mainRibbon.btnRBear2DShowBearingHist.Click += ShowHistogram;

            //RDam2D Events
            _MainWindow.mainRibbon.btnRDamField.Click += PopOutGhostViewWindow;
            _MainWindow.mainRibbon.btnRDamFlownet.Click += PopOutGhostViewWindow;
            _MainWindow.mainRibbon.btnRDamMeanGradientField.Click += PopOutGhostViewWindow;
            _MainWindow.mainRibbon.btnRDamStdDevGradientField.Click += PopOutGhostViewWindow;
            _MainWindow.mainRibbon.btnRDamMeanFluxField.Click += PopOutGhostViewWindow;
            _MainWindow.mainRibbon.btnRDamStdDevFluxField.Click += PopOutGhostViewWindow;
            _MainWindow.mainRibbon.btnRDamMeanPotentialField.Click += PopOutGhostViewWindow;
            _MainWindow.mainRibbon.btnRDamStdDevPotentialField.Click += PopOutGhostViewWindow;

            _MainWindow.mainRibbon.btnRDamFlowHist.Click += ShowHistogram;
            _MainWindow.mainRibbon.btnRDamCondHist.Click += ShowHistogram;
            _MainWindow.mainRibbon.btnRDamGradHist.Click += ShowHistogram;

            //REarth Events
            _MainWindow.mainRibbon.btnREarth2DShowField.Click += PopOutGhostViewWindow;
            _MainWindow.mainRibbon.btnREarth2DShowMesh.Click += PopOutGhostViewWindow;
        }

        private void ReloadOpenTabs(object sender, RoutedEventArgs e)
        {
            _TabItems = _Repository.LoadStoredTabs();
            _MainWindow.tabControl.DataContext = null;
            _MainWindow.tabControl.DataContext = _TabItems;
            if(_TabItems.Count() > 0 && _TabItems.Last() != null)
                _MainWindow.tabControl.SelectedItem = _TabItems.Last();
        }
        private void MainWindowClosing(object sender, CancelEventArgs e)
        {

            _Repository.StoreOpenTabs(_TabItems);

            //Close all tabs and request saves for data entry files
            if (CloseAllTabs() == false)
            {
                e.Cancel = true;
                return;
            }

            
        }
        private void ActiveTabChanged(object sender, RoutedEventArgs e)
        {
            //Notify State Manager
            if (_MainWindow.tabControl.SelectedItem != null)
            {
                if (((RFEMTabItem)_MainWindow.tabControl.SelectedItem).Type == RFEMTabType.DataInput)
                {
                    _StateManager.SetActiveScreen(((DataEntryTab)_MainWindow.tabControl.SelectedItem).ViewModel);
                }
                else
                {
                    _StateManager.SetActiveScreen(null);
                }
            }
            else
            {
                _StateManager.SetActiveScreen(null);
            }
        }

        private void RunSim(object sender, RoutedEventArgs e)
        {
            try
            {
                //string appFileName = Environment.GetCommandLineArgs()[0];
                //string directory = System.IO.Path.GetDirectoryName(appFileName);

                //directory += "\\DJ_.wav";
                //var x = new SoundPlayer(directory);
                
                //x.Play();

                switch ((RunSimButtonCommand)((RibbonButton)sender).Tag)
                {
                    case RunSimButtonCommand.Run:
                    case RunSimButtonCommand.AddToQueue:
                        _SimManager.AddSimToQueue(_StateManager.ActiveScreenViewModel.Model);
                        return;
                    case RunSimButtonCommand.CancelRun:
                    case RunSimButtonCommand.RemoveFromQueue:
                        _SimManager.CancelSim(_StateManager.ActiveScreenViewModel.Model);
                        return;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        private void ShowSummaryStats(object sender, RoutedEventArgs e)
        {
            try
            {
                //Get the path of the 
                string SummaryFilePath = _StateManager.ActiveScreenViewModel.SummaryFilePath;

                string TabName = _StateManager.ActiveScreenViewModel.BaseName + " - Stats";

                var ParentTab = (DataEntryTab)_MainWindow.tabControl.SelectedItem;

                AddAndSelectTab(_FormFactory.CreateSummaryForm(SummaryFilePath,
                                                               TabName,
                                                               ParentTab));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PopOutGhostViewWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                string ButtonName = ((RibbonButton)sender).Name;
                switch (_StateManager.ActiveProgram)
                {
                    case Program.RBear2D:

                        var RBearViewModel = (RBear2DViewModel)_StateManager.ActiveScreenViewModel;

                        switch (ButtonName)
                        {
                            case "btnRBear2DShowMesh":
                                RBearViewModel.ShowMesh();
                                return;
                            case "btnRBear2DShowField":
                                RBearViewModel.ShowField();
                                return;
                        }
                        break;

                    case Program.RDam2D:

                        var rDam2DViewModel = (RDam2DViewModel)_StateManager.ActiveScreenViewModel;

                        switch (ButtonName)
                        {
                            case "btnRDamField":
                                rDam2DViewModel.ShowField();
                                return;
                            case "btnRDamFlownet":
                                rDam2DViewModel.ShowFlownet();
                                return;
                            case "btnRDamMeanGradientField":
                                rDam2DViewModel.ShowGradientMeanField();
                                return;
                            case "btnRDamStdDevGradientField":
                                rDam2DViewModel.ShowGradientStdDevField();
                                return;
                            case "btnRDamMeanFluxField":
                                rDam2DViewModel.ShowFluxMeanField();
                                return;
                            case "btnRDamStdDevFluxField":
                                rDam2DViewModel.ShowFluxStdDevField();
                                return;
                            case "btnRDamMeanPotentialField":
                                rDam2DViewModel.ShowPotentialMeanField();
                                return;
                            case "btnRDamStdDevPotentialField":
                                rDam2DViewModel.ShowPotentialStdDevField();
                                return;

                        }
                        break;

                    case Program.REarth2D:

                        var rEarth2DViewModel = (REarth2DViewModel)_StateManager.ActiveScreenViewModel;

                        switch (ButtonName)
                        {
                            case "btnREarth2DShowField":
                                rEarth2DViewModel.ShowField();
                                return;
                            case "btnREarth2DShowMesh":
                                rEarth2DViewModel.ShowMesh();
                                return;
                        }
                        break;
                }

                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ShowHistogram(object sender, RoutedEventArgs e)
        {
            try
            {
                string ButtonName = ((RibbonButton)sender).Name;
                var ParentTab = (DataEntryTab)_MainWindow.tabControl.SelectedItem;
                switch (_StateManager.ActiveProgram)
                {
                    case Program.RBear2D:

                        if(ButtonName == "btnRBear2DShowBearingHist")
                        {
                            AddAndSelectTab(_FormFactory.CreateHistogramForm(HistogramType.RBear_Bearing,
                                                                             ParentTab));
                            return;
                        }
                        break;

                    case Program.RDam2D:

                        switch (ButtonName)
                        {
                            case "btnRDamFlowHist":
                                AddAndSelectTab(_FormFactory.CreateHistogramForm(HistogramType.RDam_FlowRate,
                                                                                 ParentTab));
                                return;
                            case "btnRDamCondHist":
                                AddAndSelectTab(_FormFactory.CreateHistogramForm(HistogramType.RDam_Conductivity,
                                                                                 ParentTab));
                                return;
                            case "btnRDamGradHist":
                                AddAndSelectTab(_FormFactory.CreateHistogramForm(HistogramType.RDam_NodeGradient,
                                                                                 ParentTab));
                                return;

                        }
                        break;
                }

                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void OpenSettingsTab(object sender, RoutedEventArgs e)
        {
            //If the tab control contains no settings tabs
            if (!_TabItems.OfType<RFEMTabItem>().Any(p => p.Type == RFEMTabType.Settings))
            {
                //Add a new settings tab
                AddAndSelectTab(_FormFactory.CreateSettingsTab());
            }
            else
            {
                //Select the existing settings tab
                _MainWindow.tabControl.Items.OfType<RFEMTabItem>().
                    Where(p => p.Type == RFEMTabType.Settings).First().IsSelected = true;
            }
        }

        private void OpenNewFile(object sender, RoutedEventArgs e)
        {
            try
            {
                //Resolve Sender
                string ButtonName;
                if (sender.GetType() == typeof(RibbonButton))
                {
                    ButtonName = ((Button)sender).Name;
                }
                else if (sender.GetType() == typeof(RibbonMenuItem))
                {
                    ButtonName = ((RibbonMenuItem)sender).Name;

                }
                else
                {
                    ButtonName = "";
                }


                switch (ButtonName)
                {
                    case "btnMRBear2d":
                        AddAndSelectTab(_FormFactory.CreateNewForm(Program.RBear2D));
                        break;
                    case "btnMRDam2d":
                        AddAndSelectTab(_FormFactory.CreateNewForm(Program.RDam2D));
                        break;
                    case "btnMREarth2d":
                        AddAndSelectTab(_FormFactory.CreateNewForm(Program.REarth2D));
                        break;
                    case "btnMRFlow2d":
                        AddAndSelectTab(_FormFactory.CreateNewForm(Program.RFlow2D));
                        break;
                    case "btnMRFlow3d":
                        AddAndSelectTab(_FormFactory.CreateNewForm(Program.RFlow3D));
                        break;
                    case "btnMRPill2d":
                        AddAndSelectTab(_FormFactory.CreateNewForm(Program.RPill2D));
                        break;
                    case "btnMRPill3d":
                        AddAndSelectTab(_FormFactory.CreateNewForm(Program.RPill3D));
                        break;
                    case "btnMRSetl2d":
                        AddAndSelectTab(_FormFactory.CreateNewForm(Program.RSetl2D));
                        break;
                    case "btnMRSetl3d":
                        AddAndSelectTab(_FormFactory.CreateNewForm(Program.RSetl3D));
                        break;
                    case "btnMRSlope2d":
                        AddAndSelectTab(_FormFactory.CreateNewForm(Program.RSlope2D));
                        break;
                    default:
                        MessageBox.Show("Unknown Sender");
                        throw new ArgumentException();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void OpenExistingFile(object sender, RoutedEventArgs e)
        {
            string filePath = "";
            bool result = false;
            Program fileType = Program.RBear2D;

            //Show dialog and retrieve the file path, as well as the type of data file.
            var Diag = new Dialogs.ReadDataFileDialog((a, b, c) =>
            {
                filePath = a;
                fileType = b;
                result = c;
            });
            Diag.ShowDialog();

            //If the user clicked ok
            if (result == true)
            {
                try
                {
                    AddAndSelectTab(_FormFactory.CreateForm(fileType, filePath));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Processing Data File", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }


            }
        }

        /// <summary>
        /// This method is called when a user clicks the save button on the ribbon quick-access menu.
        /// It saves the data-entry file selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save(object sender, RoutedEventArgs e)
        {
            try
            {
                //If this is a data entry file
                if (_StateManager.ActiveProgram != Program.None)
                {
                    //Save the file
                    _StateManager.ActiveScreenViewModel.Save();
                }
                else
                {
                    MessageBox.Show("Unable to save this document.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// This method is called when a user clicks the save-as button on the ribbon quick-access toolbar.
        /// If the current tab is a data-entry tab, it opens a dialog that attempts to save the file where the user wishes to save it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAs(object sender, RoutedEventArgs e)
        {
            try
            {
                //If the current tab is a data-entry tab
                if (_StateManager.ActiveProgram != Program.None)
                {
                    //Specify dialog options
                    SaveFileDialog diag = new SaveFileDialog();
                    diag.FileName = _StateManager.ActiveScreenViewModel.BaseName;
                    diag.Filter = "Data Files|*.dat|All files|*.*";

                    //Show dialog
                    if (diag.ShowDialog() == true)
                    {
                        //If the user clicks save, save the file at the appropriate location
                        _StateManager.ActiveScreenViewModel.SaveAs(diag.FileName);
                    }

                }
                else
                {
                    MessageBox.Show("Unable to save this document.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// This method adds and selects a new tab.
        /// </summary>
        /// <param name="newTab">
        /// The tab to be added.
        /// </param>
        private void AddAndSelectTab(RFEMTabItem newTab)
        {
            //Add the tab to this window's list of tabs
            _TabItems.Add(newTab);

            //Reset the tab controls data context so it recognizes the new tab
            _MainWindow.tabControl.DataContext = null;
            _MainWindow.tabControl.DataContext = _TabItems;

            //Select the new tab and set focus. The set focus is required to enable
            //the help command of the context menus
            _MainWindow.tabControl.SelectedItem = newTab;
            _MainWindow.tabControl.Focus();
        }


        private void CloseTab(object sender, RoutedEventArgs e)
        {

            MenuItem x = sender as MenuItem;    //The menu item that triggered the event
            RFEMTabItem owner = null;           //The tab that owns the context menu

            //The currently selected tab, if this tab is being closed, we must show another tab
            RFEMTabItem tempSelectedItem = _MainWindow.tabControl.SelectedItem as RFEMTabItem;

            //A viewmodel which, if this is a data entry tab, will tell us whether the data needs to be saved
            ISimViewModel vm = null;


            //Checks to see if a menuitem triggered the event
            if (x != null)
            {

                //Attempts to retrieve the tab to be closed from the tab property of the context menu
                owner = ((ContextMenu)x.Parent).Tag as RFEMTabItem;

                //Checks whether the tag was a tab item
                if (owner != null)
                {
                    //Assigns the viewmodel if the tab is a data input tab
                    if (owner.Type == RFEMTabType.DataInput)
                        vm = ((DataEntryTab)owner).ViewModel;

                    //if the viewmodel exists and changes have been made since the last save
                    if (vm != null && vm.ChangesHaveBeenMade)
                    {
                        //Ask the user if they want to save the changes
                        var result = MessageBox.Show("Changes have been made to the file: '" + vm.BaseName + "'. Would you like to save these changes?",
                                                        "Save Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                        //if yes, save it and coninue, if cancel, exit the routine, if no, continue
                        if (result == MessageBoxResult.Yes)
                        {
                            vm.Save();
                        }
                        else if (result == MessageBoxResult.Cancel)
                        {
                            return;
                        }

                    }

                    //Remove the tab from 
                    ((List<RFEMTabItem>)_MainWindow.tabControl.ItemsSource).Remove(owner);

                    //Reset datacontext
                    _MainWindow.tabControl.DataContext = null;
                    _MainWindow.tabControl.DataContext = _TabItems;

                    //if the tab being displayed was not the tab being closed
                    if (tempSelectedItem != null && tempSelectedItem != owner)
                    {
                        //display the tab that was being displayed when the process began
                        _MainWindow.tabControl.SelectedItem = tempSelectedItem;
                    }
                    else if (_MainWindow.tabControl.Items.Count > 0)
                    {
                        //display the first tab
                        _MainWindow.tabControl.SelectedItem = _MainWindow.tabControl.Items[0];
                    }
                }
            }

        }

        private void CmdCloseAllTabs(object sender, RoutedEventArgs e)
        {
            CloseAllTabs();
        }
        private bool CloseAllTabs()
        {
            ISimViewModel vm = null;

            //Loop through all open tabs
            foreach (RFEMTabItem item in _TabItems)
            {
                //If the tab is a data input tab, check to see if it needs to be saved
                if (item.Type == RFEMTabType.DataInput)
                {
                    //Explicitly convert to dataentrytab and retrieve viewmodel
                    vm = ((DataEntryTab)item).ViewModel;

                    //If the viewmodel exists and changes have been made since the last save
                    if (vm != null && vm.ChangesHaveBeenMade)
                    {
                        //Prompt user to save
                        var result = MessageBox.Show("Changes have been made to the file: '" + vm.BaseName + "'. Would you like to save these changes?",
                                                        "Save Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                        //If the user selects yes, save and continue
                        if (result == MessageBoxResult.Yes)
                        {
                            vm.Save();
                        }
                        //if the user selects cancel, exit the routine
                        else if (result == MessageBoxResult.Cancel)
                        {
                            return false;
                        }

                    }
                }

            }

            //Delete all tabs
            _TabItems.Clear();

            //Reset datacontext
            _MainWindow.tabControl.DataContext = null;
            _MainWindow.tabControl.DataContext = _TabItems;

            return true;
        }
    }
}
