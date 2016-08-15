using RFEMSoftware.Simulation.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace RFEMSoftware.Simulation.Desktop.CustomControls
{
    /// <summary>
    /// Interaction logic for REarthSoilDistribution.xaml
    /// </summary>
    public partial class REarthSoilDistribution : UserControl, INotifyPropertyChanged
    {


        public static DependencyProperty DistributionProperty = DependencyProperty.
                            Register("Distribution", typeof(REarthDistributionInfo), typeof(REarthSoilDistribution));
        public static DependencyProperty DistributionLabelProperty = DependencyProperty.
                            Register("DistributionLabel", typeof(string), typeof(REarthSoilDistribution));
        public static DependencyProperty DistributionTagProperty = DependencyProperty.
                            Register("DistributionTag", typeof(string), typeof(REarthSoilDistribution));



        public string DistributionTag
        {
            get { return (string)GetValue(DistributionTagProperty); }
            set
            {
                if ((string)GetValue(DistributionTagProperty) != value)
                {
                    SetValue(DistributionTagProperty, value);
                    NotifyPropertyChanged();
                }
            }
        }
        public string DistributionLabel
        {
            get { return (string)GetValue(DistributionLabelProperty); }
            set
            {
                if ((string)GetValue(DistributionLabelProperty) != value)
                {
                    SetValue(DistributionLabelProperty, value);
                    NotifyPropertyChanged();
                }
            }
        }
        public REarthDistributionInfo Distribution
        {
            get { return (REarthDistributionInfo)GetValue(DistributionProperty); }
            set
            {
                if ((REarthDistributionInfo)GetValue(DistributionProperty) != value)
                {
                    SetValue(DistributionProperty, value);
                    NotifyPropertyChanged();
                }
            }
        }

        public REarthSoilDistribution()
        {
            InitializeComponent();
        }



        public List<FrameworkElement> GetAllControls()
        {
            return new List<FrameworkElement>()
            {
                lbDist,
                cboDist,
                lbMean,
                txtMean,
                lbStdDev,
                txtStdDev,
                lbLowerBound,
                txtLowerBound,
                lbUpperBound,
                txtUpperBound,
                lbLocation,
                txtLocation,
                lbScale,
                txtScale,
                lbIntercept,
                txtIntercept,
                lbSlope,
                txtSlope,
                lbPhiFunc,
                cboPhiFunc

            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
