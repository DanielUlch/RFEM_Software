using RFEMSoftware.Simulation.Desktop.Forms;
using RFEMSoftware.Simulation.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace RFEMSoftware.Simulation.Desktop.CustomControls
{
    public class TopLevelTabItem: TabItem
    {
        private FormFactory _Factory;

        public  TabControl Tab { get; set; }

        private List<RFEMTabItem> _SubTabs;

        private DataEntryTab _DataTab;

        private ContextMenu _HeaderMenu;

        private ISimViewModel _ViewModel;
        public List<RFEMTabItem> SubTabs
        {
            get { return _SubTabs; }
        }

        public ISimViewModel ViewModel
        {
            get { return _ViewModel; }
        }
        

        public TabItemHeader CustomHeader { get; set; }

        
        public TopLevelTabItem(CommandBindingCollection commandBindings, 
                               double width, 
                               ISimViewModel viewModel,
                               RoutedEventHandler closeTopTab,
                               RoutedEventHandler closeAllTopTabs)
        {
            _ViewModel = viewModel;

            _Factory = new FormFactory(CloseTab, CmdCloseAllTabs, commandBindings, width);

            _DataTab = _Factory.CreateDataTab(viewModel);

            _SubTabs = new List<RFEMTabItem>() { _DataTab };

            Tab = new TabControl();

            Tab.DataContext = null;
            Tab.DataContext = _SubTabs;
            Tab.ItemsSource = _SubTabs;

            //this.Content = _TabControl;

            InitializeHeader(closeTopTab, closeAllTopTabs, viewModel);

        }

        private void InitializeHeader(RoutedEventHandler closeTab, RoutedEventHandler closeAllTabs, ISimViewModel viewModel)
        {
            MenuItem menuItem;

            //Build context menu for the header
            _HeaderMenu = new ContextMenu();
            _HeaderMenu.Tag = this;

            menuItem = new MenuItem();
            menuItem.Header = "Settings";
            menuItem.Click += OpenSettings;
            _HeaderMenu.Items.Add(menuItem);


            //Add a close-tab menu item and bind it to the MainViewModel's handler
            menuItem = new MenuItem();
            menuItem.Header = "Close";
            menuItem.Click += closeTab;
            _HeaderMenu.Items.Add(menuItem);

            //Add a close-all-tabs menu item and bind it to the MainViewModel's handler
            menuItem = new MenuItem();
            menuItem.Header = "Close All";
            menuItem.Click += closeAllTabs;
            _HeaderMenu.Items.Add(menuItem);

            var header = new TabItemHeader();

            Binding TextBinding = new Binding("BaseName");
            TextBinding.Source = viewModel;

            header.Image = new BitmapImage(new Uri("pack://application:,,,/Images/Project.png"));
            header.SetBinding(TabItemHeader.TextProperty, TextBinding);
            header.ContextMenu = _HeaderMenu;

            //this.Header = header;

            CustomHeader = header;

            //this.Header= new ContentControl
            //{
            //    Content = tabName,
            //    ContextMenu = _HeaderMenu
            //};
        }

        private void OpenSettings(object sender, RoutedEventArgs e)
        {
            try
            {
                if(_SubTabs.Any(x => x.Type == RFEMTabType.Settings))
                {
                    _SubTabs.Remove(_SubTabs.Where(x => x.Type == RFEMTabType.Settings).First());

                    AddAndSelectTab(new SettingsTab(CloseTab, _ViewModel, CmdCloseAllTabs));
                }
                else
                {
                    AddAndSelectTab(new SettingsTab(CloseTab, _ViewModel, CmdCloseAllTabs));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CloseTab(object sender, RoutedEventArgs e)
        {
            if (sender as MenuItem != null)
            {
                RFEMTabItem tabToClose = ((ContextMenu)((MenuItem)sender).Parent).Tag as RFEMTabItem;

                if (tabToClose != null)
                {
                    if (tabToClose == _DataTab)
                    {
                        if (_DataTab.ViewModel.ChangesHaveBeenMade)
                        {
                            var result = MessageBox.Show("Changes have been made to the file: '" +
                                                            _DataTab.ViewModel.BaseName +
                                                            "'. Would you like to save these changes?",
                                                           "Save Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                            //if yes, save it and coninue, if cancel, exit the routine, if no, continue
                            if (result == MessageBoxResult.Yes)
                            {
                                _DataTab.ViewModel.Save();
                            }
                            else if (result == MessageBoxResult.Cancel)
                            {
                                return;
                            }

                            _SubTabs.Remove(tabToClose);

                            Tab.DataContext = null;
                            Tab.ItemsSource = null;
                            Tab.DataContext = _SubTabs;
                            Tab.ItemsSource = _SubTabs;
                        }
                    }
                    else
                    {
                        _SubTabs.Remove(tabToClose);

                        Tab.DataContext = null;
                        Tab.ItemsSource = null;
                        Tab.DataContext = _SubTabs;
                        Tab.ItemsSource = _SubTabs;
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
            foreach (RFEMTabItem tab in _SubTabs)
            {
                if (tab == _DataTab)
                {
                    if (_DataTab.ViewModel.ChangesHaveBeenMade)
                    {
                        var result = MessageBox.Show("Changes have been made to the file: '" +
                                                        _DataTab.ViewModel.BaseName +
                                                        "'. Would you like to save these changes?",
                                                       "Save Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                        //if yes, save it and coninue, if cancel, exit the routine, if no, continue
                        if (result == MessageBoxResult.Yes)
                        {
                            _DataTab.ViewModel.Save();
                        }
                        else if (result == MessageBoxResult.Cancel)
                        {
                            return false;
                        }
                        _SubTabs.Remove(tab);

                        Tab.DataContext = null;
                        Tab.ItemsSource = null;
                        Tab.DataContext = _SubTabs;
                        Tab.ItemsSource = _SubTabs;
                    }
                }
                else
                {
                    _SubTabs.Remove(tab);

                    Tab.DataContext = null;
                    Tab.ItemsSource = null;
                    Tab.DataContext = _SubTabs;
                    Tab.ItemsSource = _SubTabs;
                }
            }

            return true;
        }
        public void ShowDataTab()
        {
            if(_SubTabs.Any(x => x.Type == RFEMTabType.DataInput))
            {
                _SubTabs.Remove(_SubTabs.Where(x => x.Type == RFEMTabType.DataInput).First());

                AddAndSelectTab(_Factory.CreateDataTab(_ViewModel));
            }
            else
            {
                AddAndSelectTab(_Factory.CreateDataTab(_ViewModel));
            }
        }
        public void ShowSummaryStats()
        {
            if(_SubTabs.Any(x => x.Type == RFEMTabType.SummaryStats))
            {
                _SubTabs.Remove(_SubTabs.Where(x => x.Type == RFEMTabType.SummaryStats).First());

                AddAndSelectTab(_Factory.CreateSummaryForm(_ViewModel));
            }
            else
            {
                AddAndSelectTab(_Factory.CreateSummaryForm(_ViewModel));
            }
        }
        public void ShowHistogramTab(HistogramType histType)
        {
            if(_SubTabs.Where(x => x.Type == RFEMTabType.Histogram).Any(y => ((HistogramTab)y).ViewModel.Type == histType))
            {
                _SubTabs.Remove(_SubTabs.Where(x => x.Type == RFEMTabType.Histogram).
                                         Where(y => ((HistogramTab)y).ViewModel.Type == histType).
                                         First());

                AddAndSelectTab(_Factory.CreateHistogramForm(histType, _ViewModel));
            }
            else
            {
                AddAndSelectTab(_Factory.CreateHistogramForm(histType, _ViewModel));
            }
        }
        public string GetHoveredHelpTopic()
        {
            throw new NotImplementedException();
        }
        private void AddAndSelectTab(RFEMTabItem newTab)
        {
            _SubTabs.Add(newTab);

            Tab.DataContext = null;
            Tab.ItemsSource = null;
            Tab.DataContext = _SubTabs;
            Tab.ItemsSource = _SubTabs;
            

            Tab.SelectedItem = newTab;
            Tab.Focus();

        }
        
    }
}
