using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEM_Infrastructure
{
    public static class FileReader
    {
        public static IHasDataFile Read(Program type, string filePath)
        {
            switch (type)
            {
                case Program.RBear2D:
                    return ReadRBearFile(filePath);
                case Program.RDam2D:
                    return ReadRDamFile(filePath);
                default:
                    throw new NotImplementedException("A read function has not been implemented for the selected data file.");
            }
        }
        public static string Read(string filePath)
        {
            string content;

            using (var reader = new System.IO.StreamReader(filePath))
            {
                content = reader.ReadToEnd();
            }
            return content;
        }
        private static RBear2D ReadRBearFile(string filePath)
        {
            var formData = new RBear2D();
            string Line;
            string[] LineFragments;
            int LHSLength = 48;
            int i, j;

            using (var reader = new System.IO.StreamReader(filePath))
            {
                formData.JobTitle = reader.ReadLine();
                formData.BaseName = System.IO.Path.GetFileNameWithoutExtension(filePath);

                Line = reader.ReadLine();
                formData.EchoInputDataToOutputFile = TFInverse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.ReportRunProgress = TFInverse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.WriteDebugDataToOutputFile = TFInverse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.PlotFirstRandomField = TFInverse(LineFragments[0]);
                formData.FirstRandomFieldProperty = PropertyCharInv(LineFragments[2]);

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.ProducePSPLOTOfFirstFEM = TFInverse(LineFragments[0]);

                Line = reader.ReadLine();
                formData.NormalizeBearingCapacitySamples = TFInverse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.OutputBearingCapacitySamples = TFInverse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.NElementsInXDir = LineFragments[0].ToNullableInt32();
                formData.NElementsInYDir = LineFragments[1].ToNullableInt32();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.ElementSizeInXDir = LineFragments[0].ToNullableDouble();
                formData.ElementSizeInYDir = LineFragments[1].ToNullableDouble();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.NumberOfFootings = int.Parse(LineFragments[0]);
                formData.FootingWidth = LineFragments[1].ToNullableDouble();
                if (LineFragments.Length > 2)
                    formData.FootingGap = LineFragments[2].ToNullableDouble();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.DisplacementInc = LineFragments[0].ToNullableDouble();
                formData.PlasticTol = LineFragments[1].ToNullableDouble();
                formData.BearingTol = LineFragments[2].ToNullableDouble();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.MaxNumSteps = LineFragments[0].ToNullableInt32();
                formData.MaxNumIterations = LineFragments[1].ToNullableInt32();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.CohesionDist.Mean = LineFragments[0].ToNullableDouble();
                formData.CohesionDist.StandardDev = LineFragments[1].ToNullableDouble();
                formData.CohesionDist.Type = DistributionCharInv(LineFragments[2]);

                if (LineFragments.Length == 7)
                {
                    formData.CohesionDist.LowerBound = LineFragments[3].ToNullableDouble();
                    formData.CohesionDist.UpperBound = LineFragments[4].ToNullableDouble();
                    formData.CohesionDist.Location = LineFragments[5].ToNullableDouble();
                    formData.CohesionDist.Scale = LineFragments[6].ToNullableDouble();
                }
                else if (LineFragments.Length != 3)
                    throw new FormatException("Unable to read cohesion distribution data from data file.");


                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.FrictionAngleDist.Mean = LineFragments[0].ToNullableDouble();
                formData.FrictionAngleDist.StandardDev = LineFragments[1].ToNullableDouble();

                //Friction angle distribution prefixed by t when the rv is tan(phi)
                if (LineFragments[2].ToCharArray()[0] == 't')
                {
                    formData.FrictionAnglePrefix = "t";
                    LineFragments[2] = LineFragments[2].Substring(1);
                }

                formData.FrictionAngleDist.Type = DistributionCharInv(LineFragments[2]);

                if (LineFragments.Length == 7)
                {
                    formData.FrictionAngleDist.LowerBound = LineFragments[3].ToNullableDouble();
                    formData.FrictionAngleDist.UpperBound = LineFragments[4].ToNullableDouble();
                    formData.FrictionAngleDist.Location = LineFragments[5].ToNullableDouble();
                    formData.FrictionAngleDist.Scale = LineFragments[6].ToNullableDouble();
                }
                else if (LineFragments.Length != 3)
                    throw new FormatException("Unable to read friction distriubtion data from data file.");

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.DilationAngleDist.Mean = LineFragments[0].ToNullableDouble();
                formData.DilationAngleDist.StandardDev = LineFragments[1].ToNullableDouble();
                formData.DilationAngleDist.Type = DistributionCharInv(LineFragments[2]);

                if (LineFragments.Length == 7)
                {
                    formData.DilationAngleDist.LowerBound = LineFragments[3].ToNullableDouble();
                    formData.DilationAngleDist.UpperBound = LineFragments[4].ToNullableDouble();
                    formData.DilationAngleDist.Location = LineFragments[5].ToNullableDouble();
                    formData.DilationAngleDist.Scale = LineFragments[6].ToNullableDouble();
                }
                else if (LineFragments.Length != 3)
                    throw new FormatException("Unable to read dilation angle distribution data from data file.");

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.ElasticModulusDist.Mean = LineFragments[0].ToNullableDouble();
                formData.ElasticModulusDist.StandardDev = LineFragments[1].ToNullableDouble();
                formData.ElasticModulusDist.Type = DistributionCharInv(LineFragments[2]);

                if (LineFragments.Length == 7)
                {
                    formData.ElasticModulusDist.LowerBound = LineFragments[3].ToNullableDouble();
                    formData.ElasticModulusDist.UpperBound = LineFragments[4].ToNullableDouble();
                    formData.ElasticModulusDist.Location = LineFragments[5].ToNullableDouble();
                    formData.ElasticModulusDist.Scale = LineFragments[6].ToNullableDouble();
                }
                else if (LineFragments.Length != 3)
                    throw new FormatException("Unable to read elastic modulus distribution data from data file.");

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.PoissonsRatioDist.Mean = LineFragments[0].ToNullableDouble();
                formData.PoissonsRatioDist.StandardDev = LineFragments[1].ToNullableDouble();
                formData.PoissonsRatioDist.Type = DistributionCharInv(LineFragments[2]);

                if (LineFragments.Length == 7)
                {
                    formData.PoissonsRatioDist.LowerBound = LineFragments[3].ToNullableDouble();
                    formData.PoissonsRatioDist.UpperBound = LineFragments[4].ToNullableDouble();
                    formData.PoissonsRatioDist.Location = LineFragments[5].ToNullableDouble();
                    formData.PoissonsRatioDist.Scale = LineFragments[6].ToNullableDouble();
                }
                else if (LineFragments.Length != 3)
                    throw new FormatException("Unable to read Poisson's Ratio distribution data from data file.");

                Line = reader.ReadLine();
                formData.NSimulations = Line.Substring(LHSLength).Trim().Split(' ')[0].ToNullableInt32();

                Line = reader.ReadLine();
                formData.GeneratorSeed = Line.Substring(LHSLength).Trim().Split(' ')[0].ToNullableInt32();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.CorLengthInXDir = LineFragments[0].ToNullableInt32();
                formData.CorLengthInYDir = LineFragments[1].ToNullableInt32();

                Line = reader.ReadLine();
                formData.CovFunction = CovFuncCharInv(Line.Substring(LHSLength).Trim());

                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();

                Line = reader.ReadLine();
                while (Line.Trim().Length > 0)
                {
                    LineFragments = Line.Split(' ').Where(p => p != "").ToArray();

                    i = (int)PropertyCharInv(LineFragments[0]);
                    j = (int)PropertyCharInv(LineFragments[1]);

                    formData.CorMatrix[i, j] = LineFragments[2].ToNullableDouble();
                    formData.CorMatrix[j, i] = LineFragments[2].ToNullableDouble();

                    Line = reader.ReadLine();
                }

                Line = reader.ReadLine();
                formData.ShowMeshOnDisplacedPlot = TFInverse(Line.Substring(LHSLength).Trim());

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.ShowRFOnPSPLOT = TFInverse(LineFragments[0]);
                formData.ShowLogRandomField = TFInverse(LineFragments[1]);
                formData.PSPLOTProperty = PropertyCharInv(LineFragments[2]);

                Line = reader.ReadLine();
                formData.DisplacedMeshPlotWidth = Line.Substring(LHSLength).Trim().ToNullableDouble();

            }
            return formData;
        }
        private static RDam2D ReadRDamFile(string filePath)
        {
            var formData = new RDam2D();
            string Line;
            string[] LineFragments;
            int LHSLength = 50;

            using (var reader = new System.IO.StreamReader(filePath))
            {
                formData.JobTitle = reader.ReadLine();
                formData.BaseName = System.IO.Path.GetFileNameWithoutExtension(filePath);

                Line = reader.ReadLine();
                formData.EchoInputDataToOutputFile = TFInverse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.DebugCode = int.Parse(LineFragments[0]);
                formData.RealizationNumber = int.Parse(LineFragments[1]);

                Line = reader.ReadLine();
                formData.ProduceDisplayFile = TFInverse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.ProducePSPlotOfFirstFlownet = TFInverse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.OutputGradientMeanAndStdDev = TFInverse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.OutputFlowRate = TFInverse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.OutputBlockConductivities = TFInverse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.OutputConductivityAverages = TFInverse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.GenerateUniformConductivity = TFInverse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.NumElementsInXDir = int.Parse(LineFragments[0]);
                formData.NumElementsInYDir = int.Parse(LineFragments[1]);

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                
                for(int i = 1; i <= 6; i++)
                {
                    if(i<= LineFragments.Length)
                    {
                        formData.NodesForGradientOutput.Add(int.Parse((LineFragments[i - 1])));
                    }
                    else
                    {
                        formData.NodesForGradientOutput.Add(null);
                    }
                }

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.DrainXDimension = double.Parse(LineFragments[0]);
                formData.DrainYDimension = double.Parse(LineFragments[1]);
                formData.DrainConductivity = double.Parse(LineFragments[2]);

                Line = reader.ReadLine();
                formData.DamTop = double.Parse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.DamBase = double.Parse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.DamHeight = double.Parse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.NumberOfRealizations = int.Parse(LineFragments[0]);
                formData.MaxNumberOfIterations = int.Parse(LineFragments[1]);
                formData.ConvergenceTolerance = double.Parse(LineFragments[2]);

                Line = reader.ReadLine();
                formData.GeneratorSeed = int.Parse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.CorrelationLengthInXDir = int.Parse(LineFragments[0]);
                formData.CorrelationLengthInYDir = int.Parse(LineFragments[1]);

                Line = reader.ReadLine();
                formData.ConductivityMean = double.Parse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.ConductivityStdDev = double.Parse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.CovFunction = CovFuncCharInv(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.NumEquipotentialDrops = int.Parse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.ShowEquipotentialDrops = TFInverse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.ShowStreamlines = TFInverse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.ShowMeshOnFlownet = TFInverse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.ShowLogConductivity = TFInverse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.ShowDamDimensionsOnFlownet = TFInverse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.ShowTitlesOnFlownet = TFInverse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.FlownetWidth = int.Parse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.DebugCode = int.Parse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.UseAlternateAlgoIfFirstFails = TFInverse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.RestrainFreeSurfaceNonIncreasing = TFInverse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.DampOscillations = TFInverse(Line.Substring(LHSLength));

                Line = reader.ReadLine();
                formData.ShowCentroidsOnMesh = TFInverse(Line.Substring(LHSLength));

                return formData;
            }
        }
        private static CovarianceFunction CovFuncCharInv(string covFuncChar)
        {
            switch (covFuncChar)
            {
                case "dlavx2":
                    return CovarianceFunction.dlavx2;
                case "dlafr2":
                    return CovarianceFunction.dlafr2;
                case "dlsep2":
                    return CovarianceFunction.dlsep2;
                case "dlsfr2":
                    return CovarianceFunction.dlsfr2;
                case "dlspx2":
                    return CovarianceFunction.dlspx2;
                default:
                    throw new ArgumentException("Unable to read covariance function type in data file.");

            }
        }
        private static DistributionType DistributionCharInv(string distributionChar)
        {
            switch (distributionChar.ToCharArray()[0])
            {
                case 'd':
                    return DistributionType.Deterministic;
                case 'n':
                    return DistributionType.Normal;
                case 'l':
                    return DistributionType.LogNormal;
                case 'b':
                    return DistributionType.Bounded;
                default:
                    throw new ArgumentException("Unable to read distribution type in data file.");
            }
        }
        private static PlotableProperty PropertyCharInv(string propertyChar)
        {
            switch (propertyChar)
            {
                case "c":
                    return PlotableProperty.Cohesion;
                case "phi":
                    return PlotableProperty.FrictionAngle;
                case "psi":
                    return PlotableProperty.DilationAngle;
                case "e":
                    return PlotableProperty.ElasticModulus;
                case "v":
                    return PlotableProperty.PoissonsRatio;
                default:
                    throw new ArgumentException("Invalid property character read.");
            }
        }
        private static bool TFInverse(string txt)
        {
            if (txt.Contains('t'))
            {
                return true;
            }
            else if (txt.Contains('f'))
            {
                return false;
            }
            else
            {
                throw new ArgumentException("Invalid true/false character read.");
            }
        }
    }
}
