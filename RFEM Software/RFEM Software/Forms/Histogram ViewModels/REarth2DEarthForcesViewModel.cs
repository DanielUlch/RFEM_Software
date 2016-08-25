using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFEMSoftware.Simulation.Infrastructure;
using System.ComponentModel;
using RFEMSoftware.Simulation.Infrastructure.Wrappers;

namespace RFEMSoftware.Simulation.Desktop.Forms
{
    public class REarth2DEarthForcesViewModel : IHistViewModel, INotifyPropertyChanged
    {
        private HistogramCoreViewModel _HistCore;

        private bool _VirtuallySampled;
        private int _QuantityToPlot;
        private string _FilePath;

        public event PropertyChangedEventHandler PropertyChanged;

        public REarth2DEarthForcesViewModel(string filePath, bool virtuallySampled, int nSim)
        {
            _HistCore = new HistogramCoreViewModel(nSim);

            VirtuallySampled = virtuallySampled;

            _FilePath = filePath;
        }

        public bool VirtuallySampled
        {
            get
            {
                return _VirtuallySampled;
            }
            private set
            {
                if(_VirtuallySampled != value)
                {
                    _VirtuallySampled = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int QuantityToPlot
        {
            get
            {
                return _QuantityToPlot;
            }
            set
            {
                if(_QuantityToPlot != value)
                {
                    _QuantityToPlot = value;
                    NotifyPropertyChanged();
                }
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
                return HistogramType.REarth2D_EarthForces;
            }
        }

        public HistogramHost GenerateHistogram()
        {
            throw new NotImplementedException();
        }

        public void PopOutHistogram()
        {
            throw new NotImplementedException();
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

            if (QuantityToPlot > 1) options += string.Format("-p{0} ", QuantityToPlot);

            if (_HistCore.CustomXAxis)
                options += string.Format("-x{0},{1},{2} ", _HistCore.XAxisMin,
                                                           _HistCore.XAxisMax,
                                                           _HistCore.XAxisIncrement);

            if (_HistCore.CustomYAxis)
                options += string.Format("-y{0},{1},{2} ", _HistCore.YAxisMin,
                                                           _HistCore.YAxisMax,
                                                           _HistCore.YAxisIncrement);

            HistogramExeWrapper.Run(_FilePath, options, Program.REarth2D);

            PlotPSWrapper.Run(_FilePath);
        }
        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
