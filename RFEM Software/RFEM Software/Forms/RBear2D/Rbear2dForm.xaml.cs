using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RFEM_Software.Forms;
using RFEM_Infrastructure;
using System.Globalization;
using System.Threading.Tasks;
using System.Threading;

namespace RFEM_Software.Forms
{
    /// <summary>
    /// This form is created by the main window and placed in the tab control. This
    /// form is used for data input for the RBear2d.exe application.
    /// </summary>
    public partial class Rbear2dForm : UserControl, ISimView
    {

        private RBear2dViewModel _ViewModel;

        
        private List<FrameworkElement> _HoverHelpControls;

        /// <summary>
        /// List of properties that can be plotted. This list is data bound to ComboBoxes in
        /// the form.
        /// </summary>
        public List<PlotProperty> PlotProperties;

        /// <summary>
        /// List of distributions that can be selected for the soil parameters. This list is
        /// data bound to ComboBoxes in the form.
        /// </summary>
        private List<Distribution> Distributions;

        /// <summary>
        /// List of covariance functions that can be selected. This list is bound to a ComboBox 
        /// in the form.
        /// </summary>
        private List<CovFunction> CovarianceFunctions;

        public ISimViewModel ViewModel
        {
            get
            {
                return _ViewModel;
            }
        }

        /// <summary>
        /// Contsructor for the form
        /// </summary>
        public Rbear2dForm()
        {

            _ViewModel = new RBear2dViewModel();

            this.DataContext = _ViewModel;

            //Required by framework
            InitializeComponent();

            InitializeHoveredHelpControls();

            //Initialize the various enumerations bound to ComboBoxes in the form
            InitializeEnumStructs();

            //Set the ItemsSource for each ComboBox to the appropriate List of enumerations
            SetItemsSourceForComboBoxes();

            

        }
        public Rbear2dForm(RBear2D formData)
        {
            _ViewModel = new RBear2dViewModel(formData);

            this.DataContext = _ViewModel;

            //Required by framework
            InitializeComponent();

            InitializeHoveredHelpControls();

            //Initialize the various enumerations bound to ComboBoxes in the form
            InitializeEnumStructs();

            //Set the ItemsSource for each ComboBox to the appropriate List of enumerations
            SetItemsSourceForComboBoxes();

            if(_ViewModel.PlotFirstRandomField)
                chkPlotFirstRF_Checked(null, null);
            if (_ViewModel.ProducePSPLOTOfFirstFEM)
                chkProducePSPLOTOfFirstFEM_Checked(null, null);
            if (_ViewModel.FrictionAnglePrefix == "t")
                this.rbFrictionAngleTanPhi.IsChecked = true;
            if (_ViewModel.ShowRFOnPSPLOT)
                chkShowRFOnPlot_Checked(null, null);
        }

        /// <summary>
        /// Initializes and fills the lists of enumerations. These lists are bound to 
        /// ComboBoxes in the forms.
        /// </summary>
        private void InitializeEnumStructs()
        {
            //Initialize Lists
            Distributions = new List<Distribution>();
            PlotProperties = new List<PlotProperty>();
            CovarianceFunctions = new List<CovFunction>();

            //Fill them will all defined values
            foreach (DistributionType d in Enum.GetValues(typeof(DistributionType)))
            {
                Distributions.Add(new Distribution(d));
            }

            foreach (PlotableProperty p in Enum.GetValues(typeof(PlotableProperty)))
            {
                PlotProperties.Add(new PlotProperty(p));
            }

            foreach (CovarianceFunction c in Enum.GetValues(typeof(CovarianceFunction)))
            {
                CovarianceFunctions.Add(new CovFunction(c));
            }
        }

        /// <summary>
        /// This method sets the ItemsSource of each combobox in the form to the appropriate list.
        /// </summary>
        private void SetItemsSourceForComboBoxes()
        {
            cboPlotFirstRF.ItemsSource = PlotProperties;
            cboPropertyToPlot.ItemsSource = PlotProperties;

            cboCovarianceFunc.ItemsSource = CovarianceFunctions;

            cboCohesionDist.ItemsSource = Distributions;
            cboFrictionAngleDist.ItemsSource = Distributions;
            cboDilationAngleDist.ItemsSource = Distributions;
            cboElasticModDist.ItemsSource = Distributions;
            cboPoissonRTIODist.ItemsSource = Distributions;

        }
        private void InitializeHoveredHelpControls()
        {
            _HoverHelpControls = new List<FrameworkElement>
            {
                chkEchoInput,
                chkShowRFOnPlot,
                chkShowLogRF,
                cboPropertyToPlot,
                txtDisplacedMeshWidth,
                chkOutputDebugData,
                chkPlotFirstRF,
                chkProducePSPLOTOfFirstFEM,
                chkShowMeshOnDisplacedPlot,
                chkNormalizeCapacitySamples,
                chkOutputCapacitySamples,
                txtNElementsInXDir,
                txtNElementsInYDir,
                txtElementSizeInXDir,
                txtElementSizeInYDir,
                gbNFootings,
                txtFootingWidth,
                txtFootingGap,
                txtDisplacementInc,
                txtPlasticTol,
                txtBearingTol,
                txtMaxNumSteps,
                txtMaxNumIter,
                txtNumSimulations,
                txtGeneratorSeed,
                txtCorrelationLengthXDir,
                txtCorrelationLengthYDir,
                cboCovarianceFunc,
                cboCohesionDist,
                txtCohesionMean,
                txtCohesionStdDev,
                txtCohesionLB,
                txtCohesionUB,
                txtCohesionLocation,
                txtCohesionScale,
                cboFrictionAngleDist,
                gbFrictionAngle,
                txtFrictionAngleMean,
                txtFrictionAngleStdDev,
                txtFrictionAngleLB,
                txtFrictionAngleUB,
                txtFrictionAngleLocation,
                txtFrictionAngleScale,
                cboDilationAngleDist,
                txtDilationAngleMean,
                txtDilationAngleStdDev,
                txtDilationAngleLB,
                txtDilationAngleUB,
                txtDilationAngleLocation,
                txtDilationAngleScale,
                cboElasticModDist,
                txtElasticModMean,
                txtElasticModStdDev,
                txtElasticModLB,
                txtElasticModUB,
                txtElasticModLocation,
                txtElasticModScale,
                cboPoissonRTIODist,
                txtPoissonRatioMean,
                txtPoissonRatioStdDev,
                txtPoissonRatioLB,
                txtPoissonRatioLB,
                txtPoissonRatioLocation,
                txtPoissonRatioScale,
                GridSoilPropCorrelationMatrix
            };
        }
      
        
        public string GetHoveredHelpTopic()
        {
            foreach (FrameworkElement f in _HoverHelpControls)
            {
                if (f.IsMouseOver)
                    return (string)f.Tag ;
               
            }
            return "";
        }


        /// <summary>
        /// This method changes the namescope of all context menus in the form. This is done
        /// so that their root controls can be passed in the command parameters. This method
        /// uses an extension method to recursively search the visual tree for each control
        /// in this form. Because context menus do not show up in the visual tree themselves, 
        /// we must check the context menus of every control. This must happen after the loaded 
        /// event because the tree is not availabile during initialization.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Loaded(object sender, RoutedEventArgs e)
        {
            //Initialize Context Menus
            foreach (var ctrl in this.GetChildren())
            {
                var CastCtrl = ctrl as FrameworkElement;
                if (CastCtrl != null && CastCtrl.ContextMenu != null)
                {
                    NameScope.SetNameScope(CastCtrl.ContextMenu, NameScope.GetNameScope(this));
                }
            }
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
            
        }
        #endregion

        /// <summary>
        /// Enable and make visible the ComboBox(and its label) to select which property to plot 
        /// when PlotFirstRandomField is checked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkPlotFirstRF_Checked(object sender, RoutedEventArgs e)
        {
            if(cboPlotFirstRF != null && lbPlotFirstRF != null)
            {
                cboPlotFirstRF.Visibility = Visibility.Visible;
                lbPlotFirstRF.Visibility = Visibility.Visible;
            }
                      
        }
        /// <summary>
        /// Disable and collapse ComboBox(and its label) to select which property to plot 
        /// when PlotFirstRandomField is unchecked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkPlotFirstRF_UnChecked(object sender, RoutedEventArgs e)
        {
            cboPlotFirstRF.Visibility = Visibility.Collapsed;
            lbPlotFirstRF.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Enable and make visible the StackPanel with the details of the postscript file of
        /// the first displaced finite element mesh.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkProducePSPLOTOfFirstFEM_Checked(object sender, RoutedEventArgs e)
        {
            if(spProducePSPLOTOfFirstFEM!= null)
                spProducePSPLOTOfFirstFEM.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Disable and collapse the StackPanel with the details of the postscript file of 
        /// the first displaed finite element mesh.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkProducePSPLOTOfFirstFEM_Unchecked(object sender, RoutedEventArgs e)
        {
            spProducePSPLOTOfFirstFEM.Visibility = Visibility.Collapsed;
        }

        private void chkShowRFOnPlot_Checked(object sender, RoutedEventArgs e)
        {
            if(chkShowLogRF != null && lbPropertyToPlot != null & cboPropertyToPlot != null)
            {
                chkShowLogRF.Visibility = Visibility.Visible;
                lbPropertyToPlot.Visibility = Visibility.Visible;
                cboPropertyToPlot.Visibility = Visibility.Visible;
            }
            
        }
        private void chkShowRFOnPlot_Unchecked(object sender, RoutedEventArgs e)
        {
            chkShowLogRF.Visibility = Visibility.Collapsed;
            lbPropertyToPlot.Visibility = Visibility.Collapsed;
            cboPropertyToPlot.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// This method is activated when a user indicates that there are two footings. This method
        /// enables the Footing Gap textbox and label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbTwoFootings_Checked(object sender, RoutedEventArgs e)
        {
            lbFootingGap.Visibility = Visibility.Visible;
            txtFootingGap.Visibility = Visibility.Visible;

            ((RBear2dViewModel)DataContext).NumberOfFootings = 2;
        }

        /// <summary>
        /// This method is activated when a user indicates that there is only one footing. This method
        /// hides the Footing Gap textbox and label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbTwoFootings_Unchecked(object sender, RoutedEventArgs e)
        {
            lbFootingGap.Visibility = Visibility.Collapsed;
            txtFootingGap.Visibility = Visibility.Collapsed;

            ((RBear2dViewModel)DataContext).NumberOfFootings = 1;
        }

        /// <summary>
        /// This method is activated when a user selects a distribution for the Cohesion property.
        /// It will show and hide the appropriate labels and textboxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboCohesionDist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((DistributionType)cboCohesionDist.SelectedValue)
            {
                case DistributionType.Deterministic:
                    lbCohesionMean.Visibility = Visibility.Visible;
                    txtCohesionMean.Visibility = Visibility.Visible;

                    lbCohesionStdDev.Visibility = Visibility.Collapsed;
                    txtCohesionStdDev.Visibility = Visibility.Collapsed;

                    lbCohesionLB.Visibility = Visibility.Collapsed;
                    txtCohesionLB.Visibility = Visibility.Collapsed;

                    lbCohesionUB.Visibility = Visibility.Collapsed;
                    txtCohesionUB.Visibility = Visibility.Collapsed;

                    lbCohesionLocation.Visibility = Visibility.Collapsed;
                    txtCohesionLocation.Visibility = Visibility.Collapsed;

                    lbCohesionScale.Visibility = Visibility.Collapsed;
                    txtCohesionScale.Visibility = Visibility.Collapsed;
                    break;
                case DistributionType.Normal:
                    lbCohesionMean.Visibility = Visibility.Visible;
                    txtCohesionMean.Visibility = Visibility.Visible;

                    lbCohesionStdDev.Visibility = Visibility.Visible;
                    txtCohesionStdDev.Visibility = Visibility.Visible;

                    lbCohesionLB.Visibility = Visibility.Collapsed;
                    txtCohesionLB.Visibility = Visibility.Collapsed;

                    lbCohesionUB.Visibility = Visibility.Collapsed;
                    txtCohesionUB.Visibility = Visibility.Collapsed;

                    lbCohesionLocation.Visibility = Visibility.Collapsed;
                    txtCohesionLocation.Visibility = Visibility.Collapsed;

                    lbCohesionScale.Visibility = Visibility.Collapsed;
                    txtCohesionScale.Visibility = Visibility.Collapsed;
                    break;
                case DistributionType.LogNormal:
                    lbCohesionMean.Visibility = Visibility.Visible;
                    txtCohesionMean.Visibility = Visibility.Visible;

                    lbCohesionStdDev.Visibility = Visibility.Visible;
                    txtCohesionStdDev.Visibility = Visibility.Visible;

                    lbCohesionLB.Visibility = Visibility.Collapsed;
                    txtCohesionLB.Visibility = Visibility.Collapsed;

                    lbCohesionUB.Visibility = Visibility.Collapsed;
                    txtCohesionUB.Visibility = Visibility.Collapsed;

                    lbCohesionLocation.Visibility = Visibility.Collapsed;
                    txtCohesionLocation.Visibility = Visibility.Collapsed;

                    lbCohesionScale.Visibility = Visibility.Collapsed;
                    txtCohesionScale.Visibility = Visibility.Collapsed;
                    break;
                case DistributionType.Bounded:
                    lbCohesionMean.Visibility = Visibility.Collapsed;
                    txtCohesionMean.Visibility = Visibility.Collapsed;

                    lbCohesionStdDev.Visibility = Visibility.Collapsed;
                    txtCohesionStdDev.Visibility = Visibility.Collapsed;

                    lbCohesionLB.Visibility = Visibility.Visible;
                    txtCohesionLB.Visibility = Visibility.Visible;

                    lbCohesionUB.Visibility = Visibility.Visible;
                    txtCohesionUB.Visibility = Visibility.Visible;

                    lbCohesionLocation.Visibility = Visibility.Visible;
                    txtCohesionLocation.Visibility = Visibility.Visible;

                    lbCohesionScale.Visibility = Visibility.Visible;
                    txtCohesionScale.Visibility = Visibility.Visible;
                    break;
            }
        }

        /// <summary>
        /// This method is activated when a user selects a distribution for the Friction Angle property.
        /// It will show and hide the appropriate labels and textboxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboFrictionAngleDist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((DistributionType)cboFrictionAngleDist.SelectedValue)
            {
                case DistributionType.Deterministic:
                    lbFrictionAngleMean.Visibility = Visibility.Visible;
                    txtFrictionAngleMean.Visibility = Visibility.Visible;

                    lbFrictionAngleStdDev.Visibility = Visibility.Collapsed;
                    txtFrictionAngleStdDev.Visibility = Visibility.Collapsed;

                    lbFrictionAngleLB.Visibility = Visibility.Collapsed;
                    txtFrictionAngleLB.Visibility = Visibility.Collapsed;

                    lbFrictionAngleUB.Visibility = Visibility.Collapsed;
                    txtFrictionAngleUB.Visibility = Visibility.Collapsed;

                    lbFrictionAngleLocation.Visibility = Visibility.Collapsed;
                    txtFrictionAngleLocation.Visibility = Visibility.Collapsed;

                    lbFrictionAngleScale.Visibility = Visibility.Collapsed;
                    txtFrictionAngleScale.Visibility = Visibility.Collapsed;
                    break;

                case DistributionType.Normal:
                    lbFrictionAngleMean.Visibility = Visibility.Visible;
                    txtFrictionAngleMean.Visibility = Visibility.Visible;

                    lbFrictionAngleStdDev.Visibility = Visibility.Visible;
                    txtFrictionAngleStdDev.Visibility = Visibility.Visible;

                    lbFrictionAngleLB.Visibility = Visibility.Collapsed;
                    txtFrictionAngleLB.Visibility = Visibility.Collapsed;

                    lbFrictionAngleUB.Visibility = Visibility.Collapsed;
                    txtFrictionAngleUB.Visibility = Visibility.Collapsed;

                    lbFrictionAngleLocation.Visibility = Visibility.Collapsed;
                    txtFrictionAngleLocation.Visibility = Visibility.Collapsed;

                    lbFrictionAngleScale.Visibility = Visibility.Collapsed;
                    txtFrictionAngleScale.Visibility = Visibility.Collapsed;
                    break;

                case DistributionType.LogNormal:
                    lbFrictionAngleMean.Visibility = Visibility.Visible;
                    txtFrictionAngleMean.Visibility = Visibility.Visible;

                    lbFrictionAngleStdDev.Visibility = Visibility.Visible;
                    txtFrictionAngleStdDev.Visibility = Visibility.Visible;

                    lbFrictionAngleLB.Visibility = Visibility.Collapsed;
                    txtFrictionAngleLB.Visibility = Visibility.Collapsed;

                    lbFrictionAngleUB.Visibility = Visibility.Collapsed;
                    txtFrictionAngleUB.Visibility = Visibility.Collapsed;

                    lbFrictionAngleLocation.Visibility = Visibility.Collapsed;
                    txtFrictionAngleLocation.Visibility = Visibility.Collapsed;

                    lbFrictionAngleScale.Visibility = Visibility.Collapsed;
                    txtFrictionAngleScale.Visibility = Visibility.Collapsed;
                    break;

                case DistributionType.Bounded:
                    lbFrictionAngleMean.Visibility = Visibility.Collapsed;
                    txtFrictionAngleMean.Visibility = Visibility.Collapsed;

                    lbFrictionAngleStdDev.Visibility = Visibility.Collapsed;
                    txtFrictionAngleStdDev.Visibility = Visibility.Collapsed;

                    lbFrictionAngleLB.Visibility = Visibility.Visible;
                    txtFrictionAngleLB.Visibility = Visibility.Visible;

                    lbFrictionAngleUB.Visibility = Visibility.Visible;
                    txtFrictionAngleUB.Visibility = Visibility.Visible;

                    lbFrictionAngleLocation.Visibility = Visibility.Visible;
                    txtFrictionAngleLocation.Visibility = Visibility.Visible;

                    lbFrictionAngleScale.Visibility = Visibility.Visible;
                    txtFrictionAngleScale.Visibility = Visibility.Visible;
                    break;
            }
        }

        /// <summary>
        /// This method is activated when a user selects a distribution for the Dilation Angle property.
        /// It will show and hide the appropriate labels and textboxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboDilationAngleDist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((DistributionType)cboDilationAngleDist.SelectedValue)
            {
                case DistributionType.Deterministic:
                    lbDilationAngleMean.Visibility = Visibility.Visible;
                    txtDilationAngleMean.Visibility = Visibility.Visible;

                    lbDilationAngleStdDev.Visibility = Visibility.Collapsed;
                    txtDilationAngleStdDev.Visibility = Visibility.Collapsed;

                    lbDilationAngleLB.Visibility = Visibility.Collapsed;
                    txtDilationAngleLB.Visibility = Visibility.Collapsed;

                    lbDilationAngleUB.Visibility = Visibility.Collapsed;
                    txtDilationAngleUB.Visibility = Visibility.Collapsed;

                    lbDilationAngleLocation.Visibility = Visibility.Collapsed;
                    txtDilationAngleLocation.Visibility = Visibility.Collapsed;

                    lbDilationAngleScale.Visibility = Visibility.Collapsed;
                    txtDilationAngleScale.Visibility = Visibility.Collapsed;
                    break;

                case DistributionType.Normal:
                    lbDilationAngleMean.Visibility = Visibility.Visible;
                    txtDilationAngleMean.Visibility = Visibility.Visible;

                    lbDilationAngleStdDev.Visibility = Visibility.Visible;
                    txtDilationAngleStdDev.Visibility = Visibility.Visible;

                    lbDilationAngleLB.Visibility = Visibility.Collapsed;
                    txtDilationAngleLB.Visibility = Visibility.Collapsed;

                    lbDilationAngleUB.Visibility = Visibility.Collapsed;
                    txtDilationAngleUB.Visibility = Visibility.Collapsed;

                    lbDilationAngleLocation.Visibility = Visibility.Collapsed;
                    txtDilationAngleLocation.Visibility = Visibility.Collapsed;

                    lbDilationAngleScale.Visibility = Visibility.Collapsed;
                    txtDilationAngleScale.Visibility = Visibility.Collapsed;
                    break;
                case DistributionType.LogNormal:
                    lbDilationAngleMean.Visibility = Visibility.Visible;
                    txtDilationAngleMean.Visibility = Visibility.Visible;

                    lbDilationAngleStdDev.Visibility = Visibility.Visible;
                    txtDilationAngleStdDev.Visibility = Visibility.Visible;

                    lbDilationAngleLB.Visibility = Visibility.Collapsed;
                    txtDilationAngleLB.Visibility = Visibility.Collapsed;

                    lbDilationAngleUB.Visibility = Visibility.Collapsed;
                    txtDilationAngleUB.Visibility = Visibility.Collapsed;

                    lbDilationAngleLocation.Visibility = Visibility.Collapsed;
                    txtDilationAngleLocation.Visibility = Visibility.Collapsed;

                    lbDilationAngleScale.Visibility = Visibility.Collapsed;
                    txtDilationAngleScale.Visibility = Visibility.Collapsed;
                    break;
                case DistributionType.Bounded:
                    lbDilationAngleMean.Visibility = Visibility.Collapsed;
                    txtDilationAngleMean.Visibility = Visibility.Collapsed;

                    lbDilationAngleStdDev.Visibility = Visibility.Collapsed;
                    txtDilationAngleStdDev.Visibility = Visibility.Collapsed;

                    lbDilationAngleLB.Visibility = Visibility.Visible;
                    txtDilationAngleLB.Visibility = Visibility.Visible;

                    lbDilationAngleUB.Visibility = Visibility.Visible;
                    txtDilationAngleUB.Visibility = Visibility.Visible;

                    lbDilationAngleLocation.Visibility = Visibility.Visible;
                    txtDilationAngleLocation.Visibility = Visibility.Visible;

                    lbDilationAngleScale.Visibility = Visibility.Visible;
                    txtDilationAngleScale.Visibility = Visibility.Visible;
                    break;
            }


        }

        /// <summary>
        /// This method is activated when a user selects a distribution for the Elastic Modulus property.
        /// It will show and hide the appropriate labels and textboxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboElasticModDist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((DistributionType)cboElasticModDist.SelectedValue)
            {
                case DistributionType.Deterministic:
                    lbElasticModMean.Visibility = Visibility.Visible;
                    txtElasticModMean.Visibility = Visibility.Visible;

                    lbElasticModStdDev.Visibility = Visibility.Collapsed;
                    txtElasticModStdDev.Visibility = Visibility.Collapsed;

                    lbElasticModLB.Visibility = Visibility.Collapsed;
                    txtElasticModLB.Visibility = Visibility.Collapsed;

                    lbElasticModUB.Visibility = Visibility.Collapsed;
                    txtElasticModUB.Visibility = Visibility.Collapsed;

                    lbElasticModLocation.Visibility = Visibility.Collapsed;
                    txtElasticModLocation.Visibility = Visibility.Collapsed;

                    lbElasticModScale.Visibility = Visibility.Collapsed;
                    txtElasticModScale.Visibility = Visibility.Collapsed;
                    break;

                case DistributionType.Normal:
                    lbElasticModMean.Visibility = Visibility.Visible;
                    txtElasticModMean.Visibility = Visibility.Visible;

                    lbElasticModStdDev.Visibility = Visibility.Visible;
                    txtElasticModStdDev.Visibility = Visibility.Visible;

                    lbElasticModLB.Visibility = Visibility.Collapsed;
                    txtElasticModLB.Visibility = Visibility.Collapsed;

                    lbElasticModUB.Visibility = Visibility.Collapsed;
                    txtElasticModUB.Visibility = Visibility.Collapsed;

                    lbElasticModLocation.Visibility = Visibility.Collapsed;
                    txtElasticModLocation.Visibility = Visibility.Collapsed;

                    lbElasticModScale.Visibility = Visibility.Collapsed;
                    txtElasticModScale.Visibility = Visibility.Collapsed;
                    break;

                case DistributionType.LogNormal:
                    lbElasticModMean.Visibility = Visibility.Visible;
                    txtElasticModMean.Visibility = Visibility.Visible;

                    lbElasticModStdDev.Visibility = Visibility.Visible;
                    txtElasticModStdDev.Visibility = Visibility.Visible;

                    lbElasticModLB.Visibility = Visibility.Collapsed;
                    txtElasticModLB.Visibility = Visibility.Collapsed;

                    lbElasticModUB.Visibility = Visibility.Collapsed;
                    txtElasticModUB.Visibility = Visibility.Collapsed;

                    lbElasticModLocation.Visibility = Visibility.Collapsed;
                    txtElasticModLocation.Visibility = Visibility.Collapsed;

                    lbElasticModScale.Visibility = Visibility.Collapsed;
                    txtElasticModScale.Visibility = Visibility.Collapsed;
                    break;

                case DistributionType.Bounded:
                    lbElasticModMean.Visibility = Visibility.Collapsed;
                    txtElasticModMean.Visibility = Visibility.Collapsed;

                    lbElasticModStdDev.Visibility = Visibility.Collapsed;
                    txtElasticModStdDev.Visibility = Visibility.Collapsed;

                    lbElasticModLB.Visibility = Visibility.Visible;
                    txtElasticModLB.Visibility = Visibility.Visible;

                    lbElasticModUB.Visibility = Visibility.Visible;
                    txtElasticModUB.Visibility = Visibility.Visible;

                    lbElasticModLocation.Visibility = Visibility.Visible;
                    txtElasticModLocation.Visibility = Visibility.Visible;

                    lbElasticModScale.Visibility = Visibility.Visible;
                    txtElasticModScale.Visibility = Visibility.Visible;
                    break;

            }
        }

        /// <summary>
        /// This method is activated when a user selects a distribution for the Poisson's Ratio property.
        /// It will show and hide the appropriate labels and textboxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboPoissonRTIODist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((DistributionType)cboPoissonRTIODist.SelectedValue)
            {
                case DistributionType.Deterministic:
                    lbPoissonRatioMean.Visibility = Visibility.Visible;
                    txtPoissonRatioMean.Visibility = Visibility.Visible;

                    lbPoissonRatioStdDev.Visibility = Visibility.Collapsed;
                    txtPoissonRatioStdDev.Visibility = Visibility.Collapsed;

                    lbPoissonRatioLB.Visibility = Visibility.Collapsed;
                    txtPoissonRatioLB.Visibility = Visibility.Collapsed;

                    lbPoissonRatioUB.Visibility = Visibility.Collapsed;
                    txtPoissonRatioUB.Visibility = Visibility.Collapsed;

                    lbPoissonRatioLocation.Visibility = Visibility.Collapsed;
                    txtPoissonRatioLocation.Visibility = Visibility.Collapsed;

                    lbPoissonRatioScale.Visibility = Visibility.Collapsed;
                    txtPoissonRatioScale.Visibility = Visibility.Collapsed;
                    break;

                case DistributionType.Normal:
                    lbPoissonRatioMean.Visibility = Visibility.Visible;
                    txtPoissonRatioMean.Visibility = Visibility.Visible;

                    lbPoissonRatioStdDev.Visibility = Visibility.Visible;
                    txtPoissonRatioStdDev.Visibility = Visibility.Visible;

                    lbPoissonRatioLB.Visibility = Visibility.Collapsed;
                    txtPoissonRatioLB.Visibility = Visibility.Collapsed;

                    lbPoissonRatioUB.Visibility = Visibility.Collapsed;
                    txtPoissonRatioUB.Visibility = Visibility.Collapsed;

                    lbPoissonRatioLocation.Visibility = Visibility.Collapsed;
                    txtPoissonRatioLocation.Visibility = Visibility.Collapsed;

                    lbPoissonRatioScale.Visibility = Visibility.Collapsed;
                    txtPoissonRatioScale.Visibility = Visibility.Collapsed;
                    break;

                case DistributionType.LogNormal:
                    lbPoissonRatioMean.Visibility = Visibility.Visible;
                    txtPoissonRatioMean.Visibility = Visibility.Visible;

                    lbPoissonRatioStdDev.Visibility = Visibility.Visible;
                    txtPoissonRatioStdDev.Visibility = Visibility.Visible;

                    lbPoissonRatioLB.Visibility = Visibility.Collapsed;
                    txtPoissonRatioLB.Visibility = Visibility.Collapsed;

                    lbPoissonRatioUB.Visibility = Visibility.Collapsed;
                    txtPoissonRatioUB.Visibility = Visibility.Collapsed;

                    lbPoissonRatioLocation.Visibility = Visibility.Collapsed;
                    txtPoissonRatioLocation.Visibility = Visibility.Collapsed;

                    lbPoissonRatioScale.Visibility = Visibility.Collapsed;
                    txtPoissonRatioScale.Visibility = Visibility.Collapsed;
                    break;

                case DistributionType.Bounded:
                    lbPoissonRatioMean.Visibility = Visibility.Collapsed;
                    txtPoissonRatioMean.Visibility = Visibility.Collapsed;

                    lbPoissonRatioStdDev.Visibility = Visibility.Collapsed;
                    txtPoissonRatioStdDev.Visibility = Visibility.Collapsed;

                    lbPoissonRatioLB.Visibility = Visibility.Visible;
                    txtPoissonRatioLB.Visibility = Visibility.Visible;

                    lbPoissonRatioUB.Visibility = Visibility.Visible;
                    txtPoissonRatioUB.Visibility = Visibility.Visible;

                    lbPoissonRatioLocation.Visibility = Visibility.Visible;
                    txtPoissonRatioLocation.Visibility = Visibility.Visible;

                    lbPoissonRatioScale.Visibility = Visibility.Visible;
                    txtPoissonRatioScale.Visibility = Visibility.Visible;
                    break;
            }
        }
        private void rbFrictionAnglePhi_Checked(object sender, RoutedEventArgs e)
        {
                _ViewModel.FrictionAnglePrefix = "";
        }
        private void rbFrictionAngleTanPhi_Checked(object sender, RoutedEventArgs e)
        {
            _ViewModel.FrictionAnglePrefix = "t";
        }
    }
    
}

