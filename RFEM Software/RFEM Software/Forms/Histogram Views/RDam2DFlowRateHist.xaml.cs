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
    /// Interaction logic for RDam2DFlowRateHist.xaml
    /// </summary>
    public partial class RDam2DFlowRateHist : UserControl, IHistView
    {
        private RDam2DFlowRateHistViewModel _ViewModel;
        public RDam2DFlowRateHist(int nSim, string filePath, string baseName)
        {
            InitializeComponent();

            _ViewModel = new RDam2DFlowRateHistViewModel(nSim, baseName, filePath);

            this.DataContext = _ViewModel;

            this.HistCore.DataContext = _ViewModel.HistogramCore;
        }

        public IHistViewModel ViewModel
        {
            get
            {
                return _ViewModel;
            }
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
