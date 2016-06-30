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
using RFEM_Infrastructure;

namespace RFEM_Software.Forms
{
    /// <summary>
    /// Interaction logic for RBear2DHistForm.xaml
    /// </summary>
    public partial class RBear2DHistForm : UserControl, IHistView
    {

        private RBear2DHistViewModel viewModel;

        private List<FrameworkElement> _HoverableControls;
       
        public RBear2DHistForm()
        {
            InitializeComponent();

            HistElement.Child = viewModel.GenerateHistogram();
        }
        public IHistViewModel ViewModel
        {
            get { return viewModel; }
        }
        public RBear2DHistForm(int nSim, int nFootings, string baseName, string inputFilePath)
        {
            viewModel = new RBear2DHistViewModel(nSim, nFootings, baseName, inputFilePath);

            this.DataContext = viewModel;

            InitializeComponent();

            InitializeHoverableList();

            if (nFootings < 2) gbFootingNumber.Visibility = Visibility.Collapsed;
            if (viewModel.ShowPlotTitles) chkShowPlotTitles.IsChecked = true;
            chkCustomAxis_Checked(null, null);
            chkCustomXAxis_Checked(null, null);
            chkCustomYAxis_Checked(null, null);
            chkFitDistribution_Checked(null, null);
            chkShowLineKey_Checked(null, null);

            HistElement.Child = viewModel.GenerateHistogram();
        }

        private void InitializeHoverableList()
        {
            _HoverableControls = new List<FrameworkElement>()
            {
                txtTitle,
                gbFootingNumber,
                rbFootingOne,
                rbFootingTwo,
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
                gbFittedDistribution,
                rbNormal,
                rbExponential,
                rbGamma,
                rbLogNormal,
                rbBeta,
                rbUniform,
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

        private void chkShowPlotTitles_Checked(object sender, RoutedEventArgs e)
        {
            if(lbVerticalOffset != null)
            {
                if (chkShowPlotTitles.IsChecked == true)
                {
                    lbVerticalOffset.Visibility = Visibility.Visible;
                    txtTitleVerticalOffset.Visibility = Visibility.Visible;
                    lbVerticalOffsetUnits.Visibility = Visibility.Visible;
                }
                else
                {
                    lbVerticalOffset.Visibility = Visibility.Collapsed;
                    txtTitleVerticalOffset.Visibility = Visibility.Collapsed;
                    lbVerticalOffsetUnits.Visibility = Visibility.Collapsed;
                }
            }
            
        }

        private void chkCustomAxis_Checked(object sender, RoutedEventArgs e)
        {
            if(chkCustomAxis != null)
            {
                if (chkCustomAxis.IsChecked == true)
                {
                    stakCustomAxis.Visibility = Visibility.Visible;
                }
                else
                {
                    stakCustomAxis.Visibility = Visibility.Collapsed;
                }
            }
            
        }

        private void chkCustomXAxis_Checked(object sender, RoutedEventArgs e)
        {
            if(chkCustomXAxis.IsChecked == true)
            {
                stakXAxisDetails.Visibility = Visibility.Visible;
            }
            else
            {
                stakXAxisDetails.Visibility = Visibility.Collapsed;
            }
        }

        private void chkCustomYAxis_Checked(object sender, RoutedEventArgs e)
        {
            if(chkCustomYAxis.IsChecked == true)
            {
                stakYAxisDetails.Visibility = Visibility.Visible;
            }
            else
            {
                stakYAxisDetails.Visibility = Visibility.Collapsed;
            }
        }

        private void chkFitDistribution_Checked(object sender, RoutedEventArgs e)
        {
            if(chkFitDistribution.IsChecked == true)
            {
                StakFittedDistribution.Visibility = Visibility.Visible;
            }
            else
            {
                StakFittedDistribution.Visibility = Visibility.Collapsed;
            }
        }

        private void chkShowLineKey_Checked(object sender, RoutedEventArgs e)
        {
            if(stakLineKeyDetails != null)
            {
                if (chkShowLineKey.IsChecked == true)
                {
                    stakLineKeyDetails.Visibility = Visibility.Visible;
                }
                else
                {
                    stakLineKeyDetails.Visibility = Visibility.Collapsed;
                }
            }
            
        }

        private void rbNormal_Checked(object sender, RoutedEventArgs e)
        {
            viewModel.FittedDistribution = HistogramDistribution.Normal;
        }

        private void rbLogNormal_Checked(object sender, RoutedEventArgs e)
        {
            viewModel.FittedDistribution = HistogramDistribution.LogNormal;
        }

        private void rbExponential_Checked(object sender, RoutedEventArgs e)
        {
            viewModel.FittedDistribution = HistogramDistribution.Exponential;
        }

        private void rbBeta_Checked(object sender, RoutedEventArgs e)
        {
            viewModel.FittedDistribution = HistogramDistribution.Beta;
        }

        private void rbGamma_Checked(object sender, RoutedEventArgs e)
        {
            viewModel.FittedDistribution = HistogramDistribution.Gamma;
        }

        private void rbUniform_Checked(object sender, RoutedEventArgs e)
        {
            viewModel.FittedDistribution = HistogramDistribution.Uniform;
        }

        private void rbFootingOne_Checked(object sender, RoutedEventArgs e)
        {
            viewModel.FootingNum = 1;
        }

        private void rbFootingTwo_Checked(object sender, RoutedEventArgs e)
        {
            viewModel.FootingNum = 2;
        }
        
        private void btnUpdateHistogram_Click(object sender, RoutedEventArgs e)
        {
            HistElement.Child = viewModel.GenerateHistogram();
        }

        public string GetHoveredHelpTopic()
        {
            foreach(FrameworkElement f in _HoverableControls)
            {
                if (f.IsMouseOver)
                    return (string)f.Tag;
            }
            return "";
        }

        private void btnPopOutHistogram_Click(object sender, RoutedEventArgs e)
        {
            viewModel.PopOutHistogram();
        }
    }
}
