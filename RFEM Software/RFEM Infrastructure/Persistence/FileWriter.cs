using RFEMSoftware.Simulation.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEMSoftware.Simulation.Infrastructure.Persistence
{
    internal static class FileWriter
    {
        internal static void Write(ISimModel model)
        {
            string DataFileString;
            switch (model.Type)
            {
                case Program.RBear2D:
                    DataFileString = BuildRBear2DString((RBear2D)model);
                    break;
                case Program.RDam2D:
                    DataFileString = BuildRDam2DString((RDam2D)model);
                    break;
                case Program.REarth2D:
                    DataFileString = BuildREarth2DString((REarth2D)model);
                    break;
                case Program.RFlow2D:
                    DataFileString = BuildRFlow2DString((RFlow2D)model);
                    break;
                case Program.RFlow3D:
                    DataFileString = BuildRFlow3DString((RFlow3D)model);
                    break;
                case Program.RPill2D:
                    DataFileString = BuildRPill2DString((RPill2D)model);
                    break;
                case Program.RPill3D:
                    DataFileString = BuildRPill3DString((RPill3D)model);
                    break;
                case Program.RSetl2D:
                    DataFileString = BuildRSetl2DString((RSetl2D)model);
                    break;
                case Program.RSetl3D:
                    DataFileString = BuildRSetl3DString((RSetl3D)model);
                    break;
                case Program.RSlope2D:
                    DataFileString = BuildRSlope2DString((RSlope2D)model);
                    break;
                default:
                    throw new NotImplementedException();
            }

            using (var fileWriter = new System.IO.StreamWriter(model.DataFileLocation(), false))
            {
                fileWriter.Write(DataFileString);
            }

            if (!System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                            "\\RFEM_Software"))
                System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.
                                                        LocalApplicationData) + "\\RFEM_Software");


            using (var fileWriter = new System.IO.StreamWriter(model.AppDataFileLocation, false))
            {
                fileWriter.Write(DataFileString);
            }

        }
        internal static void Write(ISimModel model, string filePath)
        {
            string DataFileString;
            switch (model.Type)
            {
                case Program.RBear2D:
                    DataFileString = BuildRBear2DString((RBear2D)model);
                    break;
                case Program.RDam2D:
                    DataFileString = BuildRDam2DString((RDam2D)model);
                    break;
                case Program.REarth2D:
                    DataFileString = BuildREarth2DString((REarth2D)model);
                    break;
                case Program.RFlow2D:
                    DataFileString = BuildRFlow2DString((RFlow2D)model);
                    break;
                case Program.RFlow3D:
                    DataFileString = BuildRFlow3DString((RFlow3D)model);
                    break;
                case Program.RPill2D:
                    DataFileString = BuildRPill2DString((RPill2D)model);
                    break;
                case Program.RPill3D:
                    DataFileString = BuildRPill3DString((RPill3D)model);
                    break;
                case Program.RSetl2D:
                    DataFileString = BuildRSetl2DString((RSetl2D)model);
                    break;
                case Program.RSetl3D:
                    DataFileString = BuildRSetl3DString((RSetl3D)model);
                    break;
                case Program.RSlope2D:
                    DataFileString = BuildRSlope2DString((RSlope2D)model);
                    break;
                default:
                    throw new NotImplementedException();
            }
            using (var fileWriter = new System.IO.StreamWriter(filePath, false))
            {
                fileWriter.Write(DataFileString);
            }

            if (!System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                            "\\RFEM_Software"))
                System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.
                                                        LocalApplicationData) + "\\RFEM_Software");


            using (var fileWriter = new System.IO.StreamWriter(model.AppDataFileLocation, false))
            {
                fileWriter.Write(DataFileString);
            }
        }
        private static string BuildRBear2DString(RBear2D model)
        {
            ReplaceNullValuesWithZero(model.CohesionDist);
            ReplaceNullValuesWithZero(model.FrictionAngleDist);
            ReplaceNullValuesWithZero(model.DilationAngleDist);
            ReplaceNullValuesWithZero(model.ElasticModulusDist);
            ReplaceNullValuesWithZero(model.PoissonsRatioDist);

            var str = new StringBuilder();

            str.AppendLine(model.JobTitle);
            str.AppendLine("Echo input data to stats file (t/f)? . . . . . .  " + model.EchoInputDataToOutputFile.ToTFString());
            str.AppendLine("Report progress to standard output (t/f)?. . . .  " + true.ToTFString());
            str.AppendLine("Dump debug data to *.stt file (t/f)? . . . . . .  " + model.WriteDebugDataToOutputFile.ToTFString());
            str.AppendLine(String.Format("Display a random field (t/f)?  . . . . . . . . .  {0} {1} {2}",
                                            model.PlotFirstRandomField.ToTFString(), "1", model.FirstRandomFieldProperty.ToDataFileString()));
            str.AppendLine(String.Format("Display displaced FE mesh (t/f)? . . . . . . . .  {0} {1}",
                                            model.ProducePSPLOTOfFirstFEM.ToTFString(), "1"));
            str.AppendLine("Normalize bearing capacity (/det)? . . . . . . .  " + model.NormalizeBearingCapacitySamples.ToTFString());
            str.AppendLine("Output bearing capacity samples? . . . . . . . .  " + model.OutputBearingCapacitySamples.ToTFString());
            str.AppendLine(String.Format("Number of elements in X and Y directions . . . .  {0} {1}", model.NElementsInXDir,
                                            model.NElementsInYDir));
            str.AppendLine(String.Format("Element size in X and Y directions . . . . . . .  {0} {1}", model.ElementSizeInXDir,
                                            model.ElementSizeInYDir));

            if (model.NumberOfFootings == 1)
            {
                str.AppendLine(string.Format("Number of footings, width  . . . . . . . . . . .  {0} {1}", model.NumberOfFootings,
                                                model.FootingWidth));
            }
            else
            {
                str.AppendLine(string.Format("Number of footings, width, gap . . . . . . . . .  {0} {1} {2}",
                                                model.NumberOfFootings, model.FootingWidth, model.FootingGap));
            }

            str.AppendLine(string.Format("Displacement increment, plastic tol, bearing tol  {0} {1} {2}",
                                            model.DisplacementInc, model.PlasticTol, model.BearingTol));
            str.AppendLine(string.Format("Max displacement steps, max iterations . . . . .  {0} {1}", model.MaxNumSteps,
                                            model.MaxNumIterations));

            if (model.CohesionDist.DistributionType == Distribution.Bounded)
            {
                str.AppendLine(string.Format("Cohesion mean/SD, dist, lower/upper/loc/scale  .  {0} {1} {2} {3} {4} {5} {6}",
                                            model.CohesionDist.Mean.ToString(), model.CohesionDist.StandardDeviation.ToString(),
                                            model.CohesionDist.DistributionType.ToDataFileString(), model.CohesionDist.LowerBound.ToString(),
                                            model.CohesionDist.UpperBound.ToString(), model.CohesionDist.Location.ToString(),
                                            model.CohesionDist.Scale.ToString()));
            }
            else
            {
                str.AppendLine(string.Format("Cohesion mean, SD, dist  . . . . . . . . . . . .  {0} {1} {2}",
                                                model.CohesionDist.Mean.ToString(), model.CohesionDist.StandardDeviation.ToString(),
                                                model.CohesionDist.DistributionType.ToDataFileString()));
            }

            if (model.FrictionAngleDist.DistributionType == Distribution.Bounded)
            {
                str.AppendLine(string.Format("Friction mean/SD, dist, lower/upper/loc/scale  .  {0} {1} {2}{3} {4} {5} {6} {7}",
                                            model.FrictionAngleDist.Mean.ToString(), model.FrictionAngleDist.StandardDeviation.ToString(),
                                            model.FrictionAngleType.ToDataFileString(),
                                            model.FrictionAngleDist.DistributionType.ToDataFileString(), model.FrictionAngleDist.LowerBound.ToString(),
                                            model.FrictionAngleDist.UpperBound.ToString(), model.FrictionAngleDist.Location.ToString(),
                                            model.FrictionAngleDist.Scale.ToString()));
            }
            else
            {

                str.AppendLine(string.Format("Friction angle mean, SD, dist  . . . . . . . . .  {0} {1} {2}{3}",
                                                model.FrictionAngleDist.Mean.ToString(), model.FrictionAngleDist.StandardDeviation.ToString(),
                                                model.FrictionAngleType.ToDataFileString(), model.FrictionAngleDist.DistributionType.ToDataFileString()));
            }

            if (model.DilationAngleDist.DistributionType == Distribution.Bounded)
            {
                str.AppendLine(string.Format("Dilation mean/SD, dist, lower/upper/loc/scale  .  {0} {1} {2} {3} {4} {5} {6}",
                                            model.DilationAngleDist.Mean.ToString(), model.DilationAngleDist.StandardDeviation.ToString(),
                                            model.DilationAngleDist.DistributionType.ToDataFileString(), model.DilationAngleDist.LowerBound.ToString(),
                                            model.DilationAngleDist.UpperBound.ToString(), model.DilationAngleDist.Location.ToString(),
                                            model.DilationAngleDist.Scale.ToString()));
            }
            else
            {
                str.AppendLine(string.Format("Dilation angle mean, SD, dist  . . . . . . . . .  {0} {1} {2}",
                                                model.DilationAngleDist.Mean.ToString(), model.DilationAngleDist.StandardDeviation.ToString(),
                                                model.DilationAngleDist.DistributionType.ToDataFileString()));
            }

            if (model.ElasticModulusDist.DistributionType == Distribution.Bounded)
            {
                str.AppendLine(string.Format("Elastic mean/SD, dist, lower/upper/loc/scale . .  {0} {1} {2} {3} {4} {5} {6}",
                                            model.ElasticModulusDist.Mean.ToString(), model.ElasticModulusDist.StandardDeviation.ToString(),
                                            model.ElasticModulusDist.DistributionType.ToDataFileString(), model.ElasticModulusDist.LowerBound.ToString(),
                                            model.ElasticModulusDist.UpperBound.ToString(), model.ElasticModulusDist.Location.ToString(),
                                            model.ElasticModulusDist.Scale.ToString()));
            }
            else
            {
                str.AppendLine(string.Format("Elastic modulus mean, SD, dist . . . . . . . . .  {0} {1} {2}",
                                                model.ElasticModulusDist.Mean.ToString(), model.ElasticModulusDist.StandardDeviation.ToString(),
                                                model.ElasticModulusDist.DistributionType.ToDataFileString()));
            }

            if (model.PoissonsRatioDist.DistributionType == Distribution.Bounded)
            {
                str.AppendLine(string.Format("Poisson's mean/SD, dist, lower/upper/loc/scale .  {0} {1} {2} {3} {4} {5} {6}",
                                            model.PoissonsRatioDist.Mean.ToString(), model.PoissonsRatioDist.StandardDeviation.ToString(),
                                            model.PoissonsRatioDist.DistributionType.ToDataFileString(), model.PoissonsRatioDist.LowerBound.ToString(),
                                            model.PoissonsRatioDist.UpperBound.ToString(), model.PoissonsRatioDist.Location.ToString(),
                                            model.PoissonsRatioDist.Scale.ToString()));
            }
            else
            {
                str.AppendLine(string.Format("Poisson's ratio mean, SD, dist . . . . . . . . .  {0} {1} {2}",
                                                model.PoissonsRatioDist.Mean.ToString(), model.PoissonsRatioDist.StandardDeviation.ToString(),
                                                model.PoissonsRatioDist.DistributionType.ToDataFileString()));
            }

            str.AppendLine("Number of realizations . . . . . . . . . . . . .  " + model.NumberOfRealizations);
            str.AppendLine("Generator seed (0 for random seed) . . . . . . .  " + model.GeneratorSeed);
            str.AppendLine(string.Format("Correlation length in X [and Y] directions . . .  {0} {1}", model.CorLengthInXDir,
                                            model.CorLengthInYDir));
            str.AppendLine("Name of covariance function  . . . . . . . . . .  " + Enum.GetName(typeof(CovarianceFunction), model.CovFunction));
            str.AppendLine();
            str.AppendLine("Material Property Correlation Matrix Data");
            str.AppendLine("=========================================");
            str.AppendLine("Property 1    Property 2    Correlation");
            str.AppendLine("----------    ----------    -----------");

            for (int i = 0; i <= 4; i++)
            {
                for (int j = i + 1; j <= 4; j++)
                {
                    if (model.CorMatrix[i, j] != 0)
                    {
                        str.AppendLine(string.Format("   {0}            {1}           {2}",
                                        ((SoilProperty)i).ToDataFileString(),
                                        ((SoilProperty)j).ToDataFileString(),
                                        model.CorMatrix[i, j]));
                    }
                }
            }

            str.AppendLine();
            str.AppendLine("Show element boundaries  . . . . . . . . . . . .  " + model.ShowMeshOnDisplacedPlot.ToTFString());
            str.AppendLine(string.Format("Show random field as background [log?] [prop?] .  {0} {1} {2}",
                                            model.ShowRFOnPSPLOT.ToTFString(), model.ShowLogRandomField.ToTFString(),
                                            model.PSPLOTProperty.ToDataFileString()));
            str.AppendLine("Width of displaced mesh output plot in inches  .  " + model.DisplacedMeshPlotWidth);
            str.AppendLine("Plot offset in x- [and y-] directions (inches) .  1.5");

            return str.ToString();
        }
        private static string BuildRDam2DString(RDam2D model)
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine(model.JobTitle);
            str.AppendLine(string.Format("Echo input data to stats file (t/f)? . . . . . .  {0}", model.EchoInputDataToOutputFile.ToTFString()));
            str.AppendLine(string.Format("Report progress to standard output (t/f)?. . . .  {0}", true.ToTFString()));

            if (model.OutputDebugInfo)
            {
                str.AppendLine("Debug code (0,1,2,or 3), [realization no.] . . .  0 0");
            }
            else
            {
                str.AppendLine(string.Format("Debug code (0,1,2,or 3), [realization no.] . . .  {0} {1}", model.DebugCode, model.RealizationNumber));
            }

            str.AppendLine("Display first log-conductivity field (t/f)?. . .  " + model.ProduceDisplayFile.ToTFString());
            str.AppendLine("Plot first stochastic flownet (t/f)? . . . . . .  " + model.ProducePSPlotOfFirstFlownet.ToTFString());
            str.AppendLine("Plot gradient statistics fields (t/f)? . . . . .  " + model.OutputGradientMeanAndStdDev.ToTFString());
            str.AppendLine("Output flow rates and free surface pos (t/f)?  .  " + model.OutputFlowRate.ToTFString());
            str.AppendLine("Output block conductivity values (t/f)?  . . . .  " + model.OutputBlockConductivities.ToTFString());
            str.AppendLine("Output arith, geom, harm cond. values (t/f)? . .  " + model.OutputConductivityAverages.ToTFString());
            str.AppendLine("Generate uniform conductivity field (t/f)? . . .  " + model.GenerateUniformConductivity.ToTFString());
            str.AppendLine(string.Format("Number of mesh elements in X, Y directions . . .  {0} {1}", model.NumElementsInXDir, model.NumElementsInYDir));

            str.Append("Node numbers for gradient realizations . . . . . ");
            foreach (int? i in model.NodesForGradientOutput)
            {
                if (i != null)
                    str.Append(" " + i);
            }
            str.Append(Environment.NewLine);

            str.AppendLine(string.Format("Drain X, Y dimensions [conductivity] . . . . . .  {0} {1} {2}", model.DrainXDimension, model.DrainYDimension, model.DrainConductivity));
            str.AppendLine("Width of top of dam  . . . . . . . . . . . . . .  " + model.DamTop);
            str.AppendLine("Width of base of dam . . . . . . . . . . . . . .  " + model.DamBase);
            str.AppendLine("Height of dam  . . . . . . . . . . . . . . . . .  " + model.DamHeight);
            str.AppendLine(string.Format("Number of realizations [max iter., tol]. . . . .  {0} {1} {2}", model.NumberOfRealizations, model.MaxNumberOfIterations, model.ConvergenceTolerance));
            str.AppendLine("Generator seed (0 for random seed) . . . . . . .  " + model.GeneratorSeed);
            str.AppendLine(string.Format("Correlation length in X [and Y] directions . . .  {0} {1}", model.CorrelationLengthInXDir, model.CorrelationLengthInYDir));
            str.AppendLine("Conductivity mean  . . . . . . . . . . . . . . .  " + model.ConductivityMean);
            str.AppendLine("Conductivity standard deviation  . . . . . . . .  " + model.ConductivityStdDev);
            str.AppendLine("Name of covariance function used by simulator  .  " + model.CovFunction);
            str.AppendLine("Number of equipotential drops on flownet . . . .  " + model.NumEquipotentialDrops);
            str.AppendLine("Show equipotential drops on flownet (t/f?. . . .  " + model.ShowEquipotentialDrops.ToTFString());
            str.AppendLine("Show streamlines on flownet (t/f)? . . . . . . .  " + model.ShowStreamlines.ToTFString());
            str.AppendLine("Show element mesh on flownet (t/f)?. . . . . . .  " + model.ShowMeshOnFlownet.ToTFString());
            str.AppendLine("Show log-conductivity field on flownet (t/f)?. .  " + model.ShowLogConductivity.ToTFString());
            str.AppendLine("Show problem dimensions on flownet (t/f)?. . . .  " + model.ShowDamDimensionsOnFlownet.ToTFString());
            str.AppendLine("Show title and subtitles on flownet (t/f)? . . .  " + model.ShowTitlesOnFlownet.ToTFString());
            str.AppendLine("Flownet plot width on page in inches . . . . . .  " + model.FlownetWidth);

            if (model.ModifyHoirzontalSpacing)
            {
                str.AppendLine("Proportionate spacing code (off/geom/lin/prop) .  " + ((int)model.SpacingAlgo + 1));
            }
            else
            {
                str.AppendLine("Proportionate spacing code (off/geom/lin/prop) .  0");
            }

            str.AppendLine("Use alternative spacing if first fails (t/f)?. .  " + model.UseAlternateAlgoIfFirstFails.ToTFString());
            str.AppendLine("Ensure free surface is non-increasing (t/f)? . .  " + model.RestrainFreeSurfaceNonIncreasing.ToTFString());
            str.AppendLine("Employ free surface damping (t/f)? . . . . . . .  " + model.DampOscillations.ToTFString());
            str.AppendLine("Show centroids on debug mesh plots (2,3 only)? .  " + model.ShowCentroidsOnMesh.ToTFString());

            return str.ToString();
        }
        private static string BuildREarth2DString(REarth2D model)
        {
            StringBuilder str = new StringBuilder();

            ReplaceNullValuesWithZero(model.Cohesion);
            ReplaceNullValuesWithZero(model.FrictionAngle);
            ReplaceNullValuesWithZero(model.DilationAngle);
            ReplaceNullValuesWithZero(model.ElasticModulus);
            ReplaceNullValuesWithZero(model.PoissonsRatio);
            ReplaceNullValuesWithZero(model.UnitWeight);
            ReplaceNullValuesWithZero(model.PressureCoefficient);


            str.AppendLine(model.JobTitle);
            str.AppendLine("Echo input data to stats file (t/f)? . . . . . .  " +
                                                            model.EchoInputDataToOutputFile.ToTFString());
            str.AppendLine("Report progress to standard output (t/f)?. . . .  " + true.ToTFString());
            str.AppendLine("Dump debug data to *.stt file (t/f)? . . . . . .  " +
                                                            model.WriteDebugDataToOutputFile.ToTFString());
            str.AppendLine(string.Format("Display a random field to *.fld (t/f)? . . . . .  {0} {1} {2}",
                                            model.PlotFirstRandomField.ToTFString(), "1",
                                            model.FirstRFPropertyToPlot.ToDataFileString()));
            str.AppendLine(string.Format("Plot displaced FE mesh to *.dis (t/f)? . . . . .  {0} {1}",
                                                            model.ProducePSPlotOfFirstFEM.ToTFString(),
                                                            "1"));
            str.AppendLine("Output soil force samples? . . . . . . . . . . .  " +
                                                            model.StoreWallReactionSamples.ToTFString());
            str.AppendLine(string.Format("Number of elements in X and Y directions . . . .  {0} {1}",
                                                            model.NElementsInXDir, model.NElementsInYDir));
            str.AppendLine(string.Format("Element size in X and Y directions . . . . . . .  {0} {1}",
                                                            model.ElementSizeInXDir, model.ElementSizeInYDir));
            str.AppendLine(string.Format("Wall depth in elements [rough?]  . . . . . . . .  {0} {1}",
                                                            model.WallExtension, model.RoughWallSurface.ToTFString()));
            str.AppendLine(string.Format("Displacement increment, plastic tol, stress tol.  {0} {1} {2}",
                                                            model.DisplacementIncrement, model.PlasticTol, model.StressTol));
            str.AppendLine(string.Format("Max displacement steps, max iterations . . . . .  {0} {1}",
                                                            model.MaxNumSteps, model.MaxNumIterations));

            switch (model.Cohesion.DistributionType)
            {
                case REarthDistributions.Bounded:

                    str.AppendLine(
                        string.Format("Cohesion mean/SD, dist, lower/upper/loc/scale  .  {0} {1} {2} {3} {4} {5} {6}",
                                      model.Cohesion.Mean.ToString(),
                                      model.Cohesion.StandardDeviation.ToString(),
                                      model.Cohesion.DistributionType.ToDataFileString(),
                                      model.Cohesion.LowerBound.ToString(),
                                      model.Cohesion.UpperBound.ToString(),
                                      model.Cohesion.Location.ToString(),
                                      model.Cohesion.Scale));

                    break;

                case REarthDistributions.fphi:

                    str.AppendLine(
                        String.Format("Cohesion mean/SD, dist, a, b, f(phi) . . . . . .  {0} {1} {2} {3} {4} {5}",
                                       model.Cohesion.Mean,
                                       model.Cohesion.StandardDeviation,
                                       model.Cohesion.DistributionType.ToDataFileString(),
                                       model.Cohesion.Intercept,
                                       model.Cohesion.Slope,
                                       model.Cohesion.PhiFunc.ToDataFileString()));
                    break;

                default:

                    str.AppendLine(
                        string.Format("Cohesion mean, SD, dist  . . . . . . . . . . . .  {0} {1} {2}",
                                      model.Cohesion.Mean,
                                      model.Cohesion.StandardDeviation,
                                      model.Cohesion.DistributionType.ToDataFileString()));
                    break;
            }

            switch (model.FrictionAngle.DistributionType)
            {
                case Distribution.Bounded:

                    str.AppendLine(
                        string.Format("Friction mean/SD, dist, lower/upper/loc/scale  .  {0} {1} {2}{3} {4} {5} {6} {7}",
                                      model.FrictionAngle.Mean,
                                      model.FrictionAngle.StandardDeviation,
                                      model.FrictionAngleType.ToDataFileString(),
                                      model.FrictionAngle.DistributionType.ToDataFileString(),
                                      model.FrictionAngle.LowerBound,
                                      model.FrictionAngle.UpperBound,
                                      model.FrictionAngle.Location,
                                      model.FrictionAngle.Scale));
                    break;

                default:

                    str.AppendLine(
                        string.Format("Friction angle mean, SD, dist  . . . . . . . . .  {0} {1} {2}{3}",
                                      model.FrictionAngle.Mean,
                                      model.FrictionAngle.StandardDeviation,
                                      model.FrictionAngleType.ToDataFileString(),
                                      model.FrictionAngle.DistributionType.ToDataFileString()));
                    break;
            }

            switch (model.DilationAngle.DistributionType)
            {
                case REarthDistributions.Bounded:

                    str.AppendLine(
                        string.Format("Dilation mean/SD, dist, lower/upper/loc/scale  .  {0} {1} {2} {3} {4} {5} {6}",
                                      model.DilationAngle.Mean,
                                      model.DilationAngle.StandardDeviation,
                                      model.DilationAngle.DistributionType.ToDataFileString(),
                                      model.DilationAngle.LowerBound,
                                      model.DilationAngle.UpperBound,
                                      model.DilationAngle.Location,
                                      model.DilationAngle.Scale));

                    break;

                case REarthDistributions.fphi:

                    str.AppendLine(
                        String.Format("Dilation mean/SD, dist, a, b, f(phi) . . . . . .  {0} {1} {2} {3} {4} {5}",
                                       model.DilationAngle.Mean,
                                       model.DilationAngle.StandardDeviation,
                                       model.DilationAngle.DistributionType.ToDataFileString(),
                                       model.DilationAngle.Intercept,
                                       model.DilationAngle.Slope,
                                       model.DilationAngle.PhiFunc.ToDataFileString()));
                    break;

                default:

                    str.AppendLine(
                        string.Format("Dilation angle mean, SD, dist  . . . . . . . . .  {0} {1} {2}",
                                      model.DilationAngle.Mean,
                                      model.DilationAngle.StandardDeviation,
                                      model.DilationAngle.DistributionType.ToDataFileString()));
                    break;
            }

            switch (model.ElasticModulus.DistributionType)
            {
                case REarthDistributions.Bounded:

                    str.AppendLine(
                        string.Format("Elastic mean/SD, dist, lower/upper/loc/scale . .  {0} {1} {2} {3} {4} {5} {6}",
                                      model.ElasticModulus.Mean,
                                      model.ElasticModulus.StandardDeviation,
                                      model.ElasticModulus.DistributionType.ToDataFileString(),
                                      model.ElasticModulus.LowerBound,
                                      model.ElasticModulus.UpperBound,
                                      model.ElasticModulus.Location,
                                      model.ElasticModulus.Scale));

                    break;

                case REarthDistributions.fphi:

                    str.AppendLine(
                        String.Format("Elastic mean/SD, dist, a, b, f(phi). . . . . . .  {0} {1} {2} {3} {4} {5}",
                                       model.ElasticModulus.Mean,
                                       model.ElasticModulus.StandardDeviation,
                                       model.ElasticModulus.DistributionType.ToDataFileString(),
                                       model.ElasticModulus.Intercept,
                                       model.ElasticModulus.Slope,
                                       model.ElasticModulus.PhiFunc.ToDataFileString()));
                    break;

                default:

                    str.AppendLine(
                        string.Format("Elastic modulus mean, SD, dist . . . . . . . . .  {0} {1} {2}",
                                      model.ElasticModulus.Mean,
                                      model.ElasticModulus.StandardDeviation,
                                      model.ElasticModulus.DistributionType.ToDataFileString()));
                    break;
            }

            switch (model.PoissonsRatio.DistributionType)
            {
                case REarthDistributions.Bounded:

                    str.AppendLine(
                        string.Format("Poisson's mean/SD, dist, lower/upper/loc/scale .  {0} {1} {2} {3} {4} {5} {6}",
                                      model.PoissonsRatio.Mean,
                                      model.PoissonsRatio.StandardDeviation,
                                      model.PoissonsRatio.DistributionType.ToDataFileString(),
                                      model.PoissonsRatio.LowerBound,
                                      model.PoissonsRatio.UpperBound,
                                      model.PoissonsRatio.Location,
                                      model.PoissonsRatio.Scale));

                    break;

                case REarthDistributions.fphi:

                    str.AppendLine(
                        String.Format("Poisson's mean/SD, dist, a, b, f(phi). . . . . .  {0} {1} {2} {3} {4} {5}",
                                       model.PoissonsRatio.Mean,
                                       model.PoissonsRatio.StandardDeviation,
                                       model.PoissonsRatio.DistributionType.ToDataFileString(),
                                       model.PoissonsRatio.Intercept,
                                       model.PoissonsRatio.Slope,
                                       model.PoissonsRatio.PhiFunc.ToDataFileString()));
                    break;

                default:

                    str.AppendLine(
                        string.Format("Poisson's ratio mean, SD, dist . . . . . . . . .  {0} {1} {2}",
                                      model.PoissonsRatio.Mean,
                                      model.PoissonsRatio.StandardDeviation,
                                      model.PoissonsRatio.DistributionType.ToDataFileString()));
                    break;
            }

            switch (model.UnitWeight.DistributionType)
            {
                case REarthDistributions.Bounded:

                    str.AppendLine(
                        string.Format("Unit weight mean/SD, dist, lower/upper/loc/scale  {0} {1} {2} {3} {4} {5} {6}",
                                      model.UnitWeight.Mean,
                                      model.UnitWeight.StandardDeviation,
                                      model.UnitWeight.DistributionType.ToDataFileString(),
                                      model.UnitWeight.LowerBound,
                                      model.UnitWeight.UpperBound,
                                      model.UnitWeight.Location,
                                      model.UnitWeight.Scale));

                    break;

                case REarthDistributions.fphi:

                    str.AppendLine(
                        String.Format("Unit weight mean/SD, dist, a, b, f(phi). . . . .  {0} {1} {2} {3} {4} {5}",
                                       model.UnitWeight.Mean,
                                       model.UnitWeight.StandardDeviation,
                                       model.UnitWeight.DistributionType.ToDataFileString(),
                                       model.UnitWeight.Intercept,
                                       model.UnitWeight.Slope,
                                       model.UnitWeight.PhiFunc.ToDataFileString()));
                    break;

                default:

                    str.AppendLine(
                        string.Format("Unit weight mean, SD, dist . . . . . . . . . . .  {0} {1} {2}",
                                      model.UnitWeight.Mean,
                                      model.UnitWeight.StandardDeviation,
                                      model.UnitWeight.DistributionType.ToDataFileString()));
                    break;
            }

            switch (model.PressureCoefficient.DistributionType)
            {
                case REarthDistributions.Bounded:

                    str.AppendLine(
                        string.Format("Pressure mean/SD, dist, lower/upper/loc/scale  .  {0} {1} {2} {3} {4} {5} {6}",
                                      model.PressureCoefficient.Mean,
                                      model.PressureCoefficient.StandardDeviation,
                                      model.PressureCoefficient.DistributionType.ToDataFileString(),
                                      model.PressureCoefficient.LowerBound,
                                      model.PressureCoefficient.UpperBound,
                                      model.PressureCoefficient.Location,
                                      model.PressureCoefficient.Scale));

                    break;

                case REarthDistributions.fphi:

                    str.AppendLine(
                        String.Format("Pressure coeff mean/SD, dist, a, b, f(phi) . . .  {0} {1} {2} {3} {4} {5}",
                                       model.PressureCoefficient.Mean,
                                       model.PressureCoefficient.StandardDeviation,
                                       model.PressureCoefficient.DistributionType.ToDataFileString(),
                                       model.PressureCoefficient.Intercept,
                                       model.PressureCoefficient.Slope,
                                       model.PressureCoefficient.PhiFunc.ToDataFileString()));
                    break;

                default:

                    str.AppendLine(
                        string.Format("Pressure coeff mean, SD, dist  . . . . . . . . .  {0} {1} {2}",
                                      model.PressureCoefficient.Mean,
                                      model.PressureCoefficient.StandardDeviation,
                                      model.PressureCoefficient.DistributionType.ToDataFileString()));
                    break;
            }

            str.AppendLine("Number of realizations . . . . . . . . . . . . .  " + model.NumberOfRealizations);
            str.AppendLine("Generator seed (0 for random seed) . . . . . . .  " + model.GeneratorSeed);
            str.AppendLine(
                string.Format("Scale of fluctuation in X [and Y] directions . .  {0} {1}",
                              model.CorrelationLengthInXDir,
                              model.CorrelationLengthInYDir));
            str.AppendLine("Name of covariance function  . . . . . . . . . .  " + model.CovFunction.ToString());

            str.AppendLine();
            str.AppendLine("Material Property Correlation Matrix Data");
            str.AppendLine("=========================================");
            str.AppendLine("Property 1    Property 2    Correlation");
            str.AppendLine("----------    ----------    -----------");

            for (int i = 0; i < 7; i++)
            {
                for (int j = i + 1; j < 7; j++)
                {
                    if (model.CorrelationMatrix[i, j] != 0)
                    {
                        str.AppendLine(
                            string.Format("   {0}            {1}           {2}",
                                          ((REarthSoilProperties)i).ToDataFileString(),
                                          ((REarthSoilProperties)j).ToDataFileString(),
                                          model.CorrelationMatrix[i, j]));
                    }
                }
            }

            str.AppendLine();

            str.AppendLine("Show element boundaries  . . . . . . . . . . . .  " +
                                                                model.ShowMeshOnDisplacedMeshPlot.ToTFString());
            str.AppendLine(
                string.Format("Show random field as background [log?] [prop?] .  {0} {1} {2}",
                              model.ShowRandomFieldOnDisplacedMesh.ToTFString(),
                              model.ShowLogRandomField.ToTFString(),
                              model.DisplacedMeshPropertyToPlot.ToDataFileString()));

            str.AppendLine("Width of displaced mesh output plot in inches  .  " + model.DisplacedMeshWidth);
            str.AppendLine("Number of soil property samples to take  . . . .  " +
                            model.SampleLocations.Where((x) => x.XCoordinate != null && x.YCoordinate != null).
                                            Count());

            str.Append("Locations of samples: (x,y) index pairs  . . . . ");

            foreach (SampleLocation l in model.SampleLocations.Where((x) => x.XCoordinate != null && x.YCoordinate != null))
            {
                str.Append(
                    string.Format(" {0} {1}",
                                  l.XCoordinate,
                                  l.YCoordinate));
            }

            str.Append("\n");

            str.AppendLine("Output sampled soil properties to *.sam? . . . .  " + model.OutputSampledSoilProperties.ToTFString());

            return str.ToString();
        }
        private static string BuildRFlow2DString(RFlow2D model)
        {
            var str = new StringBuilder();

            str.AppendLine(model.JobTitle);

            str.AppendLine("Echo input data to stats file (t/f)? . . . . . .  " + model.EchoInputDataToOutputFile.ToTFString());
            str.AppendLine("Report progress to standard output (t/f)?. . . .  t");
            str.AppendLine("Dump debug data to stats file (t/f)? . . . . . .  " + model.OutputDebugInfo.ToTFString());
            str.AppendLine("Display first log-conductivity field (t/f)?. . .  " +
                model.ProduceDisplayOfFirstLogConductivityField.ToTFString());
            str.AppendLine("Display first stochastic flownet (t/f)?. . . . .  " + model.ProducePSPlotOfFirstFlownet.ToTFString());
            str.AppendLine("Display total head mean and SD fields (t/f)? . .  " + 
                model.ProduceDisplayOfTotalHeadMeanAndStdDev.ToTFString());
            str.AppendLine(string.Format("Output flow, grads, uplifts (t/f)? all grads?  .  {0} {1}",
                model.OutputFlowRateExitGradientUpliftForce.ToTFString(),
                model.OutputDetailedExitGradientInfo.ToTFString()));
            str.AppendLine("Output block conductivity values (t/f)?  . . . .  " + 
                model.OutputBlockHydraulicConductivities.ToTFString());
            str.AppendLine("Output arith, geom, harm cond. values (t/f)? . .  " + 
                model.OutputArithmeticGeometricHarmonicHydraulicConductivityAvgs.ToTFString());
            str.AppendLine("Generate uniform conductivity field (t/f)? . . .  " + 
                model.GenerateUniformConductivityField.ToTFString());
            str.AppendLine("Number of walls  . . . . . . . . . . . . . . . .  " + model.NumberOfWalls.ToString());

            if(model.NumberOfWalls == 0)
            {
                str.AppendLine("Number of elements in the X direction  . . . . .  " + model.NElementsInXDir.ToString());
                str.AppendLine("Number of elements in the Y direction  . . . . .  " + model.NElementsInYDir.ToString());
            }
            else if(model.NumberOfWalls == 1)
            {
                str.AppendLine(string.Format("Number of elements in the X direction  . . . . .  {0} {1}",
                    model.NElementsLeftOfWall.ToString(),
                    model.NElementsRightOfWall.ToString()));
                str.AppendLine(string.Format("Number of elements in the Y direction  . . . . .  {0} {1}",
                    model.NElementsInYDir.ToString(),
                    model.DepthOfWall.ToString()));
            }
            else if(model.NumberOfWalls == 2)
            {
                str.AppendLine(string.Format("Number of elements in the X direction  . . . . .  {0} {1} {2}",
                    model.NElementsLeftOfLeftWall.ToString(),
                    model.NElementsBetweenWall.ToString(),
                    model.NElementsToTheRightOfRightWall));
                str.AppendLine(string.Format("Number of elements in the Y direction  . . . . .  {0} {1} {2}",
                    model.NElementsInYDir.ToString(),
                    model.DepthOfLeftWall.ToString(),
                    model.DepthOfRightWall.ToString()));
            }
            else
            {
                throw new NotImplementedException();
            }

            str.AppendLine(string.Format("Element size . . . . . . . . . . . . . . . . . .  {0} {1}",
                model.ElementDimensionHorrizontal.ToString(),
                model.ElementDimensionVertical.ToString()));

            str.AppendLine("Number of realizations . . . . . . . . . . . . .  " + model.NumberOfRealizations.ToString());
            str.AppendLine("Generator seed (0 for random seed) . . . . . . .  " + model.GeneratorSeed.ToString());
            str.AppendLine(string.Format("Correlation length in X [and Y] directions . . .  {0} {1}",
                model.CorrelationLengthInXDir.ToString(),
                model.CorrelationLengthInYDir.ToString()));
            str.AppendLine("Conductivity mean  . . . . . . . . . . . . . . .  " + model.HydraulicConductivityMean.ToString());
            str.AppendLine("Conductivity standard deviation  . . . . . . . .  " + model.HydraulicConductivityStdDev.ToString());
            str.AppendLine("Name of covariance function used by simulator  .  " + model.CovFunc.ToString());
            str.AppendLine("Number of contours for mean total head . . . . .  " + model.NContoursForMeanTotalHead.ToString());
            str.AppendLine("Number of contours for total head SD . . . . . .  " + model.NContoursForStdDevTotalHead.ToString());
            str.AppendLine("No. of equipotential drops to use for flownet. .  " + model.NEquipotentialDrops.ToString());
            str.AppendLine("Show log-conductivity field on flownet (t/f)?. .  " +
                model.ShowLogConductivityFieldOnFlownet.ToTFString());
            str.AppendLine("Show problem dimensions on flownet (t/f)?. . . .  " + 
                model.ShowSoilMassDimensionsOnFlownet.ToTFString());
            str.AppendLine("Show title and subtitles on flownet (t/f)? . . .  " + 
                model.ShowTitlesOnFlownet.ToTFString());
            str.AppendLine("Flownet plot width on page in inches . . . . . .  " + model.FlownetWidth.ToString());

            return str.ToString();
        }
        private static string BuildRFlow3DString(RFlow3D model)
        {
            var str = new StringBuilder();

            str.AppendLine(model.JobTitle);

            str.AppendLine("Echo input data to stats file (t/f)? . . . . . .  " + model.EchoInputDataToOutputFile.ToTFString());
            str.AppendLine("Report progress to standard output (t/f)?. . . .  t");
            str.AppendLine("Dump debug data to stats file (t/f)? . . . . . .  " + model.OutputDebugInfo.ToTFString());
            str.AppendLine("Output flow, grads, uplifts (t/f)? . . . . . . .  " + 
                model.OutputFlowRateExitGradientUpliftForce.ToTFString());
            str.AppendLine("Output block conductivity values (t/f)?  . . . .  " + 
                model.OutputBlockHydraulicConductivities.ToTFString());
            str.AppendLine("Output arith, geom, harm cond. values (t/f)? . .  " + 
                model.OutputArithmeticGeometricHarmonicHydraulicConductivityAvgs.ToTFString());
            str.AppendLine("Generate uniform conductivity field (t/f)? . . .  " + 
                model.GenerateUniformConductivityField.ToTFString());
            str.AppendLine("Number of walls  . . . . . . . . . . . . . . . .  " + model.NumberOfWalls);

            if (model.NumberOfWalls == 0)
            {
                str.AppendLine("Number of elements in the X direction  . . . . .  " + model.NElementsInXDir.ToString());
                str.AppendLine("Number of elements in the Y direction  . . . . .  " + model.NElementsInYDir.ToString());
            }
            else if (model.NumberOfWalls == 1)
            {
                str.AppendLine(string.Format("Number of elements in the X direction  . . . . .  {0} {1}",
                    model.NElementsLeftOfWall.ToString(),
                    model.NElementsRightOfWall.ToString()));
                str.AppendLine(string.Format("Number of elements in the Y direction  . . . . .  {0} {1}",
                    model.NElementsInYDir.ToString(),
                    model.DepthOfWall.ToString()));
            }
            else if (model.NumberOfWalls == 2)
            {
                str.AppendLine(string.Format("Number of elements in the X direction  . . . . .  {0} {1} {2}",
                    model.NElementsLeftOfLeftWall.ToString(),
                    model.NElementsBetweenWalls.ToString(),
                    model.NElementsRightOfRightWall));
                str.AppendLine(string.Format("Number of elements in the Y direction  . . . . .  {0} {1} {2}",
                    model.NElementsInYDir.ToString(),
                    model.DepthOfLeftWall.ToString(),
                    model.DepthOfRightWall.ToString()));
            }
            else
            {
                throw new NotImplementedException();
            }

            str.AppendLine("Number of elements in the Z direction  . . . . .  " + model.NElementsInZDir.ToString());
            str.AppendLine(string.Format("Element size . . . . . . . . . . . . . . . . . .  {0} {1} {2}",
                model.ElementDimensionXDir.ToString(),
                model.ElementDimensionYDir.ToString(),
                model.ElementDimensionZDir.ToString()));
            str.AppendLine("Number of realizations . . . . . . . . . . . . .  " + model.NumberOfRealizations.ToString());
            str.AppendLine("Generator seed (0 for random seed) . . . . . . .  " + model.GeneratorSeed.ToString());
            str.AppendLine("Conductivity mean  . . . . . . . . . . . . . . .  " + model.HydraulicConductivityMean.ToString());
            str.AppendLine("Conductivity standard deviation  . . . . . . . .  " + model.HydraulicConductivityStdDev.ToString());
            str.AppendLine(string.Format("Correlation length in X, Y, and Z directions . .  {0} {1} {2}",
                model.CorrelationLengthXDir.ToString(),
                model.CorrelationLengthYDir.ToString(),
                model.CorrelationLenghtZDir.ToString()));
            str.AppendLine("Name of covariance function used by simulator  .  " + model.CovFunc.ToString());


            return str.ToString();
        }
        private static string BuildRPill2DString(RPill2D model)
        {
            var str = new StringBuilder();

            str.AppendLine(model.JobTitle);

            str.AppendLine("Echo input data to stats file (t/f)? . . . . . .  " + model.EchoInputDataToOutputFile.ToTFString());
            str.AppendLine("Report progress to standard output (t/f)?. . . .  t");
            str.AppendLine("Dump debug data to stats file (t/f)? . . . . . .  " + model.OutputDebugInfo.ToTFString());
            str.AppendLine(string.Format("Display a random field (t/f)?  . . . . . . . . .  {0} {1} {2}",
                model.PlotFirstRF.ToTFString(), "1", model.FirstRFPropertyToPlot.ToDataFileString()));
            str.AppendLine(string.Format("Display displaced FE mesh (t/f)? . . . . . . . .  {0} {1}",
                model.ProducePSPlotOfFirstDisplacedMesh.ToTFString(),
                "1"));
            str.AppendLine("Normalize pillar capacity (/det)?. . . . . . . .  " + 
                model.NormalizePillarCapacitySamples.ToTFString());
            str.AppendLine("Output pillar capacity samples?. . . . . . . . .  " + model.OutputPillarCapacitySamples.ToTFString());
            str.AppendLine(string.Format("Number of elements in X and Y directions . . . .  {0} {1}",
                model.NElementsInXDir.ToString(),
                model.NElementsInYDir.ToString()));
            str.AppendLine(string.Format("Element size in X and Y directions . . . . . . .  {0} {1}",
                model.ElementSizeInXDir.ToString(),
                model.ElementSizeInYDir.ToString()));
            str.AppendLine("Rough loading conditions (t/f)?  . . . . . . . .  " + model.RoughLoadingCondition.ToTFString());
            str.AppendLine(string.Format("Displacement increment, plastic tol, bearing tol  {0} {1} {2}",
                model.DisplacementInc.ToString(),
                model.PlasticTol.ToString(),
                model.BearingTol.ToString()));
            str.AppendLine(string.Format("Max displacement steps, max iterations . . . . .  {0} {1}",
                model.MaxNumSteps.ToString(),
                model.MaxNumIterations.ToString()));

            if(model.Cohesion.DistributionType == Distribution.Bounded)
            {
                str.AppendLine(string.Format("Cohesion mean/SD, dist, lower/upper/loc/scale  .  {0} {1} {2} {3} {4} {5} {6}",
                    model.Cohesion.Mean.ToString(),
                    model.Cohesion.StandardDeviation.ToString(),
                    model.Cohesion.DistributionType.ToDataFileString(),
                    model.Cohesion.LowerBound.ToString(),
                    model.Cohesion.UpperBound.ToString(),
                    model.Cohesion.Location.ToString(),
                    model.Cohesion.Scale.ToString()));
            }
            else
            {
                str.AppendLine(string.Format("Cohesion mean, SD, dist  . . . . . . . . . . . .  {0} {1} {2}",
                    model.Cohesion.Mean.ToString(),
                    model.Cohesion.StandardDeviation.ToString(),
                    model.Cohesion.DistributionType.ToDataFileString()));
            }


            if (model.FrictionAngle.DistributionType == Distribution.Bounded)
            {
                str.AppendLine(string.Format("Friction mean/SD, dist, lower/upper/loc/scale  .  {0} {1} {2}{3} {4} {5} {6} {7}",
                    model.FrictionAngle.Mean.ToString(),
                    model.FrictionAngle.StandardDeviation.ToString(),
                    model.FrictionAngleType.ToDataFileString(),
                    model.FrictionAngle.DistributionType.ToDataFileString(),
                    model.FrictionAngle.LowerBound.ToString(),
                    model.FrictionAngle.UpperBound.ToString(),
                    model.FrictionAngle.Location.ToString(),
                    model.FrictionAngle.Scale.ToString()));
            }
            else
            {
                str.AppendLine(string.Format("Friction angle mean, SD, dist  . . . . . . . . .  {0} {1} {2}{3}",
                    model.FrictionAngle.Mean.ToString(),
                    model.FrictionAngle.StandardDeviation.ToString(),
                    model.FrictionAngleType.ToDataFileString(),
                    model.FrictionAngle.DistributionType.ToDataFileString()));
            }

            if (model.DilationAngle.DistributionType == Distribution.Bounded)
            {
                str.AppendLine(string.Format("Dilation mean/SD, dist, lower/upper/loc/scale  .  {0} {1} {2} {3} {4} {5} {6}",
                    model.DilationAngle.Mean.ToString(),
                    model.DilationAngle.StandardDeviation.ToString(),
                    model.DilationAngle.DistributionType.ToDataFileString(),
                    model.DilationAngle.LowerBound.ToString(),
                    model.DilationAngle.UpperBound.ToString(),
                    model.DilationAngle.Location.ToString(),
                    model.DilationAngle.Scale.ToString()));
            }
            else
            {
                str.AppendLine(string.Format("Dilation angle mean, SD, dist  . . . . . . . . .  {0} {1} {2}",
                    model.DilationAngle.Mean.ToString(),
                    model.DilationAngle.StandardDeviation.ToString(),
                    model.DilationAngle.DistributionType.ToDataFileString()));
            }

            if (model.ElasticModulus.DistributionType == Distribution.Bounded)
            {
                str.AppendLine(string.Format("Elastic mean/SD, dist, lower/upper/loc/scale . .  {0} {1} {2} {3} {4} {5} {6}",
                    model.ElasticModulus.Mean.ToString(),
                    model.ElasticModulus.StandardDeviation.ToString(),
                    model.ElasticModulus.DistributionType.ToDataFileString(),
                    model.ElasticModulus.LowerBound.ToString(),
                    model.ElasticModulus.UpperBound.ToString(),
                    model.ElasticModulus.Location.ToString(),
                    model.ElasticModulus.Scale.ToString()));
            }
            else
            {
                str.AppendLine(string.Format("Elastic modulus mean, SD, dist . . . . . . . . .  {0} {1} {2}",
                    model.ElasticModulus.Mean.ToString(),
                    model.ElasticModulus.StandardDeviation.ToString(),
                    model.ElasticModulus.DistributionType.ToDataFileString()));
            }

            if (model.PoissonsRatio.DistributionType == Distribution.Bounded)
            {
                str.AppendLine(string.Format("Poisson's mean/SD, dist, lower/upper/loc/scale .  {0} {1} {2} {3} {4} {5} {6}",
                    model.PoissonsRatio.Mean.ToString(),
                    model.PoissonsRatio.StandardDeviation.ToString(),
                    model.PoissonsRatio.DistributionType.ToDataFileString(),
                    model.PoissonsRatio.LowerBound.ToString(),
                    model.PoissonsRatio.UpperBound.ToString(),
                    model.PoissonsRatio.Location.ToString(),
                    model.PoissonsRatio.Scale.ToString()));
            }
            else
            {
                str.AppendLine(string.Format("Poisson's ratio mean, SD, dist . . . . . . . . .  {0} {1} {2}",
                    model.PoissonsRatio.Mean.ToString(),
                    model.PoissonsRatio.StandardDeviation.ToString(),
                    model.PoissonsRatio.DistributionType.ToDataFileString()));
            }

            str.AppendLine("Number of realizations . . . . . . . . . . . . .  " + model.NumberOfRealizations.ToString());
            str.AppendLine("Generator seed (0 for random seed) . . . . . . .  " + model.GeneratorSeed.ToString());
            str.AppendLine(string.Format("Correlation length in X [and Y] directions . . .  {0} {1}",
                model.CorrelationLengthInXDir.ToString(),
                model.CorrelationLengthInYDir.ToString()));
            str.AppendLine("Name of covariance function  . . . . . . . . . .  " + model.CovFunc.ToString());

            str.AppendLine();
            str.AppendLine("Material Property Correlation Matrix Data");
            str.AppendLine("=========================================");
            str.AppendLine("Property 1    Property 2    Correlation");
            str.AppendLine("----------    ----------    -----------");

            for (int i = 0; i < 5; i++)
            {
                for (int j = i + 1; j < 5; j++)
                {
                    if (model.CorrelationMatrix[i, j] != 0)
                    {
                        str.AppendLine(
                            string.Format("   {0}            {1}           {2}",
                                          ((SoilProperty)i).ToDataFileString(),
                                          ((SoilProperty)j).ToDataFileString(),
                                          model.CorrelationMatrix[i, j]));
                    }
                }
            }

            str.AppendLine();

            str.AppendLine("Show element boundaries  . . . . . . . . . . . .  " + model.ShowMeshOnDisplacedMeshPlot.ToTFString());
            str.AppendLine(string.Format("Show random field as background [log?] [prop?] .  {0} {1} {2}",
                model.ShowRFOnDisplacedMeshPlot.ToTFString(),
                model.ShowLogRF.ToTFString(),
                model.DisplacedMeshPropertyToPlot.ToDataFileString()));
            str.AppendLine("Width of displaced mesh output plot in inches  .  " + model.DisplacedMeshPlotWidth.ToString());

            return str.ToString();
        }
        private static string BuildRPill3DString(RPill3D model)
        {
            var str = new StringBuilder();

            str.AppendLine(model.JobTitle);

            str.AppendLine("Echo input data to stats file (t/f)? . . . . . .  " + model.EchoInputDataToOutputFile.ToTFString());
            str.AppendLine("Report progress to standard output (t/f)?. . . .  t");
            str.AppendLine("Dump debug data to stats file (t/f)? . . . . . .  " + model.OutputDebugInfo.ToTFString());
            str.AppendLine(string.Format("Display random field? [realization][prop][plane]  {0} {1} {2} {3} {4} {5}",
                model.PlotFirstRF.ToTFString(), "1",
                model.FirstRFPropertyToPlot.ToDataFileString(),
                model.FirstRFPerpindicularToThisAxis == Axis.XAxis ? model.FirstRFNodeIndexToPlot : 0,
                model.FirstRFPerpindicularToThisAxis == Axis.YAxis ? model.FirstRFNodeIndexToPlot : 0,
                model.FirstRFPerpindicularToThisAxis == Axis.ZAxis ? model.FirstRFNodeIndexToPlot : 0));
            str.AppendLine("Normalize pillar capacity (/det)?. . . . . . . .  " + model.NormalizePillarCapacitySamples.ToTFString());
            str.AppendLine("Output pillar capacity samples?. . . . . . . . .  " + model.OutputCapacitySamples.ToTFString());
            str.AppendLine(string.Format("Number of elements in X, Y, Z directions . . . .  {0} {1} {2}",
                model.NElementsInXDir,
                model.NElementsInYDir,
                model.NElementsInZDir));
            str.AppendLine(string.Format("Element size in X, Y, and Z directions . . . . .  {0} {1} {2}",
                model.ElementSizeInXDir,
                model.ElementSizeInYDir,
                model.ElementSizeInZDir));
            str.AppendLine("Number of nodes per element (8 or 20)  . . . . .  " +
                (model.ElementType == RPill3DElementType.EightNode ? 8 : 20));
            str.AppendLine(string.Format("Displacement increment, plastic tol, bearing tol  {0} {1} {2}",
                model.DisplacementInc,
                model.PlasticTol,
                model.BearingTol));
            str.AppendLine(string.Format("Max displacement steps, max iterations . . . . .  {0} {1}",
                model.MaxNumSteps,
                model.MaxNumIterations));


            if (model.Cohesion.DistributionType == Distribution.Bounded)
            {
                str.AppendLine(string.Format("Cohesion mean/SD, dist, lower/upper/loc/scale  .  {0} {1} {2} {3} {4} {5} {6}",
                    model.Cohesion.Mean.ToString(),
                    model.Cohesion.StandardDeviation.ToString(),
                    model.Cohesion.DistributionType.ToDataFileString(),
                    model.Cohesion.LowerBound.ToString(),
                    model.Cohesion.UpperBound.ToString(),
                    model.Cohesion.Location.ToString(),
                    model.Cohesion.Scale.ToString()));
            }
            else
            {
                str.AppendLine(string.Format("Cohesion mean, SD, dist  . . . . . . . . . . . .  {0} {1} {2}",
                    model.Cohesion.Mean.ToString(),
                    model.Cohesion.StandardDeviation.ToString(),
                    model.Cohesion.DistributionType.ToDataFileString()));
            }


            if (model.FrictionAngle.DistributionType == Distribution.Bounded)
            {
                str.AppendLine(string.Format("Friction mean/SD, dist, lower/upper/loc/scale  .  {0} {1} {2}{3} {4} {5} {6} {7}",
                    model.FrictionAngle.Mean.ToString(),
                    model.FrictionAngle.StandardDeviation.ToString(),
                    model.FrictionAngleType.ToDataFileString(),
                    model.FrictionAngle.DistributionType.ToDataFileString(),
                    model.FrictionAngle.LowerBound.ToString(),
                    model.FrictionAngle.UpperBound.ToString(),
                    model.FrictionAngle.Location.ToString(),
                    model.FrictionAngle.Scale.ToString()));
            }
            else
            {
                str.AppendLine(string.Format("Friction angle mean, SD, dist  . . . . . . . . .  {0} {1} {2}{3}",
                    model.FrictionAngle.Mean.ToString(),
                    model.FrictionAngle.StandardDeviation.ToString(),
                    model.FrictionAngleType.ToDataFileString(),
                    model.FrictionAngle.DistributionType.ToDataFileString()));
            }

            if (model.DilationAngle.DistributionType == Distribution.Bounded)
            {
                str.AppendLine(string.Format("Dilation mean/SD, dist, lower/upper/loc/scale  .  {0} {1} {2} {3} {4} {5} {6}",
                    model.DilationAngle.Mean.ToString(),
                    model.DilationAngle.StandardDeviation.ToString(),
                    model.DilationAngle.DistributionType.ToDataFileString(),
                    model.DilationAngle.LowerBound.ToString(),
                    model.DilationAngle.UpperBound.ToString(),
                    model.DilationAngle.Location.ToString(),
                    model.DilationAngle.Scale.ToString()));
            }
            else
            {
                str.AppendLine(string.Format("Dilation angle mean, SD, dist  . . . . . . . . .  {0} {1} {2}",
                    model.DilationAngle.Mean.ToString(),
                    model.DilationAngle.StandardDeviation.ToString(),
                    model.DilationAngle.DistributionType.ToDataFileString()));
            }

            if (model.ElasticModulus.DistributionType == Distribution.Bounded)
            {
                str.AppendLine(string.Format("Elastic mean/SD, dist, lower/upper/loc/scale . .  {0} {1} {2} {3} {4} {5} {6}",
                    model.ElasticModulus.Mean.ToString(),
                    model.ElasticModulus.StandardDeviation.ToString(),
                    model.ElasticModulus.DistributionType.ToDataFileString(),
                    model.ElasticModulus.LowerBound.ToString(),
                    model.ElasticModulus.UpperBound.ToString(),
                    model.ElasticModulus.Location.ToString(),
                    model.ElasticModulus.Scale.ToString()));
            }
            else
            {
                str.AppendLine(string.Format("Elastic modulus mean, SD, dist . . . . . . . . .  {0} {1} {2}",
                    model.ElasticModulus.Mean.ToString(),
                    model.ElasticModulus.StandardDeviation.ToString(),
                    model.ElasticModulus.DistributionType.ToDataFileString()));
            }

            if (model.PoissonsRatio.DistributionType == Distribution.Bounded)
            {
                str.AppendLine(string.Format("Poisson's mean/SD, dist, lower/upper/loc/scale .  {0} {1} {2} {3} {4} {5} {6}",
                    model.PoissonsRatio.Mean.ToString(),
                    model.PoissonsRatio.StandardDeviation.ToString(),
                    model.PoissonsRatio.DistributionType.ToDataFileString(),
                    model.PoissonsRatio.LowerBound.ToString(),
                    model.PoissonsRatio.UpperBound.ToString(),
                    model.PoissonsRatio.Location.ToString(),
                    model.PoissonsRatio.Scale.ToString()));
            }
            else
            {
                str.AppendLine(string.Format("Poisson's ratio mean, SD, dist . . . . . . . . .  {0} {1} {2}",
                    model.PoissonsRatio.Mean.ToString(),
                    model.PoissonsRatio.StandardDeviation.ToString(),
                    model.PoissonsRatio.DistributionType.ToDataFileString()));
            }

            str.AppendLine("Number of realizations . . . . . . . . . . . . .  " + model.NumberOfRealizations.ToString());
            str.AppendLine("Generator seed (0 for random seed) . . . . . . .  " + model.GeneratorSeed.ToString());
            str.AppendLine(string.Format("Correlation length in X, Y, and Z directions . .  {0} {1} {2}",
                model.CorrelationLengthInXDir,
                model.CorrelationLengthInYDir,
                model.CorrelationLengthInZDir));

            str.AppendLine("Name of covariance function  . . . . . . . . . .  " + model.CovFunc.ToString());

            str.AppendLine();
            str.AppendLine("Material Property Correlation Matrix Data");
            str.AppendLine("=========================================");
            str.AppendLine("Property 1    Property 2    Correlation");
            str.AppendLine("----------    ----------    -----------");

            for (int i = 0; i < 5; i++)
            {
                for (int j = i + 1; j < 5; j++)
                {
                    if (model.CorrelationMatrix[i, j] != 0)
                    {
                        str.AppendLine(
                            string.Format("   {0}            {1}           {2}",
                                          ((SoilProperty)i).ToDataFileString(),
                                          ((SoilProperty)j).ToDataFileString(),
                                          model.CorrelationMatrix[i, j]));
                    }
                }
            }

            str.AppendLine();


            return str.ToString();
        }
        private static string BuildRSetl2DString(RSetl2D model)
        {
            var str = new StringBuilder();

            str.AppendLine(model.JobTitle);

            str.AppendLine("Echo input data to stats file (t/f)? . . . . . .  " + model.EchoInputDataToOutputFile.ToTFString());
            str.AppendLine("Report progress to standard output (t/f)?. . . .  t");
            str.AppendLine("Dump debug data to stats file (t/f)? . . . . . .  " + model.OutputDebugInfo.ToTFString());
            str.AppendLine("Display first log-E field (t/f)? . . . . . . . .  " + 
                model.ProduceDisplayOfFirstLogElasticModulusField.ToTFString());
            str.AppendLine("Display first displaced FE mesh (t/f)? . . . . .  " + 
                model.ProducePSPlotOfFirstDisplacedMesh.ToTFString());
            str.AppendLine("Use K-G Selective Reduced Integration (t/f)? . .  " +
                model.UseKGSelectiveReducedIntegration.ToTFString());
            str.AppendLine("Output settlement statistics (t/f)?  . . . . . .  " + 
                model.OutputSettlementSamplesAndSummaryStats.ToTFString());
            str.AppendLine("Output effective (block) E values (t/f)? . . . .  " + 
                model.OutputBlockModulusSamplesAndSummaryStats.ToTFString());
            str.AppendLine("Output arith/geom/harm ave E-field stats (t/f)?.  " +
                model.OutputFieldAveragedModulusSamplesAndSummaryStats.ToTFString());
            str.AppendLine("Generate uniform E-fields (t/f)? . . . . . . . .  " +
                model.GenerateUniformRandomFields.ToTFString());
            str.AppendLine("Number of elements in the X direction  . . . . .  " +
                model.NElementsInXDir);
            str.AppendLine("Number of elements in the Y direction  . . . . .  " +
                model.NElementsInYDir);
            str.AppendLine(string.Format("Element size . . . . . . . . . . . . . . . . . .  {0} {1}",
                model.ElementSizeInXDir,
                model.ElementSizeInYDir));
            str.AppendLine("Number of realizations . . . . . . . . . . . . .  " +
                model.NumberOfRealizations);
            str.AppendLine("Generator seed (0 for random seed) . . . . . . .  " +
                model.GeneratorSeed);
            str.AppendLine("Poisson's ratio  . . . . . . . . . . . . . . . .  " +
                model.PoissonsRatio);
            str.AppendLine(string.Format("Elastic modulus mean and S.D.  . . . . . . . . .  {0} {1}",
                model.ElasticModulusMean,
                model.ElasticModulusStdDev));
            str.AppendLine(string.Format("Correlation lengths in X [and Y] directions  . .  {0} {1}",
                model.CorrelationLengthInXDir,
                model.CorrelationLengthInYDir));
            str.AppendLine("Name of covariance function  . . . . . . . . . .  " +
                model.CovFunc.ToString());


            str.Append("Settlement stats node list . . . . . . . . . . .  ");

            foreach (int n in model.SettlementNodeList)
            {
                str.Append(" " + n);
            }
            str.Append(Environment.NewLine);

            str.Append("Diff. sett. stats node pair list . . . . . . . .  ");

            foreach(Tuple<int,int> t in model.DifferentialNodePairList)
            {
                str.Append(" " + t.Item1 + "-" + t.Item2);
            }
            str.Append(Environment.NewLine);
            str.AppendLine();
            str.AppendLine("LOADING:");
            str.AppendLine("--------");
            str.AppendLine();
            str.AppendLine("Number of rigid footings (max 5) . . . . . . . .  " + model.RigidFootingLoads.Count());

            int a = 0;
            foreach(RigidFootingLoad2D r in model.RigidFootingLoads)
            {
                str.AppendLine(string.Format("Footing #{0}: left, right nodes, value, type . . .  {1} {2} {3} {4}",
                    a + 1,
                  r.LeftNode,
                  r.RightNode,
                  r.Load,
                  r.RoughInterface ? 1 : 0));
                a++;
            }
            a = 0;

            str.AppendLine("Number of uniformly distributed loads (max 5). .  " + model.UniformLoads.Count());
            foreach(UniformDistributedLoad2D u in model.UniformLoads)
            {
                str.AppendLine(string.Format("UDL #{0}: left, right nodes, value . . . . . . . .  {1} {2} {3}",
                    a + 1,
                    u.LeftNode,
                    u.RightNode,
                    u.Load));
                a++;
            }
            a = 0;
            str.AppendLine("Number of line loads (max 5) . . . . . . . . . .  " + model.LineLoads.Count());
            foreach(LineLoad2D l in model.LineLoads)
            {
                str.AppendLine(string.Format("LLD #{0}: node, value  . . . . . . . . . . . . . .  {1} {2}",
                    a + 1,
                    l.Node,
                    l.Load));
            }
            str.AppendLine();
            str.AppendLine("PLOTTING INFORMATION:");
            str.AppendLine("---------------------");
            str.AppendLine();

            str.AppendLine("Show log-E field on displaced mesh plot (t/f)? .  " + 
                model.OverlayLogElasticModulusFieldOnDisplacedMesh.ToTFString());
            str.AppendLine("Show problem dimensions on displaced mesh plot?.  " + 
                model.ShowProblemDimensionsOnDisplacedMesh.ToTFString());
            str.AppendLine("Show plot titles on displaced mesh plot? . . . .  " + model.ShowTitlesOnDisplacedMesh.ToTFString());
            str.AppendLine(string.Format("Displacement magnification factors (x, y dir). .  {0} {1}",
                model.DisplacementMagInXDir,
                model.DisplacementMagInYDir));
            str.AppendLine("Width of displaced mesh output plot in inches  .  " + model.DisplacedMeshWidth);

            return str.ToString();
        }
        private static string BuildRSetl3DString(RSetl3D model)
        {
            var str = new StringBuilder();

            str.AppendLine(model.JobTitle);

            str.AppendLine("Echo input data to stats file (t/f)? . . . . . .  " + model.EchoInputDataToOutputFile.ToTFString());
            str.AppendLine("Report progress to standard output (t/f)?. . . .  t");
            str.AppendLine("Dump debug data to stats file (t/f)? . . . . . .  " + model.OutputDebugInfo.ToTFString());

            str.AppendLine(string.Format("Display log-E field (t/f)? . . . . . . . . . . .  {0} {1} {2} {3} {4}",
                model.ProduceDisplayOfFirstLogElasticModulusField.ToTFString(),
                "1",
                model.LogElasticModulusFieldPerpindicularToThisAxis == Axis.XAxis ? model.LogElasticModulusFieldNodeIndex : 0,
                model.LogElasticModulusFieldPerpindicularToThisAxis == Axis.YAxis ? model.LogElasticModulusFieldNodeIndex : 0,
                model.LogElasticModulusFieldPerpindicularToThisAxis == Axis.ZAxis ? model.LogElasticModulusFieldNodeIndex : 0
                ));

            str.AppendLine(string.Format("Display first displaced FE mesh (t/f)? . . . . .  {0} {1} {2} {3} {4}",
                model.ProducePSPlotOfDisplacedMesh.ToTFString(),
                "1",
                model.DisplacedMeshPlotPerpindicularToThisAxis == Axis.XAxis ? model.DisplacedMeshNodeIndex : 0,
                model.DisplacedMeshPlotPerpindicularToThisAxis == Axis.YAxis ? model.DisplacedMeshNodeIndex : 0,
                model.DisplacedMeshPlotPerpindicularToThisAxis == Axis.ZAxis ? model.DisplacedMeshNodeIndex : 0
                ));

            str.AppendLine("Output settlement statistics (t/f)?  . . . . . .  " +
                model.OutputSettlementSamplesAndSummaryStats.ToTFString());
            str.AppendLine("Output effective (block) E values (t/f)? . . . .  " +
                model.OutputBlockModulusSamplesAndSummaryStats.ToTFString());
            str.AppendLine("Output arith/geom/harm ave E-field stats (t/f)?.  " +
                model.OutputFieldAveragedModulusSamplesAndSummaryStats.ToTFString());
            str.AppendLine("Generate uniform E-fields (t/f)? . . . . . . . .  " +
                model.GenerateUniformRandomFields.ToTFString());

            str.AppendLine(string.Format("Number of elements in X, Y, Z directions . . . .  {0} {1} {2}",
                model.NElementsInXidr,
                model.NElementsInYDir,
                model.NElementsInZDir));
            str.AppendLine(string.Format("Element size in X, Y, and Z directions . . . . .  {0} {1} {2}",
                model.ElementSizeInXDir,
                model.ElementSizeInYDir,
                model.ElementSizeInZDir));

            str.AppendLine(string.Format("Max iterations, covergence tolerance . . . . . .  {0} {1}",
                model.MaxNumIterations,
                model.ConvergenceTolerance));

            str.AppendLine("Number of realizations . . . . . . . . . . . . .  " +
                model.NumberOfRealizations);
            str.AppendLine("Generator seed (0 for random seed) . . . . . . .  " +
                model.GeneratorSeed);
            str.AppendLine("Poisson's ratio  . . . . . . . . . . . . . . . .  " +
                model.PoissonsRatio);
            str.AppendLine(string.Format("Elastic modulus mean and S.D.  . . . . . . . . .  {0} {1}",
                model.ElasticModulusMean,
                model.ElasticModulusStdDev));

            str.AppendLine(string.Format("Correlation length in X, Y, and Z directions . .  {0} {1} {2}",
                model.CorrelationLengthInXDir,
                model.CorrelationLengthInYDir,
                model.CorrelationLengthInZDir));

            str.AppendLine("Name of covariance function  . . . . . . . . . .  " + model.CovFunc.ToString());

            str.AppendLine();
            str.AppendLine("LOADING:");
            str.AppendLine("--------");
            str.AppendLine();

            str.AppendLine("Number of rigid footings (max 5) . . . . . . . .  " + model.RigidFootingLoads.Count());

            int a = 0;
            foreach(RigidFootingLoads3D r in model.RigidFootingLoads)
            {
                str.AppendLine(string.Format("{0}: x-start, x-end, y-start, y-end, load, sd . .  {1} {2} {3} {4} {5} {6}",
                    a + 1,
                    r.XStart,
                    r.XEnd,
                    r.YStart,
                    r.YEnd,
                    r.LoadMean,
                    r.LoadStdDev));
                a++;

            }
            str.AppendLine();
            str.AppendLine("PLOTTING INFORMATION:");
            str.AppendLine("---------------------");
            str.AppendLine();

            str.AppendLine("Show log-E field on displaced mesh plot (t/f)? .  " + 
                model.OverlayLogElasticModulusOnDisplacedMesh.ToTFString());
            str.AppendLine("Show problem dimensions on displaced mesh plot?.  " +
                model.ShowProblemDimensionsOnDisplacedMesh.ToTFString());
            str.AppendLine("Show plot titles on displaced mesh plot? . . . .  " + model.ShowTitlesOnDisplacedMesh.ToTFString());
            str.AppendLine(string.Format("Displacement magnification factors (x, y dir). .  {0} {1}",
                model.DisplacementMagInXDir,
                model.DisplacementMagInYDir));
            str.AppendLine("Width of displaced mesh output plot in inches  .  " + model.DisplacedMeshPlotWidth);

            return str.ToString();
        }
        private static string BuildRSlope2DString(RSlope2D model)
        {
            var str = new StringBuilder();

            str.AppendLine(model.JobTitle);

            str.AppendLine("Echo input data to stats file (t/f)? . . . . . .  " + model.EchoInputDataToOutputFile.ToTFString());
            str.AppendLine("Report progress to standard output (t/f)?. . . .  t");
            str.AppendLine("Dump debug data to stats file (t/f)? . . . . . .  " + model.OutputDebugInfo.ToTFString());

            str.AppendLine(string.Format("Display a random field (t/f)?  . . . . . . . . .  {0} {1} {2}",
                model.PlotARandomField.ToTFString(),
                "1",
                model.RFPropertyToPlot.ToDataFileString()));

            str.AppendLine(string.Format("Display displaced FE mesh (t/f)? . . . . . . . .  {0} {1}",
                model.ProducePSPlotOfDisplacedMesh.ToTFString(),
                "1"));
            str.AppendLine("Number of x-elements to left of embankment . . .  " + model.NElementsLeftOfEmbark);
            str.AppendLine("Number of x-elements to right of embankment  . .  " + model.NElementsRightOfEmbark);
            str.AppendLine("Number of y-elements in embankment . . . . . . .  " + model.NElementsInEmbark);
            str.AppendLine("Number of y-elements in foundation . . . . . . .  " + model.NElementsInFoundations);
            str.AppendLine("Slope gradient (x/y) . . . . . . . . . . . . . .  " + model.SlopeGradient);
            str.AppendLine(string.Format("Element size . . . . . . . . . . . . . . . . . .  {0} {1}",
                model.ElementSizeInXDir,
                model.ElementSizeInYDir));
            str.AppendLine("Convergence tolerance  . . . . . . . . . . . . .  " + model.ConvergenceTolerance);
            str.AppendLine("Maximum number of iterations . . . . . . . . . .  " + model.MaxNumIterations);

            if (model.Cohesion.DistributionType == Distribution.Bounded)
            {
                str.AppendLine(string.Format("Cohesion mean/SD, dist, lower/upper/loc/scale  .  {0} {1} {2} {3} {4} {5} {6}",
                    model.Cohesion.Mean.ToString(),
                    model.Cohesion.StandardDeviation.ToString(),
                    model.Cohesion.DistributionType.ToDataFileString(),
                    model.Cohesion.LowerBound.ToString(),
                    model.Cohesion.UpperBound.ToString(),
                    model.Cohesion.Location.ToString(),
                    model.Cohesion.Scale.ToString()));
            }
            else
            {
                str.AppendLine(string.Format("Cohesion mean, SD, dist  . . . . . . . . . . . .  {0} {1} {2}",
                    model.Cohesion.Mean.ToString(),
                    model.Cohesion.StandardDeviation.ToString(),
                    model.Cohesion.DistributionType.ToDataFileString()));
            }


            if (model.FrictionAngle.DistributionType == Distribution.Bounded)
            {
                str.AppendLine(string.Format("Friction mean/SD, dist, lower/upper/loc/scale  .  {0} {1} {2}{3} {4} {5} {6} {7}",
                    model.FrictionAngle.Mean.ToString(),
                    model.FrictionAngle.StandardDeviation.ToString(),
                    model.FrictionAngleType.ToDataFileString(),
                    model.FrictionAngle.DistributionType.ToDataFileString(),
                    model.FrictionAngle.LowerBound.ToString(),
                    model.FrictionAngle.UpperBound.ToString(),
                    model.FrictionAngle.Location.ToString(),
                    model.FrictionAngle.Scale.ToString()));
            }
            else
            {
                str.AppendLine(string.Format("Friction angle mean, SD, dist  . . . . . . . . .  {0} {1} {2}{3}",
                    model.FrictionAngle.Mean.ToString(),
                    model.FrictionAngle.StandardDeviation.ToString(),
                    model.FrictionAngleType.ToDataFileString(),
                    model.FrictionAngle.DistributionType.ToDataFileString()));
            }

            if (model.DilationAngle.DistributionType == Distribution.Bounded)
            {
                str.AppendLine(string.Format("Dilation mean/SD, dist, lower/upper/loc/scale  .  {0} {1} {2} {3} {4} {5} {6}",
                    model.DilationAngle.Mean.ToString(),
                    model.DilationAngle.StandardDeviation.ToString(),
                    model.DilationAngle.DistributionType.ToDataFileString(),
                    model.DilationAngle.LowerBound.ToString(),
                    model.DilationAngle.UpperBound.ToString(),
                    model.DilationAngle.Location.ToString(),
                    model.DilationAngle.Scale.ToString()));
            }
            else
            {
                str.AppendLine(string.Format("Dilation angle mean, SD, dist  . . . . . . . . .  {0} {1} {2}",
                    model.DilationAngle.Mean.ToString(),
                    model.DilationAngle.StandardDeviation.ToString(),
                    model.DilationAngle.DistributionType.ToDataFileString()));
            }

            if (model.UnitWeight.DistributionType == Distribution.Bounded)
            {
                str.AppendLine(string.Format("Unit weight mean/SD, dist, lower/upper/loc/scale  {0} {1} {2} {3} {4} {5} {6}",
                    model.UnitWeight.Mean.ToString(),
                    model.UnitWeight.StandardDeviation.ToString(),
                    model.UnitWeight.DistributionType.ToDataFileString(),
                    model.UnitWeight.LowerBound.ToString(),
                    model.UnitWeight.UpperBound.ToString(),
                    model.UnitWeight.Location.ToString(),
                    model.UnitWeight.Scale.ToString()));
            }
            else
            {
                str.AppendLine(string.Format("Unit weight mean, SD, dist . . . . . . . . . . .  {0} {1} {2}",
                    model.UnitWeight.Mean.ToString(),
                    model.UnitWeight.StandardDeviation.ToString(),
                    model.UnitWeight.DistributionType.ToDataFileString()));
            }
            if (model.ElasticModulus.DistributionType == Distribution.Bounded)
            {
                str.AppendLine(string.Format("Elastic mean/SD, dist, lower/upper/loc/scale . .  {0} {1} {2} {3} {4} {5} {6}",
                    model.ElasticModulus.Mean.ToString(),
                    model.ElasticModulus.StandardDeviation.ToString(),
                    model.ElasticModulus.DistributionType.ToDataFileString(),
                    model.ElasticModulus.LowerBound.ToString(),
                    model.ElasticModulus.UpperBound.ToString(),
                    model.ElasticModulus.Location.ToString(),
                    model.ElasticModulus.Scale.ToString()));
            }
            else
            {
                str.AppendLine(string.Format("Elastic modulus mean, SD, dist . . . . . . . . .  {0} {1} {2}",
                    model.ElasticModulus.Mean.ToString(),
                    model.ElasticModulus.StandardDeviation.ToString(),
                    model.ElasticModulus.DistributionType.ToDataFileString()));
            }

            if (model.PoissonsRatio.DistributionType == Distribution.Bounded)
            {
                str.AppendLine(string.Format("Poisson's mean/SD, dist, lower/upper/loc/scale .  {0} {1} {2} {3} {4} {5} {6}",
                    model.PoissonsRatio.Mean.ToString(),
                    model.PoissonsRatio.StandardDeviation.ToString(),
                    model.PoissonsRatio.DistributionType.ToDataFileString(),
                    model.PoissonsRatio.LowerBound.ToString(),
                    model.PoissonsRatio.UpperBound.ToString(),
                    model.PoissonsRatio.Location.ToString(),
                    model.PoissonsRatio.Scale.ToString()));
            }
            else
            {
                str.AppendLine(string.Format("Poisson's ratio mean, SD, dist . . . . . . . . .  {0} {1} {2}",
                    model.PoissonsRatio.Mean.ToString(),
                    model.PoissonsRatio.StandardDeviation.ToString(),
                    model.PoissonsRatio.DistributionType.ToDataFileString()));
            }


            str.Append("Deterministic strength reduction factors . . . . ");
            foreach(double d in model.StrengthReductionFactors)
            {
                str.Append(" " + d);
            }
            str.Append(Environment.NewLine);

            str.AppendLine("Number of realizations . . . . . . . . . . . . .  " + model.NumberOfRealizations);
            str.AppendLine("Generator seed (0 for random seed) . . . . . . .  " + model.GeneratorSeed);
            str.AppendLine(string.Format("Correlation length in X [and Y] directions . . .  {0} {1}",
                model.CorrelationLengthInXDir,
                model.CorrelationLengthInYDir));
            str.AppendLine("Name of covariance function  . . . . . . . . . .  " + model.CovFunc.ToString());

            str.AppendLine();
            str.AppendLine("Material Property Correlation Matrix Data");
            str.AppendLine("=========================================");
            str.AppendLine("Property 1    Property 2    Correlation");
            str.AppendLine("----------    ----------    -----------");

            for (int i = 0; i < 6; i++)
            {
                for (int j = i + 1; j < 6; j++)
                {
                    if (model.CorrelationMatrix[i, j] != 0)
                    {
                        str.AppendLine(
                            string.Format("   {0}            {1}           {2}",
                                          ((RSlopeSoilProperty)i).ToDataFileString(),
                                          ((RSlopeSoilProperty)j).ToDataFileString(),
                                          model.CorrelationMatrix[i, j]));
                    }
                }
            }

            str.AppendLine();

            str.AppendLine("Show element boundaries  . . . . . . . . . . . .  " + model.ShowMeshOnDisplacedMesh.ToTFString());
            str.AppendLine(string.Format("Show random field as background [log?] [prop?] .  {0} {1} {2}",
                model.ShowRFOnDisplacedMesh.ToTFString(),
                model.ShowLogRFOnDisplacedMesh.ToTFString(),
                model.DisplacedMeshPropertyToPlot.ToDataFileString()));
            str.AppendLine("Width of displaced mesh output plot in inches  .  " + model.DisplacedMeshPlotWidth);

            return str.ToString();
        }
        private static void ReplaceNullValuesWithZero(DistributionInfo dist)
        {
            if (dist.Mean == null)
                dist.Mean = 0;
            if (dist.StandardDeviation == null)
                dist.StandardDeviation = 0;
        }
        private static void ReplaceNullValuesWithZero(REarthDistributionInfo dist)
        {
            if (dist.Mean == null)
                dist.Mean = 0;
            if (dist.StandardDeviation == null)
                dist.StandardDeviation = 0;
        }
    }
   
}
