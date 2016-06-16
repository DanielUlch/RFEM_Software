using RFEM_Infrastructure;
using RFEM_Software.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RFEM_Software
{
    /// <summary>
    /// Adds a TabType field to tabs to determine which type of tab it is
    /// </summary>
    //class RFEMTabItem : TabItem
    //{
    //    public RFEMTabType TabType { get; set; }
    //}

    /// <summary>
    /// Enumeration for different TabTypes
    /// </summary>
    public enum RFEMTabType
    {
        DataInput,
        Help,
        Settings,
        Results
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
            
        }
        protected void SetTabHeader(string headerName)
        {
            this.Header = new ContentControl
            {
                Content = headerName,
                ContextMenu = _HeaderMenu
            };
        }
        protected void AddToContextMenu(MenuItem item)
        {
            _HeaderMenu.Items.Add(item);
        }

        protected void SetTabContent(FrameworkElement content)
        {
            _ScrollViewer.Content = content;
        }

    }
    public class DataEntryTab : RFEMTabItem
    {
        private ISimView _View;
        private ISimViewModel _ViewModel;
        private Program _ProgramType;

        public ISimView View
        {
            get { return _View; }
        }
        public ISimViewModel ViewModel
        {
            get { return _ViewModel; }
        }
        public Program ProgramType
        {
            get { return _ProgramType; }
        }
        public DataEntryTab(RFEMTabType type, 
                            RoutedEventHandler closeTab,
                            RoutedEventHandler closeAllTabs, 
                            ISimView view, 
                            ISimViewModel viewModel, 
                            CommandBindingCollection cmdBindings,
                            UserControl content,
                            string tabName,
                            Program programType): base(type, closeTab, closeAllTabs)
        {
            _View = view;
            _ViewModel = viewModel;
            _ProgramType = programType;

            content.CommandBindings.Clear();
            content.CommandBindings.AddRange(cmdBindings);

            base.SetTabHeader(tabName);
            base.SetTabContent(content);
        }
        
    }
    public class SettingsTab : RFEMTabItem
    {
        public SettingsTab(RFEMTabType type, RoutedEventHandler closeTab,
            RoutedEventHandler closeAllTabs) : base(type, closeTab, closeAllTabs)
        {
            base.SetTabContent(new Forms.SettingsForm());
            base.SetTabHeader("Settings");
        }

    }
    public class ResultsTab: RFEMTabItem
    {
        private DataEntryTab _DataTab;

        private string _FilePath;

        private string _TabName;

        public DataEntryTab DataTab
        {
            get { return _DataTab; }
        }
        public string FilePath
        {
            get { return _FilePath; }
        }
        public string TabName
        {
            get { return _TabName; }
        }
        public ResultsTab(RFEMTabType type,
                          RoutedEventHandler closeTab,
                          RoutedEventHandler closeAllTabs,
                          DataEntryTab dataTab,
                          string filePath,
                          string tabName): base(type, closeTab, closeAllTabs)
        {

            string SummaryStats = FileReader.Read(filePath);

            _DataTab = dataTab;
            _TabName = tabName;
            _FilePath = filePath;

            base.SetTabContent(new TextBlock() { Text = SummaryStats });
            base.SetTabHeader(tabName);
        }
        


    }
    public class HistogramTab: RFEMTabItem
    {
        private IHistView _View;
        private IHistViewModel _ViewModel;
        private DataEntryTab _DataTab;
        private string _TabName;
        private Program _ProgramType;

        public DataEntryTab DataTab
        {
            get { return _DataTab; }
        }
        public IHistView View
        {
            get { return _View; }
        }
        public IHistViewModel ViewModel
        {
            get { return _ViewModel; }
        }
        public string TabName
        {
            get { return _TabName; }
        }
        public Program ProgramType
        {
            get { return _ProgramType; }
        }
        public HistogramTab(RFEMTabType type,
                            RoutedEventHandler closeTab,
                            RoutedEventHandler closeAllTabs,
                            DataEntryTab dataTab,
                            CommandBindingCollection cmdBindings,
                            double width): base(type, closeTab, closeAllTabs)
        {
            UserControl form;
            switch (dataTab.ProgramType)
            {
                case Program.RBear2D:
                    RBear2dViewModel vm = (RBear2dViewModel)dataTab.ViewModel;
                    form = new RBear2DHistForm((int)vm.NSimulations, vm.NumberOfFootings, vm.BaseName, vm.HistFilePath);
                    break;
                default:
                    throw new NotImplementedException("Histogram tab has not been implemented for this program.");
            }
            

            form.Width = width;

            _View = (IHistView)form;
            _ViewModel = _View.ViewModel;
            _DataTab = dataTab;
            _TabName = _DataTab.ViewModel.BaseName + "-Histogram";
            _ProgramType = dataTab.ProgramType;

            form.CommandBindings.Clear();
            form.CommandBindings.AddRange(cmdBindings);

            base.SetTabHeader(_TabName);
            base.SetTabContent(form);
        }
        public HistogramTab(RFEMTabType type,
                            RoutedEventHandler closeTab,
                            RoutedEventHandler closeAllTabs,
                            CommandBindingCollection cmdBindings,
                            double width,
                            Program programType,
                            int nSim,
                            int nFootings,
                            string baseName,
                            string filePath): base(type, closeTab, closeAllTabs)
        {
            UserControl form;

            switch (programType)
            {
                case Program.RBear2D:
                    form = new RBear2DHistForm(nSim, nFootings, baseName, filePath);
                    break;
                default:
                    throw new NotImplementedException("Histogram tab has not been implemented for this program");
            }

            form.Width = width;

            _View = (IHistView)form;
            _ViewModel = _View.ViewModel;
            _DataTab = null;
            _TabName = baseName + "-Histogram";
            _ProgramType = programType;

            form.CommandBindings.Clear();
            form.CommandBindings.AddRange(cmdBindings);

            base.SetTabHeader(_TabName);
            base.SetTabContent(form);

        }
    }


}