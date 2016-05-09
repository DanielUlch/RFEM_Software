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

namespace RFEM_Software
{
    /// <summary>
    /// This form is created by the main window and placed in the tab control. This
    /// form is used for data input for the RBear2d.exe application.
    /// </summary>
    public partial class Rbear2dForm : UserControl, IHelpFiled
    {

        private RBear2dViewModel _ViewModel;

        /// <summary>
        /// Dictionary of controls and the locations of their help files
        /// </summary>
        private Dictionary<FrameworkElement, string> HelpLocations;

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

        /// <summary>
        /// Default help file location for this form
        /// </summary>
        private string DefaultHelp;

        /// <summary>
        /// Contsructor for the form
        /// </summary>
        public Rbear2dForm()
        {

            _ViewModel = new RBear2dViewModel();

            this.DataContext = _ViewModel;

            //Required by framework
            InitializeComponent();

            //Load the locations of the help files into memory
            HelpLocations = InitializeHelpLocations();

            //Initialize the various enumerations bound to ComboBoxes in the form
            InitializeEnumStructs();

            //Set the ItemsSource for each ComboBox to the appropriate List of enumerations
            SetItemsSourceForComboBoxes();

            
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
      
        /// <summary>
        /// Loads the locations of each help file into memory and stores them in a
        /// dictionary where the key is the control that is associated with that help
        /// file
        /// </summary>
        /// <returns>
        /// A dictionary of help file locations
        /// </returns>
        private Dictionary<FrameworkElement, string > InitializeHelpLocations()
        {
            var dict = new Dictionary<FrameworkElement, string>();

            //Namespace of the help files for this form
            string Base = "RFEM_Software.Help_Files.RBear_Help_Files.";

            //Default help file location for this form
            DefaultHelp = Base + "RBear2dHelp.xaml";

            //Individual control help files
            dict.Add(chkEchoInput, Base + "EchoInputToOutputFile.xaml");
            dict.Add(chkShowRFOnPlot, Base + "ShowRFOnDisplacedMeshPlot.xaml");
            dict.Add(chkShowLogRF, Base + "ShowLogRF.xaml");
            dict.Add(cboPropertyToPlot, Base + "PropertyToPlot.xaml");
            dict.Add(txtDisplacedMeshWidth, Base + "DisplacedMeshWidth.xaml");
            dict.Add(chkReportProgress, Base + "ReportProgress.xaml");
            dict.Add(chkOutputDebugData, Base + "OutputDebugData.xaml");
            dict.Add(chkPlotFirstRF, Base + "PlotFirstRF.xaml");
            dict.Add(chkProducePSPLOTOfFirstFEM, Base + "ProducePSPLOTOfFirstFEM.xaml");
            dict.Add(chkShowMeshOnDisplacedPlot, Base + "ShowMeshOnDisplacedPlot.xaml");
            dict.Add(chkNormalizeCapacitySamples, Base + "NormalizeCapacitySamples.xaml");
            dict.Add(chkOutputCapacitySamples, Base + "OutputCapacitySamples.xaml");
            dict.Add(txtNElementsInXDir, Base + "NumberOfElementsInXYDirections.xaml");
            dict.Add(txtNElementsInYDir, Base + "NumberOfElementsInXYDirections.xaml");
            dict.Add(txtElementSizeInXDir, Base + "ElementSizeInXYDirections.xaml");
            dict.Add(txtElementSizeInYDir, Base + "ElementSizeInXYDirections.xaml");
            dict.Add(gbNFootings, Base + "NSizeOfFootings.xaml");
            dict.Add(txtFootingWidth, Base + "NSizeOfFootings.xaml");
            dict.Add(txtFootingGap, Base + "NSizeOfFootings.xaml");
            dict.Add(txtDisplacementInc, Base + "DisplacementIncPlasticTolBearingTol.xaml");
            dict.Add(txtPlasticTol , Base + "DisplacementIncPlasticTolBearingTol.xaml");
            dict.Add(txtBearingTol, Base + "DisplacementIncPlasticTolBearingTol.xaml");
            dict.Add(txtMaxNumSteps, Base + "MaxStepsAndIterations.xaml");
            dict.Add(txtMaxNumIter, Base + "MaxStepsAndIterations.xaml");
            dict.Add(txtNumSimulations, Base + "NumberOfSimulations.xaml");
            dict.Add(txtGeneratorSeed, Base + "GeneratorSeed.xaml");
            dict.Add(txtCorrelationLengthXDir, Base + "CorrelationLengths.xaml");
            dict.Add(txtCorrelationLengthYDir, Base + "CorrelationLengths.xaml");
            dict.Add(cboCovarianceFunc, Base + "CovarianceFunction.xaml");
            dict.Add(cboCohesionDist, Base + "CohesionDistribution.xaml");
            dict.Add(txtCohesionMean, Base + "CohesionDistribution.xaml");
            dict.Add(txtCohesionStdDev, Base + "CohesionDistribution.xaml");
            dict.Add(txtCohesionLB, Base + "CohesionDistribution.xaml");
            dict.Add(txtCohesionUB, Base + "CohesionDistribution.xaml");
            dict.Add(txtCohesionLocation, Base + "CohesionDistribution.xaml");
            dict.Add(txtCohesionScale, Base + "CohesionDistribution.xaml");
            dict.Add(cboFrictionAngleDist, Base + "FrictionAngleDistribution.xaml");
            dict.Add(gbFrictionAngle, Base + "FrictionAngleDistribution.xaml");
            dict.Add(txtFrictionAngleMean, Base + "FrictionAngleDistribution.xaml");
            dict.Add(txtFrictionAngleStdDev, Base + "FrictionAngleDistribution.xaml");
            dict.Add(txtFrictionAngleLB, Base + "FrictionAngleDistribution.xaml");
            dict.Add(txtFrictionAngleUB, Base + "FrictionAngleDistribution.xaml");
            dict.Add(txtFrictionAngleLocation, Base + "FrictionAngleDistribution.xaml");
            dict.Add(txtFrictionAngleScale, Base + "FrictionAngleDistribution.xaml");
            dict.Add(cboDilationAngleDist, Base + "DilationAngleDistribution.xaml");
            dict.Add(txtDilationAngleMean, Base + "DilationAngleDistribution.xaml");
            dict.Add(txtDilationAngleStdDev, Base + "DilationAngleDistribution.xaml");
            dict.Add(txtDilationAngleLB, Base + "DilationAngleDistribution.xaml");
            dict.Add(txtDilationAngleUB, Base + "DilationAngleDistribution.xaml");
            dict.Add(txtDilationAngleLocation, Base + "DilationAngleDistribution.xaml");
            dict.Add(txtDilationAngleScale, Base + "DilationAngleDistribution.xaml");
            dict.Add(cboElasticModDist, Base + "ElasticModulusDistribution.xaml");
            dict.Add(txtElasticModMean, Base + "ElasticModulusDistribution.xaml");
            dict.Add(txtElasticModStdDev, Base + "ElasticModulusDistribution.xaml");
            dict.Add(txtElasticModLB, Base + "ElasticModulusDistribution.xaml");
            dict.Add(txtElasticModUB, Base + "ElasticModulusDistribution.xaml");
            dict.Add(txtElasticModLocation, Base + "ElasticModulusDistribution.xaml");
            dict.Add(txtElasticModScale, Base + "ElasticModulusDistribution.xaml");
            dict.Add(cboPoissonRTIODist, Base + "PoissonRatioDistribution.xaml");
            dict.Add(txtPoissonRatioMean, Base + "PoissonRatioDistribution.xaml");
            dict.Add(txtPoissonRatioStdDev, Base + "PoissonRatioDistribution.xaml");
            dict.Add(txtPoissonRatioLB, Base + "PoissonRatioDistribution.xaml");
            dict.Add(txtPoissonRatioUB, Base + "PoissonRatioDistribution.xaml");
            dict.Add(txtPoissonRatioLocation, Base + "PoissonRatioDistribution.xaml");
            dict.Add(txtPoissonRatioScale, Base + "PoissonRatioDistribution.xaml");
            dict.Add(GridSoilPropCorrelationMatrix, Base + "SoilPropertyCorrelationMatrix.xaml");

            return dict;       
        }

        /// <summary>
        /// Called by the main window when F1 is pressed.
        /// This method checks whether any of the help documented controls are hovered, and
        /// if so returns the help file location of that control. If not, the method returns
        /// the default help file location
        /// </summary>
        /// <returns>
        /// Help file location of the hovered control, or the default help file location
        /// </returns>
        public string hoveredHelpDocLocation()
        {
            foreach (FrameworkElement f in HelpLocations.Keys)
            {
                if (f.IsMouseOver)
                    return HelpLocations[f];
               
            }
            return DefaultHelp;
        }

        /// <summary>
        /// Called by the main window when a user clicks the help item in a context menu.
        /// The control whos menu command was activated is passed via the command parameter.
        /// This control is passed to this method as "F".
        /// This method returns the help file location of the control, or the default help
        /// file for this form
        /// </summary>
        /// <param name="F">
        /// The control whos context menu help command was activated
        /// </param>
        /// <returns>
        /// The location of the help file associated with the passed control, or the location
        /// of the default help file for this form.
        /// </returns>
        public string HelpLocation(FrameworkElement F)
        {
            if(HelpLocations[F] != null)
            {
                return HelpLocations[F];
            }
            else
            {
                return DefaultHelp;
            }
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
            throw new NotImplementedException();
        }
        /// <summary>
        /// This method is to appease the compiler. The help click command gets bound to
        /// a method in the main window at runtime.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpClickExeStub(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
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
            cboPlotFirstRF.Visibility = Visibility.Visible;
            lbPlotFirstRF.Visibility = Visibility.Visible;
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
            chkShowLogRF.Visibility = Visibility.Visible;
            lbPropertyToPlot.Visibility = Visibility.Visible;
            cboPropertyToPlot.Visibility = Visibility.Visible;
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

        private void ReviseFormData()
        {

        }

        public async Task<bool> RunSimAsync()
        {
           
            
            return _ViewModel.RunSim();
        }
        public async Task<string> RunSimTestAsync(CancellationToken token)
        {
            return await  _ViewModel.RunSimTest(token);
        }
        public async Task<string> RunSimTest2Async(CancellationToken token)
        {
            return await _ViewModel.RunSimTest2(token);
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
    public class DoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // return an invalid value in case of the value ends with a point
            return value.ToString().EndsWith(".") ? "." : value;
        }
    }
}

