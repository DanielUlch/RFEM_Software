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

namespace RFEM_Infrastructure
{
    public class RBear2D : IHasDataFile, INotifyPropertyChanged
    {

        private PlotProperty _FirstRandomFieldProperty;
        private PlotProperty _PSPLotProperty;

        private bool _CanDisplaySummaryStats;
        private bool _CanDisplayMesh;
        private bool _CanDisplayField;
        private bool _CanDisplayBearingHist;

        public event PropertyChangedEventHandler PropertyChanged;

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

        public PlotableProperty FirstRandomFieldProperty
        {
            get
            {
                return _FirstRandomFieldProperty.Value;
            }
            set
            {
                _FirstRandomFieldProperty.Value = value;
            }
        }

        public bool ProducePSPLOTOfFirstFEM { get; set; }
        public bool ShowMeshOnDisplacedPlot { get; set; }
        public bool ShowRFOnPSPLOT { get; set; }
        public bool ShowLogRandomField { get; set; }

        public PlotableProperty PSPLOTProperty
        {
            get { return _PSPLotProperty.Value; }
            set
            {
                _PSPLotProperty.Value = value;
            }
        }

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
        public int? NSimulations { get; set; }
        public int? GeneratorSeed { get; set; }

        public int? CorLengthInXDir { get; set; }
        public int? CorLengthInYDir { get; set; }

        public CovarianceFunction CovFunction { get; set; }

        public DistributionInfo CohesionDist { get; set; }

        public DistributionInfo FrictionAngleDist { get; set; }
        public string FrictionAnglePrefix { get; set; } = "";
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

        public string DataFileString()
        {
            ReplaceNullValuesWithZero(CohesionDist);
            ReplaceNullValuesWithZero(FrictionAngleDist);
            ReplaceNullValuesWithZero(DilationAngleDist);
            ReplaceNullValuesWithZero(ElasticModulusDist);
            ReplaceNullValuesWithZero(PoissonsRatioDist);

            var str = new StringBuilder();

            str.AppendLine(JobTitle);
            str.AppendLine("Echo input data to stats file (t/f)? . . . . . .  " + TFConversion(EchoInputDataToOutputFile));
            str.AppendLine("Report progress to standard output (t/f)?. . . .  " + TFConversion(true));
            str.AppendLine("Dump debug data to *.stt file (t/f)? . . . . . .  " + TFConversion(WriteDebugDataToOutputFile));
            str.AppendLine(String.Format("Display a random field (t/f)?  . . . . . . . . .  {0} {1} {2}", 
                                            TFConversion(PlotFirstRandomField),"1", _FirstRandomFieldProperty.CharacterCode));
            str.AppendLine(String.Format("Display displaced FE mesh (t/f)? . . . . . . . .  {0} {1}",
                                            TFConversion(ProducePSPLOTOfFirstFEM), "1"));
            str.AppendLine("Normalize bearing capacity (/det)? . . . . . . .  " + TFConversion(NormalizeBearingCapacitySamples));
            str.AppendLine("Output bearing capacity samples? . . . . . . . .  " + TFConversion(OutputBearingCapacitySamples));
            str.AppendLine(String.Format("Number of elements in X and Y directions . . . .  {0} {1}", NElementsInXDir,
                                            NElementsInYDir));
            str.AppendLine(String.Format("Element size in X and Y directions . . . . . . .  {0} {1}", ElementSizeInXDir,
                                            ElementSizeInYDir));

            if(NumberOfFootings == 1)
            {
                str.AppendLine(string.Format("Number of footings, width  . . . . . . . . . . .  {0} {1}", NumberOfFootings,
                                                FootingWidth));
            }
            else
            {
                str.AppendLine(string.Format("Number of footings, width, gap . . . . . . . . .  {0} {1} {2}",
                                                NumberOfFootings, FootingWidth, FootingGap));
            }

            str.AppendLine(string.Format("Displacement increment, plastic tol, bearing tol  {0} {1} {2}",
                                            DisplacementInc, PlasticTol, BearingTol));
            str.AppendLine(string.Format("Max displacement steps, max iterations . . . . .  {0} {1}", MaxNumSteps,
                                            MaxNumIterations));

            if (CohesionDist.Type == DistributionType.Bounded)
            {
                str.AppendLine(string.Format("Cohesion mean/SD, dist, lower/upper/loc/scale  .  {0} {1} {2} {3} {4} {5} {6}",
                                            CohesionDist.Mean.ToString(), CohesionDist.StandardDev.ToString(),
                                            CohesionDist.Dist.Name.ToLower(), CohesionDist.LowerBound.ToString(),
                                            CohesionDist.UpperBound.ToString(), CohesionDist.Location.ToString(),
                                            CohesionDist.Scale.ToString()));
            }
            else
            {
                str.AppendLine(string.Format("Cohesion mean, SD, dist  . . . . . . . . . . . .  {0} {1} {2}",
                                                CohesionDist.Mean.ToString(), CohesionDist.StandardDev.ToString(),
                                                CohesionDist.Dist.Name.ToLower()));
            }

            if(FrictionAngleDist.Type == DistributionType.Bounded)
            {
                str.AppendLine(string.Format("Friction mean/SD, dist, lower/upper/loc/scale  .  {0} {1} {2}{3} {4} {5} {6} {7}",
                                            FrictionAngleDist.Mean.ToString(), FrictionAngleDist.StandardDev.ToString(),
                                            FrictionAnglePrefix,
                                            FrictionAngleDist.Dist.Name.ToLower(), FrictionAngleDist.LowerBound.ToString(),
                                            FrictionAngleDist.UpperBound.ToString(), FrictionAngleDist.Location.ToString(),
                                            FrictionAngleDist.Scale.ToString()));
            }
            else
            {
                
                str.AppendLine(string.Format("Friction angle mean, SD, dist  . . . . . . . . .  {0} {1} {2}{3}",
                                                FrictionAngleDist.Mean.ToString(), FrictionAngleDist.StandardDev.ToString(),
                                                FrictionAnglePrefix, FrictionAngleDist.Dist.Name.ToLower()));
            }

            if(DilationAngleDist.Type == DistributionType.Bounded)
            {
                str.AppendLine(string.Format("Dilation mean/SD, dist, lower/upper/loc/scale  .  {0} {1} {2} {3} {4} {5} {6}",
                                            DilationAngleDist.Mean.ToString(), DilationAngleDist.StandardDev.ToString(),
                                            DilationAngleDist.Dist.Name.ToLower(), DilationAngleDist.LowerBound.ToString(),
                                            DilationAngleDist.UpperBound.ToString(), DilationAngleDist.Location.ToString(),
                                            DilationAngleDist.Scale.ToString()));
            }
            else
            {
                str.AppendLine(string.Format("Dilation angle mean, SD, dist  . . . . . . . . .  {0} {1} {2}",
                                                DilationAngleDist.Mean.ToString(), DilationAngleDist.StandardDev.ToString(),
                                                DilationAngleDist.Dist.Name.ToLower()));
            }

            if(ElasticModulusDist.Type == DistributionType.Bounded)
            {
                str.AppendLine(string.Format("Elastic mean/SD, dist, lower/upper/loc/scale . .  {0} {1} {2} {3} {4} {5} {6}",
                                            ElasticModulusDist.Mean.ToString(), ElasticModulusDist.StandardDev.ToString(),
                                            ElasticModulusDist.Dist.Name.ToLower(), ElasticModulusDist.LowerBound.ToString(),
                                            ElasticModulusDist.UpperBound.ToString(), ElasticModulusDist.Location.ToString(),
                                            ElasticModulusDist.Scale.ToString()));
            }
            else
            {
                str.AppendLine(string.Format("Elastic modulus mean, SD, dist . . . . . . . . .  {0} {1} {2}",
                                                ElasticModulusDist.Mean.ToString(), ElasticModulusDist.StandardDev.ToString(),
                                                ElasticModulusDist.Dist.Name.ToLower()));
            }

            if(PoissonsRatioDist.Type == DistributionType.Bounded)
            {
                str.AppendLine(string.Format("Poisson's mean/SD, dist, lower/upper/loc/scale .  {0} {1} {2} {3} {4} {5} {6}",
                                            PoissonsRatioDist.Mean.ToString(), PoissonsRatioDist.StandardDev.ToString(),
                                            PoissonsRatioDist.Dist.Name.ToLower(), PoissonsRatioDist.LowerBound.ToString(),
                                            PoissonsRatioDist.UpperBound.ToString(), PoissonsRatioDist.Location.ToString(),
                                            PoissonsRatioDist.Scale.ToString()));
            }
            else
            {
                str.AppendLine(string.Format("Poisson's ratio mean, SD, dist . . . . . . . . .  {0} {1} {2}",
                                                PoissonsRatioDist.Mean.ToString(), PoissonsRatioDist.StandardDev.ToString(),
                                                PoissonsRatioDist.Dist.Name.ToLower()));
            }

            str.AppendLine("Number of realizations . . . . . . . . . . . . .  " + NSimulations);
            str.AppendLine("Generator seed (0 for random seed) . . . . . . .  " + GeneratorSeed);
            str.AppendLine(string.Format("Correlation length in X [and Y] directions . . .  {0} {1}", CorLengthInXDir,
                                            CorLengthInYDir));
            str.AppendLine("Name of covariance function  . . . . . . . . . .  " + Enum.GetName(typeof(CovarianceFunction), CovFunction));
            str.AppendLine();
            str.AppendLine("Material Property Correlation Matrix Data");
            str.AppendLine("=========================================");
            str.AppendLine("Property 1    Property 2    Correlation");
            str.AppendLine("----------    ----------    -----------");

            for(int i =0; i<=4; i++)
            {
                for(int j = i+1; j<=4; j++)
                {
                    if (CorMatrix[i,j] != 0)
                    {
                        str.AppendLine(string.Format("   {0}            {1}           {2}", 
                                        (new PlotProperty((PlotableProperty)i)).CharacterCode,
                                        (new PlotProperty((PlotableProperty)j)).CharacterCode, 
                                        CorMatrix[i, j])); 
                    }
                }
            }

            str.AppendLine();
            str.AppendLine("Show element boundaries  . . . . . . . . . . . .  " + TFConversion(ShowMeshOnDisplacedPlot));
            str.AppendLine(string.Format("Show random field as background [log?] [prop?] .  {0} {1} {2}",
                                            TFConversion(ShowRFOnPSPLOT), TFConversion(ShowLogRandomField),
                                            _PSPLotProperty.CharacterCode));
            str.AppendLine("Width of displaced mesh output plot in inches  .  " + DisplacedMeshPlotWidth);
            str.AppendLine("Plot offset in x- [and y-] directions (inches) .  1.5");

            return str.ToString();
        }
        private void ReplaceNullValuesWithZero(DistributionInfo dist)
        {
            if (dist.Mean == null)
                dist.Mean = 0;
            if (dist.StandardDev == null)
                dist.StandardDev = 0;
        }
        private string TFConversion(bool val)
        {
            if (val == true)
                return "t";
            else
                return "f";
        }

        public RBear2D()
        {
            CohesionDist = new DistributionInfo(SoilProperty.Cohesion);
            FrictionAngleDist = new DistributionInfo(SoilProperty.FrictionAngle);
            DilationAngleDist = new DistributionInfo(SoilProperty.DilationAngle);
            ElasticModulusDist = new DistributionInfo(SoilProperty.ElasticModulus);
            PoissonsRatioDist = new DistributionInfo(SoilProperty.PoissonsRatio);

            _FirstRandomFieldProperty = new PlotProperty(PlotableProperty.Cohesion);
            _PSPLotProperty = new PlotProperty(PlotableProperty.Cohesion);

            

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

                            while (progress < (int)NSimulations && !token.IsCancellationRequested && !reader.EndOfStream)
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
                            while (progress < (int)NSimulations && !token.IsCancellationRequested)
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
