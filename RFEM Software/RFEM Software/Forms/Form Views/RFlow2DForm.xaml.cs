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
    /// Interaction logic for RFlow2DForm.xaml
    /// </summary>
    public partial class RFlow2DForm : UserControl, ISimView
    {
        private RFlow2DViewModel _ViewModel;
       
        public RFlow2DForm(RFlow2DViewModel viewModel)
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
