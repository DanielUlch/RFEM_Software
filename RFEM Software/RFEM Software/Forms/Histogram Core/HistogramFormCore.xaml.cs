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
    /// Interaction logic for HistogramFormCore.xaml
    /// </summary>
    public partial class HistogramFormCore : UserControl
    {
        private List<FrameworkElement> _HoverableControls;

        public List<FrameworkElement> HoverableControls
        {
            get { return _HoverableControls; }
        }
        public HistogramFormCore()
        {

            InitializeComponent();

            InitializeHoverableControls();

        }
        private void InitializeHoverableControls()
        {
            _HoverableControls = new List<FrameworkElement>()
            {
                chkShowPlotTitles,
                lbVerticalOffset,
                txtTitleVerticalOffset,
                lbVerticalOffsetUnits,
                lbNumberOfIntervals,
                txtNumInvervals,
                chkCustomAxis,
                lbXAxisLength,
                txtXAxisLength,
                lbXAxisLengthUnits,
                lbXAxisOrigin,
                txtXAxisOrigin,
                lbXAxisOriginUnits,
                lbYAxisLength,
                txtYAxisLength,
                lbYAxisLengthUnits,
                lbYAxisOrigin,
                txtYAxisOrigin,
                lbYAxisOriginUnits,
                chkUseLogXAxis,
                chkCustomAxis,
                lbXAxisMin,
                txtXAxisMin,
                lbXAxisMax,
                txtXAxisMax,
                lbXAxisIncrement,
                txtXAxisIncrement,
                chkCustomYAxis,
                lbYAxisMin,
                txtYAxisMin,
                lbYAxisMax,
                txtYAxisMax,
                lbYAxisIncrement,
                txtYAxisIncrement,
                chkFitDistribution,
                lbFittedDistributionType,
                cboFittedDistributionType,
                chkAndersonDarling,
                chkChiSquare,
                chkShowLineKey,
                lbLineKeyXOffset,
                txtLineKeyXOffset,
                lbLineKeyXOffsetUnits,
                lbLineKeyYOffset,
                txtLineKeyYOffset,
                lbLineKeyYOffsetUnits
            };
        }

        #region CompilerTricks
        /// <summary>
        /// This method is to appease the compiler. The help click command gets bound to
        /// a method in the main window at runtime.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpClickStub(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        /// <summary>
        /// This method is to appease the compiler. The help click command gets bound to
        /// a method in the main window at runtime.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpClickExeStub(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show(e.Parameter.ToString());
        }
        #endregion
    }
}
