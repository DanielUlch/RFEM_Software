using RFEMSoftware.Simulation.Infrastructure;
using RFEMSoftware.Simulation.Infrastructure.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEMSoftware.Simulation.Desktop.Forms
{
    public class RDam2DNodalGradientHistViewModel : IHistViewModel, INotifyPropertyChanged
    {
        private HistogramCoreViewModel _HistCore;
        private ObservableCollection<int> _NodesToPlot;
        private int _NodeToPlot;
        private string _FilePath;
        private string _BaseName;
        private int _QuantityToPlot;
        private int _NSim;

        public RDam2DNodalGradientHistViewModel(IEnumerable<int> availableNodes,
                                                string filePath,
                                                string baseName,
                                                int nSim)
        {
            _FilePath = filePath;
            _BaseName = baseName;
            _NSim = nSim;

            _HistCore = new HistogramCoreViewModel(nSim);
            

            _NodesToPlot = new ObservableCollection<int>(availableNodes);
            NotifyPropertyChanged("NodesToPlot");

            _QuantityToPlot = 1;
            NotifyPropertyChanged("GradientQuantity");
            NotifyPropertyChanged("FluxQuantity");
            NotifyPropertyChanged("PotentialQuantity");
        }

       

        public HistogramCoreViewModel HistogramCore
        {
            get
            {
                return _HistCore;
            }
        }

        public ObservableCollection<int> NodesToPlot
        {
            get { return _NodesToPlot; }
        }
        public int NodeToPlot
        {
            get { return _NodeToPlot; }
            set
            {
                if(_NodeToPlot != value)
                {
                    _NodeToPlot = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool GradientQuantity
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
        public bool FluxQuantity
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
        public bool PotentialQuantity
        {
            get { return (_QuantityToPlot == 3); }
            set
            {
                if (value)
                {
                    _QuantityToPlot = 3;
                    NotifyPropertyChanged();
                }
            }
        }
        public HistogramType Type
        {
            get
            {
                return HistogramType.RDam_NodeGradient;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void PrepareHistogramFiles()
        {
            string options = "";
            int ObscureValueFromPreviousImplementation = _QuantityToPlot + 3 * _NodesToPlot.IndexOf(_NodeToPlot);


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

            if (ObscureValueFromPreviousImplementation > 1) options += string.Format("-p{0} ", ObscureValueFromPreviousImplementation);

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
