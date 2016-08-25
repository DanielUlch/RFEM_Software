using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using System.Diagnostics;
using RFEMSoftware.Simulation.Infrastructure.Models;
using RFEMSoftware.Simulation.Infrastructure;
using RFEMSoftware.Simulation.Infrastructure.Persistence;
using RFEMSoftware.Simulation.Desktop.CustomControls;
using System.Windows.Input;
using RFEMSoftware.Simulation.Infrastructure.Wrappers;

namespace RFEMSoftware.Simulation.Desktop.Forms
{
    public class REarth2DViewModel: INotifyPropertyChanged, IDataErrorInfo, ISimViewModel
    {
        private bool _ChangesHaveBeenMade;

        private REarth2D _Model;

        private List<string> _Errors = new List<string>();

        private REarth2DForm _View;

        private TopLevelTabItem _MasterTab;

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
            get { throw new NotImplementedException(); }
        }
        public REarth2DViewModel(CommandBindingCollection commandBindings, double width,
                               RoutedEventHandler closeTopTab,
                               RoutedEventHandler closeAllTopTabs)
        {
            _Model = new REarth2D();
            
            _View = new REarth2DForm(this);

            _MasterTab = new TopLevelTabItem(commandBindings, width, this, closeTopTab, closeAllTopTabs);

            FileInfo = new FileManager(null);

            _Model.OutputDirectory = FileInfo.OutputDirectory;

            FileInfo.PropertyChanged += OutputDirectoryChanged;

        }
        public REarth2DViewModel(CommandBindingCollection commandBindings, double width, REarth2D model,
                               RoutedEventHandler closeTopTab,
                               RoutedEventHandler closeAllTopTabs)
        {
            _Model = model;

            _View = new REarth2DForm(this);

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

        private void OutputDirectoryChanged(object sender, PropertyChangedEventArgs e)
        {
            _Model.OutputDirectory = FileInfo.OutputDirectory;
            SaveAs(DataFilePath);

            NotifyPropertyChanged("CanDisplaySummaryStats");
            NotifyPropertyChanged("CanDisplayMesh");
            NotifyPropertyChanged("CanDisplayField");
        }

        #region Form Properties

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
        public bool WriteDebugDataToOutputFile
        {
            get { return _Model.WriteDebugDataToOutputFile; }
            set
            {
                if (_Model.WriteDebugDataToOutputFile != value)
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
                    _Model.PlotFirstRandomField = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public REarthSoilProperties FirstRFPropertyToPlot
        {
            get { return _Model.FirstRFPropertyToPlot; }
            set
            {
                if(_Model.FirstRFPropertyToPlot != value)
                {
                    _Model.FirstRFPropertyToPlot = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ProducePSPlotOfFirstFEM
        {
            get { return _Model.ProducePSPlotOfFirstFEM; }
            set
            {
                if(_Model.ProducePSPlotOfFirstFEM != value)
                {
                    _Model.ProducePSPlotOfFirstFEM = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowMeshOnDisplacedMeshPlot
        {
            get { return _Model.ShowMeshOnDisplacedMeshPlot; }
            set
            {
                if(_Model.ShowMeshOnDisplacedMeshPlot != value)
                {
                    _Model.ShowMeshOnDisplacedMeshPlot = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowRandomFieldOnDisplacedMesh
        {
            get { return _Model.ShowRandomFieldOnDisplacedMesh; }
            set
            {
                if(_Model.ShowRandomFieldOnDisplacedMesh != value)
                {
                    _Model.ShowRandomFieldOnDisplacedMesh = value;
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
        public REarthSoilProperties DisplacedMeshPropertyToPlot
        {
            get { return _Model.DisplacedMeshPropertyToPlot; }
            set
            {
                if(_Model.DisplacedMeshPropertyToPlot != value)
                {
                    _Model.DisplacedMeshPropertyToPlot = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double DisplacedMeshWidth
        {
            get { return _Model.DisplacedMeshWidth; }
            set
            {
                if(_Model.DisplacedMeshWidth != value)
                {
                    _Model.DisplacedMeshWidth = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool StoreWallReactionSamples
        {
            get { return _Model.StoreWallReactionSamples; }
            set
            {
                if(_Model.StoreWallReactionSamples != value)
                {
                    _Model.StoreWallReactionSamples = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool VirtuallySampleSoil
        {
            get { return _Model.VirtuallySampleSoil; }
            set
            {
                if(_Model.VirtuallySampleSoil != value)
                {
                    _Model.VirtuallySampleSoil = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public ObservableCollection<SampleLocation> SampleLocations
        {
            get { return _Model.SampleLocations; }
            set
            {
                if(_Model.SampleLocations != value)
                {
                    _Model.SampleLocations = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool OutputSampledSoilProperties
        {
            get { return _Model.OutputSampledSoilProperties; }
            set
            {
                if(_Model.OutputSampledSoilProperties != value)
                {
                    _Model.OutputSampledSoilProperties = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NElementsInXDir
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

        public int NElementsInYDir
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
        public double ElementSizeInXDir
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
        public double ElementSizeInYDir
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
        public int WallExtension
        {
            get { return _Model.WallExtension; }
            set
            {
                if(_Model.WallExtension != value)
                {
                    _Model.WallExtension = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool RoughWallSurface
        {
            get { return _Model.RoughWallSurface; }
            set
            {
                if(_Model.RoughWallSurface != value)
                {
                    _Model.RoughWallSurface = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double DisplacementIncrement
        {
            get { return _Model.DisplacementIncrement; }
            set
            {
                if(_Model.DisplacementIncrement != value)
                {
                    _Model.DisplacementIncrement = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double PlasticTol
        {
            get { return _Model.PlasticTol; }
            set
            {
                if (_Model.PlasticTol != value)
                {
                    _Model.PlasticTol = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double StressTol
        {
            get { return _Model.StressTol; }
            set
            {
                if(_Model.StressTol != value)
                {
                    _Model.StressTol = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int MaxNumSteps
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
        public int MaxNumIterations
        {
            get { return _Model.MaxNumIterations; }
            set
            {
                if (_Model.MaxNumIterations != value)
                {
                    _Model.MaxNumIterations = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NumSimulation
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
        public int GeneratorSeed
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
        public REarthDistributionInfo Cohesion
        {
            get { return _Model.Cohesion; }
            set
            {
                if(_Model.Cohesion != value)
                {
                    _Model.Cohesion = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public DistributionInfo FrictionAngle
        {
            get { return _Model.FrictionAngle; }
            set
            {
                if(_Model.FrictionAngle != value)
                {
                    _Model.FrictionAngle = value;
                    NotifyPropertyChanged();
                }
            }
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
        public REarthDistributionInfo DilationAngle
        {
            get { return _Model.DilationAngle; }
            set
            {
                if(_Model.DilationAngle != value)
                {
                    _Model.DilationAngle = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public REarthDistributionInfo ElasticModulus
        {
            get { return _Model.ElasticModulus; }
            set
            {
                if(_Model.ElasticModulus != value)
                {
                    _Model.ElasticModulus = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public REarthDistributionInfo PoissonsRatio
        {
            get { return _Model.PoissonsRatio; }
            set
            {
                if(_Model.PoissonsRatio != value)
                {
                    _Model.PoissonsRatio = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public REarthDistributionInfo UnitWeight
        {
            get { return _Model.UnitWeight; }
            set
            {
                if(_Model.UnitWeight != value)
                {
                    _Model.UnitWeight = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public REarthDistributionInfo PressureCoefficient
        {
            get { return _Model.PressureCoefficient; }
            set
            {
                if(_Model.PressureCoefficient != value)
                {
                    _Model.PressureCoefficient = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double[,] CorrelationMatrix
        {
            get { return _Model.CorrelationMatrix; }
            set
            {
                if(_Model.CorrelationMatrix != value)
                {
                    _Model.CorrelationMatrix = value;
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
        private string Validate(object sender, string propertyName)
        {
            string validationMessage = string.Empty;

            switch (propertyName)
            {
                case "JobTitle":
                    if (string.IsNullOrEmpty(_Model.JobTitle) || _Model.JobTitle.Trim() == "")
                        validationMessage = "Job title must have a value.";
                    break;
                case "BaseName":
                    if (string.IsNullOrEmpty(_Model.BaseName) || _Model.BaseName.Trim() == "")
                        validationMessage = "Base name must have a value.";
                    break;
                case "DisplacedMeshWidth":

                    if (DisplacedMeshWidth < 0 & ProducePSPlotOfFirstFEM)
                        validationMessage = "Displaced mesh plot must be a positive value.";
                    break;
                case "NumElementsInXDir":
                    if (_Model.NElementsInXDir < 0)
                        validationMessage = "Number of elements must be a positive integer.";
                    break;
                case "NumElementsInYDir":
                    if (_Model.NElementsInYDir < 0)
                        validationMessage = "Number of elements must be a positive integer.";
                    break;
                case "ElementSizeInXDir":
                    if (_Model.ElementSizeInXDir < 0)
                        validationMessage = "Element size must be a positive integer.";
                    break;
                case "ElementSizeInYDir":
                    if (_Model.ElementSizeInYDir < 0)
                        validationMessage = "Element size must be a positive integer.";
                    break;
                case "WallExtension":
                    if (_Model.WallExtension < 0)
                        validationMessage = "Wall extension must be a positive integer.";
                    break;
                case "MaxNumSteps":
                    if (_Model.MaxNumSteps < 0)
                        validationMessage = "Maximum number of steps must be a positive integer.";
                    break;
                case "MaxNumIterations":
                    if (_Model.MaxNumIterations < 0)
                        if (_Model.MaxNumIterations < 0)
                            validationMessage = "Maximum number of iterations must be a positive integer.";
                    break;
                case "NumSimulation":
                    if (_Model.NumberOfRealizations < 1)
                        validationMessage = "Number of simulations must be an integer greater than zero";
                    break;
                case "GeneratorSeed":
                    if (_Model.GeneratorSeed < 0)
                        validationMessage = "Generator seed must be a positive integer";
                    break;
                case "CorrelationLengthInXDir":
                    if (_Model.CorrelationLengthInXDir < 0)
                        validationMessage = "Correlation Length must be a positive integer.";
                    break;
                case "CorrelationLengthInYDir":
                    if (_Model.CorrelationLengthInYDir < 0)
                        validationMessage = "Correlation Length must be a positive integer.";
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

        public string SummaryFilePath
        {
            get
            {
                return FileInfo.OutputDirectory + "\\" + _Model.BaseName + ".stt";
            }
        }
        private string FieldFilePath
        {
            get
            {
                return FileInfo.OutputDirectory + "\\" + _Model.BaseName + ".fld";
            }
        }
        private string MeshFilePath
        {
            get
            {
                return FileInfo.OutputDirectory + "\\" + _Model.BaseName + ".dis";
            }
        }
        public bool CanDisplaySummaryStats
        {
            get { return _Model.CanDisplaySummaryStats && System.IO.File.Exists(SummaryFilePath); }
        }
        public bool CanDisplayField
        {
            get
            {
                return _Model.CanDisplayField && System.IO.File.Exists(FieldFilePath);
            }
        }
        public bool CanDisplayMesh
        {
            get
            {
                return _Model.CanDisplayMesh && System.IO.File.Exists(MeshFilePath);
            }
        }


        public Program Type
        {
            get
            {
                return Program.REarth2D;
            }
        }

        
        public event PropertyChangedEventHandler PropertyChanged;

        
      
        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            _ChangesHaveBeenMade = true;

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        

        public void ShowField()
        {
            DisplayWrapper.Run(FieldFilePath);

            GhostViewWrapper gView = new GhostViewWrapper("\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"");

            gView.Show(FileInfo.OutputDirectory + "\\graph1.ps");
        }
        public void ShowMesh()
        {
            GhostViewWrapper gView = new GhostViewWrapper("\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"");

            gView.Show(MeshFilePath);
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
    }
}
