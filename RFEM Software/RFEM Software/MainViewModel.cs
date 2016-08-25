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
        
        private List<ISimViewModel> _ViewModels = new List<ISimViewModel>();
        
        private MainWindow _MainWindow;

        private StateManager _StateManager;

        private SimManager _SimManager = new SimManager();

        private ViewModelFactory _Factory;

        private ViewModelRepository _Repository;

        public MainViewModel(MainWindow mainWindow)
        {
            _MainWindow = mainWindow;

            _StateManager = new StateManager(_SimManager);

            _Factory = new ViewModelFactory(CloseTab,
                                           CmdCloseAllTabs,
                                           _MainWindow.CommandBindings,
                                           _MainWindow.Width - 10);

            _Repository = new ViewModelRepository(_Factory);

            WireMainWindow();

            
        }


        private void WireMainWindow()
        {
            //Set Data Contexts
            _MainWindow.BottomStatusBar.DataContext = _SimManager;
            _MainWindow.mainRibbon.DataContext = _StateManager;
            _MainWindow.tabControl.DataContext = _ViewModels;
            _MainWindow.BottomGrid.DataContext = _SimManager;

            //Wire MainWindow Events
            _MainWindow.Loaded += ReloadOpenTabs;
            _MainWindow.Closing += MainWindowClosing;
            _MainWindow.tabControl.SelectionChanged += ActiveTabChanged;
            _MainWindow.btnClearHistory.Click += ClearSimHistory;

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

            //RFlow2D Events
            _MainWindow.mainRibbon.btnRFlow2dShowField.Click += PopOutGhostViewWindow;
            _MainWindow.mainRibbon.btnRFlow2DShowFlownet.Click += PopOutGhostViewWindow;

            //RFlow3D Events

            //RPill2D Events
            _MainWindow.mainRibbon.btnRPill2DShowField.Click += PopOutGhostViewWindow;
            _MainWindow.mainRibbon.btnRPill2DShowMesh.Click += PopOutGhostViewWindow;

            //RPill3D Events
            _MainWindow.mainRibbon.btnRPill3DShowField.Click += PopOutGhostViewWindow;

            //RSetl2D Events
            _MainWindow.mainRibbon.btnRSetl2DShowField.Click += PopOutGhostViewWindow;
            _MainWindow.mainRibbon.btnRSetl2DShowMesh.Click += PopOutGhostViewWindow;

            //RSetl3D Events
            _MainWindow.mainRibbon.btnRSetl3DShowField.Click += PopOutGhostViewWindow;
            _MainWindow.mainRibbon.btnRSetl3DShowMesh.Click += PopOutGhostViewWindow;

            //RSlope2D Events
            _MainWindow.mainRibbon.btnRSlope2DShowField.Click += PopOutGhostViewWindow;
            _MainWindow.mainRibbon.btnRSlope2DShowMesh.Click += PopOutGhostViewWindow;


        }

        private void ReloadOpenTabs(object sender, RoutedEventArgs e)
        {
            _ViewModels = _Repository.LoadStoredTabs();
            _MainWindow.tabControl.DataContext = null;
            _MainWindow.tabControl.DataContext = _ViewModels;

            if (_ViewModels.Count() > 0 && _ViewModels.Last() != null)
                _MainWindow.tabControl.SelectedItem = _ViewModels.Last();
        }
        private void MainWindowClosing(object sender, CancelEventArgs e)
        {

            _Repository.StoreOpenTabs(_ViewModels);

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
                _StateManager.SetActiveScreen((ISimViewModel)_MainWindow.tabControl.SelectedItem);
                _MainWindow.mainRibbon.RunControlTab.IsSelected = true;
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
                _StateManager.ActiveScreenViewModel.ShowSummaryStats();
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

                    case Program.RFlow2D:

                        var rFlow2DViewModel = (RFlow2DViewModel)_StateManager.ActiveScreenViewModel;

                        switch (ButtonName)
                        {
                            case "btnRFlow2DShowFlownet":
                                rFlow2DViewModel.ShowFlownet();
                                return;
                            case "btnRFlow2dShowField":
                                rFlow2DViewModel.ShowField();
                                return;

                        }
                        break;

                    case Program.RFlow3D:

                        var rFlow3DViewModel = (RFlow3DViewModel)_StateManager.ActiveScreenViewModel;

                        break;

                    case Program.RPill2D:

                        var rPill2DViewModel = (RPill2DViewModel)_StateManager.ActiveScreenViewModel;

                        switch (ButtonName)
                        {
                            case "btnRPill2DShowMesh":
                                rPill2DViewModel.ShowMesh();
                                return;
                            case "btnRPill2DShowField":
                                rPill2DViewModel.ShowField();
                                return;
                        }

                        break;

                    case Program.RPill3D:

                        var rPill3DViewModel = (RPill3DViewModel)_StateManager.ActiveScreenViewModel;

                        switch (ButtonName)
                        {
                            case "btnRPill3DShowField":
                                rPill3DViewModel.ShowField();
                                return;
                        }

                        break;

                    case Program.RSetl2D:

                        var rSetl2DViewModel = (RSetl2DViewModel)_StateManager.ActiveScreenViewModel;

                        switch (ButtonName)
                        {
                            case "btnRSetl2DShowMesh":
                                rSetl2DViewModel.ShowMesh();
                                return;
                            case "btnRSetl2DShowField":
                                rSetl2DViewModel.ShowField();
                                return;
                        }

                        break;

                    case Program.RSetl3D:

                        var rSetl3DViewModel = (RSetl3DViewModel)_StateManager.ActiveScreenViewModel;

                        switch (ButtonName)
                        {
                            case "btnRSetl3DShowMesh":
                                rSetl3DViewModel.ShowMesh();
                                return;
                            case "btnRSetl3DShowField":
                                rSetl3DViewModel.ShowField();
                                return;
                        }

                        break;

                    case Program.RSlope2D:

                        var rSlope2DViewModel = (RSlope2DViewModel)_StateManager.ActiveScreenViewModel;

                        switch (ButtonName)
                        {
                            case "btnRSlope2DShowMesh":
                                rSlope2DViewModel.ShowMesh();
                                return;
                            case "btnRSlope2DShowField":
                                rSlope2DViewModel.ShowField();
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
                switch (_StateManager.ActiveProgram)
                {
                    case Program.RBear2D:
                        var RBearViewModel = (RBear2DViewModel)_StateManager.ActiveScreenViewModel;

                        switch (ButtonName)
                        {
                            case "btnRBear2DShowBearingHist":
                                RBearViewModel.ShowBearingHistTab();
                                return;
                        }
                        break;
                    case Program.RDam2D:
                        var RDamViewModel = (RDam2DViewModel)_StateManager.ActiveScreenViewModel;

                        switch (ButtonName)
                        {
                            case "btnRDamFlowHist":
                                RDamViewModel.ShowFlowRateHist();
                                return;
                            case "btnRDamCondHist":
                                RDamViewModel.ShowConductivityHist();
                                return;
                            case "btnRDamGradHist":
                                RDamViewModel.ShowNodalGradientHist();
                                return;
                        }
                        break;
                }

                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void OpenSettingsTab(object sender, RoutedEventArgs e)
        {
            try
            {
                var wind = new Window() { Content = new SettingsForm() };
                
                wind.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                        AddAndSelectTab(_Factory.CreateViewModel(Program.RBear2D));
                        break;
                    case "btnMRDam2d":
                        AddAndSelectTab(_Factory.CreateViewModel(Program.RDam2D));
                        break;
                    case "btnMREarth2d":
                        AddAndSelectTab(_Factory.CreateViewModel(Program.REarth2D));
                        break;
                    case "btnMRFlow2d":
                        AddAndSelectTab(_Factory.CreateViewModel(Program.RFlow2D));
                        break;
                    case "btnMRFlow3d":
                        AddAndSelectTab(_Factory.CreateViewModel(Program.RFlow3D));
                        break;
                    case "btnMRPill2d":
                        AddAndSelectTab(_Factory.CreateViewModel(Program.RPill2D));
                        break;
                    case "btnMRPill3d":
                        AddAndSelectTab(_Factory.CreateViewModel(Program.RPill3D));
                        break;
                    case "btnMRSetl2d":
                        AddAndSelectTab(_Factory.CreateViewModel(Program.RSetl2D));
                        break;
                    case "btnMRSetl3d":
                        AddAndSelectTab(_Factory.CreateViewModel(Program.RSetl3D));
                        break;
                    case "btnMRSlope2d":
                        AddAndSelectTab(_Factory.CreateViewModel(Program.RSlope2D));
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
            //string filePath = "";
            //bool result = false;
            //Program fileType = _StateManager.ActiveScreenViewModel.Type;

            ////Show dialog and retrieve the file path, as well as the type of data file.
            //var Diag = new Dialogs.ReadDataFileDialog((a, b, c) =>
            //{
            //    filePath = a;
            //    fileType = b;
            //    result = c;
            //});
            //Diag.ShowDialog();

            string filePath = "";
            Program fileType = _StateManager.ActiveScreenViewModel.Type;

            //var fileDialog = new OpenFileDialog();
            //fileDialog.Filter = "Data File|*.dat";

            //if (fileDialog.ShowDialog() == true)
            //{
            //    filePath = fileDialog.FileName;
            //}
            //else
            //{
            //    return;
            //}

            //If the user clicked ok
            //if (result == true)
            //{
            try
            {
                   
                var fileDialog = new OpenFileDialog();
                fileDialog.Filter = "Data File|*.dat";

                if (fileDialog.ShowDialog() == true)
                {
                    filePath = fileDialog.FileName;
                    AddAndSelectTab(_Factory.CreateViewModel(fileType, filePath));
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Processing Data File", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }


            //}
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
        private void AddAndSelectTab(ISimViewModel newViewModel)
        {
            //Add the tab to this window's list of tabs
            _ViewModels.Add(newViewModel);

            //Reset the tab controls data context so it recognizes the new tab
            _MainWindow.tabControl.DataContext = null;
            _MainWindow.tabControl.DataContext = _ViewModels;

            //Select the new tab and set focus. The set focus is required to enable
            //the help command of the context menus
            _MainWindow.tabControl.SelectedItem = newViewModel;
            _MainWindow.tabControl.Focus();
        }

        private void ClearSimHistory(object sender, RoutedEventArgs e)
        {
            try
            {
                _SimManager.ClearSimHistory();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CloseTab(object sender, RoutedEventArgs e)
        {

            MenuItem x = sender as MenuItem;    //The menu item that triggered the event
            TopLevelTabItem owner = null;           //The tab that owns the context menu

            //The currently selected tab, if this tab is being closed, we must show another tab
            TopLevelTabItem tempSelectedItem = (_MainWindow.tabControl.SelectedItem as ISimViewModel).MasterTab as TopLevelTabItem;




            //Checks to see if a menuitem triggered the event
            if (x != null)
            {

                //Attempts to retrieve the tab to be closed from the tab property of the context menu
                owner = ((ContextMenu)x.Parent).Tag as TopLevelTabItem;

                //Checks whether the tag was a tab item
                if (owner != null)
                {
                    ISimViewModel vm = owner.ViewModel;

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

                    //Remove the tab from 
                    _ViewModels.Remove(vm);

                    //Reset datacontext
                    _MainWindow.tabControl.DataContext = null;
                    _MainWindow.tabControl.DataContext = _ViewModels;

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

            //Loop through all open tabs
            foreach (ISimViewModel vm in _ViewModels)
            {
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

            //Delete all tabs
            _ViewModels.Clear();

            //Reset datacontext
            _MainWindow.tabControl.DataContext = null;
            _MainWindow.tabControl.DataContext = _ViewModels;

            return true;
        }
    }
}
