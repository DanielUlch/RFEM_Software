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

        public ISimView View
        {
            get { return _View; }
        }
        public ISimViewModel ViewModel
        {
            get { return _ViewModel; }
        }

        public DataEntryTab(RFEMTabType type, 
                            RoutedEventHandler closeTab,
                            RoutedEventHandler closeAllTabs, 
                            ISimView view, 
                            ISimViewModel viewModel, 
                            CommandBindingCollection cmdBindings,
                            UserControl content,
                            string tabName): base(type, closeTab, closeAllTabs)
        {
            _View = view;
            _ViewModel = viewModel;

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

        public DataEntryTab DataTab
        {
            get { return _DataTab; }
        }
        public ResultsTab(RFEMTabType type,
                          RoutedEventHandler closeTab,
                          RoutedEventHandler closeAllTabs,
                          DataEntryTab dataTab,
                          string tabContent,
                          string tabName): base(type, closeTab, closeAllTabs)
        {
            _DataTab = dataTab;

            base.SetTabContent(new TextBlock() { Text = tabContent });
            base.SetTabHeader(tabName);
        }
        


    }
    public class HistogramTab: RFEMTabItem
    {
        private IHistView _View;
        private IHistViewModel _ViewModel;
        private DataEntryTab _DataTab;

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
        public HistogramTab(RFEMTabType type,
                            RoutedEventHandler closeTab,
                            RoutedEventHandler closeAllTabs,
                            DataEntryTab dataTab,
                            UserControl content,
                            CommandBindingCollection cmdBindings,
                            IHistView view,
                            IHistViewModel viewModel,
                            string tabName): base(type, closeTab, closeAllTabs)
        {
            _View = view;
            _ViewModel = viewModel;
            _DataTab = dataTab;

            content.CommandBindings.Clear();
            content.CommandBindings.AddRange(cmdBindings);

            base.SetTabHeader(tabName);
            base.SetTabContent(content);
        }
    }


}