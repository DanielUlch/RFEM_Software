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


namespace RFEM_Software
{
    /// <summary>
    /// This form is created by the main window and placed in the tab control. This
    /// form is used for data input for the RBear2d.exe application.
    /// </summary>
    public partial class Rbear2d : UserControl, IHelpFiled
    {

        /// <summary>
        /// Dictionary of controls and the locations of their help files
        /// </summary>
        private Dictionary<FrameworkElement, string> HelpLocations;

        /// <summary>
        /// Default help file location for this form
        /// </summary>
        private string DefaultHelp;

        /// <summary>
        /// Contsructor for the form
        /// </summary>
        public Rbear2d()
        {
            //Required by framework
            InitializeComponent();

            //Load the locations of the help files into memory
            HelpLocations = InitializeHelpLocations();
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
    }
}

