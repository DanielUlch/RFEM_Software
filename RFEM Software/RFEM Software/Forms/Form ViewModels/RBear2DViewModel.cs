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

namespace RFEMSoftware.Simulation.Desktop.Forms
{
    class RBear2DViewModel: INotifyPropertyChanged, IDataErrorInfo, ISimViewModel
    {
        private RBear2D _FormData;
        
         
        private bool _ChangesHaveBeenMade = false;



        private List<string> _Errors;

        
        public ISimModel Model
        {
            get { return _FormData; }
        }
        public string JobTitle
        {
            get { return _FormData.JobTitle; }
            set
            {
                if (_FormData.JobTitle != value)
                {
                    _FormData.JobTitle = value;
                    NotifyPropertyChanged();
                }
            }
        }



        public string BaseName
        {
            get { return _FormData.BaseName; }
            set
            {
                if (_FormData.BaseName != value)
                {
                    _FormData.BaseName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool EchoInputDataToOutputFile
        {
            get { return _FormData.EchoInputDataToOutputFile; }
            set
            {
                if(_FormData.EchoInputDataToOutputFile != value)
                {
                    _FormData.EchoInputDataToOutputFile = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool ReportRunProgress
        {
            get { return _FormData.ReportRunProgress; }
            set
            {
                if(_FormData.ReportRunProgress != value)
                {
                    _FormData.ReportRunProgress = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool WriteDebugDataToOutputFile
        {
            get { return _FormData.WriteDebugDataToOutputFile; }
            set
            {
                if(_FormData.WriteDebugDataToOutputFile != value)
                {
                    _FormData.WriteDebugDataToOutputFile = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool PlotFirstRandomField
        {
            get { return _FormData.PlotFirstRandomField; }
            set
            {
                if(_FormData.PlotFirstRandomField != value)
                {
                    _FormData.PlotFirstRandomField  = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public SoilProperty FirstRandomFieldProperty
        {
            get
            {
                return _FormData.FirstRandomFieldProperty;
            }
            set
            {
                if(_FormData.FirstRandomFieldProperty != value)
                {
                    _FormData.FirstRandomFieldProperty = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool ProducePSPLOTOfFirstFEM
        {
            get { return _FormData.ProducePSPLOTOfFirstFEM; }
            set
            {
                if(_FormData.ProducePSPLOTOfFirstFEM != value)
                {
                    _FormData.ProducePSPLOTOfFirstFEM = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("DisplacedMeshPlotWidth");
                }
            }
        }
        public bool ShowMeshOnDisplacedPlot
        {
            get { return _FormData.ShowMeshOnDisplacedPlot; }
            set
            {
                if(_FormData.ShowMeshOnDisplacedPlot != value)
                {
                    _FormData.ShowMeshOnDisplacedPlot = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowRFOnPSPLOT
        {
            get { return _FormData.ShowRFOnPSPLOT; }
            set
            {
                if(_FormData.ShowRFOnPSPLOT != value)
                {
                    _FormData.ShowRFOnPSPLOT = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowLogRandomField
        {
            get { return _FormData.ShowLogRandomField; }
            set
            {
                if(_FormData.ShowLogRandomField != value)
                {
                    _FormData.ShowLogRandomField = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public SoilProperty PSPLOTProperty
        {
            get { return _FormData.PSPLOTProperty; }
            set
            {
                if(_FormData.PSPLOTProperty != value)
                {
                    _FormData.PSPLOTProperty = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double? DisplacedMeshPlotWidth
        {
            get { return _FormData.DisplacedMeshPlotWidth; }
            set
            {
                if(_FormData.DisplacedMeshPlotWidth != value)
                {
                    _FormData.DisplacedMeshPlotWidth = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool NormalizeBearingCapacitySamples
        {
            get { return _FormData.NormalizeBearingCapacitySamples; }
            set
            {
                if(_FormData.NormalizeBearingCapacitySamples != value)
                {
                    _FormData.NormalizeBearingCapacitySamples = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool OutputBearingCapacitySamples
        {
            get { return _FormData.OutputBearingCapacitySamples; }
            set
            {
                if(_FormData.OutputBearingCapacitySamples != value)
                {
                    _FormData.OutputBearingCapacitySamples = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int? NElementsInXDir
        {
            get { return _FormData.NElementsInXDir; }
            set
            {
                if(_FormData.NElementsInXDir != value)
                {
                    _FormData.NElementsInXDir = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int? NElementsInYDir
        {
            get { return _FormData.NElementsInYDir; }
            set
            {
                if(_FormData.NElementsInYDir != value)
                {
                    _FormData.NElementsInYDir = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double? ElementSizeInXDir
        {
            get { return _FormData.ElementSizeInXDir; }
            set
            {
                if(_FormData.ElementSizeInXDir != value)
                {
                    _FormData.ElementSizeInXDir = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double? ElementSizeInYDir
        {
            get { return _FormData.ElementSizeInYDir; }
            set
            {
                if(_FormData.ElementSizeInYDir != value)
                {
                    _FormData.ElementSizeInYDir = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int NumberOfFootings
        {
            get { return _FormData.NumberOfFootings; }
            set
            {
                if(_FormData.NumberOfFootings !=value)
                {
                    _FormData.NumberOfFootings = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("FootingGap");
                }
            }
        }

        public double? FootingWidth
        {
            get { return _FormData.FootingWidth; }
            set
            {
                if(_FormData.FootingWidth != value)
                {
                    _FormData.FootingWidth = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double? FootingGap
        {
            get { return _FormData.FootingGap; }
            set
            {
                if(_FormData.FootingGap != value)
                {
                    _FormData.FootingGap = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double? DisplacementInc
        {
            get { return _FormData.DisplacementInc; }
            set
            {
                if(_FormData.DisplacementInc != value)
                {
                    _FormData.DisplacementInc = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double? PlasticTol
        {
            get { return _FormData.PlasticTol; }
            set
            {
                if(_FormData.PlasticTol != value)
                {
                    _FormData.PlasticTol = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double? BearingTol
        {
            get { return _FormData.BearingTol; }
            set
            {
                if(_FormData.BearingTol != value)
                {
                    _FormData.BearingTol = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int? MaxNumSteps
        {
            get { return _FormData.MaxNumSteps; }
            set
            {
                if(_FormData.MaxNumSteps != value)
                {
                    _FormData.MaxNumSteps = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int? MaxNumIterations
        {
            get { return _FormData.MaxNumIterations; }
            set
            {
                if(_FormData.MaxNumIterations != value)
                {
                    _FormData.MaxNumIterations = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NSimulations
        {
            get { return _FormData.NumberOfRealizations; }
            set
            {
                if(_FormData.NumberOfRealizations != value)
                {
                    _FormData.NumberOfRealizations = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int? GeneratorSeed
        {
            get { return _FormData.GeneratorSeed; }
            set
            {
                if(_FormData.GeneratorSeed != value)
                {
                    _FormData.GeneratorSeed = value;
                    NotifyPropertyChanged();
                }
            }
                
        }
        public int? CorLengthInXDir
        {
            get { return _FormData.CorLengthInXDir; }
            set
            {
                if(_FormData.CorLengthInXDir != value)
                {
                    _FormData.CorLengthInXDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int? CorLengthInYDir
        {
            get { return _FormData.CorLengthInYDir; }
            set
            {
                if(_FormData.CorLengthInYDir != value)
                {
                    _FormData.CorLengthInYDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public CovarianceFunction  CovFunction
        {
            get { return _FormData.CovFunction; }
            set
            {
                if(_FormData.CovFunction != value)
                {
                    _FormData.CovFunction = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public DistributionInfo CohesionDist
        {
            get { return _FormData.CohesionDist; }
        }
        public DistributionInfo FrictionAngleDist
        {
            get { return _FormData.FrictionAngleDist; }
        }
        
        public FrictionAngle FrictionAngleType
        {
            get { return _FormData.FrictionAngleType; }
            set
            {
                if(_FormData.FrictionAngleType != value)
                {
                    _FormData.FrictionAngleType = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public DistributionInfo DilationAngleDist
        {
            get { return _FormData.DilationAngleDist; }
        }
        public DistributionInfo ElasticModulusDist
        {
            get { return _FormData.ElasticModulusDist; }
        }
        public DistributionInfo PoissonsRatioDist
        {
            get { return _FormData.PoissonsRatioDist; }
        }

        public double?[,] CorrelationMatrix
        {
            get { return _FormData.CorMatrix; }
            set
            {
                if (_FormData.CorMatrix != value)
                {
                    _FormData.CorMatrix = value;
                    NotifyPropertyChanged();
                }
            }
        }

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

        public bool CanDisplaySummaryStats
        {
            get
            {
                return _FormData.CanDisplaySummaryStats;
            }
        }

        public bool CanDisplayMesh
        {
            get
            {
                return _FormData.CanDisplayMesh;
            }
        }

        public bool CanDisplayField
        {
            get
            {
                return _FormData.CanDisplayField;
            }
        }

        public bool CanDisplayBearingHist
        {
            get
            {
                return _FormData.CanDisplayBearingHist;
            }
        }
        public bool ChangesHaveBeenMade
        {
            get { return _ChangesHaveBeenMade; }
        }
        public string DataFilePath
        {
            get { return _FormData.AppDataFileLocation; }
        }
        public string MeshFilePath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                        "\\RFEM_Software\\" + _FormData.BaseName + ".dis";
            }
        }
        public string FieldFilePath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RFEM_Software\\" +
                            _FormData.BaseName + ".fld";
            }
        }
        public string SummaryFilePath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RFEM_Software\\" +
                    _FormData.BaseName + ".stt";
            }
        }
        public string HistFilePath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RFEM_Software\\" +
                    _FormData.BaseName + ".cap";
            }
        }
        public Program Type
        {
            get { return Program.RBear2D; }
        }
        public string this[string columnName]
        {
            get
            {
                return Validate(this, columnName);
            }
        }
        public RBear2DViewModel()
        {
            _FormData = new Infrastructure.Models.RBear2D();
            InitializeLists();

        }
        public RBear2DViewModel(Infrastructure.Models.RBear2D formData)
        {
            _FormData = formData;
            InitializeLists();
        }
        private void InitializeLists()
        {
            _FormData.CohesionDist.AddValidationDelegate(Validate);
            _FormData.FrictionAngleDist.AddValidationDelegate(Validate);
            _FormData.DilationAngleDist.AddValidationDelegate(Validate);
            _FormData.ElasticModulusDist.AddValidationDelegate(Validate);
            _FormData.PoissonsRatioDist.AddValidationDelegate(Validate);

            //_FormData.CohesionDist.PropertyChanged += this.PropertyChanged;

            _FormData.NumberOfFootings = 1;

            _ChangesHaveBeenMade = false;

            _Errors = new List<string>();

            _FormData.PropertyChanged += NotifyFormDataPropertyChanged;
        }
        public string Validate(object sender, string propertyName)
        {
            string validationMessage = string.Empty;

            switch (propertyName)
            {
                case "JobTitle":
                    if (_FormData.JobTitle == null | _FormData.JobTitle == string.Empty)
                    {
                        validationMessage = "Job title can not be null.";
                    }
                    break;
                case "BaseName":
                    if (_FormData.BaseName == null | _FormData.BaseName== string.Empty)
                    {
                        validationMessage = "Base name can not be null.";
                    }
                    break;
                case "FirstRandomFieldProperty":
                    if(_FormData.PlotFirstRandomField == true && 
                        !Enum.IsDefined(typeof(SoilProperty), _FormData.FirstRandomFieldProperty))
                    {
                        validationMessage = "Invalid property type.";
                    }
                    break;
                case "PSPLOTProperty":
                    if(_FormData.ProducePSPLOTOfFirstFEM == true &&
                        _FormData.ShowRFOnPSPLOT == true &&  
                        !Enum.IsDefined(typeof(SoilProperty), _FormData.PSPLOTProperty))
                    {
                        validationMessage = "Invalid property type.";
                    }
                    break;
                case "DisplacedMeshPlotWidth":
                    if(_FormData.ProducePSPLOTOfFirstFEM == true)
                    {
                        if(_FormData.DisplacedMeshPlotWidth == null || _FormData.DisplacedMeshPlotWidth < 0)
                        {
                            validationMessage = "Invalid property value.";
                        }
                    }
                    break;
                case "NElementsInXDir":
                    if(_FormData.NElementsInXDir == null || _FormData.NElementsInXDir < 0 )
                    {
                        validationMessage = "Number of elements must be a positive integer.";
                    }
                    break;
                case "NElementsInYDir":
                    if(_FormData.NElementsInYDir == null || _FormData.NElementsInYDir < 0 )
                    {
                        validationMessage = "Number of elements must be a positive integer.";
                    }
                    break;
                case "ElementSizeInXDir":
                    if(_FormData.ElementSizeInXDir == null || _FormData.ElementSizeInXDir < 0)
                    {
                        validationMessage = "Element size must be a positive number.";
                    }
                    break;
                case "ElementSizeInYDir":
                    if(_FormData.ElementSizeInYDir == null || _FormData.ElementSizeInYDir < 0)
                    {
                        validationMessage = "Element size must be a positive number.";
                    }
                    break;
                case "FootingWidth":
                    if(_FormData.FootingWidth == null || _FormData.FootingWidth < 0)
                    {
                        validationMessage = "Footing width must be a positive number.";
                    }
                    break;
                case "FootingGap":
                    if(_FormData.NumberOfFootings ==2 &&(_FormData.FootingGap == null || _FormData.FootingGap < 0))
                    {
                        validationMessage = "Footing gap must be a positive number.";
                    }
                    break;
                case "DisplacementInc":
                    if(_FormData.DisplacementInc == null || _FormData.DisplacementInc < 0)
                    {
                        validationMessage = "Displacement increment must be a positive number.";
                    }
                    break;
                case "PlasticTol":
                    if(_FormData.PlasticTol == null || _FormData.PlasticTol < 0)
                    {
                        validationMessage = "Plastic tol must be a positive number.";
                    }
                    break;
                case "BearingTol":
                    if(_FormData.BearingTol == null || _FormData.BearingTol < 0)
                    {
                        validationMessage = "Bearing tol must be a positive number.";
                    }
                    break;
                case "MaxNumSteps":
                    if(_FormData.MaxNumSteps == null || _FormData.MaxNumSteps < 0)
                    {
                        validationMessage = "Max number of steps must be a positive integer.";
                    }
                    break;
                case "MaxNumIterations":
                    if(_FormData.MaxNumIterations == null || _FormData.MaxNumIterations <0)
                    {
                        validationMessage = "Max number of iterations must be a positive integer.";
                    }
                    break;
                case "NSimulations":
                    if(_FormData.NumberOfRealizations == null || _FormData.NumberOfRealizations < 0)
                    {
                        validationMessage = "Number of simulations must be a positive integer.";
                    }
                    break;
                case "GeneratorSeed":
                    if(_FormData.GeneratorSeed == null )
                    {
                        validationMessage = "Generator seed must be a number";
                    }
                    break;
                case "CorLengthInXDir":
                    if(_FormData.CorLengthInXDir == null || _FormData.CorLengthInXDir < 0)
                    {
                        validationMessage = "Correlation length in x direction must be a positive number";
                    }
                    break;
                case "CorLengthInYDir":
                    if(_FormData.CorLengthInYDir == null || _FormData.CorLengthInYDir < 0)
                    {
                        validationMessage = "Correlation length in y direction must be a positive number";
                    }
                    break;
                case "Mean":
                    if(sender.GetType() == typeof(DistributionInfo))
                    {
                        if((((DistributionInfo)sender).DistributionType == Distribution.Deterministic ||
                            ((DistributionInfo)sender).DistributionType == Distribution.Normal ||
                            ((DistributionInfo)sender).DistributionType == Distribution.Lognormal)&&
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
                            }else if (((DistributionInfo)sender).StandardDeviation < 0)
                            {
                                validationMessage = "Standard deviation must be a positive value for " +
                                    Enum.GetName(typeof(SoilProperty), ((DistributionInfo)sender).SoilProp);
                            }
                        }
                          
                    }
                    break;
                case "LowerBound":
                    if(sender.GetType() == typeof(DistributionInfo))
                    {
                        if(((DistributionInfo)sender).DistributionType == Distribution.Bounded)
                        {
                            if(((DistributionInfo)sender).LowerBound == null)
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
                    if(_FormData.CorMatrix[0,1] == null)
                    {
                        validationMessage = "Correlation matrix elements must be specified";
                    }
                    else if(_FormData.CorMatrix[0,1] > 1 || _FormData.CorMatrix[0,1] < -1)
                    {
                        validationMessage = "Correlation matrix elements must be >-1 and <1";
                    }
                    break;
                case "CorrCohesionDilation":
                    if (_FormData.CorMatrix[0, 2] == null)
                    {
                        validationMessage = "Correlation matrix elements must be specified";
                    }
                    else if (_FormData.CorMatrix[0, 2] > 1 || _FormData.CorMatrix[0, 2] < -1)
                    {
                        validationMessage = "Correlation matrix elements must be >-1 and <1";
                    }
                    break;
                case "CorrCohesionElasMod":
                    if (_FormData.CorMatrix[0, 3] == null)
                    {
                        validationMessage = "Correlation matrix elements must be specified";
                    }
                    else if (_FormData.CorMatrix[0, 3] > 1 || _FormData.CorMatrix[0, 3] < -1)
                    {
                        validationMessage = "Correlation matrix elements must be >-1 and <1";
                    }
                    break;
                case "CorrCohesionPoissonRT":
                    if (_FormData.CorMatrix[0, 4] == null)
                    {
                        validationMessage = "Correlation matrix elements must be specified";
                    }
                    else if (_FormData.CorMatrix[0, 4] > 1 || _FormData.CorMatrix[0, 4] < -1)
                    {
                        validationMessage = "Correlation matrix elements must be >-1 and <1";
                    }
                    break;
                case "CorrFrictionDilation":
                    if (_FormData.CorMatrix[1, 2] == null)
                    {
                        validationMessage = "Correlation matrix elements must be specified";
                    }
                    else if (_FormData.CorMatrix[1, 2] > 1 || _FormData.CorMatrix[1, 2] < -1)
                    {
                        validationMessage = "Correlation matrix elements must be >-1 and <1";
                    }
                    break;
                case "CorrFrictionElasMod":
                    if (_FormData.CorMatrix[1, 3] == null)
                    {
                        validationMessage = "Correlation matrix elements must be specified";
                    }
                    else if (_FormData.CorMatrix[1, 3] > 1 || _FormData.CorMatrix[1, 3] < -1)
                    {
                        validationMessage = "Correlation matrix elements must be >-1 and <1";
                    }
                    break;
                case "CorrFrictionPoissonRT":
                    if (_FormData.CorMatrix[1, 4] == null)
                    {
                        validationMessage = "Correlation matrix elements must be specified";
                    }
                    else if (_FormData.CorMatrix[1, 4] > 1 || _FormData.CorMatrix[1, 4] < -1)
                    {
                        validationMessage = "Correlation matrix elements must be >-1 and <1";
                    }
                    break;
                case "CorrDilationElasMod":
                    if (_FormData.CorMatrix[2, 3] == null)
                    {
                        validationMessage = "Correlation matrix elements must be specified";
                    }
                    else if (_FormData.CorMatrix[2, 3] > 1 || _FormData.CorMatrix[2, 3] < -1)
                    {
                        validationMessage = "Correlation matrix elements must be >-1 and <1";
                    }
                    break;
                case "CorrDilationPoissionRT":
                    if (_FormData.CorMatrix[2, 4] == null)
                    {
                        validationMessage = "Correlation matrix elements must be specified";
                    }
                    else if (_FormData.CorMatrix[2, 4] > 1 || _FormData.CorMatrix[2, 4] < -1)
                    {
                        validationMessage = "Correlation matrix elements must be >-1 and <1";
                    }
                    break;
                case "CorrElasModPoissonRT":
                    if (_FormData.CorMatrix[3, 4] == null)
                    {
                        validationMessage = "Correlation matrix elements must be specified";
                    }
                    else if (_FormData.CorMatrix[3, 4] > 1 || _FormData.CorMatrix[3, 4] < -1)
                    {
                        validationMessage = "Correlation matrix elements must be >-1 and <1";
                    }
                    break;
            }

            if(validationMessage == string.Empty || validationMessage == null)
            {
                if (_Errors.Contains(propertyName))
                    _Errors.Remove(propertyName);
            }
            else
            {
                if(!_Errors.Contains(propertyName))
                {
                    _Errors.Add(propertyName);
                }
            }
            return validationMessage;
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
                ModelRepository.Store(_FormData);
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
                ModelRepository.Store(_FormData);
                ModelRepository.Store(_FormData, filePath);
                _ChangesHaveBeenMade = false;
            }
        }

        public void ShowMesh()
        {
            //Specify info for ghost view process
            var pInfo = new ProcessStartInfo();
            pInfo.UseShellExecute = true;
            pInfo.FileName = "\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"";
            pInfo.Arguments = "\"" + MeshFilePath + "\"";
            pInfo.CreateNoWindow = false;
            var p = new Process { StartInfo = pInfo };

            //Start the ghostview process
            p.Start();
        }

        public void ShowField()
        {
            var pInfo = new ProcessStartInfo();
            pInfo.UseShellExecute = false;
            pInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                        "\\RFEM_Software";
            string appFileDir = Environment.GetCommandLineArgs()[0];
            string displayFilePath = System.IO.Path.GetDirectoryName(appFileDir);
            displayFilePath += "\\Executables\\display.exe";
            pInfo.FileName = displayFilePath;
            pInfo.CreateNoWindow = true;
            pInfo.Arguments = FieldFilePath;
            var p = new Process { StartInfo = pInfo };


            p.Start();
            p.WaitForExit();

            pInfo = new ProcessStartInfo();
            pInfo.UseShellExecute = false;
            pInfo.FileName = "\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"";
            pInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                        "\\RFEM_Software";
            pInfo.Arguments = pInfo.WorkingDirectory + "\\graph1.ps";
            pInfo.CreateNoWindow = true;

            p = new Process { StartInfo = pInfo };
            p.Start();
        }
        public RBear2DHistForm CreateNewBearingHistForm()
        {
            return new RBear2DHistForm((int)NSimulations, NumberOfFootings, BaseName, HistFilePath);
        }
    }

   
}
