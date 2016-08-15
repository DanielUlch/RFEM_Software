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
using System.ComponentModel;

namespace RFEMSoftware.Simulation.Desktop.CustomControls
{
    /// <summary>
    /// Interaction logic for TabItemHeader.xaml
    /// </summary>
    public partial class TabItemHeader : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty ImageSourceProperty =
    DependencyProperty.Register("ImageSource", typeof(BitmapImage), typeof(TabItemHeader));

        public static readonly DependencyProperty TextProperty =
    DependencyProperty.Register("Text", typeof(string), typeof(TabItemHeader));

        public event PropertyChangedEventHandler PropertyChanged;

        public BitmapImage Image
        {
            get { return (BitmapImage)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value);
                PropertyChanged(this, new PropertyChangedEventArgs("Image"));
            }
        }
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public TabItemHeader()
        {
            InitializeComponent();
        }
    }
}
