using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RFEMSoftware.Simulation.Infrastructure.Models
{
    public class RSlope2D: ISimModel, INotifyPropertyChanged 
    {
        private bool _CanDisplaySummaryStats;
        private bool _CanDisplayField;
        private bool _CanDisplayMesh;
        #region Form Properties

        public string JobTitle { get; set; }
        public string BaseName { get; set; }
        public bool EchoInputDataToOutputFile { get; set; }
        public bool OutputDebugInfo { get; set; }

        public bool PlotARandomField { get; set; }

        public RSlopeSoilProperty RFPropertyToPlot { get; set; }

        public bool ProducePSPlotOfDisplacedMesh { get; set; }
        public bool ShowMeshOnDisplacedMesh { get; set; }
        public bool ShowRFOnDisplacedMesh { get; set; }
        public bool ShowLogRFOnDisplacedMesh { get; set; }

        public RSlopeSoilProperty DisplacedMeshPropertyToPlot { get; set; }

        public double DisplacedMeshPlotWidth { get; set; }

        public int NElementsLeftOfEmbark { get; set; }
        public int NElementsRightOfEmbark { get; set; }
        public int NElementsInEmbark { get; set; }
        public int NElementsInFoundations { get; set; }

        public double SlopeGradient { get; set; }
        public double ElementSizeInXDir { get; set; }
        public double ElementSizeInYDir { get; set; }

        public int NumberOfRealizations { get; set; }
        public int GeneratorSeed { get; set; }
        public int CorrelationLengthInXDir { get; set; }
        public int CorrelationLengthInYDir { get; set; }

        public CovarianceFunction CovFunc { get; set; }

        public double ConvergenceTolerance { get; set; }

        public int MaxNumIterations { get; set; }

        public List<double> StrengthReductionFactors { get; set; } = new List<double>();

        public DistributionInfo Cohesion { get; set; }
        public DistributionInfo FrictionAngle { get; set; }
        public FrictionAngle FrictionAngleType { get; set; }
        public DistributionInfo DilationAngle { get; set; }
        public DistributionInfo UnitWeight { get; set; }
        public DistributionInfo ElasticModulus { get; set; }
        public DistributionInfo PoissonsRatio { get; set; }

        public double[,] CorrelationMatrix { get; set; }


        #endregion

        public Program Type
        {
            get
            {
                return Program.RSlope2D;
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

        public RSlope2D()
        {
            Cohesion = new DistributionInfo(SoilProperty.Cohesion);
            FrictionAngle = new DistributionInfo(SoilProperty.FrictionAngle);
            DilationAngle = new DistributionInfo(SoilProperty.DilationAngle);
            UnitWeight = new DistributionInfo(SoilProperty.Cohesion); //HAcked
            ElasticModulus = new DistributionInfo(SoilProperty.ElasticModulus);
            PoissonsRatio = new DistributionInfo(SoilProperty.PoissonsRatio);

            CorrelationMatrix = new double[6, 6]
            {
                {1,0,0,0,0,0},
                {0,1,0,0,0,0},
                {0,0,1,0,0,0},
                {0,0,0,1,0,0},
                {0,0,0,0,1,0},
                {0,0,0,0,0,1}
            };
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

            directory += "\\Executables\\rslope2d.exe";

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
            if (PlotARandomField)
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
}
