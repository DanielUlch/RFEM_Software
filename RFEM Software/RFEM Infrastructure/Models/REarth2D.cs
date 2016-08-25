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
    public class REarth2D : ISimModel, INotifyPropertyChanged
    {
        private bool _CanDisplaySummaryStats;
        private bool _CanDisplayField;
        private bool _CanDisplayMesh;
        private bool _CanDisplayEarthForceHist;
        private bool _CanDisplaySoilSampleHist;

        #region Form Properties

        public Program Type
        {
            get { return Program.REarth2D; }
        }
        public string JobTitle { get; set; }
        public string BaseName { get; set; }
        public bool EchoInputDataToOutputFile { get; set; }
        public bool WriteDebugDataToOutputFile { get; set; }
        public bool PlotFirstRandomField { get; set; }
        public REarthSoilProperties FirstRFPropertyToPlot { get; set; }
        public bool ProducePSPlotOfFirstFEM { get; set; }
        public bool ShowMeshOnDisplacedMeshPlot { get; set; }
        public bool ShowRandomFieldOnDisplacedMesh { get; set; }
        public bool ShowLogRandomField { get; set; }
        public REarthSoilProperties DisplacedMeshPropertyToPlot { get; set; }
        public double DisplacedMeshWidth { get; set; }
        public bool StoreWallReactionSamples { get; set; }
        public bool VirtuallySampleSoil { get; set; }
        public ObservableCollection<SampleLocation> SampleLocations { get; set; }
        public bool OutputSampledSoilProperties { get; set; }
        public int NElementsInXDir { get; set; }
        public int NElementsInYDir { get; set; }
        public double ElementSizeInXDir { get; set; }
        public double ElementSizeInYDir { get; set; }
        public int WallExtension { get; set; }
        public bool RoughWallSurface { get; set; }
        public double DisplacementIncrement { get; set; }
        public double PlasticTol { get; set; }
        public double StressTol { get; set; }
        public int MaxNumSteps { get; set; }
        public int MaxNumIterations { get; set; }
        public int NumberOfRealizations { get; set; }
        public int GeneratorSeed { get; set; }
        public int CorrelationLengthInXDir { get; set; }
        public int CorrelationLengthInYDir { get; set; }
        public CovarianceFunction CovFunction { get; set; }

        public REarthDistributionInfo Cohesion { get; set; }
        public DistributionInfo FrictionAngle { get; set; }
        
        public FrictionAngle FrictionAngleType { get; set; }
        public REarthDistributionInfo DilationAngle { get; set; }
        public REarthDistributionInfo ElasticModulus { get; set; }
        public REarthDistributionInfo PoissonsRatio { get; set; }   
        public REarthDistributionInfo UnitWeight { get; set; }
        public REarthDistributionInfo PressureCoefficient { get; set; }  
        public double[,] CorrelationMatrix { get; set; }




        #endregion

        #region State Properties
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
                if(_CanDisplayField != value)
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
                if(_CanDisplayMesh != value)
                {
                    _CanDisplayMesh = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool CanDisplayEarthForceHist
        {
            get { return _CanDisplayEarthForceHist; }
            set
            {
                if(_CanDisplayEarthForceHist != value)
                {
                    _CanDisplayEarthForceHist = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool CanDisplaySoilSampleHist
        {
            get { return _CanDisplaySoilSampleHist; }
            set
            {
                if(_CanDisplaySoilSampleHist != value)
                {
                    _CanDisplaySoilSampleHist = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion
        public string OutputDirectory
        {
            get; set;
        }
        public string DataLocation
        {
            get { return OutputDirectory + "\\" + BaseName + ".dat"; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        

        
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public REarth2D()
        {
             SampleLocations= new ObservableCollection<SampleLocation>()
        {
            new SampleLocation() {XCoordinate =null, YCoordinate=null },
            new SampleLocation() {XCoordinate = null, YCoordinate = null },
            new SampleLocation() {XCoordinate = null, YCoordinate = null }
        };
            Cohesion = new REarthDistributionInfo(REarthSoilProperties.Cohesion);
            FrictionAngle = new DistributionInfo(SoilProperty.FrictionAngle);
            DilationAngle = new REarthDistributionInfo(REarthSoilProperties.DilationAngle);
            ElasticModulus = new REarthDistributionInfo(REarthSoilProperties.ElasticModulus);
            PoissonsRatio = new REarthDistributionInfo(REarthSoilProperties.PoissonsRatio);
            UnitWeight = new REarthDistributionInfo(REarthSoilProperties.UnitWeight);
            PressureCoefficient = new REarthDistributionInfo(REarthSoilProperties.PressureCoefficient);

            CorrelationMatrix = new double[7, 7]
            {
                {1,0,0,0,0,0,0 },
                {0,1,0,0,0,0,0 },
                {0,0,1,0,0,0,0 },
                {0,0,0,1,0,0,0 },
                {0,0,0,0,1,0,0 },
                {0,0,0,0,0,1,0 },
                {0,0,0,0,0,0,1 }
            };
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

            directory += "\\Executables\\rearth2d.exe";

            directory = "\"" + directory + "\"";

            pInfo.FileName = directory;
            pInfo.RedirectStandardOutput = true;
            pInfo.Arguments = "\"" + DataLocation + "\"";
            pInfo.UseShellExecute = false;
            pInfo.CreateNoWindow = true;
            p = new Process() { StartInfo = pInfo };

            p.Start();

            currentOp.Report("Running Simulation");


            if (WriteDebugDataToOutputFile)
            {
                using (var reader = p.StandardOutput)
                {
                    while (!reader.EndOfStream && !token.IsCancellationRequested)
                    {
                        Line = reader.ReadLine();

                        if (Line == "Analyzing realization:")
                        {

                            ProgramOutput += Line;
                            Line = reader.ReadLine();

                            if(Int32.TryParse(Line, out progress))
                            {
                                simIteration.Report(progress);
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

            currentOp.Report("Finished");
            CanDisplaySummaryStats = true;
            if (PlotFirstRandomField)
            {
                CanDisplayField = true;
            }
            if (ProducePSPlotOfFirstFEM)
            {
                CanDisplayMesh = true;
            }
            if (StoreWallReactionSamples)
            {
                CanDisplayEarthForceHist = true;
            }
            if(VirtuallySampleSoil &
                OutputSampledSoilProperties &
                SampleLocations.Where( x => x.XCoordinate != null && x.YCoordinate != null).Count() > 0)
            {
                CanDisplaySoilSampleHist = true;
            }

            return ProgramOutput;
        }
    }


    

    public struct SampleLocation: INotifyPropertyChanged
    {
        private int? _XCoordinate;
        private int? _YCoordinate;
        public int? XCoordinate
        {
            get { return _XCoordinate; }
            set
            {
                if(_XCoordinate != value)
                {
                    _XCoordinate = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("XCoordinate"));
                    }
                }
            }
        }
        public int? YCoordinate
        {
            get { return _YCoordinate; }
            set
            {
                if(_YCoordinate != value)
                {
                    _YCoordinate = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("YCoordinate"));
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
