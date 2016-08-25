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
using System.Windows.Input;

namespace RFEMSoftware.Simulation.Desktop
{
    public class ViewModelFactory
    {
        private RoutedEventHandler _CloseTab;
        private RoutedEventHandler _CloseAllTabs;
        private CommandBindingCollection _CommandBindings;
        private double _Width;

        public ViewModelFactory(RoutedEventHandler closeTab,
                                RoutedEventHandler closeAllTabs,
                                CommandBindingCollection commandBindings,
                                double width)
        {
            _CloseTab = closeTab;
            _CloseAllTabs = closeAllTabs;
            _CommandBindings = commandBindings;
            _Width = width;
        }

        public ISimViewModel CreateViewModel(Program type)
        {
            switch (type)
            {
                case Program.RBear2D:
                    return new RBear2DViewModel(_CommandBindings, _Width, _CloseTab, _CloseAllTabs);
                case Program.RDam2D:
                    return new RDam2DViewModel(_CommandBindings, _Width, _CloseTab, _CloseAllTabs);
                case Program.REarth2D:
                    return new REarth2DViewModel(_CommandBindings, _Width, _CloseTab, _CloseAllTabs);
                case Program.RFlow2D:
                    return new RFlow2DViewModel(_CommandBindings, _Width, _CloseTab, _CloseAllTabs);
                case Program.RFlow3D:
                    return new RFlow3DViewModel(_CommandBindings, _Width, _CloseTab, _CloseAllTabs);
                case Program.RPill2D:
                    return new RPill2DViewModel(_CommandBindings, _Width, _CloseTab, _CloseAllTabs);
                case Program.RPill3D:
                    return new RPill3DViewModel(_CommandBindings, _Width, _CloseTab, _CloseAllTabs);
                case Program.RSetl2D:
                    return new RSetl2DViewModel(_CommandBindings, _Width, _CloseTab, _CloseAllTabs);
                case Program.RSetl3D:
                    return new RSetl3DViewModel(_CommandBindings, _Width, _CloseTab, _CloseAllTabs);
                case Program.RSlope2D:
                    return new RSlope2DViewModel(_CommandBindings, _Width, _CloseTab, _CloseAllTabs);
                default:
                    throw new NotImplementedException();
            }

        }
        public ISimViewModel CreateViewModel(Program type, string filePath)
        {
            var model = ModelRepository.Retrieve(filePath, type);

            switch (type)
            {
                case Program.RBear2D:
                    return new RBear2DViewModel(_CommandBindings, _Width, (RBear2D)model, _CloseTab, _CloseAllTabs);
                case Program.RDam2D:
                    return new RDam2DViewModel(_CommandBindings, _Width, (RDam2D)model, _CloseTab, _CloseAllTabs);
                case Program.REarth2D:
                    return new REarth2DViewModel(_CommandBindings, _Width, (REarth2D)model, _CloseTab, _CloseAllTabs);
                case Program.RFlow2D:
                    return new RFlow2DViewModel(_CommandBindings, _Width, (RFlow2D)model, _CloseTab, _CloseAllTabs);
                case Program.RFlow3D:
                    return new RFlow3DViewModel(_CommandBindings, _Width, (RFlow3D)model, _CloseTab, _CloseAllTabs);
                case Program.RPill2D:
                    return new RPill2DViewModel(_CommandBindings, _Width, (RPill2D)model, _CloseTab, _CloseAllTabs);
                case Program.RPill3D:
                    return new RPill3DViewModel(_CommandBindings, _Width, (RPill3D)model, _CloseTab, _CloseAllTabs);
                case Program.RSetl2D:
                    return new RSetl2DViewModel(_CommandBindings, _Width, (RSetl2D)model, _CloseTab, _CloseAllTabs);
                case Program.RSetl3D:
                    return new RSetl3DViewModel(_CommandBindings, _Width, (RSetl3D)model, _CloseTab, _CloseAllTabs);
                case Program.RSlope2D:
                    return new RSlope2DViewModel(_CommandBindings, _Width, (RSlope2D)model, _CloseTab, _CloseAllTabs);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
