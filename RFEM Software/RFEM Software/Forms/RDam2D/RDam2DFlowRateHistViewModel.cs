using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFEM_Infrastructure;
using System.ComponentModel;

namespace RFEM_Software.Forms
{
    public class RDam2DFlowRateHistViewModel : IHistViewModel, INotifyPropertyChanged
    {
        private int _QuantityToPlot = 1;
        private HistogramCoreViewModel _HistCore;
        private string _BaseName;
        private string _FilePath;
        private int _NSim;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool PlotFlowRate
        {
            get { return (_QuantityToPlot == 1); }
            set
            {
                if (value)
                {
                    _QuantityToPlot = 1;
                    NotifyPropertyChanged();
                }
                   
            }
        }
        public bool PlotFreeSurface
        {
            get { return (_QuantityToPlot == 2); }
            set
            {
                if (value)
                {
                    _QuantityToPlot = 2;
                    NotifyPropertyChanged();
                }
                   
            }
        }
        public string BaseName
        {
            get
            {
                return _BaseName;
            }
        }

        public string FilePath
        {
            get
            {
                return _FilePath;
            }
        }

        public int NSim
        {
            get { return _NSim; }
        }
        public HistogramCoreViewModel HistogramCore
        {
            get
            {
                return _HistCore;
            }
        }

        public HistogramType Type
        {
            get
            {
                return HistogramType.RDam_FlowRate;
            }
        }

        public RDam2DFlowRateHistViewModel(int nSim, string baseName, string filePath)
        {
            _HistCore = new HistogramCoreViewModel(nSim);

            _BaseName = baseName;

            _FilePath = filePath;

            _NSim = nSim;
        }
        private void PrepareHistogramFiles()
        {
            string options = "";


            if (_HistCore.ShowAndersonDarlingStat & _HistCore.FitDistribution) options += "-A ";
            if (_HistCore.NumIntervals != 50) options += string.Format("-b{0} ", _HistCore.NumIntervals);

            if (_HistCore.ShowChiSquareStat & _HistCore.FitDistribution) options += "-c ";

            if (_HistCore.FitDistribution) options += string.Format("-d{0} ", (int)_HistCore.FittedDistribution);

            if (_HistCore.CustomAxis)
                options += string.Format("-e{0},{1},{2},{3} ", _HistCore.XAxisLength,
                                                               _HistCore.YAxisLength,
                                                               _HistCore.XAxisOrigin,
                                                               _HistCore.YAxisOrigin);

            if (_HistCore.ShowLineKey)
            {
                if (_HistCore.LineKeyXOffset != 0 || _HistCore.LineKeyYOffset != 0)
                    options += string.Format("-k{0},{1} ", _HistCore.LineKeyXOffset,
                                                 _HistCore.LineKeyYOffset);

            }
            else
            {
                options += "-K ";
            }

            if (_HistCore.UseLogAxis) options += "-L ";

            if (_HistCore.ShowPlotTitles)
            {
                if (_HistCore.VerticalOffset != 0) options += string.Format("-o{0} ", _HistCore.VerticalOffset);
            }
            else
            {
                options += "-T ";
            }

            if (_QuantityToPlot > 1) options += string.Format("-p{0} ", _QuantityToPlot);

            if (_HistCore.CustomXAxis)
                options += string.Format("-x{0},{1},{2} ", _HistCore.XAxisMin,
                                                           _HistCore.XAxisMax,
                                                           _HistCore.XAxisIncrement);

            if (_HistCore.CustomYAxis)
                options += string.Format("-y{0},{1},{2} ", _HistCore.YAxisMin,
                                                           _HistCore.YAxisMax,
                                                           _HistCore.YAxisIncrement);

            HistogramExeWrapper.Run(_FilePath, options, Program.RDam2D);

            PlotPSWrapper.Run(_FilePath);
        }
        public HistogramHost GenerateHistogram()
        {
            if (_HistCore.HasErrors)
                throw new InvalidOperationException("Please correct the errors in the form before generating a histogram.");

            PrepareHistogramFiles();

            return new HistogramHost(System.IO.Path.GetDirectoryName(_FilePath) + "\\graph1.ps",
                                                    "\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"",
                                                    System.IO.Path.GetDirectoryName(_FilePath));
        }

        public void PopOutHistogram()
        {
            if (_HistCore.HasErrors)
                throw new InvalidOperationException("Please correct the errors in the form before generating a histogram.");

            PrepareHistogramFiles();

            var GhostView = new GhostViewWrapper("\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"");
            GhostView.Show(System.IO.Path.GetDirectoryName(_FilePath) + "\\graph1.ps");
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
