using RFEMSoftware.Simulation.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RFEMSoftware.Simulation.Desktop.Forms
{
    /// <summary>
    /// Interaction logic for RSetl3DForm.xaml
    /// </summary>
    public partial class RSetl3DForm : UserControl, ISimView
    {
        private RSetl3DViewModel _ViewModel;

        public RSetl3DForm(RSetl3DViewModel viewModel)
        {
            InitializeComponent();

            _ViewModel = viewModel;

            this.DataContext = _ViewModel;
        }


        public string GetHoveredHelpTopic()
        {
            throw new NotImplementedException();
        }
    }
}
