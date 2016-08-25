using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RFEMSoftware.Simulation.Infrastructure.Models
{
    public class RDam2D: ISimModel, INotifyPropertyChanged
    {

        private bool _CanDisplaySummaryStats = false;
        private bool _CanDisplayField = false;
        private bool _CanDisplayFlownet = false;
        private bool _CanDisplayGradientMeanAndStdDevFields = false;
        private bool _CanDisplayFlowRateHist = false;
        private bool _CanDisplayEffectiveConductivityHist = false;
        private bool _CanDisplayNodeGradientHist = false;

        private List<int?> _NodesForGradientOutput;

        public event PropertyChangedEventHandler PropertyChanged;

        public Program Type
        {
            get { return Program.RDam2D; }
        }

        #region FormProperties
        public string JobTitle { get; set; }
        public string BaseName { get; set; }

        public bool EchoInputDataToOutputFile { get; set; }
        public bool OutputDebugInfo { get; set; }

        public int DebugCode { get; set; } = 1;

        public bool ShowCentroidsOnMesh { get; set; }
        
        public int RealizationNumber { get; set; }

        public bool ProduceDisplayFile { get; set; }
        public bool ProducePSPlotOfFirstFlownet { get; set; }
        public bool ShowStreamlines { get; set; }
        public bool ShowEquipotentialDrops { get; set; }
        public int NumEquipotentialDrops { get; set; }
        public bool ShowMeshOnFlownet { get; set; }
        public bool ShowLogConductivity { get; set; }
        public bool ShowDamDimensionsOnFlownet { get; set; }
        public bool ShowTitlesOnFlownet { get; set; }
        public double FlownetWidth { get; set; }
        public bool OutputGradientMeanAndStdDev { get; set; }
        public List<int?> NodesForGradientOutput
        {
            get
            {
                return _NodesForGradientOutput;
            }
            set
            {
                if(_NodesForGradientOutput != value)
                {
                    _NodesForGradientOutput = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool OutputFlowRate { get; set; }
        public bool OutputBlockConductivities { get; set; }
        public bool OutputConductivityAverages { get; set; }
        public bool GenerateUniformConductivity { get; set; }
        public int NumElementsInXDir { get; set; }
        public int NumElementsInYDir { get; set; }
        public bool IsDrainPresent { get; set; }
        public double DrainXDimension { get; set; }
        public double DrainYDimension { get; set; }
        public double DrainConductivity { get; set; }
        public double DamTop { get; set; }
        public double DamBase { get; set; }
        public double DamHeight { get; set; }
        public int NumberOfRealizations { get; set; }
        public int MaxNumberOfIterations { get; set; }
        public double ConvergenceTolerance { get; set; }
        public int? GeneratorSeed { get; set; }
        public int CorrelationLengthInXDir { get; set; }
        public int CorrelationLengthInYDir { get; set; }
        public double ConductivityMean { get; set; }
        public double ConductivityStdDev { get; set; }
        public CovarianceFunction CovFunction { get; set; }
        public bool ModifyHoirzontalSpacing { get; set; }
        public SpacingAlgorithm SpacingAlgo { get; set; } = SpacingAlgorithm.Geometric;
        public bool UseAlternateAlgoIfFirstFails { get; set; }
        public bool RestrainFreeSurfaceNonIncreasing { get; set; }
        public bool DampOscillations { get; set; }


        #endregion

        #region RibbonProperties
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
        public bool CanDisplayFlownet
        {
            get { return _CanDisplayFlownet; }
            set
            {
                if(_CanDisplayFlownet != value)
                {
                    _CanDisplayFlownet = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool CanDisplayField
        {
            get { return _CanDisplayField; }
            set
            {
                if(_CanDisplayField != value)
                {
                    _CanDisplayField = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool CanDisplayGradientMeanAndStdDevFields
        {
            get { return _CanDisplayGradientMeanAndStdDevFields; }
            set
            {
                if(_CanDisplayGradientMeanAndStdDevFields != value)
                {
                    _CanDisplayGradientMeanAndStdDevFields = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool CanDisplayFlowRateHist
        {
            get { return _CanDisplayFlowRateHist; }
            set
            {
                if(_CanDisplayFlowRateHist != value)
                {
                    _CanDisplayFlowRateHist = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool CanDisplayEffectiveConductivityHist
        {
            get { return _CanDisplayEffectiveConductivityHist; }
            set
            {
                if(_CanDisplayEffectiveConductivityHist != value)
                {
                    _CanDisplayEffectiveConductivityHist = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool CanDisplayNodeGradientHist
        {
            get { return _CanDisplayNodeGradientHist; }
            set
            {
                if(_CanDisplayNodeGradientHist != value)
                {
                    _CanDisplayNodeGradientHist = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region FilePaths
        public string OutputDirectory
        {
            get; set;
        }
        public string DataLocation
        {
            get { return OutputDirectory + "\\" + BaseName + ".dat"; }
        }
        #endregion

        public RDam2D()
        {
            NodesForGradientOutput = new List<int?>()
            {
                null,
                null,
                null,
                null,
                null,
                null
            };
        }

        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        public string RunSim(IProgress<int> simIteration, IProgress<string> currentOp, CancellationToken token)
        {
            currentOp.Report("Initializing");

            var ctr = default(CancellationTokenRegistration);

            if (token.CanBeCanceled)
            {
                ctr = token.Register(() =>
                {
                    currentOp.Report("Canceled");
                    //throw new OperationCanceledException();
                });
            }

            var pInfo = new ProcessStartInfo();
            Process p;
            string Line = "";
            string ProgramOutput = "";
            string appFileName = Environment.GetCommandLineArgs()[0];
            string directory = System.IO.Path.GetDirectoryName(appFileName);
            char ch;
            int progress = 0;
            string partialLine = "";

            directory += "\\Executables\\rdam2d.exe";

            directory = "\"" + directory + "\"";

            pInfo.FileName = directory;
            pInfo.RedirectStandardOutput = true;
            pInfo.Arguments = "\"" + DataLocation  + "\"";
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

            if (ProducePSPlotOfFirstFlownet)
                CanDisplayFlownet = true;
            if(ProduceDisplayFile)
                CanDisplayField = true;
            if (OutputGradientMeanAndStdDev)
                CanDisplayGradientMeanAndStdDevFields = true;
            if (OutputFlowRate)
                CanDisplayFlowRateHist = true;
            if (OutputBlockConductivities)
                CanDisplayEffectiveConductivityHist = true;
            if (OutputGradientMeanAndStdDev & NodesForGradientOutput.Where((x) => x != null).Count() > 0)
                CanDisplayNodeGradientHist = true;

            currentOp.Report("Finished");


            return ProgramOutput;
        }
        
    }
    
}
