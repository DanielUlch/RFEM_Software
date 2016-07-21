using RFEM_Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEM_Software.Forms
{
    public class HistogramCoreViewModel: IDataErrorInfo, INotifyPropertyChanged
    {
        private List<string> _Errors = new List<string>();


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

        public event PropertyChangedEventHandler PropertyChanged;

        public bool ShowPlotTitles
        {
            get { return showPlotTitles; }
            set
            {
                if (showPlotTitles != value)
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
                if (vertOffset != value)
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
                if (nIntervals != value)
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
                if (customAxis != value)
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
                if (xAxisOrigin != value)
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
                if (yAxisLength != value)
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
                if (useLogXAxis != value)
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
                if (customXAxis != value)
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
                if (xAxisMin != value)
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
                if (xAxisIncrement != value)
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
        public bool HasErrors
        {
            get { return (_Errors.Count > 0); }
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

        public HistogramCoreViewModel(int nSim)
        {
            nIntervals = (int)(0.5 + 0.05 * (double)nSim);
            if (nIntervals < 5) nIntervals = 5;
            if (nIntervals > 50) nIntervals = 50;
            
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
    }
}
