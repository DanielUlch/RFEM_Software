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
    /// Interaction logic for REarth2DEarthForces.xaml
    /// </summary>
    public partial class REarth2DEarthForces : UserControl, IHistView
    {
        REarth2DEarthForcesViewModel _ViewModel;
        public IHistViewModel ViewModel
        {
            get
            {
                return _ViewModel;
            }
        }
        public REarth2DEarthForces()
        {
            InitializeComponent();
        }
        public string GetHoveredHelpTopic()
        {
            throw new NotImplementedException();
        }

        private void btnUpdateHistogram_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HistElement.Child = _ViewModel.GenerateHistogram();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnPopOutHistogram_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _ViewModel.PopOutHistogram();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
