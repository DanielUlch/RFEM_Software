using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RFEMSoftware.Simulation.Infrastructure;
using RFEMSoftware.Simulation.Infrastructure.Models;
using System.Windows.Data;
using RFEMSoftware.Simulation.Desktop.CustomControls;

namespace RFEMSoftware.Simulation.Desktop.Forms
{
    /// <summary>
    /// This form is created by the main window and placed in the tab control. This
    /// form is used for data input for the RBear2d.exe application.
    /// </summary>
    public partial class RBear2DForm : UserControl, ISimView
    {

        private RBear2DViewModel _ViewModel;


        private List<FrameworkElement> _HoverHelpControls;
        
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
        public RBear2DForm()
        {

            _ViewModel = new RBear2DViewModel();

            this.DataContext = _ViewModel;

            //Required by framework
            InitializeComponent();

            InitializeHoveredHelpControls();

            dgCorrelationMatrix.ItemsSource = DataGridMatrixHelper.GetBindable2DArray(_ViewModel.CorrelationMatrix);

        }
        public RBear2DForm(RBear2D formData)
        {
            _ViewModel = new RBear2DViewModel(formData);

            this.DataContext = _ViewModel;

            //Required by framework
            InitializeComponent();

            InitializeHoveredHelpControls();

            dgCorrelationMatrix.ItemsSource = DataGridMatrixHelper.GetBindable2DArray(_ViewModel.CorrelationMatrix);
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
            };

            _HoverHelpControls.AddRange(ccCohesionDistribution.GetAllControls());
            _HoverHelpControls.AddRange(ccFrictionAngleDistribution.GetAllControls());
            _HoverHelpControls.AddRange(ccDilationAngleDistribution.GetAllControls());
            _HoverHelpControls.AddRange(ccElasticModulusDistribution.GetAllControls());
            _HoverHelpControls.AddRange(ccPoisssonsRatioDistribution.GetAllControls());
        }


        public string GetHoveredHelpTopic()
        {
            foreach (FrameworkElement f in _HoverHelpControls)
            {
                if (f.IsMouseOver)
                    return (string)f.Tag;

            }
            return "";
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
        /// This method is activated when a user indicates that there are two footings. This method
        /// enables the Footing Gap textbox and label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbTwoFootings_Checked(object sender, RoutedEventArgs e)
        {
            ((RBear2DViewModel)DataContext).NumberOfFootings = 2;
        }

        /// <summary>
        /// This method is activated when a user indicates that there is only one footing. This method
        /// hides the Footing Gap textbox and label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbTwoFootings_Unchecked(object sender, RoutedEventArgs e)
        {
            ((RBear2DViewModel)DataContext).NumberOfFootings = 1;
        }
        private void dgCorrelationMatrix_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridTextColumn column = e.Column as DataGridTextColumn;
            int header;
            if (int.TryParse(column.Header.ToString(), out header))
            {
                column.Header = ((SoilProperty)header).ToUIString();
            }

            Binding binding = column.Binding as Binding;
            binding.Path = new PropertyPath(binding.Path.Path + ".Value");
        }

        private void dgCorrelationMatrix_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = ((SoilProperty)e.Row.GetIndex()).ToUIString();
        }
    }

}


