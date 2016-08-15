using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RFEMSoftware.Simulation.Infrastructure.Models
{
    public class RBear2D : ISimModel, INotifyPropertyChanged
    {

        
        

        private bool _CanDisplaySummaryStats;
        private bool _CanDisplayMesh;
        private bool _CanDisplayField;
        private bool _CanDisplayBearingHist;

        public event PropertyChangedEventHandler PropertyChanged;

        public Program Type
        {
            get { return Program.RBear2D; }
        }

        public string JobTitle { get; set; }
        public string BaseName { get; set; }

        public bool EchoInputDataToOutputFile { get; set; }
        public bool ReportRunProgress { get; set; }
        public bool WriteDebugDataToOutputFile { get; set; }
        public bool PlotFirstRandomField { get; set; }

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
        public bool CanDisplayBearingHist
        {
            get { return _CanDisplayBearingHist; }
            set
            {
                if(_CanDisplayBearingHist != value)
                {
                    _CanDisplayBearingHist = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public SoilProperty FirstRandomFieldProperty { get; set; }

        public bool ProducePSPLOTOfFirstFEM { get; set; }
        public bool ShowMeshOnDisplacedPlot { get; set; }
        public bool ShowRFOnPSPLOT { get; set; }
        public bool ShowLogRandomField { get; set; }

        public SoilProperty PSPLOTProperty { get; set; }

        public double? DisplacedMeshPlotWidth { get; set; }

        public bool NormalizeBearingCapacitySamples { get; set; }
        public bool OutputBearingCapacitySamples { get; set; }

        public int? NElementsInXDir { get; set; }
        public int? NElementsInYDir { get; set; }

        public double? ElementSizeInXDir { get; set; }
        public double? ElementSizeInYDir { get; set; }

        public int NumberOfFootings { get; set; }

        public double? FootingWidth { get; set; }
        public double? FootingGap { get; set; }
        public double? DisplacementInc { get; set; }
        public double? PlasticTol { get; set; }
        public double? BearingTol { get; set; }

        public int? MaxNumSteps { get; set; }
        public int? MaxNumIterations { get; set; }
        public int NumberOfRealizations { get; set; }
        public int? GeneratorSeed { get; set; }

        public int? CorLengthInXDir { get; set; }
        public int? CorLengthInYDir { get; set; }

        public CovarianceFunction CovFunction { get; set; }

        public DistributionInfo CohesionDist { get; set; }

        public DistributionInfo FrictionAngleDist { get; set; }
        public FrictionAngle FrictionAngleType { get; set; }
        public DistributionInfo DilationAngleDist { get; set; }
        public DistributionInfo ElasticModulusDist { get; set; }
        public DistributionInfo PoissonsRatioDist { get; set; }

        public double?[,] CorMatrix { get; set; }

        public string DataFileLocation()
        {
            string appFileName = Environment.GetCommandLineArgs()[0];
            string directory = System.IO.Path.GetDirectoryName(appFileName);

            return directory + "\\" + BaseName + ".dat";

        }
        
        public RBear2D()
        {
            CohesionDist = new DistributionInfo(SoilProperty.Cohesion);
            FrictionAngleDist = new DistributionInfo(SoilProperty.FrictionAngle);
            DilationAngleDist = new DistributionInfo(SoilProperty.DilationAngle);
            ElasticModulusDist = new DistributionInfo(SoilProperty.ElasticModulus);
            PoissonsRatioDist = new DistributionInfo(SoilProperty.PoissonsRatio);

            

            

            CorMatrix = new double?[,] { { 1, 0, 0, 0, 0 },
                                         { 0, 1, 0, 0, 0 },
                                         { 0, 0, 1, 0, 0 },
                                         { 0, 0, 0, 1, 0 },
                                         { 0, 0, 0, 0, 1 }};
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

            directory += "\\Executables\\rbear2d.exe";

            directory = "\"" + directory + "\"";

            pInfo.FileName = directory;
            pInfo.RedirectStandardOutput = true;
            pInfo.Arguments = "\"" + AppDataFileLocation + "\"";
            pInfo.UseShellExecute = false;
            pInfo.CreateNoWindow = true;
            p = new Process() { StartInfo = pInfo };

            p.Start();

            currentOp.Report("Running Simulation");


            if (WriteDebugDataToOutputFile)
            {
                using(var reader = p.StandardOutput)
                {
                    while(!reader.EndOfStream && !token.IsCancellationRequested)
                    {
                        Line = reader.ReadLine();

                        if(Line == "Analyzing realization:")
                        {

                            while (progress < (int)NumberOfRealizations && !token.IsCancellationRequested && !reader.EndOfStream)
                            {
                                Line = reader.ReadLine();
                                if(Line != null)
                                {
                                    foreach (char c in Line.ToCharArray())
                                    {
                                        if (c == 'P' || c == '\r')
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
                                            break;
                                        }
                                        else
                                        {
                                            partialLine += c;
                                        }
                                        ProgramOutput += c;
                                    }
                                }
                            }
                        }
                        ProgramOutput += Line + Environment.NewLine;
                    }
                }
            }
            else
            {
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
            }


            CanDisplaySummaryStats = true;
            CanDisplayField = this.PlotFirstRandomField;
            CanDisplayMesh = this.ProducePSPLOTOfFirstFEM & this.ShowMeshOnDisplacedPlot;
            CanDisplayBearingHist = this.OutputBearingCapacitySamples;

            currentOp.Report("Finished");


            return ProgramOutput;
        }


        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public string AppDataFileLocation
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RFEM_Software\\" + BaseName + ".dat"; }
        }
    }
}
