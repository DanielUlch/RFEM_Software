using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RFEM_Infrastructure
{
    public class REarth2D : ISimModel, INotifyPropertyChanged
    {
        private bool _CanDisplaySummaryStats;
        private bool _CanDisplayField;
        private bool _CanDisplayMesh;
        private bool _CanDisplayEarthForceHist;
        private bool _CanDisplaySoilSampleHist;

        #region Form Properties

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
        public string AppDataFileLocation
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RFEM_Software\\" + BaseName + ".dat";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string DataFileLocation()
        {
            string appFileName = Environment.GetCommandLineArgs()[0];
            string directory = System.IO.Path.GetDirectoryName(appFileName);

            return directory + "\\" + BaseName + ".dat";
        }

        public string GetDataFileString()
        {
            StringBuilder str = new StringBuilder();

            ReplaceNullValuesWithZero(Cohesion);
            ReplaceNullValuesWithZero(FrictionAngle);
            ReplaceNullValuesWithZero(DilationAngle);
            ReplaceNullValuesWithZero(ElasticModulus);
            ReplaceNullValuesWithZero(PoissonsRatio);
            ReplaceNullValuesWithZero(UnitWeight);
            ReplaceNullValuesWithZero(PressureCoefficient);


            str.AppendLine(JobTitle);
            str.AppendLine("Echo input data to stats file (t/f)? . . . . . .  " + 
                                                            EchoInputDataToOutputFile.ToTFString());
            str.AppendLine("Report progress to standard output (t/f)?. . . .  " + true.ToTFString());
            str.AppendLine("Dump debug data to *.stt file (t/f)? . . . . . .  " + 
                                                            WriteDebugDataToOutputFile.ToTFString());
            str.AppendLine(string.Format("Display a random field to *.fld (t/f)? . . . . .  {0} {1} {2}",
                                            PlotFirstRandomField.ToTFString(), "1", 
                                            FirstRFPropertyToPlot.ToDataFileString()));
            str.AppendLine(string.Format("Plot displaced FE mesh to *.dis (t/f)? . . . . .  {0} {1}",
                                                            ProducePSPlotOfFirstFEM.ToTFString(),
                                                            "1"));
            str.AppendLine("Output soil force samples? . . . . . . . . . . .  " + 
                                                            StoreWallReactionSamples.ToTFString());
            str.AppendLine(string.Format("Number of elements in X and Y directions . . . .  {0} {1}",
                                                            NElementsInXDir, NElementsInYDir));
            str.AppendLine(string.Format("Element size in X and Y directions . . . . . . .  {0} {1}",
                                                            ElementSizeInXDir, ElementSizeInYDir));
            str.AppendLine(string.Format("Wall depth in elements [rough?]  . . . . . . . .  {0} {1}",
                                                            WallExtension, RoughWallSurface.ToTFString()));
            str.AppendLine(string.Format("Displacement increment, plastic tol, stress tol.  {0} {1} {2}",
                                                            DisplacementIncrement, PlasticTol, StressTol));
            str.AppendLine(string.Format("Max displacement steps, max iterations . . . . .  {0} {1}",
                                                            MaxNumSteps, MaxNumIterations));

            switch (Cohesion.DistributionType)
            {
                case REarthDistributions.Bounded:

                    str.AppendLine(
                        string.Format("Cohesion mean/SD, dist, lower/upper/loc/scale  .  {0} {1} {2} {3} {4} {5} {6}",
                                      Cohesion.Mean.ToString(),
                                      Cohesion.StdDev.ToString(),
                                      Cohesion.DistributionType.ToDataFileString(),
                                      Cohesion.LowerBound.ToString(),
                                      Cohesion.UpperBound.ToString(),
                                      Cohesion.Location.ToString(),
                                      Cohesion.Scale));

                    break;

                case REarthDistributions.fphi:

                    str.AppendLine(
                        String.Format("Cohesion mean/SD, dist, a, b, f(phi) . . . . . .  {0} {1} {2} {3} {4} {5}",
                                       Cohesion.Mean,
                                       Cohesion.StdDev,
                                       Cohesion.DistributionType.ToDataFileString(),
                                       Cohesion.Intercept,
                                       Cohesion.Slope,
                                       Cohesion.PhiFunc.ToDataFileString()));
                    break;

                default:

                    str.AppendLine(
                        string.Format("Cohesion mean, SD, dist  . . . . . . . . . . . .  {0} {1} {2}",
                                      Cohesion.Mean,
                                      Cohesion.StdDev,
                                      Cohesion.DistributionType.ToDataFileString()));
                    break;
            }

            switch (FrictionAngle.Type)
            {
                case DistributionType.Bounded:

                    str.AppendLine(
                        string.Format("Friction mean/SD, dist, lower/upper/loc/scale  .  {0} {1} {2}{3} {4} {5} {6} {7}",
                                      FrictionAngle.Mean,
                                      FrictionAngle.StandardDev,
                                      FrictionAngleType.ToDataFileString(),
                                      FrictionAngle.Type.ToDataFileString(),
                                      FrictionAngle.LowerBound,
                                      FrictionAngle.UpperBound,
                                      FrictionAngle.Location,
                                      FrictionAngle.Scale));
                    break;

                default:

                    str.AppendLine(
                        string.Format("Friction angle mean, SD, dist  . . . . . . . . .  {0} {1} {2}{3}",
                                      FrictionAngle.Mean,
                                      FrictionAngle.StandardDev,
                                      FrictionAngleType.ToDataFileString(),
                                      FrictionAngle.Type.ToDataFileString()));
                    break;
            }

            switch (DilationAngle.DistributionType)
            {
                case REarthDistributions.Bounded:

                    str.AppendLine(
                        string.Format("Dilation mean/SD, dist, lower/upper/loc/scale  .  {0} {1} {2} {3} {4} {5} {6}",
                                      DilationAngle.Mean,
                                      DilationAngle.StdDev,
                                      DilationAngle.DistributionType.ToDataFileString(),
                                      DilationAngle.LowerBound,
                                      DilationAngle.UpperBound,
                                      DilationAngle.Location,
                                      DilationAngle.Scale));

                    break;

                case REarthDistributions.fphi:

                    str.AppendLine(
                        String.Format("Dilation mean/SD, dist, a, b, f(phi) . . . . . .  {0} {1} {2} {3} {4} {5}",
                                       DilationAngle.Mean,
                                       DilationAngle.StdDev,
                                       DilationAngle.DistributionType.ToDataFileString(),
                                       DilationAngle.Intercept,
                                       DilationAngle.Slope,
                                       DilationAngle.PhiFunc.ToDataFileString()));
                    break;

                default:

                    str.AppendLine(
                        string.Format("Dilation angle mean, SD, dist  . . . . . . . . .  {0} {1} {2}",
                                      DilationAngle.Mean,
                                      DilationAngle.StdDev,
                                      DilationAngle.DistributionType.ToDataFileString()));
                    break;
            }

            switch (ElasticModulus.DistributionType)
            {
                case REarthDistributions.Bounded:

                    str.AppendLine(
                        string.Format("Elastic mean/SD, dist, lower/upper/loc/scale . .  {0} {1} {2} {3} {4} {5} {6}",
                                      ElasticModulus.Mean,
                                      ElasticModulus.StdDev,
                                      ElasticModulus.DistributionType.ToDataFileString(),
                                      ElasticModulus.LowerBound,
                                      ElasticModulus.UpperBound,
                                      ElasticModulus.Location,
                                      ElasticModulus.Scale));

                    break;

                case REarthDistributions.fphi:

                    str.AppendLine(
                        String.Format("Elastic mean/SD, dist, a, b, f(phi). . . . . . .  {0} {1} {2} {3} {4} {5}",
                                       ElasticModulus.Mean,
                                       ElasticModulus.StdDev,
                                       ElasticModulus.DistributionType.ToDataFileString(),
                                       ElasticModulus.Intercept,
                                       ElasticModulus.Slope,
                                       ElasticModulus.PhiFunc.ToDataFileString()));
                    break;

                default:

                    str.AppendLine(
                        string.Format("Elastic modulus mean, SD, dist . . . . . . . . .  {0} {1} {2}",
                                      ElasticModulus.Mean,
                                      ElasticModulus.StdDev,
                                      ElasticModulus.DistributionType.ToDataFileString()));
                    break;
            }

            switch (PoissonsRatio.DistributionType)
            {
                case REarthDistributions.Bounded:

                    str.AppendLine(
                        string.Format("Poisson's mean/SD, dist, lower/upper/loc/scale .  {0} {1} {2} {3} {4} {5} {6}",
                                      PoissonsRatio.Mean,
                                      PoissonsRatio.StdDev,
                                      PoissonsRatio.DistributionType.ToDataFileString(),
                                      PoissonsRatio.LowerBound,
                                      PoissonsRatio.UpperBound,
                                      PoissonsRatio.Location,
                                      PoissonsRatio.Scale));

                    break;

                case REarthDistributions.fphi:

                    str.AppendLine(
                        String.Format("Poisson's mean/SD, dist, a, b, f(phi). . . . . .  {0} {1} {2} {3} {4} {5}",
                                       PoissonsRatio.Mean,
                                       PoissonsRatio.StdDev,
                                       PoissonsRatio.DistributionType.ToDataFileString(),
                                       PoissonsRatio.Intercept,
                                       PoissonsRatio.Slope,
                                       PoissonsRatio.PhiFunc.ToDataFileString()));
                    break;

                default:

                    str.AppendLine(
                        string.Format("Poisson's ratio mean, SD, dist . . . . . . . . .  {0} {1} {2}",
                                      PoissonsRatio.Mean,
                                      PoissonsRatio.StdDev,
                                      PoissonsRatio.DistributionType.ToDataFileString()));
                    break;
            }

            switch (UnitWeight.DistributionType)
            {
                case REarthDistributions.Bounded:

                    str.AppendLine(
                        string.Format("Unit weight mean/SD, dist, lower/upper/loc/scale  {0} {1} {2} {3} {4} {5} {6}",
                                      UnitWeight.Mean,
                                      UnitWeight.StdDev,
                                      UnitWeight.DistributionType.ToDataFileString(),
                                      UnitWeight.LowerBound,
                                      UnitWeight.UpperBound,
                                      UnitWeight.Location,
                                      UnitWeight.Scale));

                    break;

                case REarthDistributions.fphi:

                    str.AppendLine(
                        String.Format("Unit weight mean/SD, dist, a, b, f(phi). . . . .  {0} {1} {2} {3} {4} {5}",
                                       UnitWeight.Mean,
                                       UnitWeight.StdDev,
                                       UnitWeight.DistributionType.ToDataFileString(),
                                       UnitWeight.Intercept,
                                       UnitWeight.Slope,
                                       UnitWeight.PhiFunc.ToDataFileString()));
                    break;

                default:

                    str.AppendLine(
                        string.Format("Unit weight mean, SD, dist . . . . . . . . . . .  {0} {1} {2}",
                                      UnitWeight.Mean,
                                      UnitWeight.StdDev,
                                      UnitWeight.DistributionType.ToDataFileString()));
                    break;
            }

            switch (PressureCoefficient.DistributionType)
            {
                case REarthDistributions.Bounded:

                    str.AppendLine(
                        string.Format("Pressure mean/SD, dist, lower/upper/loc/scale  .  {0} {1} {2} {3} {4} {5} {6}",
                                      PressureCoefficient.Mean,
                                      PressureCoefficient.StdDev,
                                      PressureCoefficient.DistributionType.ToDataFileString(),
                                      PressureCoefficient.LowerBound,
                                      PressureCoefficient.UpperBound,
                                      PressureCoefficient.Location,
                                      PressureCoefficient.Scale));

                    break;

                case REarthDistributions.fphi:

                    str.AppendLine(
                        String.Format("Pressure coeff mean/SD, dist, a, b, f(phi) . . .  {0} {1} {2} {3} {4} {5}",
                                       PressureCoefficient.Mean,
                                       PressureCoefficient.StdDev,
                                       PressureCoefficient.DistributionType.ToDataFileString(),
                                       PressureCoefficient.Intercept,
                                       PressureCoefficient.Slope,
                                       PressureCoefficient.PhiFunc.ToDataFileString()));
                    break;

                default:

                    str.AppendLine(
                        string.Format("Pressure coeff mean, SD, dist  . . . . . . . . .  {0} {1} {2}",
                                      PressureCoefficient.Mean,
                                      PressureCoefficient.StdDev,
                                      PressureCoefficient.DistributionType.ToDataFileString()));
                    break;
            }

            str.AppendLine("Number of realizations . . . . . . . . . . . . .  " + NumberOfRealizations);
            str.AppendLine("Generator seed (0 for random seed) . . . . . . .  " + GeneratorSeed);
            str.AppendLine(
                string.Format("Scale of fluctuation in X [and Y] directions . .  {0} {1}",
                              CorrelationLengthInXDir,
                              CorrelationLengthInYDir));
            str.AppendLine("Name of covariance function  . . . . . . . . . .  " + CovFunction.ToString());

            str.AppendLine();
            str.AppendLine("Material Property Correlation Matrix Data");
            str.AppendLine("=========================================");
            str.AppendLine("Property 1    Property 2    Correlation");
            str.AppendLine("----------    ----------    -----------");

            for(int i = 0; i < 7; i++)
            {
                for(int j = i + 1; j < 7; j++)
                {
                    if(CorrelationMatrix[i,j] != 0)
                    {
                        str.AppendLine(
                            string.Format("   {0}            {1}           {2}",
                                          ((REarthSoilProperties)i).ToDataFileString(),
                                          ((REarthSoilProperties)j).ToDataFileString(),
                                          CorrelationMatrix[i, j]));
                    }
                }
            }

            str.AppendLine();

            str.AppendLine("Show element boundaries  . . . . . . . . . . . .  " + 
                                                                ShowMeshOnDisplacedMeshPlot.ToTFString());
            str.AppendLine(
                string.Format("Show random field as background [log?] [prop?] .  {0} {1} {2}",
                              ShowRandomFieldOnDisplacedMesh.ToTFString(),
                              ShowLogRandomField.ToTFString(),
                              DisplacedMeshPropertyToPlot.ToDataFileString()));

            str.AppendLine("Width of displaced mesh output plot in inches  .  " + DisplacedMeshWidth);
            str.AppendLine("Number of soil property samples to take  . . . .  " + 
                            SampleLocations.Where( (x) => x.XCoordinate != null && x.YCoordinate != null).
                                            Count());

            str.Append("Locations of samples: (x,y) index pairs  . . . . ");

            foreach(SampleLocation l in SampleLocations.Where((x) => x.XCoordinate != null && x.YCoordinate != null))
            {
                str.Append(
                    string.Format(" {0} {1}",
                                  l.XCoordinate,
                                  l.YCoordinate));
            }

            str.Append( "\n");

            str.AppendLine("Output sampled soil properties to *.sam? . . . .  " + OutputSampledSoilProperties.ToTFString());

            return str.ToString();

        }
        private void ReplaceNullValuesWithZero(DistributionInfo dist)
        {
            if (dist.Mean == null)
                dist.Mean = 0;
            if (dist.StandardDev == null)
                dist.StandardDev = 0;
        }
        private void ReplaceNullValuesWithZero(REarthDistributionInfo dist)
        {
            if (dist.Mean == null)
                dist.Mean = 0;
            if (dist.StdDev == null)
                dist.StdDev = 0;
        }
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
            pInfo.Arguments = "\"" + AppDataFileLocation + "\"";
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
