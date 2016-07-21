using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RFEM_Infrastructure;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Diagnostics;

namespace RFEM_Software.Forms
{
    class RDam2DViewModel : INotifyPropertyChanged, IDataErrorInfo, ISimViewModel
    {
        private bool _ChangesHaveBeenMade = false;

        private int _ProgressPercentage;
        private string _CurrentOperation = "Ready";
        private string _ProgressDetails;

        private RDam2D _FormData;

        private List<string> _Errors = new List<string>();

        private ObservableCollection<NodeNumber> _NodesForGradientOutput = new ObservableCollection<NodeNumber>()
        {
            new NodeNumber {Index=1, Value=null },
            new NodeNumber {Index =2, Value= null },
            new NodeNumber {Index=3, Value=null },
            new NodeNumber {Index=4, Value=null},
            new NodeNumber {Index=5, Value=null },
            new NodeNumber {Index=6, Value= null }
        };

        public ISimModel Model
        {
            get { return _FormData; }
        }
        #region FormProperties

        public string JobTitle
        {
            get { return _FormData.JobTitle; }
            set
            {
                if(_FormData.JobTitle != value)
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
                if(_FormData.BaseName != value)
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
        public bool OutputDebugInfo
        {
            get { return _FormData.OutputDebugInfo; }
            set
            {
                if(_FormData.OutputDebugInfo != value)
                {
                    _FormData.OutputDebugInfo = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int DebugCode
        {
            get { return _FormData.DebugCode; }
            set
            {
                if(_FormData.DebugCode != value)
                {
                    _FormData.DebugCode = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("RealizationNumber");
                }
            }
        }

        public bool ShowCentroidsOnMesh
        {
            get { return _FormData.ShowCentroidsOnMesh; }
            set
            {
                if(_FormData.ShowCentroidsOnMesh != value)
                {
                    _FormData.ShowCentroidsOnMesh = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int RealizationNumber
        {
            get { return _FormData.RealizationNumber; }
            set
            {
                if(_FormData.RealizationNumber != value)
                {
                    _FormData.RealizationNumber = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool ProduceDisplayFile
        {
            get { return _FormData.ProduceDisplayFile; }
            set
            {
                if(_FormData.ProduceDisplayFile != value)
                {
                    _FormData.ProduceDisplayFile = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ProducePSPlotOfFirstFlownet
        {
            get { return _FormData.ProducePSPlotOfFirstFlownet; }
            set
            {
                if(_FormData.ProducePSPlotOfFirstFlownet != value)
                {
                    _FormData.ProducePSPlotOfFirstFlownet = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("NumEquipotentialDrops");
                    NotifyPropertyChanged("FlownetWidth");
                }
            }
        }
        public bool ShowStreamlines
        {
            get { return _FormData.ShowStreamlines; }
            set
            {
                if(_FormData.ShowStreamlines != value)
                {
                    _FormData.ShowStreamlines = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("NumEquipotentialDrops");
                }
            }
        }
        public bool ShowEquipotentialDrops
        {
            get { return _FormData.ShowEquipotentialDrops; }
            set
            {
                if(_FormData.ShowEquipotentialDrops != value)
                {
                    _FormData.ShowEquipotentialDrops = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NumEquipotentialDrops
        {
            get { return _FormData.NumEquipotentialDrops; }
            set
            {
                if(_FormData.NumEquipotentialDrops != value)
                {
                    _FormData.NumEquipotentialDrops = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowMeshOnFlownet
        {
            get { return _FormData.ShowMeshOnFlownet; }
            set
            {
                if(_FormData.ShowMeshOnFlownet != value)
                {
                    _FormData.ShowMeshOnFlownet = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowLogConductivity
        {
            get { return _FormData.ShowLogConductivity; }
            set
            {
                if(_FormData.ShowLogConductivity != value)
                {
                    _FormData.ShowLogConductivity = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowDamDimensionsOnFlownet
        {
            get { return _FormData.ShowDamDimensionsOnFlownet; }
            set
            {
                if(_FormData.ShowDamDimensionsOnFlownet != value)
                {
                    _FormData.ShowDamDimensionsOnFlownet = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowTitlesOnFlownet
        {
            get { return _FormData.ShowTitlesOnFlownet; }
            set
            {
                if(_FormData.ShowTitlesOnFlownet != value)
                {
                    _FormData.ShowTitlesOnFlownet = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double FlownetWidth
        {
            get { return _FormData.FlownetWidth; }
            set
            {
                if(_FormData.FlownetWidth != value)
                {
                    _FormData.FlownetWidth = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool OutputGradientMeanAndStdDev
        {
            get { return _FormData.OutputGradientMeanAndStdDev; }
            set
            {
                if(_FormData.OutputGradientMeanAndStdDev != value)
                {
                    _FormData.OutputGradientMeanAndStdDev = value;
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
            get { return _FormData.OutputFlowRate; }
            set
            {
                if(_FormData.OutputFlowRate != value)
                {
                    _FormData.OutputFlowRate = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool OutputBlockConductivities
        {
            get { return _FormData.OutputBlockConductivities; }
            set
            {
                if(_FormData.OutputBlockConductivities != value)
                {
                    _FormData.OutputBlockConductivities = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool OutputConductivityAverages
        {
            get { return _FormData.OutputConductivityAverages; }
            set
            {
                if(_FormData.OutputConductivityAverages != value)
                {
                    _FormData.OutputConductivityAverages = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool GenerateUniformConductivity
        {
            get { return _FormData.GenerateUniformConductivity; }
            set
            {
                if (_FormData.GenerateUniformConductivity != value)
                {
                    _FormData.GenerateUniformConductivity = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NumElementsInXDir
        {
            get { return _FormData.NumElementsInXDir; }
            set
            {
                if(_FormData.NumElementsInXDir != value)
                {
                    _FormData.NumElementsInXDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NumElementsInYDir
        {
            get { return _FormData.NumElementsInYDir; }
            set
            {
                if(_FormData.NumElementsInYDir != value)
                {
                    _FormData.NumElementsInYDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool IsDrainPresent
        {
            get { return _FormData.IsDrainPresent; }
            set
            {
                if(_FormData.IsDrainPresent != value)
                {
                    _FormData.IsDrainPresent = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("DrainXDimension");
                    NotifyPropertyChanged("DrainYDimension");
                }
            }
        }
        public double DrainXDimension
        {
            get { return _FormData.DrainXDimension; }
            set
            {
                if(_FormData.DrainXDimension != value)
                {
                    _FormData.DrainXDimension = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double DrainYDimension
        {
            get { return _FormData.DrainYDimension; }
            set
            {
                if(_FormData.DrainYDimension != value)
                {
                    _FormData.DrainYDimension = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double DrainConductivity
        {
            get { return _FormData.DrainConductivity; }
            set
            {
                if(_FormData.DrainConductivity != value)
                {
                    _FormData.DrainConductivity = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double DamTop
        {
            get { return _FormData.DamTop; }
            set
            {
                if(_FormData.DamTop != value)
                {
                    _FormData.DamTop = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double DamBase
        {
            get { return _FormData.DamBase; }
            set
            {
                if (_FormData.DamBase != value)
                {
                    _FormData.DamBase = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double DamHeight
        {
            get { return _FormData.DamHeight; }
            set
            {
                if(_FormData.DamHeight != value)
                {
                    _FormData.DamHeight = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NumberOfRealizations
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
        public int MaxNumberOfIterations
        {
            get { return _FormData.MaxNumberOfIterations; }
            set
            {
                if(_FormData.MaxNumberOfIterations != value)
                {
                    _FormData.MaxNumberOfIterations = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double ConvergenceTolerance
        {
            get { return _FormData.ConvergenceTolerance; }
            set
            {
                if(_FormData.ConvergenceTolerance != value)
                {
                    _FormData.ConvergenceTolerance = value;
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
        public int CorrelationLengthInXDir
        {
            get { return _FormData.CorrelationLengthInXDir; }
            set
            {
                if(_FormData.CorrelationLengthInXDir != value)
                {
                    _FormData.CorrelationLengthInXDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int CorrelationLengthInYDir
        {
            get { return _FormData.CorrelationLengthInYDir; }
            set
            {
                if(_FormData.CorrelationLengthInYDir != value)
                {
                    _FormData.CorrelationLengthInYDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double ConductivityMean
        {
            get { return _FormData.ConductivityMean; }
            set
            {
                if(_FormData.ConductivityMean != value)
                {
                    _FormData.ConductivityMean = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double ConductivityStdDev
        {
            get { return _FormData.ConductivityStdDev; }
            set
            {
                if(_FormData.ConductivityStdDev != value)
                {
                    _FormData.ConductivityStdDev = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public CovarianceFunction CovFunction
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
        public bool ModifyHoirzontalSpacing
        {
            get { return _FormData.ModifyHoirzontalSpacing; }
            set
            {
                if(_FormData.ModifyHoirzontalSpacing != value)
                {
                    _FormData.ModifyHoirzontalSpacing = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public SpacingAlgorithm SpacingAlgo
        {
            get { return _FormData.SpacingAlgo; }
            set
            {
                if(_FormData.SpacingAlgo != value)
                {
                    _FormData.SpacingAlgo = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool UseAlternateAlgoIfFirstFails
        {
            get { return _FormData.UseAlternateAlgoIfFirstFails; }
            set
            {
                if(_FormData.UseAlternateAlgoIfFirstFails != value)
                {
                    _FormData.UseAlternateAlgoIfFirstFails = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool RestrainFreeSurfaceNonIncreasing
        {
            get { return _FormData.RestrainFreeSurfaceNonIncreasing; }
            set
            {
                if(_FormData.RestrainFreeSurfaceNonIncreasing != value)
                {
                    _FormData.RestrainFreeSurfaceNonIncreasing = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool DampOscillations
        {
            get { return _FormData.DampOscillations; }
            set
            {
                if(_FormData.DampOscillations != value)
                {
                    _FormData.DampOscillations = value;
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
                    if (string.IsNullOrEmpty(_FormData.JobTitle) || _FormData.JobTitle.Trim() == "")
                        validationMessage = "Job title must have a value.";
                    break;
                case "BaseName":
                    if (string.IsNullOrEmpty(_FormData.BaseName) || _FormData.BaseName.Trim()=="")
                        validationMessage = "Base name must have a value.";
                    break;
                case "RealizationNumber":
                    if (_FormData.RealizationNumber < 0 & OutputDebugInfo)
                        validationMessage = "Realization number must be a positive integer.";
                    break;
                case "NumEquipotentialDrops":
                    if (_FormData.NumEquipotentialDrops < 0 & ProducePSPlotOfFirstFlownet & ShowStreamlines)
                        validationMessage = "Number of equipotential drops must be a positive integer.";
                    break;
                case "FlownetWidth":
                    if (_FormData.FlownetWidth <= 0 & ProducePSPlotOfFirstFlownet )
                        validationMessage = "Flownet width must be a positive value.";
                    break;
                case "NumElementsInXDir":
                    if (_FormData.NumElementsInXDir < 0)
                        validationMessage = "Number of elements must be a positive integer.";
                    break;
                case "NumElementsInYDir":
                    if(_FormData.NumElementsInYDir < 0)
                        validationMessage = "Number of elements must be a positive integer.";
                    break;
                case "DrainXDimension":
                    if (_FormData.DrainXDimension <= 0 & IsDrainPresent)
                        validationMessage = "Drain dimensions must be positive values.";
                    break;
                case "DrainYDimension":
                    if (_FormData.DrainYDimension <= 0 & IsDrainPresent)
                        validationMessage = "Drain dimensions must be positive values.";
                    break;
                case "DamTop":
                    if (_FormData.DamTop <= 0)
                        validationMessage = "Dam dimensions must be positive values.";
                    break;
                case "DamBase":
                    if (_FormData.DamBase <= 0)
                        validationMessage = "Dam dimensions must be positive values.";
                    break;
                case "DamHeight":
                    if (_FormData.DamHeight <= 0)
                        validationMessage = "Dam dimensions must be positive values.";
                    break;
                case "NumberOfRealizations":
                    if (_FormData.NumberOfRealizations < 1)
                        validationMessage = "Number of realizations must be a positive integer.";
                    break;
                case "MaxNumberOfIterations":
                    if (_FormData.MaxNumberOfIterations < 1)
                        validationMessage = "Maximum number of iterations must be a positive integer.";
                    break;
                case "ConvergenceTolerance":
                    if (_FormData.ConvergenceTolerance < 0 | _FormData.ConvergenceTolerance > 1)
                        validationMessage = "Convergence tolerance must be between 0 and 1.";
                    break;
                case "GeneratorSeed":
                    if (_FormData.GeneratorSeed < 0)
                        validationMessage = "Generator seed must be a positive integer.";
                    break;
                case "CorrelationLengthInXDir":
                    if (_FormData.CorrelationLengthInXDir < 0)
                        validationMessage = "Correlation length must be a positive value.";
                    break;
                case "CorrelationLengthInYDir":
                    if (_FormData.CorrelationLengthInYDir < 0)
                        validationMessage = "Correlation length must be a positive value.";
                    break;
                case "ConductivityStdDev":
                    if (_FormData.ConductivityStdDev < 0)
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
                return _FormData.CanDisplaySummaryStats;
            }
        }
        public bool CanDisplayFlownet
        {
            get { return _FormData.CanDisplayFlownet; }
        }
        public bool CanDisplayField
        {
            get { return _FormData.CanDisplayField; }
        }
        public bool CanDisplayGradientMeanAndStdDevFields
        {
            get { return _FormData.CanDisplayGradientMeanAndStdDevFields; }
        }
        public bool CanDisplayFlowRateHist
        {
            get { return _FormData.CanDisplayFlowRateHist; }
        }
        public bool CanDisplayEffectiveConductivityHist
        {
            get { return _FormData.CanDisplayEffectiveConductivityHist; }
        }
        public bool CanDisplayNodeGradientHist
        {
            get { return _FormData.CanDisplayNodeGradientHist; }
        }
        public bool ChangesHaveBeenMade
        {
            get
            {
                return _ChangesHaveBeenMade;
            }
        }

        public string CurrentOperation
        {
            get
            {
                return _CurrentOperation;
            }

            set
            {
                if(_CurrentOperation != value)
                {
                    _CurrentOperation = value;
                    NotifyPropertyChanged();
                }
                
            }
        }

        

        
        
        public string ProgressDetails
        {
            get
            {
                return _ProgressDetails;
            }

            set
            {
                if(_ProgressDetails != value)
                {
                    _ProgressDetails = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int ProgressPercentage
        {
            get
            {
                return _ProgressPercentage;
            }

            set
            {
                if(_ProgressPercentage != value)
                {
                    _ProgressPercentage = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string DataFilePath
        {
            get
            {
                return _FormData.AppDataFileLocation;
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
        public string FlownetFilePath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RFEM_Software\\" +
                            _FormData.BaseName + ".net";
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
        public Program Type
        {
            get
            {
                return Program.RDam2D;
            }
        }

        

        public RDam2DViewModel()
        {

            _FormData = new RDam2D();

            _FormData.PropertyChanged += NotifyFormDataPropertyChanged;

            foreach (NodeNumber n in _NodesForGradientOutput)
            {
                n.PropertyChanged += NodeNumberCollectionChanged;
            }

            
        }
        public RDam2DViewModel(RDam2D formData)
        {
            _FormData = formData;

            _FormData.PropertyChanged += NotifyFormDataPropertyChanged;

            for(int i = 0; i <= 5; i++)
            {
                _NodesForGradientOutput[i].Value = _FormData.NodesForGradientOutput[i];
                _NodesForGradientOutput[i].PropertyChanged += NodeNumberCollectionChanged;
            }
        }
        public Task<string> RunSimAsync(CancellationToken token)
        {
            if (HasErrors)
            {
                MessageBox.Show("Please correct errors in form before running the simulation.");
                return Task.FromResult("");
            }
            else
            {

                FileWriter.Write(_FormData);
                var tsk = Task<string>.Run(() => _FormData.RunSim(new Progress<int>(p =>
                {
                    ProgressPercentage = p * 100 / (int)_FormData.NumberOfRealizations;
                    ProgressDetails = string.Format("Realization {0}/{1}", p, _FormData.NumberOfRealizations);
                }), new Progress<string>(ps =>
                {
                    CurrentOperation = ps;
                }), token));

                ProgressPercentage = 0;
                ProgressDetails = "";
                return tsk;
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
                FileWriter.Write(_FormData);
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
                FileWriter.Write(_FormData);
                FileWriter.Write(_FormData, filePath);
                _ChangesHaveBeenMade = false;
            }
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
        public void ShowFlownet()
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
            pInfo.Arguments = FlownetFilePath;
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
        public void ShowGradientMeanField()
        {
            var GstView = new GhostViewWrapper("\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"");

            string FilePath= Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RFEM_Software\\" +
                    _FormData.BaseName + ".gpm";

            GstView.Show(FilePath);
        }
        public void ShowGradientStdDevField()
        {
            var GstView = new GhostViewWrapper("\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"");

            string FilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RFEM_Software\\" +
                    _FormData.BaseName + ".gps";

            GstView.Show(FilePath);
        }
        public void ShowFluxMeanField()
        {
            var GstView = new GhostViewWrapper("\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"");

            string FilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RFEM_Software\\" +
                    _FormData.BaseName + ".fpm";

            GstView.Show(FilePath);
        }
        public void ShowFluxStdDevField()
        {
            var GstView = new GhostViewWrapper("\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"");

            string FilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RFEM_Software\\" +
                    _FormData.BaseName + ".fps";

            GstView.Show(FilePath);
        }
        public void ShowPotentialMeanField()
        {
            var GstView = new GhostViewWrapper("\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"");

            string FilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RFEM_Software\\" +
                    _FormData.BaseName + ".hpm";

            GstView.Show(FilePath);
        }
        public void ShowPotentialStdDevField()
        {
            var GstView = new GhostViewWrapper("\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"");

            string FilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RFEM_Software\\" +
                    _FormData.BaseName + ".hps";

            GstView.Show(FilePath);
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

            string FlowFilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + 
                                "\\RFEM_Software\\" + _FormData.BaseName + ".flo";

            return new RDam2DFlowRateHist(_FormData.NumberOfRealizations, FlowFilePath, _FormData.BaseName);
        }
        public RDam2DConductivityHist CreateNewConducivityHistForm()
        {
            string ConductivityFilePath= Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                "\\RFEM_Software\\" + _FormData.BaseName + ".cnd";

            return new RDam2DConductivityHist(_FormData.NumberOfRealizations,
                                                ConductivityFilePath,
                                                _FormData.BaseName,
                                                _FormData.OutputBlockConductivities,
                                                _FormData.OutputConductivityAverages);
        }
        public RDam2DNodalGradientHist CreateNewNodalGradientHistForm()
        {
            string NodalFilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                "\\RFEM_Software\\" + _FormData.BaseName + ".grd";

            return new RDam2DNodalGradientHist(_FormData.NodesForGradientOutput.Where(x => x!=null).
                                                                                Select(x => x.Value).
                                                                                ToList<int>(),
                                                NodalFilePath,
                                                _FormData.BaseName,
                                                _FormData.NumberOfRealizations);
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
