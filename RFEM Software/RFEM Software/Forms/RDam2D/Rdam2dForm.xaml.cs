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
    /// Interaction logic for Rdam2dForm.xaml
    /// </summary>
    public partial class Rdam2dForm : UserControl, ISimView
    {
        private RDam2DViewModel _ViewModel;
        public Rdam2dForm()
        {
            _ViewModel = new RDam2DViewModel();

            this.DataContext = _ViewModel;

            InitializeComponent();

            
        }
        public Rdam2dForm(RDam2D formData)
        {
            _ViewModel = new RDam2DViewModel(formData);

            this.DataContext = _ViewModel;

            InitializeComponent();


        }

        public ISimViewModel ViewModel
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

        private void rbDebugCode2_Checked(object sender, RoutedEventArgs e)
        {
            _ViewModel.DebugCode = 2;
        }

        private void rbDebugCode3_Checked(object sender, RoutedEventArgs e)
        {
            _ViewModel.DebugCode = 3;
        }

        private void rbDebugCode1_Checked(object sender, RoutedEventArgs e)
        {
            _ViewModel.DebugCode = 1;
        }

        private void rbGeometricAlgo_Checked(object sender, RoutedEventArgs e)
        {
            _ViewModel.SpacingAlgo = RFEM_Infrastructure.SpacingAlgorithm.Geometric;
        }

        private void rbLinearAlgo_Checked(object sender, RoutedEventArgs e)
        {
            _ViewModel.SpacingAlgo = RFEM_Infrastructure.SpacingAlgorithm.Linear;
        }

        private void rbProportionalAlgo_Checked(object sender, RoutedEventArgs e)
        {
            _ViewModel.SpacingAlgo = RFEM_Infrastructure.SpacingAlgorithm.Proportional;
        }
    }
}
