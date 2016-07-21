using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace RFEM_Software.Custom_Controls
{
    public class RFEMReader: FlowDocumentReader
    {
        private static Dictionary<string, string> _HelpLocations = new Dictionary<string, string>()
        {
            //Default
            {"", "Help Files/AppHelp.xaml" },

            //Common Among Sim Forms
            {"JobTitle", "/Help Files/RBear Help Files/JobTitle.xaml" },
            {"BaseName", "/Help Files/RBear Help Files/BaseName.xaml" },
            {"EchoInputToOutputFile", "/Help Files/RBear Help Files/EchoInputToOutputFile.xaml" },
            

            //RBear2D Files
            {"CohesionDistribution", "/Help Files/RBear Help Files/CohesionDistribution.xaml" },
            {"CorrelationLengths", "/Help Files/RBear Help Files/CorrelationLengths.xaml" },
            {"CovarianceFunction", "/Help Files/RBear Help Files/CovarianceFunction.xaml" },
            {"DilationAngleDistribution", "/Help Files/RBear Help Files/DilationAngleDistribution.xaml" },
            {"DisplacedMeshWidth", "/Help Files/RBear Help Files/DisplacedMeshWidth.xaml" },
            {"DisplacementIncPlasticTolBearingTol", "/Help Files/RBear Help Files/DisplacementIncPlasticTolBearingTol.xaml" },
            {"ElasticModulusDistribution", "Help File/RBear Help Files/ElasticModulusDistribution.xaml"},
            {"ElementSizeInXYDirections", "Help Files/RBear Help Files/ElementSizeInXYDirections.xaml" },
            {"FrictionAngleDistribution", "Help Files/RBear Help Files/FrictionAngleDistribution.xaml" },
            {"GeneratorSeed", "Help Files/RBear Help Files/GeneratorSeed.xaml" },
            {"MaxStepsAndIterations", "Help Files/RBear Help Files/MaxStepsAndIterations.xaml" },
            {"NormalizeCapacitySamples", "Help Files/RBear Help Files/NormalizeCapacitySamples.xaml" },
            {"NSizeOfFootings", "Help Files/RBear Help Files/NSizeOfFootings.xaml" },
            {"NumberOfElementsInXYDirections", "Help Files/RBear Help Files/NumberOfElementsInXYDirections.xaml" },
            {"NumberOfSimulations", "Help Files/RBear Help Files/NumberOfSimulations.xaml" },
            {"OutputCapacitySamples", "Help Files/RBear Help Files/OutputCapacitySamples.xaml" },
            {"OutputDebugData", "Help Files/RBear Help Files/OutputDebugData.xaml" },
            {"PlotFirstRF", "Help Files/RBear Help Files/PlotFirstRF.xaml" },
            {"PoissonRatioDistribution", "Help Files/RBear Help Files/PoissonRatioDistribution.xaml" },
            {"ProducePSPLOTOfFirstFEM", "Help Files/RBear Help Files/ProducePSPLOTOfFirstFEM.xaml" },
            {"PropertyToPlot", "Help Files/RBear Help Files/PropertyToPlot.xaml" },
            {"RBear2dHelp", "Help Files/RBear Help Files/RBear2dHelp.xaml" },
            {"ShowLogRF", "Help Files/RBear Help Files/ShowLogRF.xaml" },
            {"ShowMeshOnDisplacedPlot", "Help Files/RBear Help Files/ShowMeshOnDisplacedPlot.xaml" },
            {"ShowRFOnDisplacedMeshPlot", "Help Files/RBear Help Files/ShowRFOnDisplacedMeshPlot.xaml" },
            {"SoilPropertyCorrelationMatrix", "Help Files/RBear Help Files/SoilPropertyCorrelationMatrix.xaml" },

            //RBear2DHist Files
            { "RBearHistHelp", "/Help Files/RBearHist Help Files/RBearHistHelp.xaml"},
            {"FootingNumberHelp", "/Help Files/RBearHist Help Files/FootingNumberHelp.xaml"},
            {"ShowTitlesHelp", "/Help Files/RBearHist Help Files/ShowTitlesHelp.xaml" },
            { "NumIntervalsHelp", "/Help Files/RBearHist Help Files/NumIntervalsHelp.xaml"},
            { "LengthOriginAxesHelp", "/Help Files/RBearHist Help Files/LengthOriginAxesHelp.xaml"},
            { "LogScaleXAxisHelp", "/Help Files/RBearHist Help Files/LogScaleXAxisHelp.xaml"},
            { "XAxisDetailsHelp", "/Help Files/RBearHist Help Files/XAxisDetailsHelp.xaml" },
            { "YAxisDetailsHelp", "/Help Files/RBearHist Help Files/YAxisDetailsHelp.xaml"},
            { "DistributionHelp", "/Help Files/RBearHist Help Files/DistributionHelp.xaml" },
            { "AndersonDarlingHelp", "/Help Files/RBearHist Help Files/AndersonDarlingHelp.xaml" },
            { "ChiSquareHelp", "/Help Files/RBearHist Help Files/ChiSquareHelp.xaml" },
            { "LineKeyHelp", "/Help Files/RBearHist Help Files/LineKeyHelp.xaml"},

            //RDam2D Files
            {"OutputDebugInformation", "Help Files/RDam Help Files/OutputDebugInformation.xaml" },
            {"ProduceDisplayFile", "Help Files/RDam Help Files/ProduceDisplayFile.xaml" },
            {"ShowStreamlinesOrDrops", "Help Files/RDam Help Files/ShowStreamlinesOrDrops.xaml" },
            {"ShowMeshOnFlownet", "Help Files/RDam Help Files/ShowMeshOnFlownet.xaml" },
            {"ShowLogConductivityFieldOnFlownet", "Help Files/RDam Help Files/ShowLogConductivityFieldOnFlownet.xaml" },
            {"ShowDamDimensionsOnFlownet", "Help Files/RDam Help Files/ShowDamDimensionsOnFlownet.xaml" },
            {"ShowTitlesOnFlownet", "Help Files/RDam Help Files/ShowTitlesOnFlownet.xaml" },
            {"FlownetWidth", "Help Files/RDam Help Files/FlownetWidth.xaml" },
            {"OutputGradientMeanAndStdDev", "Help Files/RDam Help Files/OutputGradientMeanAndStdDev.xaml" },
            {"NodesForGradientOutput", "Help Files/RDam Help Files/NodesForGradientOutput.xaml" },
            {"OutputFlowRate", "Help Files/RDam Help Files/OutputFlowRate.xaml" },
            {"OutputBlockConductivities", "Help Files/RDam Help Files/OutputBlockConductivities.xaml" },
            {"OutputConductivityAverages",  "Help Files/RDam Help Files/OutputConductivityAverages.xaml" },
            {"GenerateUniformConductivity", "Help Files/RDam Help Files/GenerateUniformConductivity.xaml" },
            {"NumberOfFiniteElements", "Help Files/RDam Help Files/NumberOfFiniteElements.xaml" },
            {"DrainDimensionsAndConductivity", "Help Files/RDam Help Files/DrainDimensionsAndConductivity.xaml" },
            {"EarthDamDimensions", "Help Files/RDam Help Files/EarthDamDimensions.xaml" },
            {"NumberOfRealizations", "Help Files/RDam Help Files/NumberOfRealizations.xaml"},
            {"NumberOfIterationsAndConvergence", "Help Files/RDam Help Files/NumberOfIterationsAndConvergence.xaml" },
            {"RDamGeneratorSeed", "Help Files/RDam Help Files/RDamGeneratorSeed.xaml" },
            {"ConductivityMeanAndStdDev", "Help Files/RDam Help Files/ConductivityMeanAndStdDev.xaml" },
            {"RDamCovarianceFunction", "Help Files/RDam Help Files/RDamCovarianceFunction.xaml" },
            {"ModifiedSpacingAlgorithms", "Help Files/RDam Help Files/ModifiedSpacingAlgorithms.xaml" },
            {"UseAlternateAlgoIfFirstFails", "Help Files/RDam Help Files/UseAlternateAlgoIfFirstFails.xaml" },
            {"RestrainFreeSurfaceNonIncreasing", "Help Files/RDam Help Files/RestrainFreeSurfaceNonIncreasing.xaml" },
            {"DampOscillations", "Help Files/RDam Help Files/DampOscillations.xaml" },

            //RDam2D Histogram FIles
            {"FlowRateHistogram", "Help Files/RDamHist Help Files/FlowRateHistogram.xaml" },
            {"FlowPropertyToPlot", "Help Files/RDamHist Help Files/FlowPropertyToPlot.xaml" },
            {"ConductivityHistogram", "Help Files/RDamHist Help Files/ConductivityHistogram.xaml" },
            {"ConductivityTypeToPlot", "Help Files/RDamHist Help Files/ConductivityTypeToPlot.xaml" },
            {"NodalHistograms", "Help Files/RDamHist Help Files/NodalHistograms.xaml" },
            {"NodalHistQuantityToPlot", "Help Files/RDamHist Help Files/NodalHistQuantityToPlot.xaml" },

            //REarth2D Help Files
            {"REarthOutputDebugInformation", "Help Files/REarth Help Files/REarthOutputDebugInformation.xaml" },
            {"PlotFirstRandomField", "Help Files/REarth Help Files/PlotFirstRandomField.xaml" },
            {"REarthProducePSPlotOfFirstFEM","Help Files/REarth Help Files/REarthProducePSPlotOfFirstFEM.xaml" },
            {"ShowMeshOnDisplacedMeshPlot", "Help Files/REarth Help Files/ShowMeshOnDisplacedMeshPlot.xaml" },
            {"ShowRandomFieldOnDisplacedMesh", "Help Files/REarth Help Files/ShowRandomFieldOnDisplacedMesh.xaml" },
            {"REarthDisplacedMeshWidth", "Help Files/REarth Help Files/REarthDisplacedMeshWidth.xaml" },
            {"StoreWallReactionSamples", "Help Files/REarth Help Files/StoreWallReactionSamples.xaml" },
            {"VirtuallySampleSoil", "Help Files/REarth Help Files/VirtuallySampleSoil.xaml" },
            {"SampleLocations", "Help Files/REarth Help Files/SampleLocations.xaml" },
            {"OutputSampledSoilProperties", "Help Files/REarth Help Files/OutputSampledSoilProperties.xaml" },
            {"REarthNumberOfElements", "Help Files/REarth Help Files/REarthNumberOfElements.xaml" },
            {"REarthElementSize", "Help Files/REarth Help Files/REarthElementSize.xaml" },
            {"WallDepthAndRoughness", "Help Files/REarth Help Files/WallDepthAndRoughness.xaml" },
            {"DisplacementIncPlasticTolStressTol", "Help Files/REarth Help Files/DisplacementIncPlasticTolStressTol.xaml" },
            {"REarthMaxNumStepsIterations", "Help Files/REarth Help Files/REarthMaxNumStepsIterations.xaml" },
            {"REarthNumSimulations", "Help Files/REarth Help Files/REarthNumSimulations.xaml" },
            {"REarthGeneratorSeed", "Help Files/REarth Help Files/REarthGeneratorSeed.xaml" },
            {"REarthCorrelationLengths", "Help Files/REarth Help Files/REarthCorrelationLengths.xaml" },
            {"REarthCovarianceFunction", "Help Files/REarth Help Files/REarthCovarianceFunction.xaml" }
        };

        public void LoadHelpTopic(string topic)
        {
            string HelpLocation = _HelpLocations[topic];

            Uri Resource = new Uri(HelpLocation, UriKind.Relative);

            FlowDocument Doc = Application.LoadComponent(Resource) as FlowDocument;

            base.Document = Doc;
        }
        public void LoadHelpTopic()
        {
            string HelpLocation = _HelpLocations[""];

            Uri Resource = new Uri(HelpLocation, UriKind.Relative);

            FlowDocument Doc = Application.LoadComponent(Resource) as FlowDocument;

            base.Document = Doc;
        }
    }
}
