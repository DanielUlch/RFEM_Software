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

namespace RFEM_Software.Forms
{
    /// <summary>
    /// Interaction logic for Rdam2dForm.xaml
    /// </summary>
    public partial class Rdam2dForm : UserControl, ISimView
    {
        public Rdam2dForm()
        {
            InitializeComponent();
        }

        public ISimViewModel ViewModel
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string HelpLocation(FrameworkElement F)
        {
            throw new NotImplementedException();
        }

        public string hoveredHelpDocLocation()
        {
            throw new NotImplementedException();
        }
    }
}
