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
using System.Windows.Shapes;
using System.ComponentModel;
using RFEMSoftware.Simulation.Infrastructure;

namespace RFEMSoftware.Simulation.Desktop.Dialogs
{
    /// <summary>
    /// Interaction logic for ReadDataFileDialog.xaml
    /// </summary>
    public partial class ReadDataFileDialog : Window, INotifyPropertyChanged
    {
        private List<Program> _ProgramTypes;
        private string _FilePath;

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<string, Program, bool> _SetValues;

        public List<Program> ProgramTypes
        {
            get { return _ProgramTypes; }
        }
        public string FilePath
        {
            get { return _FilePath; }
            set
            {
                if (_FilePath != value)
                {
                    _FilePath = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public ReadDataFileDialog(Action<string, Program, bool> setValues)
        {
            InitializeComponent();
            InitializeLists();

            FilePath = "Select File";

            _SetValues = setValues;

            this.DataContext = this;
        }
        private void InitializeLists()
        {
            _ProgramTypes = new List<Program>();
            foreach(Program p in Enum.GetValues(typeof(Program)))
            {
                _ProgramTypes.Add(p);
            }
        }

        private void btnBrowseFiles_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Filter = "Data File|*.dat";

            if(fileDialog.ShowDialog() == true)
            {
                FilePath = fileDialog.FileName;
            }

        }
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            _SetValues.Invoke("", Program.RBear2D, false);
            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if(lvDataFileTypes.SelectedItem == null)
            {
                MessageBox.Show("Please select a data file type", "Invalid Data File Type", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else
            {
                if (!System.IO.File.Exists(_FilePath))
                {
                    MessageBox.Show("Please select a valid file", "Invalid File Path", MessageBoxButton.OK,

                        MessageBoxImage.Error);
                }
                else
                {
                    _SetValues.Invoke(_FilePath, (Program)lvDataFileTypes.SelectedItem, true);
                    this.Close();
                }
            }
        }
    }
}
