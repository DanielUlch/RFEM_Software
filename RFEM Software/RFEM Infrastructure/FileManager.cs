using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEMSoftware.Simulation.Infrastructure
{
    public class FileManager: INotifyPropertyChanged
    {
        private string _OutputDirectory;
        public string OriginFileLocation { get; private set; }
        public string OutputDirectory
        {
            get { return _OutputDirectory; }
            set
            {
                if(_OutputDirectory != value)
                {
                    _OutputDirectory = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public FileManager(string originFileLocation)
        {
            //Set default directory
            OutputDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + 
                "\\RFEMSoftware";

            OriginFileLocation = originFileLocation;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
