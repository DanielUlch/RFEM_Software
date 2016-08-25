
using RFEMSoftware.Simulation.Desktop.CustomControls;
using RFEMSoftware.Simulation.Desktop.Forms;
using RFEMSoftware.Simulation.Infrastructure;
using RFEMSoftware.Simulation.Infrastructure.Models;
using RFEMSoftware.Simulation.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RFEMSoftware.Simulation.Desktop
{
    public class FormFactory
    {
        private RoutedEventHandler _CloseTab;
        private RoutedEventHandler _CloseAllTabs;
        private CommandBindingCollection _CommandBindings;
        private double _Width;

        public FormFactory(RoutedEventHandler closeTab,
                           RoutedEventHandler closeAllTabs,
                           CommandBindingCollection commandBindings,
                           double width)
        {
            _CloseTab = closeTab;
            _CloseAllTabs = closeAllTabs;
            _CommandBindings = commandBindings;
            _Width = width;
        }
        
        public DataEntryTab CreateDataTab(ISimViewModel viewModel)
        {
            return new DataEntryTab(_CloseTab,
                                    _CloseAllTabs,
                                    viewModel,
                                    _CommandBindings);
        }
        public SummaryStatsTab CreateSummaryForm(ISimViewModel viewModel)
        {

            string Stats = FileReader.Read(viewModel.SummaryFilePath);
            TextBlock content = new TextBlock() { Text = Stats };

            return new SummaryStatsTab(_CloseTab,
                                       _CloseAllTabs,
                                       viewModel,
                                       content);
        }
        public HistogramTab CreateHistogramForm(HistogramType histType, ISimViewModel viewModel)
        {


            return new HistogramTab(_CloseTab,
                                    _CloseAllTabs,
                                    _CommandBindings,
                                    _Width,
                                    viewModel,
                                    histType);
        }
        public SettingsTab CreateSettingsTab(ISimViewModel viewModel)
        {
            return new SettingsTab(_CloseTab,
                                   viewModel,
                                   _CloseAllTabs);
        }
      
    }


}
