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

namespace RFEM_Software.Forms
{
    /// <summary>
    /// Interaction logic for RDam2DNodalGradientHist.xaml
    /// </summary>
    public partial class RDam2DNodalGradientHist : UserControl, IHistView
    {
        private RDam2DNodalGradientHistViewModel _ViewModel;
        public RDam2DNodalGradientHist(IEnumerable<int> availableNodes,
                                        string filePath,
                                        string baseName,
                                        int nSim)
        {
            InitializeComponent();

            _ViewModel = new RDam2DNodalGradientHistViewModel(availableNodes, filePath, baseName, nSim);

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
