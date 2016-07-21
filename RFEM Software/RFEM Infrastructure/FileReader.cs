using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEM_Infrastructure
{
    public static class FileReader
    {
        public static ISimModel Read(Program type, string filePath)
        {
            switch (type)
            {
                case Program.RBear2D:
                    return ReadRBearFile(filePath);
                case Program.RDam2D:
                    return ReadRDamFile(filePath);
                case Program.REarth2D:
                    return ReadREarth2DFile(filePath);
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
                formData.NumberOfRealizations = int.Parse(Line.Substring(LHSLength).Trim().Split(' ')[0]);

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
            List<int?> Nodes = new List<int?>();

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
                if (Line.Length <= LHSLength)
                    LineFragments = new string[0];
                else
                    LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                
                for(int i = 1; i <= 6; i++)
                {
                    if(i<= LineFragments.Length)
                    {
                        formData.NodesForGradientOutput[i - 1] = int.Parse((LineFragments[i - 1]));
                    }
                    else
                    {
                        formData.NodesForGradientOutput[i - 1] = null;
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
        private static REarth2D ReadREarth2DFile(string filePath)
        {
            var formData = new REarth2D();
            string Line;
            string[] LineFragments;
            int LHSLength = 50;

            using (var reader = new System.IO.StreamReader(filePath))
            {
                formData.JobTitle = reader.ReadLine();
                formData.BaseName = System.IO.Path.GetFileNameWithoutExtension(filePath);

                formData.EchoInputDataToOutputFile = reader.ReadLine().Substring(LHSLength).ToBool();

                reader.ReadLine();

                formData.WriteDebugDataToOutputFile = reader.ReadLine().Substring(LHSLength).ToBool();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.PlotFirstRandomField = LineFragments[0].ToBool();
                formData.FirstRFPropertyToPlot = LineFragments[2].PropertyCharInv();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.ProducePSPlotOfFirstFEM = LineFragments[0].ToBool();

                formData.StoreWallReactionSamples = reader.ReadLine().Substring(LHSLength).ToBool();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.NElementsInXDir = int.Parse(LineFragments[0]);
                formData.NElementsInYDir = int.Parse(LineFragments[1]);

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.ElementSizeInXDir = double.Parse(LineFragments[0]);
                formData.ElementSizeInYDir = double.Parse(LineFragments[1]);

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.WallExtension = int.Parse(LineFragments[0]);
                formData.RoughWallSurface = LineFragments[1].ToBool();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.DisplacementIncrement = double.Parse(LineFragments[0]);
                formData.PlasticTol = double.Parse(LineFragments[1]);
                formData.StressTol = double.Parse(LineFragments[2]);

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.MaxNumSteps = int.Parse(LineFragments[0]);
                formData.MaxNumIterations = int.Parse(LineFragments[1]);

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.Cohesion.Mean = double.Parse(LineFragments[0]);
                formData.Cohesion.StdDev = double.Parse(LineFragments[1]);
                formData.Cohesion.DistributionType = LineFragments[2].REarthDistCharInv();
                if (LineFragments.Length == 3)
                {
                    
                }
                else if(LineFragments.Length == 6)
                {
                    formData.Cohesion.Intercept = double.Parse(LineFragments[3]);
                    formData.Cohesion.Slope = double.Parse(LineFragments[4]);
                    formData.Cohesion.PhiFunc = LineFragments[5].PhiCharInv();
                }
                else if(LineFragments.Length == 7)
                {
                    formData.Cohesion.LowerBound = double.Parse(LineFragments[3]);
                    formData.Cohesion.UpperBound = double.Parse(LineFragments[4]);
                    formData.Cohesion.Location = double.Parse(LineFragments[5]);
                    formData.Cohesion.Scale = double.Parse(LineFragments[6]);
                }
                else
                {
                    throw new FormatException("Unable to read distribution data from data file.");
                }

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');

                //Friction angle distribution prefixed by t when the rv is tan(phi)
                if (LineFragments[2].ToCharArray()[0] == 't')
                {
                    formData.FrictionAngleType = FrictionAngle.TanPhi;
                    LineFragments[2] = LineFragments[2].Substring(1);
                }

                formData.FrictionAngle.Mean = double.Parse(LineFragments[0]);
                formData.FrictionAngle.StandardDev = double.Parse(LineFragments[1]);
                formData.FrictionAngle.Type = LineFragments[2].DistCharInv();
                

                if (LineFragments.Length == 3)
                {

                }
                else if (LineFragments.Length == 7)
                {
                    formData.FrictionAngle.LowerBound = double.Parse(LineFragments[3]);
                    formData.FrictionAngle.UpperBound = double.Parse(LineFragments[4]);
                    formData.FrictionAngle.Location = double.Parse(LineFragments[5]);
                    formData.FrictionAngle.Scale = double.Parse(LineFragments[6]);
                }
                else
                {
                    throw new FormatException("Unable to read distribution data from data file.");
                }

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.DilationAngle.Mean = double.Parse(LineFragments[0]);
                formData.DilationAngle.StdDev = double.Parse(LineFragments[1]);
                formData.DilationAngle.DistributionType = LineFragments[2].REarthDistCharInv();
                if (LineFragments.Length == 3)
                {

                }
                else if (LineFragments.Length == 6)
                {
                    formData.DilationAngle.Intercept = double.Parse(LineFragments[3]);
                    formData.DilationAngle.Slope = double.Parse(LineFragments[4]);
                    formData.DilationAngle.PhiFunc = LineFragments[5].PhiCharInv();
                }
                else if (LineFragments.Length == 7)
                {
                    formData.DilationAngle.LowerBound = double.Parse(LineFragments[3]);
                    formData.DilationAngle.UpperBound = double.Parse(LineFragments[4]);
                    formData.DilationAngle.Location = double.Parse(LineFragments[5]);
                    formData.DilationAngle.Scale = double.Parse(LineFragments[6]);
                }
                else
                {
                    throw new FormatException("Unable to read distribution data from data file.");
                }

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.ElasticModulus.Mean = double.Parse(LineFragments[0]);
                formData.ElasticModulus.StdDev = double.Parse(LineFragments[1]);
                formData.ElasticModulus.DistributionType = LineFragments[2].REarthDistCharInv();
                if (LineFragments.Length == 3)
                {

                }
                else if (LineFragments.Length == 6)
                {
                    formData.ElasticModulus.Intercept = double.Parse(LineFragments[3]);
                    formData.ElasticModulus.Slope = double.Parse(LineFragments[4]);
                    formData.ElasticModulus.PhiFunc = LineFragments[5].PhiCharInv();
                }
                else if (LineFragments.Length == 7)
                {
                    formData.ElasticModulus.LowerBound = double.Parse(LineFragments[3]);
                    formData.ElasticModulus.UpperBound = double.Parse(LineFragments[4]);
                    formData.ElasticModulus.Location = double.Parse(LineFragments[5]);
                    formData.ElasticModulus.Scale = double.Parse(LineFragments[6]);
                }
                else
                {
                    throw new FormatException("Unable to read distribution data from data file.");
                }



                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.PoissonsRatio.Mean = double.Parse(LineFragments[0]);
                formData.PoissonsRatio.StdDev = double.Parse(LineFragments[1]);
                formData.PoissonsRatio.DistributionType = LineFragments[2].REarthDistCharInv();
                if (LineFragments.Length == 3)
                {

                }
                else if (LineFragments.Length == 6)
                {
                    formData.PoissonsRatio.Intercept = double.Parse(LineFragments[3]);
                    formData.PoissonsRatio.Slope = double.Parse(LineFragments[4]);
                    formData.PoissonsRatio.PhiFunc = LineFragments[5].PhiCharInv();
                }
                else if (LineFragments.Length == 7)
                {
                    formData.PoissonsRatio.LowerBound = double.Parse(LineFragments[3]);
                    formData.PoissonsRatio.UpperBound = double.Parse(LineFragments[4]);
                    formData.PoissonsRatio.Location = double.Parse(LineFragments[5]);
                    formData.PoissonsRatio.Scale = double.Parse(LineFragments[6]);
                }
                else
                {
                    throw new FormatException("Unable to read distribution data from data file.");
                }


                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.UnitWeight.Mean = double.Parse(LineFragments[0]);
                formData.UnitWeight.StdDev = double.Parse(LineFragments[1]);
                formData.UnitWeight.DistributionType = LineFragments[2].REarthDistCharInv();
                if (LineFragments.Length == 3)
                {

                }
                else if (LineFragments.Length == 6)
                {
                    formData.UnitWeight.Intercept = double.Parse(LineFragments[3]);
                    formData.UnitWeight.Slope = double.Parse(LineFragments[4]);
                    formData.UnitWeight.PhiFunc = LineFragments[5].PhiCharInv();
                }
                else if (LineFragments.Length == 7)
                {
                    formData.UnitWeight.LowerBound = double.Parse(LineFragments[3]);
                    formData.UnitWeight.UpperBound = double.Parse(LineFragments[4]);
                    formData.UnitWeight.Location = double.Parse(LineFragments[5]);
                    formData.UnitWeight.Scale = double.Parse(LineFragments[6]);
                }
                else
                {
                    throw new FormatException("Unable to read distribution data from data file.");
                }



                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.PressureCoefficient.Mean = double.Parse(LineFragments[0]);
                formData.PressureCoefficient.StdDev = double.Parse(LineFragments[1]);
                formData.PressureCoefficient.DistributionType = LineFragments[2].REarthDistCharInv();
                if (LineFragments.Length == 3)
                {

                }
                else if (LineFragments.Length == 6)
                {
                    formData.PressureCoefficient.Intercept = double.Parse(LineFragments[3]);
                    formData.PressureCoefficient.Slope = double.Parse(LineFragments[4]);
                    formData.PressureCoefficient.PhiFunc = LineFragments[5].PhiCharInv();
                }
                else if (LineFragments.Length == 7)
                {
                    formData.PressureCoefficient.LowerBound = double.Parse(LineFragments[3]);
                    formData.PressureCoefficient.UpperBound = double.Parse(LineFragments[4]);
                    formData.PressureCoefficient.Location = double.Parse(LineFragments[5]);
                    formData.PressureCoefficient.Scale = double.Parse(LineFragments[6]);
                }
                else
                {
                    throw new FormatException("Unable to read distribution data from data file.");
                }

                formData.NumberOfRealizations = int.Parse(reader.ReadLine().Substring(LHSLength).Trim());
                formData.GeneratorSeed = int.Parse(reader.ReadLine().Substring(LHSLength).Trim());

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.CorrelationLengthInXDir = int.Parse(LineFragments[0]);
                formData.CorrelationLengthInYDir = int.Parse(LineFragments[1]);

                formData.CovFunction = CovFuncCharInv(reader.ReadLine().Substring(LHSLength).Trim());

                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();

                Line = reader.ReadLine();
                while (Line.Trim().Length > 0)
                {
                    LineFragments = Line.Split(' ').Where(p => p != "").ToArray();

                    int i = (int)PropertyCharInv(LineFragments[0]);
                    int j = (int)PropertyCharInv(LineFragments[1]);

                    formData.CorrelationMatrix[i, j] = double.Parse(LineFragments[2]);
                    formData.CorrelationMatrix[j, i] = double.Parse(LineFragments[2]);

                    Line = reader.ReadLine();
                }

                formData.ShowMeshOnDisplacedMeshPlot = reader.ReadLine().Substring(LHSLength).Trim().ToBool();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                formData.ShowRandomFieldOnDisplacedMesh = LineFragments[0].Trim().ToBool();
                formData.ShowLogRandomField = LineFragments[1].Trim().ToBool();
                formData.DisplacedMeshPropertyToPlot = LineFragments[2].PropertyCharInv();

                formData.DisplacedMeshWidth = double.Parse(reader.ReadLine().Substring(LHSLength).Trim());

                int NumSamples = int.Parse(reader.ReadLine().Substring(LHSLength).Trim());

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');

                for (int k = 1; k <= NumSamples; k++)
                {

                    var sample = new SampleLocation()
                    {
                        XCoordinate = (int?)int.Parse(LineFragments[2 * k - 2]),
                        YCoordinate = int.Parse(LineFragments[2 * k - 1])
                    };

                    formData.SampleLocations[k - 1] = sample;
                    

                }
                

                formData.OutputSampledSoilProperties = reader.ReadLine().Substring(LHSLength).Trim().ToBool();
            }

            return formData;

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
                    return DistributionType.Lognormal;
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
