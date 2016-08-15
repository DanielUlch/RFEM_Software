using Microsoft.Win32;
using RFEMSoftware.Simulation.Desktop.CustomControls;
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


namespace RFEMSoftware.Simulation.Desktop
{
    /// <summary>
    /// This window is the main application window. User's create tabs and run simulations 
    /// from within this window.
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {


        /// <summary>
        /// Column close to synchronize layer 1 and layer 2 help pane width. When the help
        /// pane is pinned to layer 1, it must occupy the appropriate space on both layers.
        /// </summary>
        private ColumnDefinition column1CloneForLayer1;





        private MainViewModel _ViewModel;

        /// <summary>
        /// Constructor for the window.
        /// </summary>
        public MainWindow()
        {
            

            //Required by framework
            InitializeComponent();

            _ViewModel = new MainViewModel(this);

            //Initialize dummy column to link layer 1 and 2 for the help pane
            column1CloneForLayer1 = new ColumnDefinition();
            column1CloneForLayer1.SharedSizeGroup = "column1";

            //Wire the button events from the ribbon to routines in the main window
            WireRibbonEvents();

        }

        /// <summary>
        /// This method hooks up ribbon events to private methods in this class.
        /// </summary>
        private void WireRibbonEvents()
        {
 
            this.mainRibbon.btnRibbonHelp.Click += btnRibbonHelp_Click;

        }


        #region Help Pane Code
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

        #endregion

        #region Help Command Execution
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
                if (tab.GetType() == typeof(DataEntryTab))
                {
                    //Get the selected form
                    var helpTab = ((DataEntryTab)tab).View;

                    //Ask the selected form for the help topic associated with a hovered control
                    string helpLocation = helpTab.GetHoveredHelpTopic();

                    //Load the help file
                    LoadReaderNew(helpLocation);

                }
                else if (tab.GetType() == typeof(HistogramTab))
                {
                    //Get the selected form
                    var helpTab = ((HistogramTab)tab).View;

                    //Ask the selected form for the help topic associated with a hovered control
                    string helpLocation = helpTab.GetHoveredHelpTopic();

                    //Load the help file
                    LoadReaderNew(helpLocation);
                }
                else
                {
                    //Load the defaul help file for this application
                    LoadReaderNew("");
                }
            }
            else
            {
                //Load the defaul help file for this application
                LoadReaderNew("");
            }    
        }


        /// <summary>
        /// This method tells the context menus that the NewHelp command can be executed on the main window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewHelpClickCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        private void NewHelpClickExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            LoadReaderNew((string)e.Parameter);
        }


        private void LoadReaderNew(string topic)
        {
            HelpReader.LoadHelpTopic(topic);

            //If the help pane is not pinned, pin it
            if (helpPaneButton.Visibility == Visibility.Visible)
            {
                layer2.Visibility = Visibility.Visible;
                layer2.ColumnDefinitions[1].Width = new GridLength(200);
                DockPane();
            }
        }
    

        /// <summary>
        /// This method is called when the user clicks the help button on the right of the ribbon.
        /// Currently it loads the default help file into the help-reader.
        /// </summary>
        private void btnRibbonHelp_Click(object sender, RoutedEventArgs e)
        {
            
            LoadReaderNew("");
        }


        #endregion

        
        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void btnBottomExpander_Click(object sender, RoutedEventArgs e)
        {
            
            BottomGrid.Visibility = Visibility.Visible;
            btnBottomExpander.Visibility = Visibility.Collapsed;
            ExpanderRow.Height = new GridLength(150);
            BottomSplitterRow.Height = new GridLength(5);
            BottomStatusBar.Visibility = Visibility.Collapsed;
            
        }

        private void btnBottomCollapser_Click(object sender, RoutedEventArgs e)
        {
            BottomSplitterRow.Height = new GridLength(0);
            ExpanderRow.Height = new GridLength(0);
            BottomGrid.Visibility = Visibility.Collapsed;
            btnBottomExpander.Visibility = Visibility.Visible;
            BottomStatusBar.Visibility = Visibility.Visible;
        }
    }
}

