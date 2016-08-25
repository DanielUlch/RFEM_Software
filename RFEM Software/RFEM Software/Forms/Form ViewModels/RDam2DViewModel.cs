using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Diagnostics;
using RFEMSoftware.Simulation.Infrastructure.Models;
using RFEMSoftware.Simulation.Infrastructure;
using RFEMSoftware.Simulation.Infrastructure.Persistence;
using RFEMSoftware.Simulation.Infrastructure.Wrappers;
using RFEMSoftware.Simulation.Desktop.CustomControls;
using System.Windows.Input;

namespace RFEMSoftware.Simulation.Desktop.Forms
{
    public class RDam2DViewModel : INotifyPropertyChanged, IDataErrorInfo, ISimViewModel
    {
        private bool _ChangesHaveBeenMade = false;
        
        private RDam2D _Model;

        private RDam2DForm _View;

        private TopLevelTabItem _MasterTab;

        private List<string> _Errors = new List<string>();

        public FileManager FileInfo { get; private set; }

        private ObservableCollection<NodeNumber> _NodesForGradientOutput = new ObservableCollection<NodeNumber>()
        {
            new NodeNumber {Index=1, Value=null },
            new NodeNumber {Index =2, Value= null },
            new NodeNumber {Index=3, Value=null },
            new NodeNumber {Index=4, Value=null},
            new NodeNumber {Index=5, Value=null },
            new NodeNumber {Index=6, Value= null }
        };

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

                foreach(RFEMTabItem tab in _MasterTab.SubTabs)
                {
                    if(tab.Type == RFEMTabType.DataInput)
                    {
                        s += RDam2DItem.DataFile + ",";
                    }
                    else if(tab.Type == RFEMTabType.SummaryStats)
                    {
                        s += RDam2DItem.SummaryStats + ",";
                    }
                    else if(tab.Type == RFEMTabType.Histogram)
                    {
                        HistogramType type = ((HistogramTab)tab).ViewModel.Type;

                        if(type == HistogramType.RDam_Conductivity)
                        {
                            s += RDam2DItem.ConductivityHist + ",";
                        }
                        else if(type == HistogramType.RDam_FlowRate)
                        {
                            s += RDam2DItem.FlowRateHist + ",";
                        }
                        else if(type == HistogramType.RDam_NodeGradient)
                        {
                            s += RDam2DItem.NodalGradientHist + ",";
                        }
                    }
                }

                return s.Substring(0, s.Length - 1);
            }
        }

        public RDam2DViewModel(CommandBindingCollection commandBindings, double width,
                               RoutedEventHandler closeTopTab,
                               RoutedEventHandler closeAllTopTabs)
        {

            _Model = new RDam2D();

            _Model.PropertyChanged += NotifyFormDataPropertyChanged;

            foreach (NodeNumber n in _NodesForGradientOutput)
            {
                n.PropertyChanged += NodeNumberCollectionChanged;
            }

            _View = new RDam2DForm(this);

            _MasterTab = new TopLevelTabItem(commandBindings, width, this, closeTopTab, closeAllTopTabs);

            FileInfo = new FileManager(null);

            _Model.OutputDirectory = FileInfo.OutputDirectory;

            FileInfo.PropertyChanged += OutputDirectoryChanged;


        }
        public RDam2DViewModel(CommandBindingCollection commandBindings, double width,RDam2D model,
                               RoutedEventHandler closeTopTab,
                               RoutedEventHandler closeAllTopTabs)
        {
            _Model = model;

            _Model.PropertyChanged += NotifyFormDataPropertyChanged;

            for (int i = 0; i <= 5; i++)
            {
                _NodesForGradientOutput[i].Value = _Model.NodesForGradientOutput[i];
                _NodesForGradientOutput[i].PropertyChanged += NodeNumberCollectionChanged;
            }

            _View = new RDam2DForm(this);

            _MasterTab = new TopLevelTabItem(commandBindings, width, this, closeTopTab, closeAllTopTabs);

            FileInfo = new FileManager(model.DataLocation);

            _Model.OutputDirectory = FileInfo.OutputDirectory;

            FileInfo.PropertyChanged += OutputDirectoryChanged;
        }

        public void ShowSummaryStats()
        {
            _MasterTab.ShowSummaryStats();
        }
        public void ShowDataTab()
        {
            _MasterTab.ShowDataTab();
        }
        public void ShowConductivityHist()
        {
            _MasterTab.ShowHistogramTab(HistogramType.RDam_Conductivity);
        }
        public void ShowFlowRateHist()
        {
            _MasterTab.ShowHistogramTab(HistogramType.RDam_FlowRate);
        }
        public void ShowNodalGradientHist()
        {
            _MasterTab.ShowHistogramTab(HistogramType.RDam_NodeGradient);
        }

        private void OutputDirectoryChanged(object sender, PropertyChangedEventArgs e)
        {
            _Model.OutputDirectory = FileInfo.OutputDirectory;
            SaveAs(DataFilePath);

            NotifyPropertyChanged("CanDisplaySummaryStats");
            NotifyPropertyChanged("CanDisplayFlownet");
            NotifyPropertyChanged("CanDisplayField");
            NotifyPropertyChanged("CanDisplayGradientMeanAndStdDevFields");
            NotifyPropertyChanged("CanDisplayFlowRateHist");
            NotifyPropertyChanged("CanDisplayEffectiveConductivityHist");
            NotifyPropertyChanged("CanDisplayNodeGradientHist");
        }

        #region FormProperties

        public string JobTitle
        {
            get { return _Model.JobTitle; }
            set
            {
                if(_Model.JobTitle != value)
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
                if(_Model.BaseName != value)
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
        public bool OutputDebugInfo
        {
            get { return _Model.OutputDebugInfo; }
            set
            {
                if(_Model.OutputDebugInfo != value)
                {
                    _Model.OutputDebugInfo = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int DebugCode
        {
            get { return _Model.DebugCode; }
            set
            {
                if(_Model.DebugCode != value)
                {
                    _Model.DebugCode = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("RealizationNumber");
                }
            }
        }

        public bool ShowCentroidsOnMesh
        {
            get { return _Model.ShowCentroidsOnMesh; }
            set
            {
                if(_Model.ShowCentroidsOnMesh != value)
                {
                    _Model.ShowCentroidsOnMesh = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int RealizationNumber
        {
            get { return _Model.RealizationNumber; }
            set
            {
                if(_Model.RealizationNumber != value)
                {
                    _Model.RealizationNumber = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool ProduceDisplayFile
        {
            get { return _Model.ProduceDisplayFile; }
            set
            {
                if(_Model.ProduceDisplayFile != value)
                {
                    _Model.ProduceDisplayFile = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ProducePSPlotOfFirstFlownet
        {
            get { return _Model.ProducePSPlotOfFirstFlownet; }
            set
            {
                if(_Model.ProducePSPlotOfFirstFlownet != value)
                {
                    _Model.ProducePSPlotOfFirstFlownet = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("NumEquipotentialDrops");
                    NotifyPropertyChanged("FlownetWidth");
                }
            }
        }
        public bool ShowStreamlines
        {
            get { return _Model.ShowStreamlines; }
            set
            {
                if(_Model.ShowStreamlines != value)
                {
                    _Model.ShowStreamlines = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("NumEquipotentialDrops");
                }
            }
        }
        public bool ShowEquipotentialDrops
        {
            get { return _Model.ShowEquipotentialDrops; }
            set
            {
                if(_Model.ShowEquipotentialDrops != value)
                {
                    _Model.ShowEquipotentialDrops = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NumEquipotentialDrops
        {
            get { return _Model.NumEquipotentialDrops; }
            set
            {
                if(_Model.NumEquipotentialDrops != value)
                {
                    _Model.NumEquipotentialDrops = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowMeshOnFlownet
        {
            get { return _Model.ShowMeshOnFlownet; }
            set
            {
                if(_Model.ShowMeshOnFlownet != value)
                {
                    _Model.ShowMeshOnFlownet = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowLogConductivity
        {
            get { return _Model.ShowLogConductivity; }
            set
            {
                if(_Model.ShowLogConductivity != value)
                {
                    _Model.ShowLogConductivity = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowDamDimensionsOnFlownet
        {
            get { return _Model.ShowDamDimensionsOnFlownet; }
            set
            {
                if(_Model.ShowDamDimensionsOnFlownet != value)
                {
                    _Model.ShowDamDimensionsOnFlownet = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowTitlesOnFlownet
        {
            get { return _Model.ShowTitlesOnFlownet; }
            set
            {
                if(_Model.ShowTitlesOnFlownet != value)
                {
                    _Model.ShowTitlesOnFlownet = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double FlownetWidth
        {
            get { return _Model.FlownetWidth; }
            set
            {
                if(_Model.FlownetWidth != value)
                {
                    _Model.FlownetWidth = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool OutputGradientMeanAndStdDev
        {
            get { return _Model.OutputGradientMeanAndStdDev; }
            set
            {
                if(_Model.OutputGradientMeanAndStdDev != value)
                {
                    _Model.OutputGradientMeanAndStdDev = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public ObservableCollection<NodeNumber> NodesForGradientOutput
        {
            get { return _NodesForGradientOutput; }
            set
            {
                _NodesForGradientOutput = value;
            }
        }
        public bool OutputFlowRate
        {
            get { return _Model.OutputFlowRate; }
            set
            {
                if(_Model.OutputFlowRate != value)
                {
                    _Model.OutputFlowRate = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool OutputBlockConductivities
        {
            get { return _Model.OutputBlockConductivities; }
            set
            {
                if(_Model.OutputBlockConductivities != value)
                {
                    _Model.OutputBlockConductivities = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool OutputConductivityAverages
        {
            get { return _Model.OutputConductivityAverages; }
            set
            {
                if(_Model.OutputConductivityAverages != value)
                {
                    _Model.OutputConductivityAverages = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool GenerateUniformConductivity
        {
            get { return _Model.GenerateUniformConductivity; }
            set
            {
                if (_Model.GenerateUniformConductivity != value)
                {
                    _Model.GenerateUniformConductivity = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NumElementsInXDir
        {
            get { return _Model.NumElementsInXDir; }
            set
            {
                if(_Model.NumElementsInXDir != value)
                {
                    _Model.NumElementsInXDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NumElementsInYDir
        {
            get { return _Model.NumElementsInYDir; }
            set
            {
                if(_Model.NumElementsInYDir != value)
                {
                    _Model.NumElementsInYDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool IsDrainPresent
        {
            get { return _Model.IsDrainPresent; }
            set
            {
                if(_Model.IsDrainPresent != value)
                {
                    _Model.IsDrainPresent = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("DrainXDimension");
                    NotifyPropertyChanged("DrainYDimension");
                }
            }
        }
        public double DrainXDimension
        {
            get { return _Model.DrainXDimension; }
            set
            {
                if(_Model.DrainXDimension != value)
                {
                    _Model.DrainXDimension = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double DrainYDimension
        {
            get { return _Model.DrainYDimension; }
            set
            {
                if(_Model.DrainYDimension != value)
                {
                    _Model.DrainYDimension = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double DrainConductivity
        {
            get { return _Model.DrainConductivity; }
            set
            {
                if(_Model.DrainConductivity != value)
                {
                    _Model.DrainConductivity = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double DamTop
        {
            get { return _Model.DamTop; }
            set
            {
                if(_Model.DamTop != value)
                {
                    _Model.DamTop = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double DamBase
        {
            get { return _Model.DamBase; }
            set
            {
                if (_Model.DamBase != value)
                {
                    _Model.DamBase = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double DamHeight
        {
            get { return _Model.DamHeight; }
            set
            {
                if(_Model.DamHeight != value)
                {
                    _Model.DamHeight = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NumberOfRealizations
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
        public int MaxNumberOfIterations
        {
            get { return _Model.MaxNumberOfIterations; }
            set
            {
                if(_Model.MaxNumberOfIterations != value)
                {
                    _Model.MaxNumberOfIterations = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double ConvergenceTolerance
        {
            get { return _Model.ConvergenceTolerance; }
            set
            {
                if(_Model.ConvergenceTolerance != value)
                {
                    _Model.ConvergenceTolerance = value;
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
        public int CorrelationLengthInXDir
        {
            get { return _Model.CorrelationLengthInXDir; }
            set
            {
                if(_Model.CorrelationLengthInXDir != value)
                {
                    _Model.CorrelationLengthInXDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int CorrelationLengthInYDir
        {
            get { return _Model.CorrelationLengthInYDir; }
            set
            {
                if(_Model.CorrelationLengthInYDir != value)
                {
                    _Model.CorrelationLengthInYDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double ConductivityMean
        {
            get { return _Model.ConductivityMean; }
            set
            {
                if(_Model.ConductivityMean != value)
                {
                    _Model.ConductivityMean = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double ConductivityStdDev
        {
            get { return _Model.ConductivityStdDev; }
            set
            {
                if(_Model.ConductivityStdDev != value)
                {
                    _Model.ConductivityStdDev = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public CovarianceFunction CovFunction
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
        public bool ModifyHoirzontalSpacing
        {
            get { return _Model.ModifyHoirzontalSpacing; }
            set
            {
                if(_Model.ModifyHoirzontalSpacing != value)
                {
                    _Model.ModifyHoirzontalSpacing = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public SpacingAlgorithm SpacingAlgo
        {
            get { return _Model.SpacingAlgo; }
            set
            {
                if(_Model.SpacingAlgo != value)
                {
                    _Model.SpacingAlgo = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool UseAlternateAlgoIfFirstFails
        {
            get { return _Model.UseAlternateAlgoIfFirstFails; }
            set
            {
                if(_Model.UseAlternateAlgoIfFirstFails != value)
                {
                    _Model.UseAlternateAlgoIfFirstFails = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool RestrainFreeSurfaceNonIncreasing
        {
            get { return _Model.RestrainFreeSurfaceNonIncreasing; }
            set
            {
                if(_Model.RestrainFreeSurfaceNonIncreasing != value)
                {
                    _Model.RestrainFreeSurfaceNonIncreasing = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool DampOscillations
        {
            get { return _Model.DampOscillations; }
            set
            {
                if(_Model.DampOscillations != value)
                {
                    _Model.DampOscillations = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region Validation
        private bool HasErrors
        {
            get
            {
                return (_Errors.Count > 0);
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
                    if (string.IsNullOrEmpty(_Model.JobTitle) || _Model.JobTitle.Trim() == "")
                        validationMessage = "Job title must have a value.";
                    break;
                case "BaseName":
                    if (string.IsNullOrEmpty(_Model.BaseName) || _Model.BaseName.Trim()=="")
                        validationMessage = "Base name must have a value.";
                    break;
                case "RealizationNumber":
                    if (_Model.RealizationNumber < 0 & OutputDebugInfo)
                        validationMessage = "Realization number must be a positive integer.";
                    break;
                case "NumEquipotentialDrops":
                    if (_Model.NumEquipotentialDrops < 0 & ProducePSPlotOfFirstFlownet & ShowStreamlines)
                        validationMessage = "Number of equipotential drops must be a positive integer.";
                    break;
                case "FlownetWidth":
                    if (_Model.FlownetWidth <= 0 & ProducePSPlotOfFirstFlownet )
                        validationMessage = "Flownet width must be a positive value.";
                    break;
                case "NumElementsInXDir":
                    if (_Model.NumElementsInXDir < 0)
                        validationMessage = "Number of elements must be a positive integer.";
                    break;
                case "NumElementsInYDir":
                    if(_Model.NumElementsInYDir < 0)
                        validationMessage = "Number of elements must be a positive integer.";
                    break;
                case "DrainXDimension":
                    if (_Model.DrainXDimension <= 0 & IsDrainPresent)
                        validationMessage = "Drain dimensions must be positive values.";
                    break;
                case "DrainYDimension":
                    if (_Model.DrainYDimension <= 0 & IsDrainPresent)
                        validationMessage = "Drain dimensions must be positive values.";
                    break;
                case "DamTop":
                    if (_Model.DamTop <= 0)
                        validationMessage = "Dam dimensions must be positive values.";
                    break;
                case "DamBase":
                    if (_Model.DamBase <= 0)
                        validationMessage = "Dam dimensions must be positive values.";
                    break;
                case "DamHeight":
                    if (_Model.DamHeight <= 0)
                        validationMessage = "Dam dimensions must be positive values.";
                    break;
                case "NumberOfRealizations":
                    if (_Model.NumberOfRealizations < 1)
                        validationMessage = "Number of realizations must be a positive integer.";
                    break;
                case "MaxNumberOfIterations":
                    if (_Model.MaxNumberOfIterations < 1)
                        validationMessage = "Maximum number of iterations must be a positive integer.";
                    break;
                case "ConvergenceTolerance":
                    if (_Model.ConvergenceTolerance < 0 | _Model.ConvergenceTolerance > 1)
                        validationMessage = "Convergence tolerance must be between 0 and 1.";
                    break;
                case "GeneratorSeed":
                    if (_Model.GeneratorSeed < 0)
                        validationMessage = "Generator seed must be a positive integer.";
                    break;
                case "CorrelationLengthInXDir":
                    if (_Model.CorrelationLengthInXDir < 0)
                        validationMessage = "Correlation length must be a positive value.";
                    break;
                case "CorrelationLengthInYDir":
                    if (_Model.CorrelationLengthInYDir < 0)
                        validationMessage = "Correlation length must be a positive value.";
                    break;
                case "ConductivityStdDev":
                    if (_Model.ConductivityStdDev < 0)
                        validationMessage = "Hydraulic conductivity standard deviation must be a positive value.";
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
        #endregion


        public bool CanDisplaySummaryStats
        {
            get
            {
                return _Model.CanDisplaySummaryStats && System.IO.File.Exists(SummaryFilePath);
            }
        }
        public bool CanDisplayFlownet
        {
            get { return _Model.CanDisplayFlownet && System.IO.File.Exists(FlownetFilePath); }
        }
        public bool CanDisplayField
        {
            get { return _Model.CanDisplayField && System.IO.File.Exists(FieldFilePath); }
        }
        public bool CanDisplayGradientMeanAndStdDevFields
        {
            get { return _Model.CanDisplayGradientMeanAndStdDevFields && System.IO.File.Exists(GradientMeanPath); }
        }
        public bool CanDisplayFlowRateHist
        {
            get { return _Model.CanDisplayFlowRateHist && System.IO.File.Exists(FlowRatePath); }
        }
        public bool CanDisplayEffectiveConductivityHist
        {
            get { return _Model.CanDisplayEffectiveConductivityHist && System.IO.File.Exists(EffectiveConductivityPath); }
        }
        public bool CanDisplayNodeGradientHist
        {
            get { return _Model.CanDisplayNodeGradientHist && System.IO.File.Exists(NodalGradientPath); }
        }
        public bool ChangesHaveBeenMade
        {
            get
            {
                return _ChangesHaveBeenMade;
            }
        }


        public string DataFilePath
        {
            get
            {
                return _Model.DataLocation;
            }
        }
        private string FieldFilePath
        {
            get
            {
                return FileInfo.OutputDirectory + "\\" + _Model.BaseName + ".fld";
            }
        }
        private string FlownetFilePath
        {
            get
            {
                return FileInfo.OutputDirectory + "\\" + _Model.BaseName + ".net";
            }
        }
        
        public string SummaryFilePath
        {
            get
            {
                return FileInfo.OutputDirectory + "\\" + _Model.BaseName + ".stt";
            }
        }
        private string GradientMeanPath
        {
            get
            {
                return FileInfo.OutputDirectory + "\\" + _Model.BaseName + ".gpm";
            }
        }
        private string GradientStdDevPath
        {
            get
            {
                return FileInfo.OutputDirectory + "\\" + _Model.BaseName + ".gps";
            }
        }
        private string FluxMeanPath
        {
            get
            {
                return FileInfo.OutputDirectory + "\\" + _Model.BaseName + ".fpm";
            }
        }
        private string FluxStdDevPath
        {
            get
            {
                return FileInfo.OutputDirectory + "\\" + _Model.BaseName + ".fps";
            }
        }
        private string PotentialMeanPath
        {
            get
            {
                return FileInfo.OutputDirectory + "\\" + _Model.BaseName + ".hpm";
            }
        }
        private string PotentialStdDevPath
        {
            get
            {
                return FileInfo.OutputDirectory + "\\" + _Model.BaseName + ".hps";
            }
        }
        private string FlowRatePath
        {
            get
            {
                return FileInfo.OutputDirectory + "\\" + _Model.BaseName + ".flo";
            }
        }
        private string EffectiveConductivityPath
        {
            get
            {
                return FileInfo.OutputDirectory + "\\" + _Model.BaseName + ".cnd";
            }
        }
        private string NodalGradientPath
        {
            get
            {
                return FileInfo.OutputDirectory + "\\" + _Model.BaseName + ".grd";
            }
        }
        public Program Type
        {
            get
            {
                return Program.RDam2D;
            }
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

        public void ShowField()
        {

            DisplayWrapper.Run(FieldFilePath);

            GhostViewWrapper gView = new GhostViewWrapper("\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"");

            gView.Show(FileInfo.OutputDirectory + "\\graph1.ps");
            
        }
        public void ShowFlownet()
        {
            GhostViewWrapper gView = new GhostViewWrapper("\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"");

            gView.Show(FlownetFilePath);
        }
        public void ShowGradientMeanField()
        {
            GhostViewWrapper gView = new GhostViewWrapper("\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"");

            gView.Show(GradientMeanPath);
        }
        public void ShowGradientStdDevField()
        {
            GhostViewWrapper gView = new GhostViewWrapper("\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"");

            gView.Show(GradientStdDevPath);
        }
        public void ShowFluxMeanField()
        {
            GhostViewWrapper gView = new GhostViewWrapper("\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"");

            gView.Show(FluxMeanPath);
        }
        public void ShowFluxStdDevField()
        {
            GhostViewWrapper gView = new GhostViewWrapper("\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"");

            gView.Show(FluxStdDevPath);
        }
        public void ShowPotentialMeanField()
        {
            GhostViewWrapper gView = new GhostViewWrapper("\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"");

            gView.Show(PotentialMeanPath);
        }
        public void ShowPotentialStdDevField()
        {
            GhostViewWrapper gView = new GhostViewWrapper("\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"");

            gView.Show(PotentialStdDevPath);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
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
        private void NodeNumberCollectionChanged(object sender, PropertyChangedEventArgs e) 
        {
            
        }

        public RDam2DFlowRateHist CreateNewFlowRateHistForm()
        {
            return new RDam2DFlowRateHist(_Model.NumberOfRealizations, FlownetFilePath, _Model.BaseName);
        }
        public RDam2DConductivityHist CreateNewConducivityHistForm()
        {

            return new RDam2DConductivityHist(_Model.NumberOfRealizations,
                                                EffectiveConductivityPath,
                                                _Model.BaseName,
                                                _Model.OutputBlockConductivities,
                                                _Model.OutputConductivityAverages);
        }
        public RDam2DNodalGradientHist CreateNewNodalGradientHistForm()
        {

            return new RDam2DNodalGradientHist(_Model.NodesForGradientOutput.Where(x => x!=null).
                                                                                Select(x => x.Value).
                                                                                ToList<int>(),
                                                NodalGradientPath,
                                                _Model.BaseName,
                                                _Model.NumberOfRealizations);
        }
    }



    public class NodeNumber : INotifyPropertyChanged
    {
        private int? _Value;
        public int Index { get; set; }
        public int? Value
        {
            get { return _Value; }
            set
            {
                if (_Value != value)
                {
                    _Value = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }


}
