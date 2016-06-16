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
        private static Dictionary<string, string> helpFiles = new Dictionary<string, string>()
        {
            { "RBearHistHelp", "/Help Files/RBearHist Help Files/RBearHistHelp.xaml"},
            {"FootingNumberHelp", "/Help Files/RBearHist Help Files/FootingNumberHelp.xaml"},
            {"ShowTitlesHelp", "/Help Files/RBearHist Help Files/ShowTitlesHelp.xaml" },
            { "NumIntervalsHelp", "/Help Files/RBearHist Help Files/NumIntervalsHelp.xaml"},
            { "LengthOriginAxesHelp", "/Help Files/RBearHist Help Files/LengthOriginAxesHelp.xaml"},
            { "LogScaleXAxisHelp", "/Help Files/RBearHist Help Files/LogScaleXAxisHelp.xaml"},
            { "XAxisDetailsHelp", "/Help Files/RBearHist Help Files/XAxisDetailsHelp.xaml" },
            { "YAxisDetailsHelp", "/Help Files/RBearHist Help Files/YAxisDetailsHelp.xaml"},
            { "DistributionHelp", "/Help Files/RBearHist Help Files/DistributionHelp.xaml" },
            { "AndersonDarlingHelp", "/Help Files/RBearHist Help Files/AndersonDarlingHelp.xaml" },
            { "ChiSquareHelp", "/Help Files/RBearHist Help Files/ChiSquareHelp.xaml" },
            { "LineKeyHelp", "/Help Files/RBearHist Help Files/LineKeyHelp.xaml"}
        };

        private RBear2DHistViewModel viewModel;

       
        public RBear2DHistForm()
        {
            InitializeComponent();
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

            if (nFootings < 2) gbFootingNumber.Visibility = Visibility.Collapsed;
            if (viewModel.ShowPlotTitles) chkShowPlotTitles.IsChecked = true;
            chkCustomAxis_Checked(null, null);
            chkCustomXAxis_Checked(null, null);
            chkCustomYAxis_Checked(null, null);
            chkFitDistribution_Checked(null, null);
            chkShowLineKey_Checked(null, null);
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
        public string helpLocation(string topic)
        {
            return helpFiles[topic];
        }

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
       
    }
}
