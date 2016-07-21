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
       
        public IHistViewModel ViewModel
        {
            get { return viewModel; }
        }
        public RBear2DHistForm(int nSim, int nFootings, string baseName, string inputFilePath)
        {
            viewModel = new RBear2DHistViewModel(nSim, nFootings, baseName, inputFilePath);

            this.DataContext = viewModel;
            
            InitializeComponent();

            this.HistCore.DataContext = viewModel.HistogramCore;

            InitializeHoverableList();
            
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
            };

            _HoverableControls.AddRange(HistCore.HoverableControls);
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
            try
            {
                HistElement.Child = viewModel.GenerateHistogram();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
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
            try
            {
                viewModel.PopOutHistogram();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
