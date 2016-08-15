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
    /// Interaction logic for SoilDistribution.xaml
    /// </summary>
    public partial class SoilDistribution : UserControl, INotifyPropertyChanged
    {

        public static DependencyProperty DistributionProperty = DependencyProperty.
                            Register("Distribution", typeof(DistributionInfo), typeof(SoilDistribution));
        public static DependencyProperty DistributionLabelProperty = DependencyProperty.
                            Register("DistributionLabel", typeof(string), typeof(SoilDistribution));
        public static DependencyProperty DistributionTagProperty = DependencyProperty.
                            Register("DistributionTag", typeof(string), typeof(SoilDistribution));
        public static DependencyProperty IsFrictionAngleProperty = DependencyProperty.
                            Register("IsFrictionAngle", typeof(bool), typeof(SoilDistribution));
        public static DependencyProperty FrictionAngleTypeProperty = DependencyProperty.
                            Register("FrictionAngleType", typeof(FrictionAngle), typeof(SoilDistribution));
        public SoilDistribution()
        {
            InitializeComponent();
        }
        public FrictionAngle FrictionAngleType
        {
            get { return (FrictionAngle)GetValue(FrictionAngleTypeProperty); }
            set
            {
                if((FrictionAngle)GetValue(FrictionAngleTypeProperty) != value)
                {
                    SetValue(FrictionAngleTypeProperty, value);
                    NotifyPropertyChanged();
                }
            }
        }
        public bool IsFrictionAngle
        {
            get { return (bool)GetValue(IsFrictionAngleProperty); }
            set
            {
                if((bool)GetValue(IsFrictionAngleProperty) != value)
                {
                    SetValue(IsFrictionAngleProperty, value);
                    NotifyPropertyChanged();
                }
            }
        }
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
                if((string)GetValue(DistributionLabelProperty) != value)
                {
                    SetValue(DistributionLabelProperty, value);
                    NotifyPropertyChanged();
                }
            }
        }
        public DistributionInfo Distribution
        {
            get { return (DistributionInfo)GetValue(DistributionProperty); }
            set
            {
                if ((DistributionInfo)GetValue(DistributionProperty) != value)
                {
                    SetValue(DistributionProperty, value);
                    NotifyPropertyChanged();
                }
            }
        }

        public List<FrameworkElement> GetAllControls()
        {
            var l = new List<FrameworkElement>()
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
                txtScale
            };

            if (IsFrictionAngle)
            {
                l.Add(lbFrictionAngleType);
                l.Add(cboFrictionAngleType);
            }

            return l;
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
