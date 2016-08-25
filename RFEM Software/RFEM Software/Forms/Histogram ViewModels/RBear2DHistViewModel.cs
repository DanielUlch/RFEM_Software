using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using RFEMSoftware.Simulation.Infrastructure;
using RFEMSoftware.Simulation.Infrastructure.Wrappers;

namespace RFEMSoftware.Simulation.Desktop.Forms
{
    public class RBear2DHistViewModel : IHistViewModel, INotifyPropertyChanged
    {
        private int _NSim;
        private int _NFooting;
        private string _BaseName;

        private int footingNum = 1;

        
        private string _InputFilePath;

        private HistogramCoreViewModel _HistCore;

        public HistogramType Type
        {
            get { return HistogramType.RBear_Bearing; }
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
       
        
        public HistogramCoreViewModel HistogramCore
        {
            get { return _HistCore; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public RBear2DHistViewModel(int nSim, int nFootings, string baseName, string inputFilePath)
        {
            _HistCore = new HistogramCoreViewModel(nSim);

            if (nFootings == 1) footingNum = 1;

            _BaseName = baseName;
            _InputFilePath = inputFilePath;

            _NSim = nSim;
            _NFooting = nFootings;
            
            
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
            

            if (_HistCore.ShowAndersonDarlingStat & _HistCore.FitDistribution) options += "-A ";
            if (_HistCore.NumIntervals != 50) options += string.Format("-b{0} ", _HistCore.NumIntervals);

            if (_HistCore.ShowChiSquareStat & _HistCore.FitDistribution) options += "-c ";

            if (_HistCore.FitDistribution) options += string.Format("-d{0} ", (int)_HistCore.FittedDistribution);

            if (_HistCore.CustomAxis) options += string.Format("-e{0},{1},{2},{3} ", _HistCore.XAxisLength,
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

            if (footingNum > 1) options += string.Format("-p{0} ", footingNum);

            if (_HistCore.CustomXAxis) options += string.Format("-x{0},{1},{2} ", _HistCore.XAxisMin,
                                                                                  _HistCore.XAxisMax,
                                                                                  _HistCore.XAxisIncrement);

            if (_HistCore.CustomYAxis) options += string.Format("-y{0},{1},{2} ", _HistCore.YAxisMin,
                                                                                  _HistCore.YAxisMax,
                                                                                  _HistCore.YAxisIncrement);

            HistogramExeWrapper.Run(_InputFilePath, options, Program.RBear2D);

            PlotPSWrapper.Run(_InputFilePath);

        }
        public HistogramHost GenerateHistogram()
        {
            if (_HistCore.HasErrors)
                throw new InvalidOperationException("Please correct the errors in the form before generating a histogram.");
            PrepareHistogramFiles();

            return new HistogramHost(System.IO.Path.GetDirectoryName(_InputFilePath) + "\\graph1.ps",
                                                    "\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"",
                                                    System.IO.Path.GetDirectoryName(_InputFilePath));
        }
        public void PopOutHistogram()
        {
            if (_HistCore.HasErrors)
                throw new InvalidOperationException("Please correct the errors in the form before generating a histogram.");

            PrepareHistogramFiles();

            var GhostView = new GhostViewWrapper("\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"");
            GhostView.Show(System.IO.Path.GetDirectoryName(_InputFilePath) + "\\graph1.ps");
        }
    }
}
