using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        private List<RFEMTabItem> tabItems;

        //Dummy column for help pane layer
        private ColumnDefinition column1CloneForLayer1;

        private string ApplicationHelpLocation = "RFEM_Software.Help_Files.AppHelp.xaml";
        public MainWindow()
        {
            InitializeComponent();

            tabItems = new List<RFEMTabItem>();
            tabControl.DataContext = tabItems;

            //Initialize dummy column
            column1CloneForLayer1 = new ColumnDefinition();
            column1CloneForLayer1.SharedSizeGroup = "column1";


        }
        public void AddNewDataInput(UserControl newTabContent, string tabBase)
        {
            RFEMTabItem NewTab = new RFEMTabItem();

            ContextMenu headerMenu;
            MenuItem menuItem;

            newTabContent.CommandBindings.Clear();
            newTabContent.CommandBindings.AddRange(this.CommandBindings);
            
            
            NewTab.TabType = RFEMTabType.DataInput;
            NewTab.Content = newTabContent;




            //Build header
            headerMenu = new ContextMenu();

            menuItem = new MenuItem();
            menuItem.Header = "Close";
            menuItem.Click += CloseTab;
            headerMenu.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Close All";
            menuItem.Click += CloseAllTabs;
            headerMenu.Items.Add(menuItem);

            NewTab.Header = new ContentControl {Content= tabBase,
                                                ContextMenu=headerMenu};


            tabItems.Add(NewTab);

            tabControl.DataContext = null;
            tabControl.DataContext = tabItems;
            tabControl.SelectedItem = NewTab;

            tabControl.Focus();

        }


/*
        public void AddNewHelpTab(string helpContent, string tabName)
        {
            ScrollViewer HelpScroller = new ScrollViewer();
            TextBlock HelpText = new TextBlock();
            RFEMTabItem NewTab = new RFEMTabItem();


            HelpText.Text = helpContent;
            HelpScroller.Content = HelpText;

            NewTab.Header = tabName;
            NewTab.TabType = RFEMTabType.Help;
            NewTab.Content = HelpScroller;


            tabItems.Add(NewTab);

            tabControl.DataContext = null;
            tabControl.DataContext = tabItems;
            tabControl.SelectedItem = NewTab;


        }
        */
        private void CloseTab(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Close Tab Stub");
        }
        private void CloseAllTabs(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Close all tabs stub");
        }
        private void TabSelectionChanged(object sender, RoutedEventArgs e)
        {
            if(this.tabControl.SelectedItem != null &&
                this.tabControl.SelectedItem.GetType() == typeof(RFEMTabItem))
            {
                if (((RFEMTabItem)this.tabControl.SelectedItem).TabType == RFEMTabType.DataInput){
                    this.mainRibbon.RunTools.Visibility = Visibility.Visible;
                }
                else
                {
                    this.mainRibbon.RunTools.Visibility = Visibility.Collapsed;
                }
            }
                

        }


        //Dock-Undock Help Pane
        private void helpPanePin_Click(object sender, RoutedEventArgs e)
        {
            if (helpPaneButton.Visibility == Visibility.Collapsed)
                UndockPane();
            else
                DockPane();   
        }
        //Show help pane when hovering over the button
        private void helpPaneButton_MouseEnter(object sender, RoutedEventArgs e)
        {
            layer2.Visibility = Visibility.Visible;

        }
        //Hide undocked pane when mouse enters layer 1
        private void layer1_MouseEnter(object sender, RoutedEventArgs e)
        {
            if (helpPaneButton.Visibility == Visibility.Visible)
                layer2.Visibility = Visibility.Collapsed;
        }

        //Dock pane and collapse side button
        private void DockPane()
        {
            helpPaneButton.Visibility = Visibility.Collapsed;
            helpPanePinImage.Source = new BitmapImage(new Uri("/Images/Unpin.png", UriKind.Relative));

            layer1.ColumnDefinitions.Add(column1CloneForLayer1);
            //layer1.ColumnDefinitions.Insert(1, column1CloneForLayer1);
        }
        private void UndockPane()
        {
            layer2.Visibility = Visibility.Visible;
            helpPaneButton.Visibility = Visibility.Visible;
            helpPanePinImage.Source = new BitmapImage(new Uri("/Images/Pin.png", UriKind.Relative));

            layer1.ColumnDefinitions.Remove(column1CloneForLayer1);
        }
        
        private void helpPane_MouseEnter(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            //Store data files in isolated storage
            MessageBox.Show("Conditional ask for save stub");
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            //Load data files from isolated storage
            MessageBox.Show("Load open windows stub");
        }
        private void HelpClick_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void HelpClick_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var tabContent = (Forms.IHelpFiled)((RFEMTabItem)tabControl.SelectedItem).Content;
            string helpLocation = tabContent.HelpLocation((FrameworkElement)e.Parameter);
            LoadReader(helpLocation);
        }

        //Works only for F1
        private void HelpCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void HelpExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (tabControl.Items.Count > 0)
            {
                var tab = (RFEMTabItem)tabControl.SelectedItem;

                if (tab.TabType == RFEMTabType.DataInput)
                {
                    var helpTab = (RFEM_Software.Forms.IHelpFiled)tab.Content;
                    string helpLocation = helpTab.hoveredHelpDocLocation();
                    LoadReader(helpLocation);

                } else
                {
                    LoadReader(ApplicationHelpLocation);
                }
            }
            else
            {
                LoadReader(ApplicationHelpLocation);
            }
            
        }
        private void LoadReader(string helpLocation)
        {
            var helpStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(helpLocation);
            FlowDocument helpContent = System.Windows.Markup.XamlReader.Load(helpStream) as FlowDocument;
            HelpReader.Document = helpContent;
            if (helpPaneButton.Visibility == Visibility.Visible)
            {
                layer2.Visibility = Visibility.Visible;
                layer2.ColumnDefinitions[1].Width = new GridLength(200);
                DockPane();
            }
        }
        public void LoadReader()
        {
            LoadReader(ApplicationHelpLocation);
        }

    }


    public partial class RFEMRibbon
    {
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Settings Stub");
        }
        private void RibbonHelp_Click(object sender, RoutedEventArgs e)
        {
            //Find Host Window
            MainWindow myWindow = (MainWindow)Window.GetWindow(this);

            myWindow.LoadReader();
        }

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
                
                
                
                switch (ButtonName)
                {
                    case "btnMRBear2d":
                        //MessageBox.Show("MRBear2d Stub");
                        myWindow.AddNewDataInput(new Rbear2d(), "RBear2d");
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
    public static class CustomCommands
    {
        public static readonly RoutedUICommand HelpClick = new RoutedUICommand("Help", "HelpClick",
                           typeof(CustomCommands));
    }
}

