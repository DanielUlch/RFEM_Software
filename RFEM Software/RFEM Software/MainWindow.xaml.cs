using RFEM_Infrastructure;
using RFEM_Software.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;

using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace RFEM_Software
{
    /// <summary>
    /// This window is the main application window. User's create tabs and run simulations 
    /// from within this window.
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        /// <summary>
        /// List of all tab items open in the window.
        /// </summary>
        private List<RFEMTabItem> tabItems;

        /// <summary>
        /// Column close to synchronize layer 1 and layer 2 help pane width. When the help
        /// pane is pinned to layer 1, it must occupy the appropriate space on both layers.
        /// </summary>
        private ColumnDefinition column1CloneForLayer1;

        /// <summary>
        /// Location of the application help file
        /// </summary>
        private string ApplicationHelpLocation = "RFEM_Software.Help_Files.AppHelp.xaml";

        private CancellationTokenSource _TokenSource;
        private bool _CurrentlyRunningSim;

        /// <summary>
        /// Constructor for the window
        /// </summary>
        public MainWindow()
        {
            //Required by framework
            InitializeComponent();

            //Bind the tab control's datacontext to the window's list of tabs
            tabItems = new List<RFEMTabItem>();
            tabControl.DataContext = tabItems;

            //Initialize dummy column to link layer 1 and 2 for the help pane
            column1CloneForLayer1 = new ColumnDefinition();
            column1CloneForLayer1.SharedSizeGroup = "column1";

            this.mainRibbon.btn_RunSim.Click += btnRunSim_Click;
            this.mainRibbon.btnOpenExisting.Click += OpenExistingFile;

            this.mainRibbon.btnShowSummaryStats.Click += btnShowSummaryStats_Click;
            this.mainRibbon.btnShowMesh.Click += btnShowMesh_Click;
            this.mainRibbon.btnShowField.Click += btnShowField_Click;
            this.mainRibbon.btnShowBearingHist.Click += btnShowBearingHist_Click;

            this.mainRibbon.btnSettings.Click += btnSettings_Click;


        }
        /// <summary>
        /// This method is called by the ribbon. The ribbon passes the input form that the
        /// user wishes to display, as well as the header for the new tab. The method creates
        /// and displays the tab.
        /// </summary>
        /// <param name="newTabContent">
        /// The data input form that will be displayed in the new tab.
        /// </param>
        /// <param name="tabName">
        /// The header for the new tab.
        /// </param>
        public void AddNewDataInput(UserControl newTabContent, string tabName)
        {
            var scrollViewer = new ScrollViewer();
            RFEMTabItem NewTab = new RFEMTabItem();
            ContextMenu headerMenu;
            MenuItem menuItem;

            //Deletes the command bindings used to trick the compiler
            newTabContent.CommandBindings.Clear();
            //Binds the form's commands to this window's handlers
            newTabContent.CommandBindings.AddRange(this.CommandBindings);

            //Build context menu for the header
            headerMenu = new ContextMenu();
            headerMenu.Tag = NewTab;

            //Add a close-tab menu item and bind it to this window's handler
            menuItem = new MenuItem();
            menuItem.Header = "Close";
            menuItem.Click += CloseTab;
            headerMenu.Items.Add(menuItem);

            //Add a close-all-tabs menu item and bind it to this window's handler
            menuItem = new MenuItem();
            menuItem.Header = "Close All";
            menuItem.Click += CloseAllTabs;
            headerMenu.Items.Add(menuItem);

            //Build the header
            NewTab.Header = new ContentControl {Content= tabName,
                                                ContextMenu=headerMenu};

            //Signal that the new tab is a data input tab
            NewTab.TabType = RFEMTabType.DataInput;

            newTabContent.Width = this.Width - 10;

            //Place the form into a scrollviewer
            scrollViewer.Content = newTabContent;
            scrollViewer.HorizontalAlignment = HorizontalAlignment.Left;
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            

            //Place the scrollviewer in the new tab
            NewTab.Content = scrollViewer;

            //Add the tab to this window's list of tabs
            tabItems.Add(NewTab);

            //Reset the tab controls data context so it recognizes the new tab
            tabControl.DataContext = null;
            tabControl.DataContext = tabItems;

            //Select the new tab and set focus. The set focus is required to enable
            //the help command of the context menus
            tabControl.SelectedItem = NewTab;
            tabControl.Focus();

        }

        public void AddNewResultsTab(string tabContent, string tabName)
        {
            var scrollViewer = new ScrollViewer();
            RFEMTabItem NewTab = new RFEMTabItem();
            ContextMenu headerMenu;
            MenuItem menuItem;
            
            //Build context menu for the header
            headerMenu = new ContextMenu();
            headerMenu.Tag = NewTab;


            //Add a close-tab menu item and bind it to this window's handler
            menuItem = new MenuItem();
            menuItem.Header = "Close";
            menuItem.Click += CloseTab;
            headerMenu.Items.Add(menuItem);

            //Add a close-all-tabs menu item and bind it to this window's handler
            menuItem = new MenuItem();
            menuItem.Header = "Close All";
            menuItem.Click += CloseAllTabs;
            headerMenu.Items.Add(menuItem);

            //Build the header
            NewTab.Header = new ContentControl
            {
                Content = tabName,
                ContextMenu = headerMenu
            };

            //Signal that the new tab is a results tab
            NewTab.TabType = RFEMTabType.Results;

            //Place the string into a textblock in a scrollviewer
            var tb = new TextBlock();
            tb.Text = tabContent;
            scrollViewer.Content = tb;
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            //Place the scrollviewer in the new tab
            NewTab.Content = scrollViewer;

            //Add the tab to this window's list of tabs
            tabItems.Add(NewTab);

            //Reset the tab controls data context so it recognizes the new tab
            tabControl.DataContext = null;
            tabControl.DataContext = tabItems;

            //Select the new tab and set focus. The set focus is required to enable
            //the help command of the context menus
            tabControl.SelectedItem = NewTab;
            tabControl.Focus();
        }
        private void AddNewHistogramTab(UserControl tabContent, string tabName)
        {
            var scrollViewer = new ScrollViewer();
            RFEMTabItem NewTab = new RFEMTabItem();
            ContextMenu headerMenu;
            MenuItem menuItem;

            //Deletes the command bindings used to trick the compiler
            tabContent.CommandBindings.Clear();
            //Binds the form's commands to this window's handlers
            tabContent.CommandBindings.AddRange(this.CommandBindings);

            //Build context menu for the header
            headerMenu = new ContextMenu();
            headerMenu.Tag = NewTab;

            //Add a close-tab menu item and bind it to this window's handler
            menuItem = new MenuItem();
            menuItem.Header = "Close";
            menuItem.Click += CloseTab;
            headerMenu.Items.Add(menuItem);

            //Add a close-all-tabs menu item and bind it to this window's handler
            menuItem = new MenuItem();
            menuItem.Header = "Close All";
            menuItem.Click += CloseAllTabs;
            headerMenu.Items.Add(menuItem);

            //Build the header
            NewTab.Header = new ContentControl
            {
                Content = tabName,
                ContextMenu = headerMenu
            };

            //Signal that the new tab is a data input tab
            NewTab.TabType = RFEMTabType.Results;

            tabContent.Width = this.Width - 10;

            //Place the form into a scrollviewer
            scrollViewer.Content = tabContent;
            scrollViewer.HorizontalAlignment = HorizontalAlignment.Left;
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;


            //Place the scrollviewer in the new tab
            NewTab.Content = scrollViewer;

            //Add the tab to this window's list of tabs
            tabItems.Add(NewTab);

            //Reset the tab controls data context so it recognizes the new tab
            tabControl.DataContext = null;
            tabControl.DataContext = tabItems;

            //Select the new tab and set focus. The set focus is required to enable
            //the help command of the context menus
            tabControl.SelectedItem = NewTab;
            tabControl.Focus();
        }


        ///NOT IMPLEMENTED YET////////////////////////////////////
        /// <summary>
        /// This method is called from the context menu of a tab header. When implemented,
        /// it will have to pass the targeted tab to this method. This method will also
        /// have to check if saves are needed and prompt the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseTab(object sender, RoutedEventArgs e)
        {
            MenuItem x = sender as MenuItem;
            RFEMTabItem owner = null;
            RFEMTabItem tempSelectedItem = tabControl.SelectedItem as RFEMTabItem;
            if(x != null)
            {
                owner = ((ContextMenu)x.Parent).Tag as RFEMTabItem;
                if(owner!= null)
                {
                  
                    ((List<RFEMTabItem>)tabControl.ItemsSource).Remove(owner);
                    
                    tabControl.DataContext = null;
                    tabControl.DataContext = tabItems;
                    if(tempSelectedItem != null && tempSelectedItem != owner)
                    {
                        tabControl.SelectedItem = tempSelectedItem;
                    }
                }
            }

        }

        ///NOT IMPLEMENTED YET///////////////////////////////////////////
        /// <summary>
        /// This method will close all tabs that are open. It will have to check if each tab
        /// needs to be saved.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseAllTabs(object sender, RoutedEventArgs e)
        {
            tabItems.Clear();
            tabControl.DataContext = null;
            tabControl.DataContext = tabItems;
        }

        /// <summary>
        /// This method checks whether the tab being selected is a data input tab.
        /// If it is, it activates the conditional ribbon tab for run control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabSelectionChanged(object sender, RoutedEventArgs e)
        {
            if(this.tabControl.SelectedItem != null &&
                this.tabControl.SelectedItem.GetType() == typeof(RFEMTabItem))
            {
                if (((RFEMTabItem)this.tabControl.SelectedItem).TabType == RFEMTabType.DataInput){
                    this.mainRibbon.RunTools.Visibility = Visibility.Visible;
                    this.mainRibbon.tabRunControl.IsSelected = true;
                    this.DataContext = ((UserControl)((ScrollViewer)((RFEMTabItem)this.tabControl.SelectedItem).Content).Content).DataContext;
                    
                    var Binding = new Binding("CanDisplaySummaryStats");
                    Binding.Source = this.DataContext;
                    this.mainRibbon.btnShowSummaryStats.SetBinding(RibbonButton.IsEnabledProperty, Binding);

                    Binding = new Binding("CanDisplayMesh");
                    Binding.Source = this.DataContext;
                    this.mainRibbon.btnShowMesh.SetBinding(RibbonButton.IsEnabledProperty, Binding);

                    Binding = new Binding("CanDisplayField");
                    Binding.Source = this.DataContext;
                    this.mainRibbon.btnShowField.SetBinding(RibbonButton.IsEnabledProperty, Binding);

                    Binding = new Binding("CanDisplayBearingHist");
                    Binding.Source = this.DataContext;
                    this.mainRibbon.btnShowBearingHist.SetBinding(RibbonButton.IsEnabledProperty, Binding);
                }
                else
                {
                    this.mainRibbon.RunTools.Visibility = Visibility.Collapsed;
                }
            }
                

        }

        /// <summary>
        /// This method is activated when a user clicks on the pin button
        /// on the help pane. It docks, or undocks the help pane.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpPanePin_Click(object sender, RoutedEventArgs e)
        {
            if (helpPaneButton.Visibility == Visibility.Collapsed)
                UndockPane();
            else
                DockPane();   
        }

        /// <summary>
        /// This method is activated when the user is hovering over the help tab butoon.
        /// It makes the help pane visible, but undocked. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpPaneButton_MouseEnter(object sender, RoutedEventArgs e)
        {
            layer2.Visibility = Visibility.Visible;

        }

        /// <summary>
        /// This method is called when the user hovers over the tab control.
        /// If the help pane button is visible(which means that the help pane is
        /// not currently docked), this method hides the undocked help pane.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layer1_MouseEnter(object sender, RoutedEventArgs e)
        {
            if (helpPaneButton.Visibility == Visibility.Visible)
                layer2.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// This method docks the help pane to layer 1. It also changes the pin
        /// picture in the help pane to an unpin picture.
        /// </summary>
        private void DockPane()
        {
            //Hide help pane tab on the right
            helpPaneButton.Visibility = Visibility.Collapsed;

            //Change pin picture in help pane pin button to unpin picture
            helpPanePinImage.Source = new BitmapImage(new Uri("/Images/Unpin.png", UriKind.Relative));

            //Insert the clone of the help pane column into layer 1
            layer1.ColumnDefinitions.Add(column1CloneForLayer1);
        }

        /// <summary>
        /// This method undocks the help pane from layer 1.
        /// </summary>
        private void UndockPane()
        {
            layer2.Visibility = Visibility.Visible;

            //Show help pane side button
            helpPaneButton.Visibility = Visibility.Visible;

            //Change the unpin picture on the help pin button to a pin picture
            helpPanePinImage.Source = new BitmapImage(new Uri("/Images/Pin.png", UriKind.Relative));

            //Remove the help pane column clone from layer 1
            layer1.ColumnDefinitions.Remove(column1CloneForLayer1);
        }
        
        ///NOT IMPLEMENTED YET//////////////////////////////
        /// <summary>
        /// This method will check to see if tabs need to be persisted, and
        /// prompt the user accordingly.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            Properties.Settings.Default.Save();
            base.OnClosing(e);
            //Store data files in isolated storage
            MessageBox.Show("Conditional ask for save stub");
        }

        ///NOT IMPLEMENTED YET//////////////////////////////////////////////
        /// <summary>
        /// This method will load persisted saved tabs.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            //Load data files from isolated storage
            MessageBox.Show("Load open windows stub");
        }

        /// <summary>
        /// This method tells the context menus that they can execute on the main window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpClick_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        /// <summary>
        /// This method asks the selected form for the help file associated with
        /// the command parameter. The form will return the location of the help file for the
        /// control specified in the command parameter, or a default help file location.
        /// This method then calls LoadReader() which loads the given help file into
        /// the flow document reader in the help pane.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">
        /// Contains a control in its parameter, which is the control that the help
        /// command originated from.
        /// </param>
        private void HelpClick_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Get the form from the selected tab
            var tabContent = (ISimView)((ScrollViewer)((RFEMTabItem)tabControl.SelectedItem).Content).Content;

            //Ask the form for the location of the help file
            string helpLocation = tabContent.HelpLocation((FrameworkElement)e.Parameter);

            //Load the help file into the flowdocuement reader
            LoadReader(helpLocation);
        }

        /// <summary>
        /// This method tells the system that the help command can be handled. This help
        /// command will be run when a user presses F1, and is not the HelpClick command
        /// that is activated when a user activates the help item in a context menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        /// <summary>
        /// This method is activated when a user presses F1. It will ask the currently
        /// selected form if the user is hovering a control. If it is, the form will return
        /// the location of the help file for that control. If not, the form will return 
        ///  the location of a default help file for the form. The help file is then loaded
        /// into the flow document reader in the help pane. If there is no currently selected
        /// form, the main window will load the default help file for the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            //Check if any tabs are open
            if (tabControl.Items.Count > 0)
            {
                //Get the selected tab
                var tab = (RFEMTabItem)tabControl.SelectedItem;

                //If the tab is a data input tab
                if (tab.TabType == RFEMTabType.DataInput)
                {
                    //Get the selected form
                    var helpTab = ((ISimView)((ScrollViewer)tab.Content).Content);

                    //Ask the selected form for the help file associated with a hovered control
                    string helpLocation = helpTab.hoveredHelpDocLocation();

                    //Load the help file
                    LoadReader(helpLocation);

                } else
                {
                    //Load the defaul help file for this application
                    LoadReader(ApplicationHelpLocation);
                }
            }
            else
            {
                //Load the defaul help file for this application
                LoadReader(ApplicationHelpLocation);
            }    
        }

        private void NewHelpClickCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NewHelpClickExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            //Get the form from the selected tab
            var tabContent = (IHistView)((ScrollViewer)((RFEMTabItem)tabControl.SelectedItem).Content).Content;

            //Ask the form for the location of the help file
            string helpLocation = tabContent.helpLocation((string)e.Parameter);

            //Load the help file into the flowdocuement reader
            LoadReaderNew(new Uri(helpLocation, UriKind.Relative));
        }

        /// <summary>
        /// This method takes a help file location string and loads the help file into the
        /// flow document reader in the help pane. It is called by the Help and HelpClick
        /// command handlers.
        /// </summary>
        /// <param name="helpLocation">
        /// The location of the help flow document as an embedded resource.
        /// </param>
        private void LoadReader(string helpLocation)
        {
            //Get the help file as a file stream
            var helpStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(helpLocation);

            //Read the file stream into a flow docuement
            FlowDocument helpContent = System.Windows.Markup.XamlReader.Load(helpStream) as FlowDocument;

            //Display the flow document in the flowdocument reader
            HelpReader.Document = helpContent;

            //If the help pane is not pinned, pin it
            if (helpPaneButton.Visibility == Visibility.Visible)
            {
                layer2.Visibility = Visibility.Visible;
                layer2.ColumnDefinitions[1].Width = new GridLength(200);
                DockPane();
            }
        }

        private void LoadReaderNew(Uri path)
        {
            FlowDocument doc = Application.LoadComponent(path) as FlowDocument;

            if(doc != null)
            {
                HelpReader.Document = doc;

                //If the help pane is not pinned, pin it
                if (helpPaneButton.Visibility == Visibility.Visible)
                {
                    layer2.Visibility = Visibility.Visible;
                    layer2.ColumnDefinitions[1].Width = new GridLength(200);
                    DockPane();
                }
            }
        }
        /// <summary>
        /// External facing method to allow the ribbon to ask for the default help file to be loaded.
        /// This happens when the user presses the help button on the right side of the ribbon.
        /// </summary>
        public void LoadReader()
        {
            LoadReader(ApplicationHelpLocation);
        }

        private async Task RunSimAsync()
        {
            string uriSource;

            this.progressBar.Visibility = Visibility.Visible;
            this.lblSimDetails.Visibility = Visibility.Visible;
            _TokenSource = new CancellationTokenSource();
            var token = _TokenSource.Token;
            _CurrentlyRunningSim = true;

            this.mainRibbon.btn_RunSim.Label = "Cancel Run";
            uriSource = "pack://application:,,,/RFEM Software;component/Images/Cancel.png";
            this.mainRibbon.btn_RunSim.LargeImageSource = new ImageSourceConverter().ConvertFromString(uriSource) as ImageSource;


            try
            {
                var tsk = await ((ISimView)((ScrollViewer)((
                                RFEMTabItem)this.tabControl.SelectedItem).Content).Content).ViewModel.RunSimAsync(token);
                if (!token.IsCancellationRequested)
                {
                    MessageBox.Show(tsk);
                }
                
            }catch(OperationCanceledException oex)
            {
                this.lblStatus.Content = "Run Canelled";
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.mainRibbon.btn_RunSim.Label = "Run Simulation";
                uriSource = "pack://application:,,,/RFEM Software;component/Images/BrokenFactoryBlue.png";
                this.mainRibbon.btn_RunSim.LargeImageSource = new ImageSourceConverter().ConvertFromString(uriSource) as ImageSource;

                _TokenSource = null;
                _CurrentlyRunningSim = false;
                this.progressBar.Visibility = Visibility.Hidden;
                this.lblSimDetails.Visibility = Visibility.Hidden;
            }
            
            
        }
        private async void btnRunSim_Click(object sender, RoutedEventArgs e)
        {
            if (!_CurrentlyRunningSim)
            {
                await RunSimAsync();
            }
            else
            {
                try
                {
                    _TokenSource.Cancel();
                }
                catch (OperationCanceledException oex)
                {
                    this.lblStatus.Content = "Run Canelled";
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    this.mainRibbon.btn_RunSim.Label = "Run Simulation";
                    string uriSource = "pack://application:,,,/RFEM Software;component/Images/BrokenFactoryBlue.png";
                    this.mainRibbon.btn_RunSim.LargeImageSource = new ImageSourceConverter().ConvertFromString(uriSource) as ImageSource;

                    _TokenSource = null;
                    _CurrentlyRunningSim = false;
                    this.progressBar.Visibility = Visibility.Hidden;
                    this.lblSimDetails.Visibility = Visibility.Hidden;
                }
                
            }
        }
        private void btnShowSummaryStats_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string SummaryFilePath = ((ISimViewModel)this.DataContext).SummaryFilePath;
                string SummaryStats;

                using(var reader = new System.IO.StreamReader(SummaryFilePath))
                {
                    SummaryStats = reader.ReadToEnd();
                }

                AddNewResultsTab(SummaryStats, "Results");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnShowMesh_Click(object sender, RoutedEventArgs e)
        {
            try {
                var pInfo = new ProcessStartInfo();
                pInfo.UseShellExecute = true;
                pInfo.FileName = "\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"";
                pInfo.Arguments = "\"" + ((ISimViewModel)this.DataContext).MeshFilePath + "\"";
                pInfo.CreateNoWindow = false;

                var p = new Process { StartInfo = pInfo };
                p.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnShowField_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var pInfo = new ProcessStartInfo();
                pInfo.UseShellExecute = false;
                pInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                            "\\RFEM_Software";
                string appFileDir = Environment.GetCommandLineArgs()[0];
                string displayFilePath = System.IO.Path.GetDirectoryName(appFileDir);
                displayFilePath += "\\Executables\\display.exe";
                pInfo.FileName = displayFilePath;
                pInfo.CreateNoWindow = true;
                pInfo.Arguments = ((ISimViewModel)this.DataContext).FieldFilePath;

                var p = new Process { StartInfo = pInfo };
                p.Start();
                p.WaitForExit();

                pInfo = new ProcessStartInfo();
                pInfo.UseShellExecute = false;
                pInfo.FileName = "\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"";
                pInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                            "\\RFEM_Software";
                pInfo.Arguments = pInfo.WorkingDirectory + "\\graph1.ps";
                pInfo.CreateNoWindow = true;

                p = new Process { StartInfo = pInfo };
                p.Start();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnShowBearingHist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(this.DataContext.GetType() == typeof(RBear2dViewModel))
                {
                    var vm = (RBear2dViewModel)this.DataContext;
                    var form = new RBear2DHistForm((int)vm.NSimulations, vm.NumberOfFootings, vm.BaseName, vm.HistFilePath);
                    AddNewHistogramTab(form, vm.BaseName);
                }
                else
                {
                    throw new NotImplementedException("Histogram for this sim not found.");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        private void OpenExistingFile(object sender, RoutedEventArgs e)
        {
            //var fileDialog = new System.Windows.Forms.OpenFileDialog();

            //fileDialog.Filter = "Data Files|*.dat";

            //if(fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    MessageBox.Show(fileDialog.FileName);
            //}
            string filePath="";
            bool result = false;
            Program fileType=Program.RBear2D;
            var Diag = new Dialogs.ReadDataFileDialog((a,b,c) =>
            {
                filePath = a;
                fileType = b;
                result = c;
            });


            Diag.ShowDialog();

            if (result == true)
            {
                try
                {
                    IHasDataFile formData = FileReader.Read(fileType, filePath);
                    UserControl form = FormBuilder.Build(formData, fileType);
                    AddNewDataInput(form, formData.BaseName);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Processing Data File", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                

            }
            else
            {
                MessageBox.Show("False");
            }
        }
    
        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            if(!tabControl.Items.OfType<RFEMTabItem>().Any(p=> ((RFEMTabItem)p).TabType == RFEMTabType.Settings))
            {
                var scrollViewer = new ScrollViewer();
                RFEMTabItem NewTab = new RFEMTabItem();
                ContextMenu headerMenu;
                MenuItem menuItem;

                //Build context menu for the header
                headerMenu = new ContextMenu();
                headerMenu.Tag = NewTab;

                //Add a close-tab menu item and bind it to this window's handler
                menuItem = new MenuItem();
                menuItem.Header = "Close";
                menuItem.Click += CloseTab;
                headerMenu.Items.Add(menuItem);

                //Add a close-all-tabs menu item and bind it to this window's handler
                menuItem = new MenuItem();
                menuItem.Header = "Close All";
                menuItem.Click += CloseAllTabs;
                headerMenu.Items.Add(menuItem);

                //Build the header
                NewTab.Header = new ContentControl
                {
                    Content = "Settings",
                    ContextMenu = headerMenu
                };

                //Signal that the new tab is a settings tab
                NewTab.TabType = RFEMTabType.Settings;

                //Place the form into a scrollviewer
                scrollViewer.Content = new Forms.SettingsForm();
                scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

                //Place the scrollviewer in the new tab
                NewTab.Content = scrollViewer;

                //Add the tab to this window's list of tabs
                tabItems.Add(NewTab);

                //Reset the tab controls data context so it recognizes the new tab
                tabControl.DataContext = null;
                tabControl.DataContext = tabItems;

                //Select the new tab and set focus. The set focus is required to enable
                //the help command of the context menus
                tabControl.SelectedItem = NewTab;
                tabControl.Focus();
            }
            else
            {
                tabControl.Items.OfType<RFEMTabItem>().Where(p => ((RFEMTabItem)p).TabType == RFEMTabType.Settings).First().IsSelected = true;
            }
            
        }
    }

    /// <summary>
    /// Partial class of the ribbon that is used in the main window. The ribbon contains a 
    /// variety of buttons that trigger events on the main window.
    /// </summary>
    public partial class RFEMRibbon
    {
        
        ///NOT IMPLEMENTED YET////////////////////////////////////
        /// <summary>
        /// This button will allow the user to change various application settings. It may
        /// take them to a menu tab or to the auxillary menu in the ribbon menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Settings Stub");
        }

        /// <summary>
        /// This method is called when the user clicks the help button on the right
        /// side of the ribbon. It asks the main menu to display the application help
        /// file in the help pane.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonHelp_Click(object sender, RoutedEventArgs e)
        {
            //Find Host Window
            MainWindow myWindow = (MainWindow)Window.GetWindow(this);

            //Ask it to display application help
            myWindow.LoadReader();
        }

        /// <summary>
        /// This method is called when the user clicks any of the buttons in the 
        /// new data file ribbon group on the home tab. It figures out which button called it
        /// and asks the main window to open the appropriate data input tab.
        /// </summary>
        /// <param name="sender">
        /// Ribbon button or Ribbon menu item that was pressed.
        /// </param>
        /// <param name="e"></param>
        private void NewDataFile(object sender, RoutedEventArgs e)
        {
            try
            {
                //Find Host Window
                MainWindow myWindow = (MainWindow)Window.GetWindow(this);

                //Resolve Sender
                string ButtonName;
                if (sender.GetType() == typeof(RibbonButton))
                {
                     ButtonName = ((Button)sender).Name;
                }
                else if (sender.GetType() == typeof(RibbonMenuItem))
                {
                    ButtonName = ((RibbonMenuItem)sender).Name;

                } else
                {
                    ButtonName = "";
                }
                
                
                //Ask the main window to open the appropriate tab
                switch (ButtonName)
                {
                    case "btnMRBear2d":
                        myWindow.AddNewDataInput(new Rbear2dForm(), "RBear2d");
                        break;
                    case "btnMRDam2d":
                        MessageBox.Show("MRDamn2d Stub");
                        break;
                    case "btnMREarth2d":
                        MessageBox.Show("MREarth2d Stub");
                        break;
                    case "btnMRFlow2d":
                        MessageBox.Show("MRFlow2d Stub");
                        break;
                    case "btnMRFlow3d":
                        MessageBox.Show("MRFlow3d Stub");
                        break;
                    case "btnMRPill2d":
                        MessageBox.Show("MRPill2d Stub");
                        break;
                    case "btnMRPill3d":
                        MessageBox.Show("MRPill3d Stub");
                        break;
                    case "btnMRSetl2d":
                        MessageBox.Show("MRSetl2d Stub");
                        break;
                    case "btnMRSetl3d":
                        MessageBox.Show("MRSetl3d Stub");
                        break;
                    case "btnMRSlope2d":
                        MessageBox.Show("MRSlope2d Stub");
                        break;
                    default:
                        MessageBox.Show("Unknown Sender");
                        throw new ArgumentException();
                }
            }
            catch (InvalidCastException ex)
            {
                //A new button has been added that is not the appropriate type
                MessageBox.Show(ex.Message );

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        
    }
}


namespace RFEM_Software.Commands
{
    /// <summary>
    /// Custom HelpClick command that allows differentiation between an F1 press and a context menu
    /// help command. This command is for the context menu item click.
    /// </summary>
    public static class CustomCommands
    {
        public static readonly RoutedUICommand HelpClick = new RoutedUICommand("Help", "HelpClick",
                           typeof(CustomCommands));
        public static readonly RoutedUICommand NewHelpClick = new RoutedUICommand("Help", "NewHelpClick", typeof(CustomCommands));
    }
}

public static class Extensions
{
    /// <summary>
    /// This extension method was taken from stack overflow. It allows the visual tree to be searched
    /// for all children controls. It is used to find all of the context menus in a form.
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="recurse"></param>
    /// <returns></returns>
    public static IEnumerable<Visual> GetChildren(this Visual parent, bool recurse = true)
    {
        if (parent != null)
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                // Retrieve child visual at specified index value.
                var child = VisualTreeHelper.GetChild(parent, i) as Visual;

                if (child != null)
                {
                    yield return child;

                    if (recurse)
                    {
                        foreach (var grandChild in child.GetChildren(true))
                        {
                            yield return grandChild;
                        }
                    }
                }
            }
        }
    }
}

