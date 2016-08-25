using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RFEMSoftware.Simulation.Infrastructure.Models
{
    public class RSetl3D: ISimModel, INotifyPropertyChanged
    {
        private bool _CanDisplaySummaryStats;
        private bool _CanDisplayField;
        private bool _CanDisplayMesh;

        #region Form Properties

        public string JobTitle { get; set; }
        public string BaseName { get; set; }
        public bool EchoInputDataToOutputFile { get; set; }
        public bool OutputDebugInfo { get; set; }

        public bool ProduceDisplayOfFirstLogElasticModulusField { get; set; }

        public int LogElasticModulusFieldNodeIndex { get; set; }

        public Axis LogElasticModulusFieldPerpindicularToThisAxis { get; set; }

        public bool ProducePSPlotOfDisplacedMesh { get; set; }

        public int DisplacedMeshNodeIndex { get; set; }

        public Axis DisplacedMeshPlotPerpindicularToThisAxis { get; set; }

        public bool OverlayLogElasticModulusOnDisplacedMesh { get; set; }
        public bool ShowProblemDimensionsOnDisplacedMesh { get; set; }
        public bool ShowTitlesOnDisplacedMesh { get; set; }

        public double DisplacementMagInXDir { get; set; }
        public double DisplacementMagInYDir { get; set; }
        public double DisplacedMeshPlotWidth { get; set; }

        public bool OutputSettlementSamplesAndSummaryStats { get; set; }
        public bool OutputBlockModulusSamplesAndSummaryStats { get; set; }
        public bool OutputFieldAveragedModulusSamplesAndSummaryStats { get; set; }
        public bool GenerateUniformRandomFields { get; set; }

        public int NElementsInXidr { get; set; }
        public int NElementsInYDir { get; set; }
        public int NElementsInZDir { get; set; }

        public double ElementSizeInXDir { get; set; }
        public double ElementSizeInYDir { get; set; }
        public double ElementSizeInZDir { get; set; }

        public int MaxNumIterations { get; set; }

        public double ConvergenceTolerance { get; set; }

        public int NumberOfRealizations { get; set; }
        public int GeneratorSeed { get; set; }

        public double PoissonsRatio { get; set; }
        public double ElasticModulusMean { get; set; }
        public double ElasticModulusStdDev { get; set; }

        public int CorrelationLengthInXDir { get; set; }
        public int CorrelationLengthInYDir { get; set; }
        public int CorrelationLengthInZDir { get; set; }

        public CovarianceFunction3D CovFunc { get; set; }

        public ObservableCollection<RigidFootingLoads3D> RigidFootingLoads { get; set; } = 
            new ObservableCollection<RigidFootingLoads3D>();

        #endregion

        public Program Type
        {
            get
            {
                return Program.RSetl3D;
            }
        }

        public bool CanDisplaySummaryStats
        {
            get { return _CanDisplaySummaryStats; }
            set
            {
                if (_CanDisplaySummaryStats != value)
                {
                    _CanDisplaySummaryStats = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool CanDisplayField
        {
            get { return _CanDisplayField; }
            set
            {
                if (_CanDisplayField != value)
                {
                    _CanDisplayField = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool CanDisplayMesh
        {
            get { return _CanDisplayMesh; }
            set
            {
                if (_CanDisplayMesh != value)
                {
                    _CanDisplayMesh = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string OutputDirectory
        {
            get; set;
        }
        public string DataLocation
        {
            get { return OutputDirectory + "\\" + BaseName + ".dat"; }
        }

        public string RunSim(IProgress<int> simIteration, IProgress<string> currentOp, CancellationToken token)
        {
            var pInfo = new ProcessStartInfo();
            Process p;
            string Line = "";
            string ProgramOutput = "";
            string appFileName = Environment.GetCommandLineArgs()[0];
            string directory = System.IO.Path.GetDirectoryName(appFileName);
            char ch;
            int progress = 0;
            string partialLine = "";

            directory += "\\Executables\\rsetl3d.exe";

            directory = "\"" + directory + "\"";

            pInfo.FileName = directory;
            pInfo.RedirectStandardOutput = true;
            pInfo.Arguments = "\"" + DataLocation + "\"";
            pInfo.UseShellExecute = false;
            pInfo.CreateNoWindow = true;
            p = new Process() { StartInfo = pInfo };

            p.Start();

            currentOp.Report("Running Simulation");

            using (var reader = p.StandardOutput)
            {
                while (!reader.EndOfStream && !token.IsCancellationRequested)
                {
                    Line = reader.ReadLine();

                    if (Line == "Analyzing realization:")
                    {
                        while (progress < (int)NumberOfRealizations && !token.IsCancellationRequested)
                        {
                            ch = (char)reader.Read();
                            if (ch == ' ' || ch == '\r')
                            {
                                if (partialLine.Length != 0)
                                {
                                    bool result = Int32.TryParse(partialLine, out progress);
                                    if (result)
                                    {
                                        simIteration.Report(progress);
                                    }
                                }
                                partialLine = "";
                            }
                            else
                            {
                                partialLine += ch;
                            }
                            ProgramOutput += ch;
                        }

                    }
                    ProgramOutput += Line + Environment.NewLine;
                }
            }

            CanDisplaySummaryStats = true;

            if (ProduceDisplayOfFirstLogElasticModulusField)
                CanDisplayField = true;
            if (ProducePSPlotOfDisplacedMesh)
                CanDisplayMesh = true;

            currentOp.Report("Finished");


            return ProgramOutput;
        }

        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion



    }
    public struct RigidFootingLoads3D: INotifyPropertyChanged
    {
        private int _XStart;
        private int _XEnd;
        private int _YStart;
        private int _YEnd;
        private double _LoadMean;
        private double _LoadStdDev;

        public int XStart
        {
            get { return _XStart; }
            set
            {
                if(_XStart != value)
                {
                    _XStart = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int XEnd
        {
            get { return _XEnd; }
            set
            {
                if(_XEnd != value)
                {
                    _XEnd = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int YStart
        {
            get { return _YEnd; }
            set
            {
                if(_YEnd != value)
                {
                    _YEnd = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int YEnd
        {
            get { return _YEnd; }
            set
            {
                if(_YEnd != value)
                {
                    _YEnd = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double LoadMean
        {
            get { return _LoadMean; }
            set
            {
                if(_LoadMean != value)
                {
                    _LoadMean = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public double LoadStdDev
        {
            get { return _LoadStdDev; }
            set
            {
                if(_LoadStdDev != value)
                {
                    _LoadStdDev = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
