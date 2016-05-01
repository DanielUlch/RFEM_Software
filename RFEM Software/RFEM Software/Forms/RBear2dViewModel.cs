using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEM_Software.Forms
{
    class RBear2dViewModel: INotifyPropertyChanged, IDataErrorInfo
    {
        private string _JobTitle;
        private string _BaseName;

        private bool _EchoInputDataToOutputFile;
        private bool _ReportRunProgress;
        private bool _WriteDebugDataToOutputFile;
        private bool _PlotFirstRandomField;

        private PlotableProperty _FirstRandomFieldProperty;

        private bool _ProducePSPLOTOfFirstFEM;
        private bool _ShowMeshOnDisplacedPlot;
        private bool _ShowRFOnPSPLOT;
        private bool _ShowLogRandomField;

        private PlotableProperty _PSPLOTProperty;

        private double? _DisplacedMeshPlotWidth;

        private bool _NormalizeBearingCapacitySamples;
        private bool _OutputBearingCapacitySamples;

        private int? _NElementsInXDir;
        private int? _NElementsInYDir;

        private double? _ElementSizeInXDir;
        private double? _ElementSizeInYDir;

        private int _NumberOfFootings;

        private double? _FootingWidth;
        private double? _FootingGap;
        private double? _DisplacementInc;
        private double? _PlasticTol;
        private double? _BearingTol;

        private int? _MaxNumSteps;
        private int? _MaxNumIterations;
        private int? _NSimulations;
        private int? _GeneratorSeed;

        private int? _CorLengthInXDir;
        private int? _CorLengthInYDir;

        private CovarianceFunction _CovFunction;

        private DistributionInfo _CohesionDist;
        private DistributionInfo _FrictionAngleDist;
        private DistributionInfo _DilationAngleDist;
        private DistributionInfo _ElasticModulusDist;
        private DistributionInfo _PoissonsRatioDist;

        private double[,] _CovMatrix;

        private bool _ChangesHaveBeenMade;

        public string JobTitle
        {
            get { return _JobTitle; }
            set
            {
                if (_JobTitle != value)
                {
                    _JobTitle = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string BaseName
        {
            get { return _BaseName; }
            set
            {
                if (_BaseName != value)
                {
                    _BaseName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool EchoInputDataToOutputFile
        {
            get { return _EchoInputDataToOutputFile; }
            set
            {
                if(_EchoInputDataToOutputFile != value)
                {
                    _EchoInputDataToOutputFile = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool ReportRunProgress
        {
            get { return _ReportRunProgress; }
            set
            {
                if(_ReportRunProgress != value)
                {
                    _ReportRunProgress = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool WriteDebugDataToOutputFile
        {
            get { return _WriteDebugDataToOutputFile; }
            set
            {
                if(_WriteDebugDataToOutputFile != value)
                {
                    _WriteDebugDataToOutputFile = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool PlotFirstRandomField
        {
            get { return _PlotFirstRandomField; }
            set
            {
                if(_PlotFirstRandomField != value)
                {
                    _PlotFirstRandomField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public PlotableProperty FirstRandomFieldProperty
        {
            get
            {
                return _FirstRandomFieldProperty;
            }
            set
            {
                if(_FirstRandomFieldProperty != value)
                {
                    _FirstRandomFieldProperty = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool ProducePSPLOTOfFirstFEM
        {
            get { return _ProducePSPLOTOfFirstFEM; }
            set
            {
                if(_ProducePSPLOTOfFirstFEM != value)
                {
                    _ProducePSPLOTOfFirstFEM = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("DisplacedMeshPlotWidth");
                }
            }
        }
        public bool ShowMeshOnDisplacedPlot
        {
            get { return _ShowMeshOnDisplacedPlot; }
            set
            {
                if(_ShowMeshOnDisplacedPlot != value)
                {
                    _ShowMeshOnDisplacedPlot = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowRFOnPSPLOT
        {
            get { return _ShowRFOnPSPLOT; }
            set
            {
                if(_ShowRFOnPSPLOT != value)
                {
                    _ShowRFOnPSPLOT = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowLogRandomField
        {
            get { return _ShowLogRandomField; }
            set
            {
                if(_ShowLogRandomField != value)
                {
                    _ShowLogRandomField = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public PlotableProperty PSPLOTProperty
        {
            get { return _PSPLOTProperty; }
            set
            {
                if(_PSPLOTProperty != value)
                {
                    _PSPLOTProperty = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double? DisplacedMeshPlotWidth
        {
            get { return _DisplacedMeshPlotWidth; }
            set
            {
                if(_DisplacedMeshPlotWidth != value)
                {
                    _DisplacedMeshPlotWidth = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool NormalizeBearingCapacitySamples
        {
            get { return _NormalizeBearingCapacitySamples; }
            set
            {
                if(_NormalizeBearingCapacitySamples != value)
                {
                    _NormalizeBearingCapacitySamples = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool OutputBearingCapacitySamples
        {
            get { return _OutputBearingCapacitySamples; }
            set
            {
                if(_OutputBearingCapacitySamples != value)
                {
                    _OutputBearingCapacitySamples = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int? NElementsInXDir
        {
            get { return _NElementsInXDir; }
            set
            {
                if(_NElementsInXDir != value)
                {
                    _NElementsInXDir = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int? NElementsInYDir
        {
            get { return _NElementsInYDir; }
            set
            {
                if(_NElementsInYDir != value)
                {
                    _NElementsInYDir = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double? ElementSizeInXDir
        {
            get { return _ElementSizeInXDir; }
            set
            {
                if(_ElementSizeInXDir != value)
                {
                    _ElementSizeInXDir = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double? ElementSizeInYDir
        {
            get { return _ElementSizeInYDir; }
            set
            {
                if(_ElementSizeInYDir != value)
                {
                    _ElementSizeInYDir = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int NumberOfFootings
        {
            get { return _NumberOfFootings; }
            set
            {
                if(_NumberOfFootings !=value)
                {
                    _NumberOfFootings = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("FootingGap");
                }
            }
        }

        public double? FootingWidth
        {
            get { return _FootingWidth; }
            set
            {
                if(_FootingWidth != value)
                {
                    _FootingWidth = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double? FootingGap
        {
            get { return _FootingGap; }
            set
            {
                if(_FootingGap != value)
                {
                    _FootingGap = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double? DisplacementInc
        {
            get { return _DisplacementInc; }
            set
            {
                if(_DisplacementInc != value)
                {
                    _DisplacementInc = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double? PlasticTol
        {
            get { return _PlasticTol; }
            set
            {
                if(_PlasticTol != value)
                {
                    _PlasticTol = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double? BearingTol
        {
            get { return _BearingTol; }
            set
            {
                if(_BearingTol != value)
                {
                    _BearingTol = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int? MaxNumSteps
        {
            get { return _MaxNumSteps; }
            set
            {
                if(_MaxNumSteps != value)
                {
                    _MaxNumSteps = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int? MaxNumIterations
        {
            get { return _MaxNumIterations; }
            set
            {
                if(_MaxNumIterations != value)
                {
                    _MaxNumIterations = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int? NSimulations
        {
            get { return _NSimulations; }
            set
            {
                if(_NSimulations != value)
                {
                    _NSimulations = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int? GeneratorSeed
        {
            get { return _GeneratorSeed; }
            set
            {
                if(_GeneratorSeed != value)
                {
                    _GeneratorSeed = value;
                    NotifyPropertyChanged();
                }
            }
                
        }
        public int? CorLengthInXDir
        {
            get { return _CorLengthInXDir; }
            set
            {
                if(_CorLengthInXDir != value)
                {
                    _CorLengthInXDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int? CorLengthInYDir
        {
            get { return _CorLengthInYDir; }
            set
            {
                if(_CorLengthInYDir != value)
                {
                    _CorLengthInYDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public CovarianceFunction  CovFunction
        {
            get { return _CovFunction; }
            set
            {
                if(_CovFunction != value)
                {
                    _CovFunction = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public RBear2dViewModel()
        {
            _CohesionDist = new DistributionInfo();
            _FrictionAngleDist = new DistributionInfo();
            _DilationAngleDist = new DistributionInfo();
            _ElasticModulusDist = new DistributionInfo();
            _PoissonsRatioDist = new DistributionInfo();

            _CovMatrix = new double[,] { { 1, 0, 0, 0, 0 },
                                         { 0, 1, 0, 0, 0 },
                                         { 0, 0, 1, 0, 0 },
                                         { 0, 0, 0, 1, 0 },
                                         { 0, 0, 0, 0, 1 }};
            _NumberOfFootings = 1;

            _ChangesHaveBeenMade = false;
        }

        public string Error
        {
            get
            {
                return ".....";
            }
        }

        public string this[string columnName]
        {
            get
            {
                return Validate(columnName);
            }
        }

        public string Validate(string propertyName)
        {
            string validationMessage = string.Empty;

            switch (propertyName)
            {
                case "JobTitle":
                    if(_JobTitle == null)
                    {
                        validationMessage = "Job title can not be null.";
                    }
                    break;
                case "BaseName":
                    if(_BaseName == null)
                    {
                        validationMessage = "Base name can not be null.";
                    }
                    break;
                case "FirstRandomFieldProperty":
                    if(_PlotFirstRandomField == true && !Enum.IsDefined(typeof(PlotableProperty), _FirstRandomFieldProperty))
                    {
                        validationMessage = "Invalid property type.";
                    }
                    break;
                case "PSPLOTProperty":
                    if(_ProducePSPLOTOfFirstFEM == true &&
                        _ShowRFOnPSPLOT == true &&  
                        Enum.IsDefined(typeof(PlotableProperty), _PSPLOTProperty))
                    {
                        validationMessage = "Invalid property type.";
                    }
                    break;
                case "DisplacedMeshPlotWidth":
                    if(_ProducePSPLOTOfFirstFEM == true)
                    {
                        if(_DisplacedMeshPlotWidth == null || _DisplacedMeshPlotWidth < 0)
                        {
                            validationMessage = "Invalid property value.";
                        }
                    }
                    break;
                case "NElementsInXDir":
                    if(_NElementsInXDir == null || _NElementsInXDir < 0 )
                    {
                        validationMessage = "Number of elements must be a positive integer.";
                    }
                    break;
                case "NElementsInYDir":
                    if(_NElementsInYDir == null || _NElementsInYDir < 0 )
                    {
                        validationMessage = "Number of elements must be a positive integer.";
                    }
                    break;
                case "ElementSizeInXDir":
                    if(_ElementSizeInXDir == null || _ElementSizeInXDir < 0)
                    {
                        validationMessage = "Element size must be a positive number.";
                    }
                    break;
                case "ElementSizeInYDir":
                    if(_ElementSizeInYDir == null || _ElementSizeInYDir < 0)
                    {
                        validationMessage = "Element size must be a positive number.";
                    }
                    break;
                case "FootingWidth":
                    if(_FootingWidth == null || _FootingWidth < 0)
                    {
                        validationMessage = "Footing width must be a positive number.";
                    }
                    break;
                case "FootingGap":
                    if(_NumberOfFootings ==2 &&(_FootingGap == null || _FootingGap < 0))
                    {
                        validationMessage = "Footing gap must be a positive number.";
                    }
                    break;
                case "DisplacementInc":
                    if(_DisplacementInc == null || _DisplacementInc < 0)
                    {
                        validationMessage = "Displacement increment must be a positive number.";
                    }
                    break;
                case "PlasticTol":
                    if(_PlasticTol == null || _PlasticTol < 0)
                    {
                        validationMessage = "Plastic tol must be a positive number.";
                    }
                    break;
                case "BearingTol":
                    if(_BearingTol == null || _BearingTol < 0)
                    {
                        validationMessage = "Bearing tol must be a positive number.";
                    }
                    break;
                case "MaxNumSteps":
                    if(_MaxNumSteps == null || _MaxNumSteps < 0)
                    {
                        validationMessage = "Max number of steps must be a positive integer.";
                    }
                    break;
                case "MaxNumIterations":
                    if(_MaxNumIterations == null || _MaxNumIterations <0)
                    {
                        validationMessage = "Max number of iterations must be a positive integer.";
                    }
                    break;
                case "NSimulations":
                    if(_NSimulations == null || _NSimulations < 0)
                    {
                        validationMessage = "Number of simulations must be a positive integer.";
                    }
                    break;
                case "GeneratorSeed":
                    if(_GeneratorSeed == null )
                    {
                        validationMessage = "Generator seed must be a number";
                    }
                    break;
                case "CorLengthInXDir":
                    if(_CorLengthInXDir == null || _CorLengthInXDir < 0)
                    {
                        validationMessage = "Correlation length in x direction must be a positive number";
                    }
                    break;
                case "CorLengthInYDir":
                    if(_CorLengthInYDir == null || _CorLengthInYDir < 0)
                    {
                        validationMessage = "Correlation length in y direction must be a positive number";
                    }
                    break;
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
    }
   
}
