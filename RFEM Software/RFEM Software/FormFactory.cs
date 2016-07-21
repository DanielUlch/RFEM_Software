using RFEM_Infrastructure;
using RFEM_Software.Custom_Controls;
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
        public DataEntryTab CreateNewForm(Program formType)
        {
            ISimView view;
            switch (formType)
            {
                case Program.RBear2D:

                    view = new Rbear2DForm();
                    return new DataEntryTab(RFEMTabType.DataInput,
                                            _CloseTab,
                                            _CloseAllTabs,
                                            view,
                                            view.ViewModel,
                                            _CommandBindings,
                                            (UserControl)view,
                                            "RBear2D",
                                            Program.RBear2D);
                case Program.RDam2D:

                    view = new Rdam2dForm();
                    return new DataEntryTab(RFEMTabType.DataInput,
                                            _CloseTab,
                                            _CloseAllTabs,
                                            view,
                                            view.ViewModel,
                                            _CommandBindings,
                                            (UserControl)view,
                                            "RDam2D",
                                            Program.RDam2D);

                case Program.REarth2D:

                    view = new REarth2DForm();
                    return new DataEntryTab(RFEMTabType.DataInput,
                                            _CloseTab,
                                            _CloseAllTabs,
                                            view,
                                            view.ViewModel,
                                            _CommandBindings,
                                            (UserControl)view,
                                            "REarth2D",
                                            Program.REarth2D);
            }

            throw new NotImplementedException();
        }
        public DataEntryTab CreateForm(Program formType, string filePath)
        {
            ISimModel formData = FileReader.Read(formType, filePath);

            ISimView view;

            switch (formType)
            {
                case Program.RBear2D:
                    view = new Rbear2DForm((RBear2D)formData);
                    break;
                case Program.RDam2D:
                    view = new Rdam2dForm((RDam2D)formData);
                    break;
                case Program.REarth2D:
                    view = new REarth2DForm((REarth2D)formData);
                    break;
                default:
                    throw new NotImplementedException();
            }

            ((UserControl)view).Width = _Width;

            return new DataEntryTab(RFEMTabType.DataInput,
                                    _CloseTab,
                                    _CloseAllTabs,
                                    view,
                                    view.ViewModel,
                                    _CommandBindings,
                                    (UserControl)view,
                                    view.ViewModel.BaseName,
                                    formType);
            
        }
        public ResultsTab CreateSummaryForm(string filePath, string tabName, DataEntryTab parentTab)
        {


            return new ResultsTab(RFEMTabType.Results,
                                _CloseTab,
                                _CloseAllTabs,
                                parentTab,
                                filePath,
                                tabName);
        }
        public HistogramTab CreateHistogramForm(HistogramType histType, DataEntryTab parentTab)
        {


            return new HistogramTab(RFEMTabType.Results,
                                    _CloseTab,
                                    _CloseAllTabs,
                                    parentTab,
                                    _CommandBindings,
                                    _Width,
                                    histType);
        }
        public RBear2DHistForm CreateRBearHistFromStoredData()
        {
            throw new NotImplementedException();
        }
        public SettingsTab CreateSettingsTab()
        {
            return new SettingsTab(RFEMTabType.Settings,
                                   _CloseTab,
                                   _CloseAllTabs);
        }
      
    }


}
