using RFEMSoftware.Simulation.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEMSoftware.Simulation.Infrastructure.Persistence
{
    public static class FileReader
    {
        internal static ISimModel Read(Program type, string filePath)
        {
            switch (type)
            {
                case Program.RBear2D:
                    return ReadRBearFile(filePath);
                case Program.RDam2D:
                    return ReadRDamFile(filePath);
                case Program.REarth2D:
                    return ReadREarth2DFile(filePath);
                case Program.RFlow2D:
                    return ReadRFlow2DFile(filePath);
                case Program.RFlow3D:
                    return ReadRFlow3DFile(filePath);
                case Program.RPill2D:
                    return ReadRPill2DFile(filePath);
                case Program.RPill3D:
                    return ReadRPill3DFile(filePath);
                case Program.RSetl2D:
                    return ReadRSetl2DFile(filePath);
                case Program.RSetl3D:
                    return ReadRSetl3DFile(filePath);
                case Program.RSlope2D:
                    return ReadRSlope2DFile(filePath);
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
                formData.CohesionDist.StandardDeviation = LineFragments[1].ToNullableDouble();
                formData.CohesionDist.DistributionType = DistributionCharInv(LineFragments[2]);

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
                formData.FrictionAngleDist.StandardDeviation = LineFragments[1].ToNullableDouble();

                //Friction angle distribution prefixed by t when the rv is tan(phi)
                if (LineFragments[2].ToCharArray()[0] == 't')
                {
                    formData.FrictionAngleType = FrictionAngle.TanPhi;
                    LineFragments[2] = LineFragments[2].Substring(1);
                }

                formData.FrictionAngleDist.DistributionType = DistributionCharInv(LineFragments[2]);

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
                formData.DilationAngleDist.StandardDeviation = LineFragments[1].ToNullableDouble();
                formData.DilationAngleDist.DistributionType = DistributionCharInv(LineFragments[2]);

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
                formData.ElasticModulusDist.StandardDeviation = LineFragments[1].ToNullableDouble();
                formData.ElasticModulusDist.DistributionType = DistributionCharInv(LineFragments[2]);

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
                formData.PoissonsRatioDist.StandardDeviation = LineFragments[1].ToNullableDouble();
                formData.PoissonsRatioDist.DistributionType = DistributionCharInv(LineFragments[2]);

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
                formData.FlownetWidth = double.Parse(Line.Substring(LHSLength));

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
                formData.FirstRFPropertyToPlot = LineFragments[2].REarthPropertyCharInv();

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
                formData.Cohesion.StandardDeviation = double.Parse(LineFragments[1]);
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
                formData.FrictionAngle.StandardDeviation = double.Parse(LineFragments[1]);
                formData.FrictionAngle.DistributionType = LineFragments[2].DistCharInv();
                

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
                formData.DilationAngle.StandardDeviation = double.Parse(LineFragments[1]);
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
                formData.ElasticModulus.StandardDeviation = double.Parse(LineFragments[1]);
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
                formData.PoissonsRatio.StandardDeviation = double.Parse(LineFragments[1]);
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
                formData.UnitWeight.StandardDeviation = double.Parse(LineFragments[1]);
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
                formData.PressureCoefficient.StandardDeviation = double.Parse(LineFragments[1]);
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
                formData.DisplacedMeshPropertyToPlot = LineFragments[2].REarthPropertyCharInv();

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

        private static RFlow2D ReadRFlow2DFile(string filePath)
        {
            var model = new RFlow2D();
            string Line;
            string[] LineFragments;
            int LHSLength = 50;

            using (var reader = new System.IO.StreamReader(filePath))
            {
                model.JobTitle = reader.ReadLine();
                model.BaseName = System.IO.Path.GetFileNameWithoutExtension(filePath);

                model.EchoInputDataToOutputFile = reader.ReadLine().Substring(LHSLength).ToBool();
                reader.ReadLine();
                model.OutputDebugInfo = reader.ReadLine().Substring(LHSLength).ToBool();
                model.ProduceDisplayOfFirstLogConductivityField = reader.ReadLine().Substring(LHSLength).ToBool();
                model.ProducePSPlotOfFirstFlownet = reader.ReadLine().Substring(LHSLength).ToBool();
                model.ProduceDisplayOfTotalHeadMeanAndStdDev = reader.ReadLine().Substring(LHSLength).ToBool();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.OutputFlowRateExitGradientUpliftForce = LineFragments[0].ToBool();
                model.OutputDetailedExitGradientInfo = LineFragments[1].ToBool();

                model.OutputBlockHydraulicConductivities = reader.ReadLine().Substring(LHSLength).ToBool();
                model.OutputArithmeticGeometricHarmonicHydraulicConductivityAvgs = reader.ReadLine().Substring(LHSLength).ToBool();
                model.GenerateUniformConductivityField = reader.ReadLine().Substring(LHSLength).ToBool();

                model.NumberOfWalls = int.Parse(reader.ReadLine().Substring(LHSLength));

                if(model.NumberOfWalls == 0)
                {
                    model.NElementsInXDir = int.Parse(reader.ReadLine().Substring(LHSLength));
                    model.NElementsInYDir = int.Parse(reader.ReadLine().Substring(LHSLength));
                }
                else if(model.NumberOfWalls == 1)
                {
                    Line = reader.ReadLine();
                    LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                    model.NElementsLeftOfWall = int.Parse(LineFragments[0]);
                    model.NElementsRightOfWall = int.Parse(LineFragments[1]);

                    Line = reader.ReadLine();
                    LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                    model.NElementsInYDir = int.Parse(LineFragments[0]);
                    model.DepthOfWall = int.Parse(LineFragments[1]);

                }
                else if(model.NumberOfWalls == 2)
                {
                    Line = reader.ReadLine();
                    LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                    model.NElementsLeftOfLeftWall = int.Parse(LineFragments[0]);
                    model.NElementsBetweenWall = int.Parse(LineFragments[1]);
                    model.NElementsToTheRightOfRightWall = int.Parse(LineFragments[2]);

                    Line = reader.ReadLine();
                    LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                    model.NElementsInYDir = int.Parse(LineFragments[0]);
                    model.DepthOfLeftWall = int.Parse(LineFragments[1]);
                    model.DepthOfRightWall = int.Parse(LineFragments[2]);
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.ElementDimensionHorrizontal = double.Parse(LineFragments[0]);
                model.ElementDimensionVertical = double.Parse(LineFragments[1]);

                model.NumberOfRealizations = int.Parse(reader.ReadLine().Substring(LHSLength));
                model.GeneratorSeed = int.Parse(reader.ReadLine().Substring(LHSLength));

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.CorrelationLengthInXDir = (int)double.Parse(LineFragments[0]);
                model.CorrelationLengthInYDir = (int)double.Parse(LineFragments[1]);

                model.HydraulicConductivityMean = double.Parse(reader.ReadLine().Substring(LHSLength));
                model.HydraulicConductivityStdDev = double.Parse(reader.ReadLine().Substring(LHSLength));
                model.CovFunc = CovFuncCharInv(reader.ReadLine().Substring(LHSLength).Trim());
                model.NContoursForMeanTotalHead = int.Parse(reader.ReadLine().Substring(LHSLength));
                model.NContoursForStdDevTotalHead = int.Parse(reader.ReadLine().Substring(LHSLength));
                model.NEquipotentialDrops = int.Parse(reader.ReadLine().Substring(LHSLength));
                model.ShowLogConductivityFieldOnFlownet = reader.ReadLine().Substring(LHSLength).ToBool();
                model.ShowSoilMassDimensionsOnFlownet = reader.ReadLine().Substring(LHSLength).ToBool();
                model.ShowTitlesOnFlownet = reader.ReadLine().Substring(LHSLength).ToBool();
                model.FlownetWidth = double.Parse(reader.ReadLine().Substring(LHSLength));

            }



                return model;
        }
        public static RFlow3D ReadRFlow3DFile(string filePath)
        {
            var model = new RFlow3D();
            string Line;
            string[] LineFragments;
            int LHSLength = 50;

            using (var reader = new System.IO.StreamReader(filePath))
            {
                model.JobTitle = reader.ReadLine();
                model.BaseName = System.IO.Path.GetFileNameWithoutExtension(filePath);

                model.EchoInputDataToOutputFile = reader.ReadLine().Substring(LHSLength).ToBool();
                reader.ReadLine();
                model.OutputDebugInfo = reader.ReadLine().Substring(LHSLength).ToBool();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.OutputFlowRateExitGradientUpliftForce = LineFragments[0].ToBool();

                model.OutputBlockHydraulicConductivities = reader.ReadLine().Substring(LHSLength).ToBool();
                model.OutputArithmeticGeometricHarmonicHydraulicConductivityAvgs = reader.ReadLine().Substring(LHSLength).ToBool();
                model.GenerateUniformConductivityField = reader.ReadLine().Substring(LHSLength).ToBool();

                model.NumberOfWalls = int.Parse(reader.ReadLine().Substring(LHSLength));

                if (model.NumberOfWalls == 0)
                {
                    model.NElementsInXDir = int.Parse(reader.ReadLine().Substring(LHSLength));
                    model.NElementsInYDir = int.Parse(reader.ReadLine().Substring(LHSLength));
                }
                else if (model.NumberOfWalls == 1)
                {
                    Line = reader.ReadLine();
                    LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                    model.NElementsLeftOfWall = int.Parse(LineFragments[0]);
                    model.NElementsRightOfWall = int.Parse(LineFragments[1]);

                    Line = reader.ReadLine();
                    LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                    model.NElementsInYDir = int.Parse(LineFragments[0]);
                    model.DepthOfWall = int.Parse(LineFragments[1]);

                }
                else if (model.NumberOfWalls == 2)
                {
                    Line = reader.ReadLine();
                    LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                    model.NElementsLeftOfLeftWall = int.Parse(LineFragments[0]);
                    model.NElementsBetweenWalls = int.Parse(LineFragments[1]);
                    model.NElementsRightOfRightWall = int.Parse(LineFragments[2]);

                    Line = reader.ReadLine();
                    LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                    model.NElementsInYDir = int.Parse(LineFragments[0]);
                    model.DepthOfLeftWall = int.Parse(LineFragments[1]);
                    model.DepthOfRightWall = int.Parse(LineFragments[2]);
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }

                model.NElementsInZDir = int.Parse(reader.ReadLine().Substring(LHSLength));

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.ElementDimensionXDir = double.Parse(LineFragments[0]);
                model.ElementDimensionYDir = double.Parse(LineFragments[1]);
                model.ElementDimensionZDir = double.Parse(LineFragments[2]);

                model.NumberOfRealizations = int.Parse(reader.ReadLine().Substring(LHSLength));
                model.GeneratorSeed = int.Parse(reader.ReadLine().Substring(LHSLength));
                model.HydraulicConductivityMean = double.Parse(reader.ReadLine().Substring(LHSLength));
                model.HydraulicConductivityStdDev = double.Parse(reader.ReadLine().Substring(LHSLength));

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.CorrelationLengthXDir = (int)double.Parse(LineFragments[0]);
                model.CorrelationLengthYDir = (int)double.Parse(LineFragments[1]);
                model.CorrelationLenghtZDir = (int)double.Parse(LineFragments[2]);

                model.CovFunc = CovFuncCharInv3D(reader.ReadLine().Substring(LHSLength));


            }
            
                return model;
        }
        public static RPill2D ReadRPill2DFile(string filePath)
        {
            var model = new RPill2D();
            string Line;
            string[] LineFragments;
            int LHSLength = 50;

            using (var reader = new System.IO.StreamReader(filePath))
            {
                model.JobTitle = reader.ReadLine();
                model.BaseName = System.IO.Path.GetFileNameWithoutExtension(filePath);

                model.EchoInputDataToOutputFile = reader.ReadLine().Substring(LHSLength).ToBool();
                reader.ReadLine();
                model.OutputDebugInfo = reader.ReadLine().Substring(LHSLength).ToBool();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.PlotFirstRF = LineFragments[0].ToBool();
                model.FirstRFPropertyToPlot = LineFragments[2].PropertyCharInv();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.ProducePSPlotOfFirstDisplacedMesh = LineFragments[0].ToBool();

                model.NormalizePillarCapacitySamples = reader.ReadLine().Substring(LHSLength).ToBool();
                model.OutputPillarCapacitySamples = reader.ReadLine().Substring(LHSLength).ToBool();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.NElementsInXDir = int.Parse(LineFragments[0]);
                model.NElementsInYDir = int.Parse(LineFragments[1]);

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.ElementSizeInXDir = double.Parse(LineFragments[0]);
                model.ElementSizeInYDir = double.Parse(LineFragments[1]);

                model.RoughLoadingCondition = reader.ReadLine().Substring(LHSLength).ToBool();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.DisplacementInc = double.Parse(LineFragments[0]);
                model.PlasticTol = double.Parse(LineFragments[1]);
                model.BearingTol = double.Parse(LineFragments[2]);

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.MaxNumSteps = int.Parse(LineFragments[0]);
                model.MaxNumIterations = int.Parse(LineFragments[1]);

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                if(LineFragments.Count() == 7)
                {
                    model.Cohesion.Mean = double.Parse(LineFragments[0]);
                    model.Cohesion.StandardDeviation = double.Parse(LineFragments[1]);
                    model.Cohesion.DistributionType = LineFragments[2].DistCharInv();
                    model.Cohesion.LowerBound = double.Parse(LineFragments[3]);
                    model.Cohesion.UpperBound = double.Parse(LineFragments[4]);
                    model.Cohesion.Location = double.Parse(LineFragments[5]);
                    model.Cohesion.Scale = double.Parse(LineFragments[6]);
                }
                else if(LineFragments.Count() == 3)
                {
                    model.Cohesion.Mean = double.Parse(LineFragments[0]);
                    model.Cohesion.StandardDeviation = double.Parse(LineFragments[1]);
                    model.Cohesion.DistributionType = LineFragments[2].DistCharInv();
                }
                else
                {
                    throw new FormatException();
                }

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');

                //Friction angle distribution prefixed by t when the rv is tan(phi)
                if (LineFragments[2].ToCharArray()[0] == 't')
                {
                    model.FrictionAngleType = FrictionAngle.TanPhi;
                    LineFragments[2] = LineFragments[2].Substring(1);
                }

                if (LineFragments.Count() == 7)
                {
                    model.FrictionAngle.Mean = double.Parse(LineFragments[0]);
                    model.FrictionAngle.StandardDeviation = double.Parse(LineFragments[1]);
                    model.FrictionAngle.DistributionType = LineFragments[2].DistCharInv();
                    model.FrictionAngle.LowerBound = double.Parse(LineFragments[3]);
                    model.FrictionAngle.UpperBound = double.Parse(LineFragments[4]);
                    model.FrictionAngle.Location = double.Parse(LineFragments[5]);
                    model.FrictionAngle.Scale = double.Parse(LineFragments[6]);
                }
                else if (LineFragments.Count() == 3)
                {
                    model.FrictionAngle.Mean = double.Parse(LineFragments[0]);
                    model.FrictionAngle.StandardDeviation = double.Parse(LineFragments[1]);
                    model.FrictionAngle.DistributionType = LineFragments[2].DistCharInv();
                }
                else
                {
                    throw new FormatException();
                }


                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                if (LineFragments.Count() == 7)
                {
                    model.DilationAngle.Mean = double.Parse(LineFragments[0]);
                    model.DilationAngle.StandardDeviation = double.Parse(LineFragments[1]);
                    model.DilationAngle.DistributionType = LineFragments[2].DistCharInv();
                    model.DilationAngle.LowerBound = double.Parse(LineFragments[3]);
                    model.DilationAngle.UpperBound = double.Parse(LineFragments[4]);
                    model.DilationAngle.Location = double.Parse(LineFragments[5]);
                    model.DilationAngle.Scale = double.Parse(LineFragments[6]);
                }
                else if (LineFragments.Count() == 3)
                {
                    model.DilationAngle.Mean = double.Parse(LineFragments[0]);
                    model.DilationAngle.StandardDeviation = double.Parse(LineFragments[1]);
                    model.DilationAngle.DistributionType = LineFragments[2].DistCharInv();
                }
                else
                {
                    throw new FormatException();
                }

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                if (LineFragments.Count() == 7)
                {
                    model.ElasticModulus.Mean = double.Parse(LineFragments[0]);
                    model.ElasticModulus.StandardDeviation = double.Parse(LineFragments[1]);
                    model.ElasticModulus.DistributionType = LineFragments[2].DistCharInv();
                    model.ElasticModulus.LowerBound = double.Parse(LineFragments[3]);
                    model.ElasticModulus.UpperBound = double.Parse(LineFragments[4]);
                    model.ElasticModulus.Location = double.Parse(LineFragments[5]);
                    model.ElasticModulus.Scale = double.Parse(LineFragments[6]);
                }
                else if (LineFragments.Count() == 3)
                {
                    model.ElasticModulus.Mean = double.Parse(LineFragments[0]);
                    model.ElasticModulus.StandardDeviation = double.Parse(LineFragments[1]);
                    model.ElasticModulus.DistributionType = LineFragments[2].DistCharInv();
                }
                else
                {
                    throw new FormatException();
                }

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                if (LineFragments.Count() == 7)
                {
                    model.PoissonsRatio.Mean = double.Parse(LineFragments[0]);
                    model.PoissonsRatio.StandardDeviation = double.Parse(LineFragments[1]);
                    model.PoissonsRatio.DistributionType = LineFragments[2].DistCharInv();
                    model.PoissonsRatio.LowerBound = double.Parse(LineFragments[3]);
                    model.PoissonsRatio.UpperBound = double.Parse(LineFragments[4]);
                    model.PoissonsRatio.Location = double.Parse(LineFragments[5]);
                    model.PoissonsRatio.Scale = double.Parse(LineFragments[6]);
                }
                else if (LineFragments.Count() == 3)
                {
                    model.PoissonsRatio.Mean = double.Parse(LineFragments[0]);
                    model.PoissonsRatio.StandardDeviation = double.Parse(LineFragments[1]);
                    model.PoissonsRatio.DistributionType = LineFragments[2].DistCharInv();
                }
                else
                {
                    throw new FormatException();
                }


                model.NumberOfRealizations = int.Parse(reader.ReadLine().Substring(LHSLength));
                model.GeneratorSeed = int.Parse(reader.ReadLine().Substring(LHSLength));

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.CorrelationLengthInXDir = (int)double.Parse(LineFragments[0]);
                model.CorrelationLengthInYDir = (int)double.Parse(LineFragments[1]);

                model.CovFunc = CovFuncCharInv(reader.ReadLine().Substring(LHSLength).Trim());

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

                    model.CorrelationMatrix[i, j] = double.Parse(LineFragments[2]);
                    model.CorrelationMatrix[j, i] = double.Parse(LineFragments[2]);

                    Line = reader.ReadLine();
                }

                model.ShowMeshOnDisplacedMeshPlot = reader.ReadLine().Substring(LHSLength).ToBool();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.ShowRFOnDisplacedMeshPlot = LineFragments[0].ToBool();
                model.ShowLogRF = LineFragments[1].ToBool();
                model.DisplacedMeshPropertyToPlot = LineFragments[2].PropertyCharInv();

                model.DisplacedMeshPlotWidth = double.Parse(reader.ReadLine().Substring(LHSLength));

            }

            return model;
        }
        public static RPill3D ReadRPill3DFile(string filePath)
        {
            var model = new RPill3D();
            string Line;
            string[] LineFragments;
            int LHSLength = 50;

            using (var reader = new System.IO.StreamReader(filePath))
            {

                model.JobTitle = reader.ReadLine();
                model.BaseName = System.IO.Path.GetFileNameWithoutExtension(filePath);

                model.EchoInputDataToOutputFile = reader.ReadLine().Substring(LHSLength).ToBool();
                reader.ReadLine();
                model.OutputDebugInfo = reader.ReadLine().Substring(LHSLength).ToBool();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.PlotFirstRF = LineFragments[0].ToBool();
                model.FirstRFPropertyToPlot = LineFragments[2].PropertyCharInv();
                
                if(int.Parse(LineFragments[3]) != 0)
                {
                    model.FirstRFNodeIndexToPlot = int.Parse(LineFragments[3]);
                    model.FirstRFPerpindicularToThisAxis = Axis.XAxis;
                }
                else if(int.Parse(LineFragments[4]) != 0)
                {
                    model.FirstRFNodeIndexToPlot = int.Parse(LineFragments[4]);
                    model.FirstRFPerpindicularToThisAxis = Axis.YAxis;
                }
                else if(int.Parse(LineFragments[5]) != 0)
                {
                    model.FirstRFNodeIndexToPlot = int.Parse(LineFragments[5]);
                    model.FirstRFPerpindicularToThisAxis = Axis.ZAxis;
                }

                model.NormalizePillarCapacitySamples = reader.ReadLine().Substring(LHSLength).ToBool();
                model.OutputCapacitySamples = reader.ReadLine().Substring(LHSLength).ToBool();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.NElementsInXDir = int.Parse(LineFragments[0]);
                model.NElementsInYDir = int.Parse(LineFragments[1]);
                model.NElementsInZDir = int.Parse(LineFragments[2]);

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.ElementSizeInXDir = double.Parse(LineFragments[0]);
                model.ElementSizeInYDir = double.Parse(LineFragments[1]);
                model.ElementSizeInZDir = double.Parse(LineFragments[2]);

                Line = reader.ReadLine();

                if(int.Parse(Line.Substring(LHSLength).Trim()) == 8)
                {
                    model.ElementType = RPill3DElementType.EightNode;
                }
                else if(int.Parse(Line.Substring(LHSLength).Trim()) == 20)
                {
                    model.ElementType = RPill3DElementType.TwentyNode;
                }

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.DisplacementInc = double.Parse(LineFragments[0]);
                model.PlasticTol  = double.Parse(LineFragments[1]);
                model.BearingTol = double.Parse(LineFragments[2]);

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.MaxNumSteps = int.Parse(LineFragments[0]);
                model.MaxNumIterations = int.Parse(LineFragments[1]);

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                if (LineFragments.Count() == 7)
                {
                    model.Cohesion.Mean = double.Parse(LineFragments[0]);
                    model.Cohesion.StandardDeviation = double.Parse(LineFragments[1]);
                    model.Cohesion.DistributionType = LineFragments[2].DistCharInv();
                    model.Cohesion.LowerBound = double.Parse(LineFragments[3]);
                    model.Cohesion.UpperBound = double.Parse(LineFragments[4]);
                    model.Cohesion.Location = double.Parse(LineFragments[5]);
                    model.Cohesion.Scale = double.Parse(LineFragments[6]);
                }
                else if (LineFragments.Count() == 3)
                {
                    model.Cohesion.Mean = double.Parse(LineFragments[0]);
                    model.Cohesion.StandardDeviation = double.Parse(LineFragments[1]);
                    model.Cohesion.DistributionType = LineFragments[2].DistCharInv();
                }
                else
                {
                    throw new FormatException();
                }

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');

                //Friction angle distribution prefixed by t when the rv is tan(phi)
                if (LineFragments[2].ToCharArray()[0] == 't')
                {
                    model.FrictionAngleType = FrictionAngle.TanPhi;
                    LineFragments[2] = LineFragments[2].Substring(1);
                }

                if (LineFragments.Count() == 7)
                {
                    model.FrictionAngle.Mean = double.Parse(LineFragments[0]);
                    model.FrictionAngle.StandardDeviation = double.Parse(LineFragments[1]);
                    model.FrictionAngle.DistributionType = LineFragments[2].DistCharInv();
                    model.FrictionAngle.LowerBound = double.Parse(LineFragments[3]);
                    model.FrictionAngle.UpperBound = double.Parse(LineFragments[4]);
                    model.FrictionAngle.Location = double.Parse(LineFragments[5]);
                    model.FrictionAngle.Scale = double.Parse(LineFragments[6]);
                }
                else if (LineFragments.Count() == 3)
                {
                    model.FrictionAngle.Mean = double.Parse(LineFragments[0]);
                    model.FrictionAngle.StandardDeviation = double.Parse(LineFragments[1]);
                    model.FrictionAngle.DistributionType = LineFragments[2].DistCharInv();
                }
                else
                {
                    throw new FormatException();
                }


                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                if (LineFragments.Count() == 7)
                {
                    model.DilationAngle.Mean = double.Parse(LineFragments[0]);
                    model.DilationAngle.StandardDeviation = double.Parse(LineFragments[1]);
                    model.DilationAngle.DistributionType = LineFragments[2].DistCharInv();
                    model.DilationAngle.LowerBound = double.Parse(LineFragments[3]);
                    model.DilationAngle.UpperBound = double.Parse(LineFragments[4]);
                    model.DilationAngle.Location = double.Parse(LineFragments[5]);
                    model.DilationAngle.Scale = double.Parse(LineFragments[6]);
                }
                else if (LineFragments.Count() == 3)
                {
                    model.DilationAngle.Mean = double.Parse(LineFragments[0]);
                    model.DilationAngle.StandardDeviation = double.Parse(LineFragments[1]);
                    model.DilationAngle.DistributionType = LineFragments[2].DistCharInv();
                }
                else
                {
                    throw new FormatException();
                }

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                if (LineFragments.Count() == 7)
                {
                    model.ElasticModulus.Mean = double.Parse(LineFragments[0]);
                    model.ElasticModulus.StandardDeviation = double.Parse(LineFragments[1]);
                    model.ElasticModulus.DistributionType = LineFragments[2].DistCharInv();
                    model.ElasticModulus.LowerBound = double.Parse(LineFragments[3]);
                    model.ElasticModulus.UpperBound = double.Parse(LineFragments[4]);
                    model.ElasticModulus.Location = double.Parse(LineFragments[5]);
                    model.ElasticModulus.Scale = double.Parse(LineFragments[6]);
                }
                else if (LineFragments.Count() == 3)
                {
                    model.ElasticModulus.Mean = double.Parse(LineFragments[0]);
                    model.ElasticModulus.StandardDeviation = double.Parse(LineFragments[1]);
                    model.ElasticModulus.DistributionType = LineFragments[2].DistCharInv();
                }
                else
                {
                    throw new FormatException();
                }

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                if (LineFragments.Count() == 7)
                {
                    model.PoissonsRatio.Mean = double.Parse(LineFragments[0]);
                    model.PoissonsRatio.StandardDeviation = double.Parse(LineFragments[1]);
                    model.PoissonsRatio.DistributionType = LineFragments[2].DistCharInv();
                    model.PoissonsRatio.LowerBound = double.Parse(LineFragments[3]);
                    model.PoissonsRatio.UpperBound = double.Parse(LineFragments[4]);
                    model.PoissonsRatio.Location = double.Parse(LineFragments[5]);
                    model.PoissonsRatio.Scale = double.Parse(LineFragments[6]);
                }
                else if (LineFragments.Count() == 3)
                {
                    model.PoissonsRatio.Mean = double.Parse(LineFragments[0]);
                    model.PoissonsRatio.StandardDeviation = double.Parse(LineFragments[1]);
                    model.PoissonsRatio.DistributionType = LineFragments[2].DistCharInv();
                }
                else
                {
                    throw new FormatException();
                }

                model.NumberOfRealizations = int.Parse(reader.ReadLine().Substring(LHSLength));
                model.GeneratorSeed = int.Parse(reader.ReadLine().Substring(LHSLength));

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.CorrelationLengthInXDir = (int)double.Parse(LineFragments[0]);
                model.CorrelationLengthInYDir = (int)double.Parse(LineFragments[1]);
                model.CorrelationLengthInZDir = (int)double.Parse(LineFragments[2]);

                model.CovFunc = CovFuncCharInv3D(reader.ReadLine().Substring(LHSLength).Trim());

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

                    model.CorrelationMatrix[i, j] = double.Parse(LineFragments[2]);
                    model.CorrelationMatrix[j, i] = double.Parse(LineFragments[2]);

                    Line = reader.ReadLine();
                }
            }

            return model;
        }
        public static RSetl2D ReadRSetl2DFile(string filePath)
        {
            var model = new RSetl2D();
            string Line;
            string[] LineFragments;
            int LHSLength = 50;

            using (var reader = new System.IO.StreamReader(filePath))
            {
                model.JobTitle = reader.ReadLine();
                model.BaseName = System.IO.Path.GetFileNameWithoutExtension(filePath);

                model.EchoInputDataToOutputFile = reader.ReadLine().Substring(LHSLength).ToBool();
                reader.ReadLine();
                model.OutputDebugInfo = reader.ReadLine().Substring(LHSLength).ToBool();

                model.ProduceDisplayOfFirstLogElasticModulusField = reader.ReadLine().Substring(LHSLength).ToBool();
                model.ProducePSPlotOfFirstDisplacedMesh = reader.ReadLine().Substring(LHSLength).ToBool();
                model.UseKGSelectiveReducedIntegration = reader.ReadLine().Substring(LHSLength).ToBool();
                model.OutputSettlementSamplesAndSummaryStats = reader.ReadLine().Substring(LHSLength).ToBool();
                model.OutputBlockModulusSamplesAndSummaryStats = reader.ReadLine().Substring(LHSLength).ToBool();
                model.OutputFieldAveragedModulusSamplesAndSummaryStats = reader.ReadLine().Substring(LHSLength).ToBool();
                model.GenerateUniformRandomFields = reader.ReadLine().Substring(LHSLength).ToBool();
                model.NElementsInXDir = int.Parse(reader.ReadLine().Substring(LHSLength));
                model.NElementsInYDir = int.Parse(reader.ReadLine().Substring(LHSLength));

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.ElementSizeInXDir = double.Parse(LineFragments[0]);
                model.ElementSizeInYDir = double.Parse(LineFragments[1]);

                model.NumberOfRealizations = int.Parse(reader.ReadLine().Substring(LHSLength));
                model.GeneratorSeed = int.Parse(reader.ReadLine().Substring(LHSLength));
                model.PoissonsRatio = double.Parse(reader.ReadLine().Substring(LHSLength));

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.ElasticModulusMean = double.Parse(LineFragments[0]);
                model.ElasticModulusStdDev = double.Parse(LineFragments[1]);

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.CorrelationLengthInXDir = (int)double.Parse(LineFragments[0]);
                model.CorrelationLengthInYDir = (int)double.Parse(LineFragments[1]);

                model.CovFunc = CovFuncCharInv(reader.ReadLine().Substring(LHSLength).Trim());

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');

                foreach(string s in LineFragments)
                {
                    int a;
                    if(int.TryParse(s, out a))
                    {
                        model.SettlementNodeList.Add(a);
                    }
                }

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');

                foreach(string s in LineFragments)
                {
                    var MetaLineFragments = s.Split('-');
                    int a, b;
                    if(int.TryParse(MetaLineFragments[0], out a) &
                        int.TryParse(MetaLineFragments[1], out b))
                    {
                        var t = new Tuple<int, int>(a, b);
                        model.DifferentialNodePairList.Add(t);
                    }
                }

                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();

                int n = int.Parse(reader.ReadLine().Substring(LHSLength));
                for(int i = 1; i<= n; i++)
                {
                    Line = reader.ReadLine();
                    LineFragments = Line.Substring(LHSLength).Trim().Split(' ');

                    var r = new RigidFootingLoad2D()
                    {
                        LeftNode = int.Parse(LineFragments[0]),
                        RightNode = int.Parse(LineFragments[1]),
                        Load = double.Parse(LineFragments[2]),
                        RoughInterface = int.Parse(LineFragments[3]) == 1 ? true : false
                    };

                    model.RigidFootingLoads.Add(r);
                }

                n = int.Parse(reader.ReadLine().Substring(LHSLength));
                for (int i = 1; i <= n; i++)
                {
                    Line = reader.ReadLine();
                    LineFragments = Line.Substring(LHSLength).Trim().Split(' ');

                    var u = new UniformDistributedLoad2D()
                    {
                        LeftNode = int.Parse(LineFragments[0]),
                        RightNode = int.Parse(LineFragments[1]),
                        Load = double.Parse(LineFragments[2])
                    };

                    model.UniformLoads.Add(u);
                }

                n = int.Parse(reader.ReadLine().Substring(LHSLength));
                for (int i = 1; i <= n; i++)
                {
                    Line = reader.ReadLine();
                    LineFragments = Line.Substring(LHSLength).Trim().Split(' ');

                    var l = new LineLoad2D()
                    {
                        Node = int.Parse(LineFragments[0]),
                        Load = double.Parse(LineFragments[1])
                    };
                }

                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();

                model.OverlayLogElasticModulusFieldOnDisplacedMesh = reader.ReadLine().Substring(LHSLength).ToBool();
                model.ShowProblemDimensionsOnDisplacedMesh = reader.ReadLine().Substring(LHSLength).ToBool();
                model.ShowTitlesOnDisplacedMesh = reader.ReadLine().Substring(LHSLength).ToBool();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.DisplacementMagInXDir = double.Parse(LineFragments[0]);
                model.DisplacementMagInYDir = double.Parse(LineFragments[1]);

                model.DisplacedMeshWidth = double.Parse(reader.ReadLine().Substring(LHSLength));

                
            }
            return model;
        }
        public static RSetl3D ReadRSetl3DFile(string filePath)
        {
            var model = new RSetl3D();
            string Line;
            string[] LineFragments;
            int LHSLength = 50;

            using (var reader = new System.IO.StreamReader(filePath))
            {
                model.JobTitle = reader.ReadLine();
                model.BaseName = System.IO.Path.GetFileNameWithoutExtension(filePath);

                model.EchoInputDataToOutputFile = reader.ReadLine().Substring(LHSLength).ToBool();
                reader.ReadLine();
                model.OutputDebugInfo = reader.ReadLine().Substring(LHSLength).ToBool();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');

                model.ProduceDisplayOfFirstLogElasticModulusField = LineFragments[0].ToBool();
                
                if(int.Parse(LineFragments[2]) != 0)
                {
                    model.LogElasticModulusFieldPerpindicularToThisAxis = Axis.XAxis;
                    model.LogElasticModulusFieldNodeIndex = int.Parse(LineFragments[2]);
                }
                else if(int.Parse(LineFragments[3]) != 0)
                {
                    model.LogElasticModulusFieldPerpindicularToThisAxis = Axis.YAxis;
                    model.LogElasticModulusFieldNodeIndex = int.Parse(LineFragments[3]);
                }
                else if(int.Parse(LineFragments[4]) != 0)
                {
                    model.LogElasticModulusFieldPerpindicularToThisAxis = Axis.ZAxis;
                    model.LogElasticModulusFieldNodeIndex = int.Parse(LineFragments[4]);
                }

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');

                model.ProducePSPlotOfDisplacedMesh = LineFragments[0].ToBool();

                if (int.Parse(LineFragments[2]) != 0)
                {
                    model.DisplacedMeshPlotPerpindicularToThisAxis = Axis.XAxis;
                    model.DisplacedMeshNodeIndex = int.Parse(LineFragments[2]);
                }
                else if (int.Parse(LineFragments[3]) != 0)
                {
                    model.DisplacedMeshPlotPerpindicularToThisAxis = Axis.YAxis;
                    model.DisplacedMeshNodeIndex = int.Parse(LineFragments[3]);
                }
                else if (int.Parse(LineFragments[4]) != 0)
                {
                    model.DisplacedMeshPlotPerpindicularToThisAxis = Axis.ZAxis;
                    model.DisplacedMeshNodeIndex = int.Parse(LineFragments[4]);
                }

                model.OutputSettlementSamplesAndSummaryStats = reader.ReadLine().Substring(LHSLength).ToBool();
                model.OutputBlockModulusSamplesAndSummaryStats = reader.ReadLine().Substring(LHSLength).ToBool();
                model.OutputFieldAveragedModulusSamplesAndSummaryStats = reader.ReadLine().Substring(LHSLength).ToBool();
                model.GenerateUniformRandomFields = reader.ReadLine().Substring(LHSLength).ToBool();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');

                model.NElementsInXidr = int.Parse(LineFragments[0]);
                model.NElementsInYDir = int.Parse(LineFragments[1]);
                model.NElementsInZDir = int.Parse(LineFragments[2]);

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');

                model.ElementSizeInXDir = double.Parse(LineFragments[0]);
                model.ElementSizeInYDir = double.Parse(LineFragments[1]);
                model.ElementSizeInZDir = double.Parse(LineFragments[2]);

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.MaxNumIterations = int.Parse(LineFragments[0]);
                model.ConvergenceTolerance = double.Parse(LineFragments[1]);

                model.NumberOfRealizations = int.Parse(reader.ReadLine().Substring(LHSLength));
                model.GeneratorSeed = int.Parse(reader.ReadLine().Substring(LHSLength));
                model.PoissonsRatio = double.Parse(reader.ReadLine().Substring(LHSLength));

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.ElasticModulusMean = double.Parse(LineFragments[0]);
                model.ElasticModulusStdDev = double.Parse(LineFragments[1]);

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.CorrelationLengthInXDir = (int)double.Parse(LineFragments[0]);
                model.CorrelationLengthInYDir = (int)double.Parse(LineFragments[1]);
                model.CorrelationLengthInZDir = (int)double.Parse(LineFragments[2]);

                model.CovFunc = CovFuncCharInv3D(reader.ReadLine().Substring(LHSLength).Trim());

                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();

                int n = int.Parse(reader.ReadLine().Substring(LHSLength));
                for (int i = 1; i <= n; i++)
                {
                    Line = reader.ReadLine();
                    LineFragments = Line.Substring(LHSLength).Trim().Split(' ');

                    var r = new RigidFootingLoads3D()
                    {
                        XStart = int.Parse(LineFragments[0]),
                        XEnd = int.Parse(LineFragments[1]),
                        YStart = int.Parse(LineFragments[2]),
                        YEnd = int.Parse(LineFragments[3]),
                        LoadMean = double.Parse(LineFragments[4]),
                        LoadStdDev = double.Parse(LineFragments[5])
                    };

                    model.RigidFootingLoads.Add(r);
                }

                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();

                model.OverlayLogElasticModulusOnDisplacedMesh = reader.ReadLine().Substring(LHSLength).ToBool();
                model.ShowProblemDimensionsOnDisplacedMesh = reader.ReadLine().Substring(LHSLength).ToBool();
                model.ShowTitlesOnDisplacedMesh = reader.ReadLine().Substring(LHSLength).ToBool();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.DisplacementMagInXDir = double.Parse(LineFragments[0]);
                model.DisplacementMagInYDir = double.Parse(LineFragments[1]);

                model.DisplacedMeshPlotWidth = double.Parse(reader.ReadLine().Substring(LHSLength));
            }

            return model;
        }
        public static RSlope2D ReadRSlope2DFile(string filePath)
        {
            var model = new RSlope2D();
            string Line;
            string[] LineFragments;
            int LHSLength = 50;

            using (var reader = new System.IO.StreamReader(filePath))
            {
                model.JobTitle = reader.ReadLine();
                model.BaseName = System.IO.Path.GetFileNameWithoutExtension(filePath);

                model.EchoInputDataToOutputFile = reader.ReadLine().Substring(LHSLength).ToBool();
                reader.ReadLine();
                model.OutputDebugInfo = reader.ReadLine().Substring(LHSLength).ToBool();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');

                model.PlotARandomField = LineFragments[0].ToBool();
                model.RFPropertyToPlot = LineFragments[2].RSlopePropertyInv();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');

                model.ProducePSPlotOfDisplacedMesh = LineFragments[0].ToBool();

                model.NElementsLeftOfEmbark = int.Parse(reader.ReadLine().Substring(LHSLength));
                model.NElementsRightOfEmbark = int.Parse(reader.ReadLine().Substring(LHSLength));
                model.NElementsInEmbark = int.Parse(reader.ReadLine().Substring(LHSLength));
                model.NElementsInFoundations = int.Parse(reader.ReadLine().Substring(LHSLength));

                model.SlopeGradient = double.Parse(reader.ReadLine().Substring(LHSLength));

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');

                model.ElementSizeInXDir = double.Parse(LineFragments[0]);
                model.ElementSizeInYDir = double.Parse(LineFragments[1]);

                model.ConvergenceTolerance = double.Parse(reader.ReadLine().Substring(LHSLength));

                model.MaxNumIterations = int.Parse(reader.ReadLine().Substring(LHSLength));

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                if (LineFragments.Count() == 7)
                {
                    model.Cohesion.Mean = double.Parse(LineFragments[0]);
                    model.Cohesion.StandardDeviation = double.Parse(LineFragments[1]);
                    model.Cohesion.DistributionType = LineFragments[2].DistCharInv();
                    model.Cohesion.LowerBound = double.Parse(LineFragments[3]);
                    model.Cohesion.UpperBound = double.Parse(LineFragments[4]);
                    model.Cohesion.Location = double.Parse(LineFragments[5]);
                    model.Cohesion.Scale = double.Parse(LineFragments[6]);
                }
                else if (LineFragments.Count() == 3)
                {
                    model.Cohesion.Mean = double.Parse(LineFragments[0]);
                    model.Cohesion.StandardDeviation = double.Parse(LineFragments[1]);
                    model.Cohesion.DistributionType = LineFragments[2].DistCharInv();
                }
                else
                {
                    throw new FormatException();
                }

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                LineFragments = LineFragments.Where(s => s.Length != 0).ToArray();

                //Friction angle distribution prefixed by t when the rv is tan(phi)
                if (LineFragments[2].ToCharArray()[0] == 't')
                {
                    model.FrictionAngleType = FrictionAngle.TanPhi;
                    LineFragments[2] = LineFragments[2].Substring(1);
                }

                if (LineFragments.Count() == 7)
                {
                    model.FrictionAngle.Mean = double.Parse(LineFragments[0]);
                    model.FrictionAngle.StandardDeviation = double.Parse(LineFragments[1]);
                    model.FrictionAngle.DistributionType = LineFragments[2].DistCharInv();
                    model.FrictionAngle.LowerBound = double.Parse(LineFragments[3]);
                    model.FrictionAngle.UpperBound = double.Parse(LineFragments[4]);
                    model.FrictionAngle.Location = double.Parse(LineFragments[5]);
                    model.FrictionAngle.Scale = double.Parse(LineFragments[6]);
                }
                else if (LineFragments.Count() == 3)
                {
                    model.FrictionAngle.Mean = double.Parse(LineFragments[0]);
                    model.FrictionAngle.StandardDeviation = double.Parse(LineFragments[1]);
                    model.FrictionAngle.DistributionType = LineFragments[2].DistCharInv();
                }
                else
                {
                    throw new FormatException();
                }


                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                if (LineFragments.Count() == 7)
                {
                    model.DilationAngle.Mean = double.Parse(LineFragments[0]);
                    model.DilationAngle.StandardDeviation = double.Parse(LineFragments[1]);
                    model.DilationAngle.DistributionType = LineFragments[2].DistCharInv();
                    model.DilationAngle.LowerBound = double.Parse(LineFragments[3]);
                    model.DilationAngle.UpperBound = double.Parse(LineFragments[4]);
                    model.DilationAngle.Location = double.Parse(LineFragments[5]);
                    model.DilationAngle.Scale = double.Parse(LineFragments[6]);
                }
                else if (LineFragments.Count() == 3)
                {
                    model.DilationAngle.Mean = double.Parse(LineFragments[0]);
                    model.DilationAngle.StandardDeviation = double.Parse(LineFragments[1]);
                    model.DilationAngle.DistributionType = LineFragments[2].DistCharInv();
                }
                else
                {
                    throw new FormatException();
                }

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                if (LineFragments.Count() == 7)
                {
                    model.UnitWeight.Mean = double.Parse(LineFragments[0]);
                    model.UnitWeight.StandardDeviation = double.Parse(LineFragments[1]);
                    model.UnitWeight.DistributionType = LineFragments[2].DistCharInv();
                    model.UnitWeight.LowerBound = double.Parse(LineFragments[3]);
                    model.UnitWeight.UpperBound = double.Parse(LineFragments[4]);
                    model.UnitWeight.Location = double.Parse(LineFragments[5]);
                    model.UnitWeight.Scale = double.Parse(LineFragments[6]);
                }
                else if (LineFragments.Count() == 3)
                {
                    model.UnitWeight.Mean = double.Parse(LineFragments[0]);
                    model.UnitWeight.StandardDeviation = double.Parse(LineFragments[1]);
                    model.UnitWeight.DistributionType = LineFragments[2].DistCharInv();
                }
                else
                {
                    throw new FormatException();
                }

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                if (LineFragments.Count() == 7)
                {
                    model.ElasticModulus.Mean = double.Parse(LineFragments[0]);
                    model.ElasticModulus.StandardDeviation = double.Parse(LineFragments[1]);
                    model.ElasticModulus.DistributionType = LineFragments[2].DistCharInv();
                    model.ElasticModulus.LowerBound = double.Parse(LineFragments[3]);
                    model.ElasticModulus.UpperBound = double.Parse(LineFragments[4]);
                    model.ElasticModulus.Location = double.Parse(LineFragments[5]);
                    model.ElasticModulus.Scale = double.Parse(LineFragments[6]);
                }
                else if (LineFragments.Count() == 3)
                {
                    model.ElasticModulus.Mean = double.Parse(LineFragments[0]);
                    model.ElasticModulus.StandardDeviation = double.Parse(LineFragments[1]);
                    model.ElasticModulus.DistributionType = LineFragments[2].DistCharInv();
                }
                else
                {
                    throw new FormatException();
                }

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                if (LineFragments.Count() == 7)
                {
                    model.PoissonsRatio.Mean = double.Parse(LineFragments[0]);
                    model.PoissonsRatio.StandardDeviation = double.Parse(LineFragments[1]);
                    model.PoissonsRatio.DistributionType = LineFragments[2].DistCharInv();
                    model.PoissonsRatio.LowerBound = double.Parse(LineFragments[3]);
                    model.PoissonsRatio.UpperBound = double.Parse(LineFragments[4]);
                    model.PoissonsRatio.Location = double.Parse(LineFragments[5]);
                    model.PoissonsRatio.Scale = double.Parse(LineFragments[6]);
                }
                else if (LineFragments.Count() == 3)
                {
                    model.PoissonsRatio.Mean = double.Parse(LineFragments[0]);
                    model.PoissonsRatio.StandardDeviation = double.Parse(LineFragments[1]);
                    model.PoissonsRatio.DistributionType = LineFragments[2].DistCharInv();
                }
                else
                {
                    throw new FormatException();
                }

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');

                foreach(string s in LineFragments)
                {
                    double a;
                    if(double.TryParse(s, out a))
                    {
                        model.StrengthReductionFactors.Add(a);
                    }
                }


                model.NumberOfRealizations = int.Parse(reader.ReadLine().Substring(LHSLength));
                model.GeneratorSeed = int.Parse(reader.ReadLine().Substring(LHSLength));

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.CorrelationLengthInXDir = (int)double.Parse(LineFragments[0]);
                model.CorrelationLengthInYDir = (int)double.Parse(LineFragments[1]);

                model.CovFunc = CovFuncCharInv(reader.ReadLine().Substring(LHSLength).Trim());

                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();

                Line = reader.ReadLine();
                while (Line.Trim().Length > 0)
                {
                    LineFragments = Line.Split(' ').Where(p => p != "").ToArray();

                    int i = (int)LineFragments[0].RSlopePropertyInv();
                    int j = (int)LineFragments[1].RSlopePropertyInv();

                    model.CorrelationMatrix[i, j] = double.Parse(LineFragments[2]);
                    model.CorrelationMatrix[j, i] = double.Parse(LineFragments[2]);

                    Line = reader.ReadLine();
                }


                model.ShowMeshOnDisplacedMesh = reader.ReadLine().Substring(LHSLength).ToBool();

                Line = reader.ReadLine();
                LineFragments = Line.Substring(LHSLength).Trim().Split(' ');
                model.ShowRFOnDisplacedMesh = LineFragments[0].ToBool();
                model.ShowLogRFOnDisplacedMesh = LineFragments[1].ToBool();
                model.DisplacedMeshPropertyToPlot = LineFragments[2].RSlopePropertyInv();

                model.DisplacedMeshPlotWidth = double.Parse(reader.ReadLine().Substring(LHSLength));

            }
            return model;
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
        private static CovarianceFunction3D CovFuncCharInv3D(string covFuncChar)
        {
            switch (covFuncChar)
            {
                case "dlavx3":
                    return CovarianceFunction3D.dlavx3;
                case "dlafs3":
                    return CovarianceFunction3D.dlafs3;
                case "dlsep3":
                    return CovarianceFunction3D.dlsep3;
                case "dlsfr3":
                    return CovarianceFunction3D.dlsfr3;
                case "dlspx3":
                    return CovarianceFunction3D.dlspx3;
                default:
                    throw new ArgumentException("Unable to read covariance function type in data file.");

            }
        }
        private static Distribution DistributionCharInv(string distributionChar)
        {
            switch (distributionChar.ToCharArray()[0])
            {
                case 'd':
                    return Distribution.Deterministic;
                case 'n':
                    return Distribution.Normal;
                case 'l':
                    return Distribution.Lognormal;
                case 'b':
                    return Distribution.Bounded;
                default:
                    throw new ArgumentException("Unable to read distribution type in data file.");
            }
        }
        private static SoilProperty PropertyCharInv(string propertyChar)
        {
            switch (propertyChar)
            {
                case "c":
                    return SoilProperty.Cohesion;
                case "phi":
                    return SoilProperty.FrictionAngle;
                case "psi":
                    return SoilProperty.DilationAngle;
                case "e":
                    return SoilProperty.ElasticModulus;
                case "v":
                    return SoilProperty.PoissonsRatio;
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
