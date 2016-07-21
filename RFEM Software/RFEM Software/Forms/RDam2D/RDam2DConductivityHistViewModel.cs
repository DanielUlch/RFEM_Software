using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFEM_Infrastructure;
using System.ComponentModel;

namespace RFEM_Software.Forms
{
    public class RDam2DConductivityHistViewModel : IHistViewModel
    {
        private string _FilePath;
        private string _BaseName;
        private int _NSim;
        private HistogramCoreViewModel _HistCore;

        private bool _CanPlotBlockConductivity;
        private bool _CanPlotArithGeoHarmConductivities;

        private int _ConductivityToPlot;

        public event PropertyChangedEventHandler PropertyChanged;

        public RDam2DConductivityHistViewModel(int nSim, 
                                               string filePath, 
                                               string baseName,
                                               bool canPlotBlockConductivity,
                                               bool canPlotArithGeoHarmConductivities)
        {
            _FilePath = filePath;
            _BaseName = baseName;
            _NSim = nSim;

            _HistCore = new HistogramCoreViewModel(nSim);

            _CanPlotBlockConductivity = canPlotBlockConductivity;
            _CanPlotArithGeoHarmConductivities = canPlotArithGeoHarmConductivities;

            if (_CanPlotBlockConductivity)
            {
                _ConductivityToPlot = 1;
            }
            else
            {
                _ConductivityToPlot = 2;
            }

            NotifyPropertyChanged("CanPlotBlockConductivity");
            NotifyPropertyChanged("CanPlotArithGeoHarmConductivities");
               

        }
        public bool CanPlotBlockConductivity
        {
            get { return _CanPlotBlockConductivity; }
        }
        public bool CanPlotArithGeoHarmConductivities
        {
            get { return _CanPlotArithGeoHarmConductivities; }
        }
        public bool BlockConductivitry
        {
            get { return (_ConductivityToPlot == 1); }
            set
            {
                if (value)
                {
                    _ConductivityToPlot = 1;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ArithmeticConductivity
        {
            get { return _ConductivityToPlot == 2; }
            set
            {
                if (value)
                {
                    _ConductivityToPlot = 2;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool GeometricConductivity
        {
            get { return _ConductivityToPlot == 3; }
            set
            {
                if (value)
                {
                    _ConductivityToPlot = 3;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool HarmonicConductivity
        {
            get { return _ConductivityToPlot == 4; }
            set
            {
                if (value)
                {
                    _ConductivityToPlot = 4;
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
                return HistogramType.RDam_Conductivity;
            }
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

            if (_ConductivityToPlot > 1) options += string.Format("-p{0} ", _ConductivityToPlot);

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
