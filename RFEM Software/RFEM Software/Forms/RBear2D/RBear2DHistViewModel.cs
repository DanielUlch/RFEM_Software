using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFEM_Infrastructure;
using System.ComponentModel;
using System.Diagnostics;

namespace RFEM_Software.Forms
{
    public class RBear2DHistViewModel : IHistViewModel, IDataErrorInfo, INotifyPropertyChanged
    {
        private int _NSim;
        private int _NFooting;
        private string _BaseName;

        private int footingNum = 1;
        private bool showPlotTitles = true;
        private double vertOffset = 0;
        private int nIntervals;

        private bool customAxis = false;
        private double xAxisLength = 5;
        private double xAxisOrigin = 1.5;
        private double yAxisLength = 4;
        private double yAxisOrigin = 1.5;

        private bool useLogXAxis = false;

        private bool customXAxis = false;
        private double xAxisMin = 0;
        private double xAxisMax = 10;
        private double xAxisIncrement = 2;

        private bool customYAxis = false;
        private double yAxisMin = 0;
        private double yAxisMax = 10;
        private double yAxisIncrement = 2;

        private bool fitDistribution = false;
        private HistogramDistribution fittedDistribution = HistogramDistribution.LogNormal;

        private bool showAndersonDarlingStat = false;
        private bool showChiSquareStat = true;

        private bool showLineKey = true;

        private double lineKeyXOffset = 0;
        private double lineKeyYOffset = 0;
        
        private string _InputFilePath;

        private List<string> _Errors = new List<string>();

        public int NSim
        {
            get { return _NSim; }
        }
        public int NFootings
        {
            get { return _NFooting; }
        }
        public string BaseName
        {
            get { return _BaseName; }
        }

        public int FootingNum
        {
            get { return footingNum; }
            set
            {
                if(footingNum != value)
                {
                    footingNum = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowPlotTitles
        {
            get { return showPlotTitles; }
            set
            {
                if(showPlotTitles != value)
                {
                    showPlotTitles = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double VerticalOffset
        {
            get { return vertOffset; }
            set
            {
                if(vertOffset != value)
                {
                    vertOffset = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NumIntervals
        {
            get { return nIntervals; }
            set
            {
                if(nIntervals != value)
                {
                    nIntervals = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool CustomAxis
        {
            get { return customAxis; }
            set
            {
                if(customAxis != value)
                {
                    customAxis = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double XAxisLength
        {
            get { return xAxisLength; }
            set
            {
                if (xAxisLength != value)
                {
                    xAxisLength = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double XAxisOrigin
        {
            get { return xAxisOrigin; }
            set
            {
                if(xAxisOrigin != value)
                {
                    xAxisOrigin = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double YAxisLength
        {
            get { return yAxisLength; }
            set
            {
                if(yAxisLength != value)
                {
                    yAxisLength = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double YAxisOrigin
        {
            get { return yAxisOrigin; }
            set
            {
                if (yAxisOrigin != value)
                {
                    yAxisOrigin = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool UseLogAxis
        {
            get { return useLogXAxis; }
            set
            {
                if(useLogXAxis != value)
                {
                    useLogXAxis = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool CustomXAxis
        {
            get { return customXAxis; }
            set
            {
                if(customXAxis!= value)
                {
                    customXAxis = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double XAxisMin
        {
            get { return xAxisMin; }
            set
            {
                if(xAxisMin != value)
                {
                    xAxisMin = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double XAxisMax
        {
            get { return xAxisMax; }
            set
            {
                if (xAxisMax != value)
                {
                    xAxisMax = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double XAxisIncrement
        {
            get { return xAxisIncrement; }
            set
            {
                if(xAxisIncrement!= value)
                {
                    xAxisIncrement = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool CustomYAxis
        {
            get { return customYAxis; }
            set
            {
                if (customYAxis != value)
                {
                    customYAxis = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double YAxisMin
        {
            get { return yAxisMin; }
            set
            {
                if (yAxisMin != value)
                {
                    yAxisMin = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double YAxisMax
        {
            get { return yAxisMax; }
            set
            {
                if (yAxisMax != value)
                {
                    yAxisMax = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double YAxisIncrement
        {
            get { return yAxisIncrement; }
            set
            {
                if (yAxisIncrement != value)
                {
                    yAxisIncrement = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool FitDistribution
        {
            get { return fitDistribution; }
            set
            {
                if (fitDistribution != value)
                {
                    fitDistribution = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public HistogramDistribution FittedDistribution
        {
            get { return fittedDistribution; }
            set
            {
                if (fittedDistribution != value)
                {
                    fittedDistribution = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowAndersonDarlingStat
        {
            get { return showAndersonDarlingStat; }
            set
            {
                if (showAndersonDarlingStat != value)
                {
                    showAndersonDarlingStat = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowChiSquareStat
        {
            get { return showChiSquareStat; }
            set
            {
                if (showChiSquareStat != value)
                {
                    showChiSquareStat = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowLineKey
        {
            get { return showLineKey; }
            set
            {
                if (showLineKey != value)
                {
                    showLineKey = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double LineKeyXOffset
        {
            get { return lineKeyXOffset; }
            set
            {
                if (lineKeyXOffset != value)
                {
                    LineKeyXOffset = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double LineKeyYOffset
        {
            get { return lineKeyYOffset; }
            set
            {
                if (lineKeyYOffset != value)
                {
                    lineKeyYOffset = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string FilePath
        {
            get { return _InputFilePath; }
        }
        public string Error
        {
            get
            {
                string result = this[string.Empty];
                if (result != null && result.Trim().Length == 0)
                {
                    result = null;
                }
                return result;
            }
        }

        public string this[string columnName]
        {
            get
            {
                return Validate(this, columnName);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public RBear2DHistViewModel(int nSim, int nFootings, string baseName, string inputFilePath)
        {
            nIntervals = (int)(0.5 + 0.05 * (double)nSim);
            if (nIntervals < 5) nIntervals = 5;
            if (nIntervals > 50) nIntervals = 50;


            if (nFootings == 1) footingNum = 1;

            _BaseName = baseName;
            _InputFilePath = inputFilePath;
            
            
        }
        private string Validate(object sender, string propertyName)
        {
            string validationMessage = string.Empty;

            switch (propertyName)
            {
                case "NumIntervals":
                    if (nIntervals < 1) validationMessage = "Number of intervals must be >= 1.";
                    break;
                case "XAxisLength":
                    if (xAxisLength <= 0 & customAxis) validationMessage = "X-Axis length must be a positive number.";
                    break;
                case "YAxisLength":
                    if (yAxisLength <= 0 & customAxis) validationMessage = "Y-Axis length must be a positive number.";
                    break;
                case "XAxisOrigin":
                    if (xAxisOrigin <= 0 & customAxis) validationMessage = "X-Axis origin position must be a positive number.";
                    break;
                case "YAxisOrigin":
                    if (yAxisOrigin <= 0 & customAxis) validationMessage = "Y-Axis origin position must be a positive number.";
                    break;
                case "XAxisIncrement":
                    if (xAxisIncrement <= 0 & customXAxis) validationMessage = "X-Axis increment must be a positive number.";
                    break;
                case "YAxisIncrement":
                    if (yAxisIncrement <= 0 & customYAxis) validationMessage = "Y-Axis increment must be a positive number";
                    break;
            }
            
            if (validationMessage == string.Empty || validationMessage == null)
            {
                if (_Errors.Contains(propertyName))
                    _Errors.Remove(propertyName);
            }
            else
            {
                if (!_Errors.Contains(propertyName))
                {
                    _Errors.Add(propertyName);
                }
            }

            return validationMessage;

        }
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private void PrepareHistogramFiles()
        {
            string options = "";
            string appFileName = Environment.GetCommandLineArgs()[0];
            string directory = System.IO.Path.GetDirectoryName(appFileName);
            var pInfo = new ProcessStartInfo();
            Process p;

            directory += "\\Executables\\hist_bear2d.exe";
            directory = "\"" + directory + "\"";

            if (showAndersonDarlingStat & FitDistribution) options += "-A ";
            if (nIntervals != 50) options += string.Format("-b{0} ", nIntervals);

            if (showChiSquareStat & FitDistribution) options += "-c ";

            if (FitDistribution) options += string.Format("-d{0} ", (int)FittedDistribution);

            if (customAxis) options += string.Format("-e{0},{1},{2},{3} ", xAxisLength, yAxisLength, xAxisOrigin, yAxisOrigin);

            if (showLineKey)
            {
                if (lineKeyXOffset != 0 || lineKeyYOffset != 0)
                    options += string.Format("-k{0},{1} ", lineKeyXOffset,
                                                 lineKeyYOffset);

            }
            else
            {
                options += "-K ";
            }

            if (useLogXAxis) options += "-L ";

            if (showPlotTitles)
            {
                if (vertOffset != 0) options += string.Format("-o{0} ", vertOffset);
            }
            else
            {
                options += "-T ";
            }

            if (footingNum > 1) options += string.Format("-p{0} ", footingNum);

            if (customXAxis) options += string.Format("-x{0},{1},{2} ", xAxisMin, xAxisMax, xAxisIncrement);

            if (customYAxis) options += string.Format("-y{0},{1},{2} ", yAxisMin, yAxisMax, yAxisIncrement);

            pInfo.FileName = directory;
            pInfo.Arguments = _InputFilePath + " " + options;
            pInfo.CreateNoWindow = true;
            pInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(_InputFilePath);

            p = new Process() { StartInfo = pInfo };

            p.Start();
            p.WaitForExit();

            pInfo = new ProcessStartInfo();
            pInfo.FileName = System.IO.Path.GetDirectoryName(appFileName) + "\\Executables\\plotps.exe";
            pInfo.Arguments = System.IO.Path.GetDirectoryName(_InputFilePath) + "\\hist.plotps";
            pInfo.CreateNoWindow = true;
            pInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(_InputFilePath);

            p = new Process() { StartInfo = pInfo };
            p.Start();
            p.WaitForExit();
        }
        public HistogramHost GenerateHistogram()
        {
            PrepareHistogramFiles();

            return new HistogramHost(System.IO.Path.GetDirectoryName(_InputFilePath) + "\\graph1.ps",
                                                    "\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"",
                                                    System.IO.Path.GetDirectoryName(_InputFilePath));
        }
        public void PopOutHistogram()
        {
            ProcessStartInfo pInfo;
            Process p;

            PrepareHistogramFiles();


            pInfo = new ProcessStartInfo();
            pInfo.FileName = "\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"";
            pInfo.Arguments = System.IO.Path.GetDirectoryName(_InputFilePath) + "\\graph1.ps";
            pInfo.CreateNoWindow = true;
            pInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(_InputFilePath);

            p = new Process() { StartInfo = pInfo };
            p.Start();
        }
    }
}
