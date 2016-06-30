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

namespace RFEM_Software
{
    /// <summary>
    /// Ribbon control used in the main window.
    /// </summary>
    public partial class RFEMRibbon : UserControl
    {
        public delegate void NewDataFileRequestedHandler(object sender, RoutedEventArgs e);
        public event NewDataFileRequestedHandler NewDataFileRequested;
        public RFEMRibbon()
        {
            InitializeComponent();
        }

        private void NewDataFile(object sender, RoutedEventArgs e)
        {
            NewDataFileRequested(sender, e);
        }
        
    }
}
