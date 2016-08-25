
using RFEMSoftware.Simulation.Desktop.Forms;
using RFEMSoftware.Simulation.Infrastructure;
using RFEMSoftware.Simulation.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RFEMSoftware.Simulation.Desktop.CustomControls
{

    public enum RFEMTabType
    {
        DataInput,
        Histogram,
        Settings,
        SummaryStats
    }

    public class RFEMTabItem: TabItem
    {
        private RFEMTabType _Type;
        private ContextMenu _HeaderMenu;
        private ScrollViewer _ScrollViewer;
        
       

        public RFEMTabType Type
        {
            get { return _Type; }
        }
        public RFEMTabItem(RFEMTabType type, RoutedEventHandler closeTab,
                            RoutedEventHandler closeAllTabs)
        {
            _Type = type;


            _ScrollViewer = new ScrollViewer();
            MenuItem menuItem;

            //Build context menu for the header
            _HeaderMenu = new ContextMenu();
            _HeaderMenu.Tag = this;

            //Add a close-tab menu item and bind it to this window's handler
            menuItem = new MenuItem();
            menuItem.Header = "Close";
            menuItem.Click += closeTab;
            _HeaderMenu.Items.Add(menuItem);

            //Add a close-all-tabs menu item and bind it to this window's handler
            menuItem = new MenuItem();
            menuItem.Header = "Close All";
            menuItem.Click += closeAllTabs;
            _HeaderMenu.Items.Add(menuItem);

            //Build the header
            this.Header = new ContentControl
            {
                Content = "Header",
                ContextMenu = _HeaderMenu
            };
            
            _ScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            //Place the scrollviewer in the new tab
            this.Content = _ScrollViewer;
            
            this.Padding = new Thickness() { Left = 0, Right = 0, Top = 0, Bottom = 0 };

        }
        protected void SetTabHeader(ISimViewModel viewModel, string BaseNameExtension)
        {
            

            var header = new TabItemHeader();
            switch (_Type)
            {
                case RFEMTabType.DataInput:
                    header.Image = new BitmapImage(new Uri("pack://application:,,,/Images/DataForm.png"));
                    break;
                case RFEMTabType.Settings:
                    header.Image = new BitmapImage(new Uri("pack://application:,,,/Images/SettingsForm.png"));
                    break;
                case RFEMTabType.SummaryStats:
                    header.Image = new BitmapImage(new Uri("pack://application:,,,/Images/AccountingBrown.png"));
                    break;
                case RFEMTabType.Histogram:
                    header.Image = new BitmapImage(new Uri("pack://application:,,,/Images/NormalHist.png"));
                    break;
            }
            Binding TextBinding = new Binding("BaseName");
            TextBinding.Source = viewModel;
            TextBinding.StringFormat = "{0}" + BaseNameExtension;

            header.SetBinding(TabItemHeader.TextProperty, TextBinding);
            
            header.ContextMenu = _HeaderMenu;

            this.Header = header;
        }
        protected void AddToContextMenu(MenuItem item)
        {
            _HeaderMenu.Items.Add(item);
        }

        protected void SetTabContent(FrameworkElement content)
        {
            _ScrollViewer.Content = content;
            content.HorizontalAlignment = HorizontalAlignment.Left;
        }

    }
    public class DataEntryTab : RFEMTabItem
    {
        private ISimView _View;
        private ISimViewModel _ViewModel;
        private Program _ProgramType;
        
        public ISimViewModel ViewModel
        {
            get { return _ViewModel; }
        }
        public Program ProgramType
        {
            get { return _ProgramType; }
        }
        public DataEntryTab(RoutedEventHandler closeTab,
                            RoutedEventHandler closeAllTabs,
                            ISimViewModel viewModel, 
                            CommandBindingCollection cmdBindings): base(RFEMTabType.DataInput, closeTab, closeAllTabs)
        {
            _View = viewModel.View;
            _ViewModel = viewModel;
            _ProgramType = viewModel.Type;

            ((UserControl)_View).CommandBindings.Clear();
            ((UserControl)_View).CommandBindings.AddRange(cmdBindings);

            base.SetTabHeader(viewModel, " - Data");
            base.SetTabContent(((UserControl)_View));
        }
        
    }
    public class SettingsTab : RFEMTabItem
    {
        public SettingsTab(RoutedEventHandler closeTab,
                           ISimViewModel viewModel,
                           RoutedEventHandler closeAllTabs) : base(RFEMTabType.Settings, closeTab, closeAllTabs)
        {
            base.SetTabContent(new ProjectSettings(viewModel.FileInfo));
            base.SetTabHeader(viewModel, " - Settings");
        }

    }
    public class SummaryStatsTab: RFEMTabItem
    {

        public SummaryStatsTab(RoutedEventHandler closeTab,
                               RoutedEventHandler closeAllTabs,
                               ISimViewModel viewModel,
                               FrameworkElement SummaryControl): base(RFEMTabType.SummaryStats, closeTab, closeAllTabs)
        {
            
            base.SetTabContent(SummaryControl);
            base.SetTabHeader(viewModel, " - Stats");
        }
        


    }
    public class HistogramTab: RFEMTabItem
    {
        private IHistView _View;
        private IHistViewModel _ViewModel;
        
        public IHistView View
        {
            get { return _View; }
        }
        public IHistViewModel ViewModel
        {
            get { return _ViewModel; }
        }

        public HistogramTab(RoutedEventHandler closeTab,
                            RoutedEventHandler closeAllTabs,
                            CommandBindingCollection cmdBindings,
                            double width,
                            ISimViewModel viewModel,
                            HistogramType histType): base(RFEMTabType.Histogram, closeTab, closeAllTabs)
        {
            UserControl form;
            string tabNameExtension;

            switch (histType)
            {
                case HistogramType.RBear_Bearing:
                    RBear2DViewModel vm = (RBear2DViewModel)viewModel;
                    form = vm.CreateNewBearingHistForm();
                    tabNameExtension = " - Bearing Hist";
                    break;
                case HistogramType.RDam_FlowRate:
                    form =((RDam2DViewModel)viewModel).CreateNewFlowRateHistForm();
                    tabNameExtension = " - Flow Rate Hist";
                    break;
                case HistogramType.RDam_Conductivity:
                    form = ((RDam2DViewModel)viewModel).CreateNewConducivityHistForm();
                    tabNameExtension = " - Conductivity Hist";
                    break;
                case HistogramType.RDam_NodeGradient:
                    form = ((RDam2DViewModel)viewModel).CreateNewNodalGradientHistForm();
                    tabNameExtension = " - Nodal Gradient Hist";
                    break;
                    
                default:
                    throw new NotImplementedException("Histogram tab has not been implemented for this program.");
            }
            

            form.Width = width;

            _View = (IHistView)form;
            _ViewModel = _View.ViewModel;

            form.CommandBindings.Clear();
            form.CommandBindings.AddRange(cmdBindings);

            base.SetTabHeader(viewModel, tabNameExtension);
            base.SetTabContent(form);
        }
    }


}