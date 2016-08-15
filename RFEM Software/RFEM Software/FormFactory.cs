
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
        public DataEntryTab CreateNewForm(Program formType)
        {
            ISimView view;
            switch (formType)
            {
                case Program.RBear2D:

                    view = new RBear2DForm();
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

                    view = new RDam2DForm();
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

                case Program.RFlow2D:

                    view = new RFlow2DForm();
                    return new DataEntryTab(RFEMTabType.DataInput,
                                            _CloseTab,
                                            _CloseAllTabs,
                                            view,
                                            view.ViewModel,
                                            _CommandBindings,
                                            (UserControl)view,
                                            "RFlow2D",
                                            Program.RFlow2D);

                case Program.RFlow3D:

                    view = new RFlow3DForm();
                    return new DataEntryTab(RFEMTabType.DataInput,
                                            _CloseTab,
                                            _CloseAllTabs,
                                            view,
                                            view.ViewModel,
                                            _CommandBindings,
                                            (UserControl)view,
                                            "RFlow3D",
                                            Program.RFlow3D);

                case Program.RPill2D:

                    view = new RPill2DForm();
                    return new DataEntryTab(RFEMTabType.DataInput,
                                            _CloseTab,
                                            _CloseAllTabs,
                                            view,
                                            view.ViewModel,
                                            _CommandBindings,
                                            (UserControl)view,
                                            "RPill2D",
                                            Program.RPill2D);

                case Program.RPill3D:

                    view = new RPill3DForm();
                    return new DataEntryTab(RFEMTabType.DataInput,
                                            _CloseTab,
                                            _CloseAllTabs,
                                            view,
                                            view.ViewModel,
                                            _CommandBindings,
                                            (UserControl)view,
                                            "RPill3D",
                                            Program.RPill3D);

                case Program.RSetl2D:

                    view = new RSetl2DForm();
                    return new DataEntryTab(RFEMTabType.DataInput,
                                            _CloseTab,
                                            _CloseAllTabs,
                                            view,
                                            view.ViewModel,
                                            _CommandBindings,
                                            (UserControl)view,
                                            "RSetl2D",
                                            Program.RSetl2D);

                case Program.RSetl3D:

                    view = new RSetl3DForm();
                    return new DataEntryTab(RFEMTabType.DataInput,
                                            _CloseTab,
                                            _CloseAllTabs,
                                            view,
                                            view.ViewModel,
                                            _CommandBindings,
                                            (UserControl)view,
                                            "RSetl3D",
                                            Program.RSetl3D);

                case Program.RSlope2D:

                    view = new RSlope2DForm();
                    return new DataEntryTab(RFEMTabType.DataInput,
                                            _CloseTab,
                                            _CloseAllTabs,
                                            view,
                                            view.ViewModel,
                                            _CommandBindings,
                                            (UserControl)view,
                                            "RSlope2D",
                                            Program.RSlope2D);

            }

            throw new NotImplementedException();
        }
        public DataEntryTab CreateForm(Program formType, string filePath)
        {
            ISimModel formData = ModelRepository.Retrieve(filePath, formType);

            ISimView view;

            switch (formType)
            {
                case Program.RBear2D:
                    view = new RBear2DForm((RBear2D)formData);
                    break;
                case Program.RDam2D:
                    view = new Forms.RDam2DForm((RDam2D)formData);
                    break;
                case Program.REarth2D:
                    view = new REarth2DForm((REarth2D)formData);
                    break;
                case Program.RFlow2D:
                    view = new RFlow2DForm((RFlow2D)formData);
                    break;
                case Program.RFlow3D:
                    view = new RFlow3DForm((RFlow3D)formData);
                    break;
                case Program.RPill2D:
                    view = new RPill2DForm((RPill2D)formData);
                    break;
                case Program.RPill3D:
                    view = new RPill3DForm((RPill3D)formData);
                    break;
                case Program.RSetl2D:
                    view = new RSetl2DForm((RSetl2D)formData);
                    break;
                case Program.RSetl3D:
                    view = new RSetl3DForm((RSetl3D)formData);
                    break;
                case Program.RSlope2D:
                    view = new RSlope2DForm((RSlope2D)formData);
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
