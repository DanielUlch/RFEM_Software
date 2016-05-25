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
using System.Windows.Controls.Primitives;




namespace RFEM_Software.Custom_Controls
{
    /// <summary>
    /// Interaction logic for RFEMToolTip.xaml
    /// </summary>
    public class RFEMToolTip : ToolTip, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static DependencyProperty ToolTipMessageProperty;
        public static DependencyProperty ErrorMessageProperty;
        public static DependencyProperty HasErrorsProperty;
        public static DependencyProperty TitleProperty;

        static RFEMToolTip()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RFEMToolTip), 
                new FrameworkPropertyMetadata(typeof(RFEMToolTip)));

            ToolTipMessageProperty = DependencyProperty.Register("ToolTipMessage", typeof(string), typeof(RFEMToolTip));
            ErrorMessageProperty = DependencyProperty.Register("ErrorMessage", typeof(string), typeof(RFEMToolTip));
            HasErrorsProperty = DependencyProperty.Register("HasErrors", typeof(bool), typeof(RFEMToolTip));
            TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(RFEMToolTip));
        }
        public bool HasErrors
        {
            get { return (bool)GetValue(HasErrorsProperty); }
            set
            {
                if ((bool)GetValue(HasErrorsProperty) != value)
                {
                    SetValue(HasErrorsProperty, value);

                    PropertyChanged(this, new PropertyChangedEventArgs("HasErrors"));
                }
            }
        }
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set
            {
                if((string)GetValue(TitleProperty) != value)
                {
                    SetValue(TitleProperty, value);
                    PropertyChanged(this, new PropertyChangedEventArgs("Title"));
                }
            }
        }
        public string ToolTipMessage
        {
            get { return (string)GetValue(ToolTipMessageProperty); }
            set
            {
                if ((string)GetValue(ToolTipMessageProperty) != value)
                {
                    SetValue(ToolTipMessageProperty, value);
                    PropertyChanged(this, new PropertyChangedEventArgs("ToolTipMessage"));
                }
            }
        }
        public string ErrorMessage
        {
            get { return (string)GetValue(ErrorMessageProperty); }
            set
            {
                if ((string)GetValue(ErrorMessageProperty) != value)
                {
                    SetValue(ErrorMessageProperty, value);
                    PropertyChanged(this, new PropertyChangedEventArgs("ErrorMessage"));
                }

            }
        }
    }
}
