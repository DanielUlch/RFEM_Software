using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.Diagnostics;
using RFEMSoftware.Simulation.Infrastructure.Models;
using RFEMSoftware.Simulation.Infrastructure;
using RFEMSoftware.Simulation.Infrastructure.Persistence;
using System.Windows.Input;
using RFEMSoftware.Simulation.Desktop.CustomControls;
using RFEMSoftware.Simulation.Infrastructure.Wrappers;

namespace RFEMSoftware.Simulation.Desktop.Forms
{
    public class RBear2DViewModel: INotifyPropertyChanged, IDataErrorInfo, ISimViewModel
    {
        private RBear2D _Model;

        private RBear2DForm _View;

        private TopLevelTabItem _MasterTab;
        
        private bool _ChangesHaveBeenMade = false;

        private List<string> _Errors = new List<string>();

        public FileManager FileInfo { get; private set; }

        public TopLevelTabItem MasterTab
        {
            get { return _MasterTab; }
        }
        public ISimModel Model
        {
            get { return _Model; }
        }
        public ISimView View
        {
            get { return _View; }
        }
        public string StorageString
        {
            get
            {
                string s = "";

                foreach (RFEMTabItem tab in _MasterTab.SubTabs)
                {
                    if(tab.Type == RFEMTabType.DataInput)
                    {
                        s += RBear2DItem.DataFile + ",";
                    }
                    else if(tab.Type == RFEMTabType.SummaryStats)
                    {
                        s += RBear2DItem.SummaryStats + ",";
                    }
                    else if(tab.Type == RFEMTabType.Histogram)
                    {
                        s += RBear2DItem.BearingHist + ",";
                    }
                }

                return s.Substring(0, s.Length - 1);
            }
        }
        public RBear2DViewModel(CommandBindingCollection commandBindings, double width,
                               RoutedEventHandler closeTopTab,
                               RoutedEventHandler closeAllTopTabs)
        {
            _Model = new RBear2D();

            _View = new RBear2DForm(this);
            
            _MasterTab = new TopLevelTabItem(commandBindings, width, this, closeTopTab, closeAllTopTabs);

            FileInfo = new FileManager(null);

            _Model.OutputDirectory = FileInfo.OutputDirectory;

            FileInfo.PropertyChanged += OutputDirectoryChanged;

            InitializeLists();

        }
        public RBear2DViewModel(CommandBindingCollection commandBindings, 
                                double width, 
                                RBear2D model,
                                RoutedEventHandler closeTopTab,
                                RoutedEventHandler closeAllTopTabs)
        {
            _Model = model;

            _View = new RBear2DForm(this);

            _MasterTab = new TopLevelTabItem(commandBindings, width, this, closeTopTab, closeAllTopTabs);

            FileInfo = new FileManager(model.DataLocation);

            _Model.OutputDirectory = FileInfo.OutputDirectory;

            FileInfo.PropertyChanged += OutputDirectoryChanged;

            InitializeLists();
        }

        public void ShowSummaryStats()
        {
            _MasterTab.ShowSummaryStats();
        }
        public void ShowDataTab()
        {
            _MasterTab.ShowDataTab();
        }
        public void ShowBearingHistTab()
        {
            _MasterTab.ShowHistogramTab(HistogramType.RBear_Bearing);
        }
        #region Form Properties
        public string JobTitle
        {
            get { return _Model.JobTitle; }
            set
            {
                if (_Model.JobTitle != value)
                {
                    _Model.JobTitle = value;
                    NotifyPropertyChanged();
                }
            }
        }



        public string BaseName
        {
            get { return _Model.BaseName; }
            set
            {
                if (_Model.BaseName != value)
                {
                    _Model.BaseName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool EchoInputDataToOutputFile
        {
            get { return _Model.EchoInputDataToOutputFile; }
            set
            {
                if(_Model.EchoInputDataToOutputFile != value)
                {
                    _Model.EchoInputDataToOutputFile = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool ReportRunProgress
        {
            get { return _Model.ReportRunProgress; }
            set
            {
                if(_Model.ReportRunProgress != value)
                {
                    _Model.ReportRunProgress = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool WriteDebugDataToOutputFile
        {
            get { return _Model.WriteDebugDataToOutputFile; }
            set
            {
                if(_Model.WriteDebugDataToOutputFile != value)
                {
                    _Model.WriteDebugDataToOutputFile = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool PlotFirstRandomField
        {
            get { return _Model.PlotFirstRandomField; }
            set
            {
                if(_Model.PlotFirstRandomField != value)
                {
                    _Model.PlotFirstRandomField  = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public SoilProperty FirstRandomFieldProperty
        {
            get
            {
                return _Model.FirstRandomFieldProperty;
            }
            set
            {
                if(_Model.FirstRandomFieldProperty != value)
                {
                    _Model.FirstRandomFieldProperty = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool ProducePSPLOTOfFirstFEM
        {
            get { return _Model.ProducePSPLOTOfFirstFEM; }
            set
            {
                if(_Model.ProducePSPLOTOfFirstFEM != value)
                {
                    _Model.ProducePSPLOTOfFirstFEM = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("DisplacedMeshPlotWidth");
                }
            }
        }
        public bool ShowMeshOnDisplacedPlot
        {
            get { return _Model.ShowMeshOnDisplacedPlot; }
            set
            {
                if(_Model.ShowMeshOnDisplacedPlot != value)
                {
                    _Model.ShowMeshOnDisplacedPlot = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowRFOnPSPLOT
        {
            get { return _Model.ShowRFOnPSPLOT; }
            set
            {
                if(_Model.ShowRFOnPSPLOT != value)
                {
                    _Model.ShowRFOnPSPLOT = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowLogRandomField
        {
            get { return _Model.ShowLogRandomField; }
            set
            {
                if(_Model.ShowLogRandomField != value)
                {
                    _Model.ShowLogRandomField = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public SoilProperty PSPLOTProperty
        {
            get { return _Model.PSPLOTProperty; }
            set
            {
                if(_Model.PSPLOTProperty != value)
                {
                    _Model.PSPLOTProperty = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double? DisplacedMeshPlotWidth
        {
            get { return _Model.DisplacedMeshPlotWidth; }
            set
            {
                if(_Model.DisplacedMeshPlotWidth != value)
                {
                    _Model.DisplacedMeshPlotWidth = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool NormalizeBearingCapacitySamples
        {
            get { return _Model.NormalizeBearingCapacitySamples; }
            set
            {
                if(_Model.NormalizeBearingCapacitySamples != value)
                {
                    _Model.NormalizeBearingCapacitySamples = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool OutputBearingCapacitySamples
        {
            get { return _Model.OutputBearingCapacitySamples; }
            set
            {
                if(_Model.OutputBearingCapacitySamples != value)
                {
                    _Model.OutputBearingCapacitySamples = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int? NElementsInXDir
        {
            get { return _Model.NElementsInXDir; }
            set
            {
                if(_Model.NElementsInXDir != value)
                {
                    _Model.NElementsInXDir = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int? NElementsInYDir
        {
            get { return _Model.NElementsInYDir; }
            set
            {
                if(_Model.NElementsInYDir != value)
                {
                    _Model.NElementsInYDir = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double? ElementSizeInXDir
        {
            get { return _Model.ElementSizeInXDir; }
            set
            {
                if(_Model.ElementSizeInXDir != value)
                {
                    _Model.ElementSizeInXDir = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double? ElementSizeInYDir
        {
            get { return _Model.ElementSizeInYDir; }
            set
            {
                if(_Model.ElementSizeInYDir != value)
                {
                    _Model.ElementSizeInYDir = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int NumberOfFootings
        {
            get { return _Model.NumberOfFootings; }
            set
            {
                if(_Model.NumberOfFootings !=value)
                {
                    _Model.NumberOfFootings = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("FootingGap");
                }
            }
        }

        public double? FootingWidth
        {
            get { return _Model.FootingWidth; }
            set
            {
                if(_Model.FootingWidth != value)
                {
                    _Model.FootingWidth = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double? FootingGap
        {
            get { return _Model.FootingGap; }
            set
            {
                if(_Model.FootingGap != value)
                {
                    _Model.FootingGap = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double? DisplacementInc
        {
            get { return _Model.DisplacementInc; }
            set
            {
                if(_Model.DisplacementInc != value)
                {
                    _Model.DisplacementInc = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double? PlasticTol
        {
            get { return _Model.PlasticTol; }
            set
            {
                if(_Model.PlasticTol != value)
                {
                    _Model.PlasticTol = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double? BearingTol
        {
            get { return _Model.BearingTol; }
            set
            {
                if(_Model.BearingTol != value)
                {
                    _Model.BearingTol = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int? MaxNumSteps
        {
            get { return _Model.MaxNumSteps; }
            set
            {
                if(_Model.MaxNumSteps != value)
                {
                    _Model.MaxNumSteps = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int? MaxNumIterations
        {
            get { return _Model.MaxNumIterations; }
            set
            {
                if(_Model.MaxNumIterations != value)
                {
                    _Model.MaxNumIterations = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NSimulations
        {
            get { return _Model.NumberOfRealizations; }
            set
            {
                if(_Model.NumberOfRealizations != value)
                {
                    _Model.NumberOfRealizations = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int? GeneratorSeed
        {
            get { return _Model.GeneratorSeed; }
            set
            {
                if(_Model.GeneratorSeed != value)
                {
                    _Model.GeneratorSeed = value;
                    NotifyPropertyChanged();
                }
            }
                
        }
        public int? CorLengthInXDir
        {
            get { return _Model.CorLengthInXDir; }
            set
            {
                if(_Model.CorLengthInXDir != value)
                {
                    _Model.CorLengthInXDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int? CorLengthInYDir
        {
            get { return _Model.CorLengthInYDir; }
            set
            {
                if(_Model.CorLengthInYDir != value)
                {
                    _Model.CorLengthInYDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public CovarianceFunction  CovFunction
        {
            get { return _Model.CovFunction; }
            set
            {
                if(_Model.CovFunction != value)
                {
                    _Model.CovFunction = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public DistributionInfo CohesionDist
        {
            get { return _Model.CohesionDist; }
        }
        public DistributionInfo FrictionAngleDist
        {
            get { return _Model.FrictionAngleDist; }
        }
        
        public FrictionAngle FrictionAngleType
        {
            get { return _Model.FrictionAngleType; }
            set
            {
                if(_Model.FrictionAngleType != value)
                {
                    _Model.FrictionAngleType = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public DistributionInfo DilationAngleDist
        {
            get { return _Model.DilationAngleDist; }
        }
        public DistributionInfo ElasticModulusDist
        {
            get { return _Model.ElasticModulusDist; }
        }
        public DistributionInfo PoissonsRatioDist
        {
            get { return _Model.PoissonsRatioDist; }
        }

        public double?[,] CorrelationMatrix
        {
            get { return _Model.CorMatrix; }
            set
            {
                if (_Model.CorMatrix != value)
                {
                    _Model.CorMatrix = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Validation
        public bool HasErrors
        {
            get
            {
                return (_Errors.Count > 0);
            }
        }

        public string Error
        {
            get
            {
                string result = this[string.Empty];
                if (result != null && result.Trim().Length == 0)
                {
                    result = null;
                }
                return result;
            }
        }
        public string this[string columnName]
        {
            get
            {
                return Validate(this, columnName);
            }
        }
        public string Validate(object sender, string propertyName)
        {
            string validationMessage = string.Empty;

            switch (propertyName)
            {
                case "JobTitle":
                    if (_Model.JobTitle == null | _Model.JobTitle == string.Empty)
                    {
                        validationMessage = "Job title can not be null.";
                    }
                    break;
                case "BaseName":
                    if (_Model.BaseName == null | _Model.BaseName == string.Empty)
                    {
                        validationMessage = "Base name can not be null.";
                    }
                    break;
                case "FirstRandomFieldProperty":
                    if (_Model.PlotFirstRandomField == true &&
                        !Enum.IsDefined(typeof(SoilProperty), _Model.FirstRandomFieldProperty))
                    {
                        validationMessage = "Invalid property type.";
                    }
                    break;
                case "PSPLOTProperty":
                    if (_Model.ProducePSPLOTOfFirstFEM == true &&
                        _Model.ShowRFOnPSPLOT == true &&
                        !Enum.IsDefined(typeof(SoilProperty), _Model.PSPLOTProperty))
                    {
                        validationMessage = "Invalid property type.";
                    }
                    break;
                case "DisplacedMeshPlotWidth":
                    if (_Model.ProducePSPLOTOfFirstFEM == true)
                    {
                        if (_Model.DisplacedMeshPlotWidth == null || _Model.DisplacedMeshPlotWidth < 0)
                        {
                            validationMessage = "Invalid property value.";
                        }
                    }
                    break;
                case "NElementsInXDir":
                    if (_Model.NElementsInXDir == null || _Model.NElementsInXDir < 0)
                    {
                        validationMessage = "Number of elements must be a positive integer.";
                    }
                    break;
                case "NElementsInYDir":
                    if (_Model.NElementsInYDir == null || _Model.NElementsInYDir < 0)
                    {
                        validationMessage = "Number of elements must be a positive integer.";
                    }
                    break;
                case "ElementSizeInXDir":
                    if (_Model.ElementSizeInXDir == null || _Model.ElementSizeInXDir < 0)
                    {
                        validationMessage = "Element size must be a positive number.";
                    }
                    break;
                case "ElementSizeInYDir":
                    if (_Model.ElementSizeInYDir == null || _Model.ElementSizeInYDir < 0)
                    {
                        validationMessage = "Element size must be a positive number.";
                    }
                    break;
                case "FootingWidth":
                    if (_Model.FootingWidth == null || _Model.FootingWidth < 0)
                    {
                        validationMessage = "Footing width must be a positive number.";
                    }
                    break;
                case "FootingGap":
                    if (_Model.NumberOfFootings == 2 && (_Model.FootingGap == null || _Model.FootingGap < 0))
                    {
                        validationMessage = "Footing gap must be a positive number.";
                    }
                    break;
                case "DisplacementInc":
                    if (_Model.DisplacementInc == null || _Model.DisplacementInc < 0)
                    {
                        validationMessage = "Displacement increment must be a positive number.";
                    }
                    break;
                case "PlasticTol":
                    if (_Model.PlasticTol == null || _Model.PlasticTol < 0)
                    {
                        validationMessage = "Plastic tol must be a positive number.";
                    }
                    break;
                case "BearingTol":
                    if (_Model.BearingTol == null || _Model.BearingTol < 0)
                    {
                        validationMessage = "Bearing tol must be a positive number.";
                    }
                    break;
                case "MaxNumSteps":
                    if (_Model.MaxNumSteps == null || _Model.MaxNumSteps < 0)
                    {
                        validationMessage = "Max number of steps must be a positive integer.";
                    }
                    break;
                case "MaxNumIterations":
                    if (_Model.MaxNumIterations == null || _Model.MaxNumIterations < 0)
                    {
                        validationMessage = "Max number of iterations must be a positive integer.";
                    }
                    break;
                case "NSimulations":
                    if (_Model.NumberOfRealizations == null || _Model.NumberOfRealizations < 0)
                    {
                        validationMessage = "Number of simulations must be a positive integer.";
                    }
                    break;
                case "GeneratorSeed":
                    if (_Model.GeneratorSeed == null)
                    {
                        validationMessage = "Generator seed must be a number";
                    }
                    break;
                case "CorLengthInXDir":
                    if (_Model.CorLengthInXDir == null || _Model.CorLengthInXDir < 0)
                    {
                        validationMessage = "Correlation length in x direction must be a positive number";
                    }
                    break;
                case "CorLengthInYDir":
                    if (_Model.CorLengthInYDir == null || _Model.CorLengthInYDir < 0)
                    {
                        validationMessage = "Correlation length in y direction must be a positive number";
                    }
                    break;
                case "Mean":
                    if (sender.GetType() == typeof(DistributionInfo))
                    {
                        if ((((DistributionInfo)sender).DistributionType == Distribution.Deterministic ||
                            ((DistributionInfo)sender).DistributionType == Distribution.Normal ||
                            ((DistributionInfo)sender).DistributionType == Distribution.Lognormal) &&
                            ((DistributionInfo)sender).Mean == null)
                        {
                            validationMessage = "Mean must be specified for " +
                                Enum.GetName(typeof(SoilProperty), ((DistributionInfo)sender).SoilProp) +
                                " " + Enum.GetName(typeof(Distribution), ((DistributionInfo)sender).DistributionType) +
                                " distribution.";
                        }
                    }
                    break;
                case "StandardDev":
                    if (sender.GetType() == typeof(DistributionInfo))
                    {
                        if ((((DistributionInfo)sender).DistributionType == Distribution.Normal ||
                            ((DistributionInfo)sender).DistributionType == Distribution.Lognormal))
                        {
                            if (((DistributionInfo)sender).StandardDeviation == null)
                            {
                                validationMessage = "Standard deviation must be specified for " +
                                    Enum.GetName(typeof(SoilProperty), ((DistributionInfo)sender).SoilProp) +
                                    " " + Enum.GetName(typeof(Distribution), ((DistributionInfo)sender).DistributionType) +
                                    " distribution.";
                            }
                            else if (((DistributionInfo)sender).StandardDeviation < 0)
                            {
                                validationMessage = "Standard deviation must be a positive value for " +
                                    Enum.GetName(typeof(SoilProperty), ((DistributionInfo)sender).SoilProp);
                            }
                        }

                    }
                    break;
                case "LowerBound":
                    if (sender.GetType() == typeof(DistributionInfo))
                    {
                        if (((DistributionInfo)sender).DistributionType == Distribution.Bounded)
                        {
                            if (((DistributionInfo)sender).LowerBound == null)
                            {
                                validationMessage = "Lower bound must be specified for " +
                                Enum.GetName(typeof(SoilProperty), ((DistributionInfo)sender).SoilProp) +
                                " " + Enum.GetName(typeof(Distribution), ((DistributionInfo)sender).DistributionType) +
                                " distribution.";
                            }
                        }
                    }
                    break;
                case "UpperBound":
                    if (sender.GetType() == typeof(DistributionInfo))
                    {
                        if (((DistributionInfo)sender).DistributionType == Distribution.Bounded)
                        {
                            if (((DistributionInfo)sender).UpperBound == null)
                            {
                                validationMessage = "Upper bound must be specified for " +
                                Enum.GetName(typeof(SoilProperty), ((DistributionInfo)sender).SoilProp) +
                                " " + Enum.GetName(typeof(Distribution), ((DistributionInfo)sender).DistributionType) +
                                " distribution.";
                            }
                        }
                    }
                    break;
                case "Location":
                    if (sender.GetType() == typeof(DistributionInfo))
                    {
                        if (((DistributionInfo)sender).DistributionType == Distribution.Bounded)
                        {
                            if (((DistributionInfo)sender).Location == null)
                            {
                                validationMessage = "Location must be specified for " +
                                Enum.GetName(typeof(SoilProperty), ((DistributionInfo)sender).SoilProp) +
                                " " + Enum.GetName(typeof(Distribution), ((DistributionInfo)sender).DistributionType) +
                                " distribution.";
                            }
                        }
                    }
                    break;
                case "Scale":
                    if (sender.GetType() == typeof(DistributionInfo))
                    {
                        if (((DistributionInfo)sender).DistributionType == Distribution.Bounded)
                        {
                            if (((DistributionInfo)sender).Scale == null)
                            {
                                validationMessage = "Scale must be specified for " +
                                Enum.GetName(typeof(SoilProperty), ((DistributionInfo)sender).SoilProp) +
                                " " + Enum.GetName(typeof(Distribution), ((DistributionInfo)sender).DistributionType) +
                                " distribution.";
                            }
                        }
                    }
                    break;
                case "CorrCohesionFriction":
                    if (_Model.CorMatrix[0, 1] == null)
                    {
                        validationMessage = "Correlation matrix elements must be specified";
                    }
                    else if (_Model.CorMatrix[0, 1] > 1 || _Model.CorMatrix[0, 1] < -1)
                    {
                        validationMessage = "Correlation matrix elements must be >-1 and <1";
                    }
                    break;
                case "CorrCohesionDilation":
                    if (_Model.CorMatrix[0, 2] == null)
                    {
                        validationMessage = "Correlation matrix elements must be specified";
                    }
                    else if (_Model.CorMatrix[0, 2] > 1 || _Model.CorMatrix[0, 2] < -1)
                    {
                        validationMessage = "Correlation matrix elements must be >-1 and <1";
                    }
                    break;
                case "CorrCohesionElasMod":
                    if (_Model.CorMatrix[0, 3] == null)
                    {
                        validationMessage = "Correlation matrix elements must be specified";
                    }
                    else if (_Model.CorMatrix[0, 3] > 1 || _Model.CorMatrix[0, 3] < -1)
                    {
                        validationMessage = "Correlation matrix elements must be >-1 and <1";
                    }
                    break;
                case "CorrCohesionPoissonRT":
                    if (_Model.CorMatrix[0, 4] == null)
                    {
                        validationMessage = "Correlation matrix elements must be specified";
                    }
                    else if (_Model.CorMatrix[0, 4] > 1 || _Model.CorMatrix[0, 4] < -1)
                    {
                        validationMessage = "Correlation matrix elements must be >-1 and <1";
                    }
                    break;
                case "CorrFrictionDilation":
                    if (_Model.CorMatrix[1, 2] == null)
                    {
                        validationMessage = "Correlation matrix elements must be specified";
                    }
                    else if (_Model.CorMatrix[1, 2] > 1 || _Model.CorMatrix[1, 2] < -1)
                    {
                        validationMessage = "Correlation matrix elements must be >-1 and <1";
                    }
                    break;
                case "CorrFrictionElasMod":
                    if (_Model.CorMatrix[1, 3] == null)
                    {
                        validationMessage = "Correlation matrix elements must be specified";
                    }
                    else if (_Model.CorMatrix[1, 3] > 1 || _Model.CorMatrix[1, 3] < -1)
                    {
                        validationMessage = "Correlation matrix elements must be >-1 and <1";
                    }
                    break;
                case "CorrFrictionPoissonRT":
                    if (_Model.CorMatrix[1, 4] == null)
                    {
                        validationMessage = "Correlation matrix elements must be specified";
                    }
                    else if (_Model.CorMatrix[1, 4] > 1 || _Model.CorMatrix[1, 4] < -1)
                    {
                        validationMessage = "Correlation matrix elements must be >-1 and <1";
                    }
                    break;
                case "CorrDilationElasMod":
                    if (_Model.CorMatrix[2, 3] == null)
                    {
                        validationMessage = "Correlation matrix elements must be specified";
                    }
                    else if (_Model.CorMatrix[2, 3] > 1 || _Model.CorMatrix[2, 3] < -1)
                    {
                        validationMessage = "Correlation matrix elements must be >-1 and <1";
                    }
                    break;
                case "CorrDilationPoissionRT":
                    if (_Model.CorMatrix[2, 4] == null)
                    {
                        validationMessage = "Correlation matrix elements must be specified";
                    }
                    else if (_Model.CorMatrix[2, 4] > 1 || _Model.CorMatrix[2, 4] < -1)
                    {
                        validationMessage = "Correlation matrix elements must be >-1 and <1";
                    }
                    break;
                case "CorrElasModPoissonRT":
                    if (_Model.CorMatrix[3, 4] == null)
                    {
                        validationMessage = "Correlation matrix elements must be specified";
                    }
                    else if (_Model.CorMatrix[3, 4] > 1 || _Model.CorMatrix[3, 4] < -1)
                    {
                        validationMessage = "Correlation matrix elements must be >-1 and <1";
                    }
                    break;
            }

            if (validationMessage == string.Empty || validationMessage == null)
            {
                if (_Errors.Contains(propertyName))
                    _Errors.Remove(propertyName);
            }
            else
            {
                if (!_Errors.Contains(propertyName))
                {
                    _Errors.Add(propertyName);
                }
            }
            return validationMessage;
        }


        #endregion

        #region State Properties
        public Program Type
        {
            get { return Program.RBear2D; }
        }
        public bool CanDisplaySummaryStats
        {
            get
            {
                return _Model.CanDisplaySummaryStats && System.IO.File.Exists(SummaryFilePath);
            }
        }

        public bool CanDisplayMesh
        {
            get
            {
                return _Model.CanDisplayMesh && System.IO.File.Exists(MeshFilePath);
            }
        }

        public bool CanDisplayField
        {
            get
            {
                return _Model.CanDisplayField && System.IO.File.Exists(FieldFilePath);
            }
        }

        public bool CanDisplayBearingHist
        {
            get
            {
                return _Model.CanDisplayBearingHist && System.IO.File.Exists(HistFilePath);
            }
        }
        public bool ChangesHaveBeenMade
        {
            get { return _ChangesHaveBeenMade; }
        }

        #endregion


        public string DataFilePath
        {
            get { return _Model.DataLocation; }
        }
        public string MeshFilePath
        {
            get
            {
                return FileInfo.OutputDirectory + "\\" + _Model.BaseName + ".dis";
            }
        }
        public string FieldFilePath
        {
            get
            {
                return FileInfo.OutputDirectory + "\\" + _Model.BaseName + ".fld";
            }
        }
        public string SummaryFilePath
        {
            get
            {
                return FileInfo.OutputDirectory + "\\" + _Model.BaseName + ".stt";
            }
        }
        public string HistFilePath
        {
            get
            {
                return FileInfo.OutputDirectory + "\\" + _Model.BaseName + ".cap";
            }
        }

        
        private void OutputDirectoryChanged(object sender, PropertyChangedEventArgs e)
        {
            _Model.OutputDirectory = FileInfo.OutputDirectory;
            SaveAs(DataFilePath);

            NotifyPropertyChanged("CanDisplaySummaryStats");
            NotifyPropertyChanged("CanDisplayMesh");
            NotifyPropertyChanged("CanDisplayField");
            NotifyPropertyChanged("CanDisplayBearingHist");
        }
        
        private void InitializeLists()
        {
            _Model.CohesionDist.AddValidationDelegate(Validate);
            _Model.FrictionAngleDist.AddValidationDelegate(Validate);
            _Model.DilationAngleDist.AddValidationDelegate(Validate);
            _Model.ElasticModulusDist.AddValidationDelegate(Validate);
            _Model.PoissonsRatioDist.AddValidationDelegate(Validate);

            //_FormData.CohesionDist.PropertyChanged += this.PropertyChanged;

            _Model.NumberOfFootings = 1;

            _ChangesHaveBeenMade = false;

            _Errors = new List<string>();

            _Model.PropertyChanged += NotifyFormDataPropertyChanged;
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName="")
        {
            _ChangesHaveBeenMade = true;

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }     
        }
        protected void NotifyFormDataPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _ChangesHaveBeenMade = true;
            PropertyChanged(this, new PropertyChangedEventArgs(e.PropertyName));
        }

        
        public void Save()
        {
            if (HasErrors)
            {
                MessageBox.Show("Please correct errors in form before saving.");
            }
            else
            {
                ModelRepository.Store(_Model);
                _ChangesHaveBeenMade = false;
            }
        }
        public void SaveAs(string filePath)
        {
            if (HasErrors)
            {
                MessageBox.Show("Please correct errors in form before saving.");
            }
            else
            {
                ModelRepository.Store(_Model);
                ModelRepository.Store(_Model, filePath);
                _ChangesHaveBeenMade = false;
            }
        }

        public void ShowMesh()
        {
            GhostViewWrapper gView = new GhostViewWrapper("\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"");

            gView.Show(MeshFilePath );
        }

        public void ShowField()
        {
            DisplayWrapper.Run(FieldFilePath);

            GhostViewWrapper gView = new GhostViewWrapper("\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"");

            gView.Show(FileInfo.OutputDirectory + "\\graph1.ps");
        }
        public RBear2DHistForm CreateNewBearingHistForm()
        {
            return new RBear2DHistForm((int)NSimulations, NumberOfFootings, BaseName, HistFilePath);
        }
    }

   
}
