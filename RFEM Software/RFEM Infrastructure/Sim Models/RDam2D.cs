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

namespace RFEM_Infrastructure
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
        public string AppDataFileLocation
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RFEM_Software\\" + BaseName + ".dat"; 
            }
        }

        public string DataFileLocation()
        {
            string appFileName = Environment.GetCommandLineArgs()[0];
            string directory = System.IO.Path.GetDirectoryName(appFileName);

            return directory + "\\" + BaseName + ".dat";
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
        public string GetDataFileString()
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine(JobTitle);
            str.AppendLine(string.Format("Echo input data to stats file (t/f)? . . . . . .  {0}", TFConversion(EchoInputDataToOutputFile)));
            str.AppendLine(string.Format("Report progress to standard output (t/f)?. . . .  {0}", TFConversion(true)));

            if (OutputDebugInfo)
            {
                str.AppendLine("Debug code (0,1,2,or 3), [realization no.] . . .  0 0");
            }
            else
            {
                str.AppendLine(string.Format("Debug code (0,1,2,or 3), [realization no.] . . .  {0} {1}", DebugCode, RealizationNumber));
            }

            str.AppendLine("Display first log-conductivity field (t/f)?. . .  " + TFConversion(ProduceDisplayFile));
            str.AppendLine("Plot first stochastic flownet (t/f)? . . . . . .  " + TFConversion(ProducePSPlotOfFirstFlownet));
            str.AppendLine("Plot gradient statistics fields (t/f)? . . . . .  " + TFConversion(OutputGradientMeanAndStdDev));
            str.AppendLine("Output flow rates and free surface pos (t/f)?  .  " + TFConversion(OutputFlowRate));
            str.AppendLine("Output block conductivity values (t/f)?  . . . .  " + TFConversion(OutputBlockConductivities));
            str.AppendLine("Output arith, geom, harm cond. values (t/f)? . .  " + TFConversion(OutputConductivityAverages));
            str.AppendLine("Generate uniform conductivity field (t/f)? . . .  " + TFConversion(GenerateUniformConductivity));
            str.AppendLine(string.Format("Number of mesh elements in X, Y directions . . .  {0} {1}", NumElementsInXDir, NumElementsInYDir));

            str.Append("Node numbers for gradient realizations . . . . . ");
            foreach(int? i in NodesForGradientOutput)
            {
                if(i != null)
                    str.Append(" " + i);
            }
            str.Append(Environment.NewLine);

            str.AppendLine(string.Format("Drain X, Y dimensions [conductivity] . . . . . .  {0} {1} {2}", DrainXDimension, DrainYDimension, DrainConductivity));
            str.AppendLine("Width of top of dam  . . . . . . . . . . . . . .  " + DamTop);
            str.AppendLine("Width of base of dam . . . . . . . . . . . . . .  " + DamBase);
            str.AppendLine("Height of dam  . . . . . . . . . . . . . . . . .  " + DamHeight);
            str.AppendLine(string.Format("Number of realizations [max iter., tol]. . . . .  {0} {1} {2}", NumberOfRealizations, MaxNumberOfIterations, ConvergenceTolerance));
            str.AppendLine("Generator seed (0 for random seed) . . . . . . .  " + GeneratorSeed);
            str.AppendLine(string.Format("Correlation length in X [and Y] directions . . .  {0} {1}", CorrelationLengthInXDir, CorrelationLengthInYDir));
            str.AppendLine("Conductivity mean  . . . . . . . . . . . . . . .  " + ConductivityMean);
            str.AppendLine("Conductivity standard deviation  . . . . . . . .  " + ConductivityStdDev);
            str.AppendLine("Name of covariance function used by simulator  .  " + CovFunction);
            str.AppendLine("Number of equipotential drops on flownet . . . .  " + NumEquipotentialDrops);
            str.AppendLine("Show equipotential drops on flownet (t/f?. . . .  " + TFConversion(ShowEquipotentialDrops));
            str.AppendLine("Show streamlines on flownet (t/f)? . . . . . . .  " + TFConversion(ShowStreamlines));
            str.AppendLine("Show element mesh on flownet (t/f)?. . . . . . .  " + TFConversion(ShowMeshOnFlownet));
            str.AppendLine("Show log-conductivity field on flownet (t/f)?. .  " + TFConversion(ShowLogConductivity));
            str.AppendLine("Show problem dimensions on flownet (t/f)?. . . .  " + TFConversion(ShowDamDimensionsOnFlownet));
            str.AppendLine("Show title and subtitles on flownet (t/f)? . . .  " + TFConversion(ShowTitlesOnFlownet));
            str.AppendLine("Flownet plot width on page in inches . . . . . .  " + FlownetWidth);

            if (ModifyHoirzontalSpacing)
            {
                str.AppendLine("Proportionate spacing code (off/geom/lin/prop) .  " + ((int)SpacingAlgo + 1));
            }
            else
            {
                str.AppendLine("Proportionate spacing code (off/geom/lin/prop) .  0");
            }

            str.AppendLine("Use alternative spacing if first fails (t/f)?. .  " + TFConversion(UseAlternateAlgoIfFirstFails));
            str.AppendLine("Ensure free surface is non-increasing (t/f)? . .  " + TFConversion(RestrainFreeSurfaceNonIncreasing));
            str.AppendLine("Employ free surface damping (t/f)? . . . . . . .  " + TFConversion(DampOscillations));
            str.AppendLine("Show centroids on debug mesh plots (2,3 only)? .  " + TFConversion(ShowCentroidsOnMesh));

            return str.ToString();
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
            pInfo.Arguments = "\"" + AppDataFileLocation + "\"";
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
        private string TFConversion(bool val)
        {
            if (val == true)
                return "t";
            else
                return "f";
        }
    }
    
}
