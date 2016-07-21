using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RFEM_Infrastructure;
using System.Collections.ObjectModel;
using System.Windows;
using System.Diagnostics;

namespace RFEM_Software.Forms
{
    class REarth2DViewModel: INotifyPropertyChanged, IDataErrorInfo, ISimViewModel
    {
        private bool _ChangesHaveBeenMade;
        private string _CurrentOperation;
        private int _ProgressPercentage;
        private string _ProgressDetails;

        private REarth2D _FormData;

        private List<string> _Errors = new List<string>();

        public ISimModel Model
        {
            get { return _FormData; }
        }

        #region Form Properties

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
        public bool WriteDebugDataToOutputFile
        {
            get { return _FormData.WriteDebugDataToOutputFile; }
            set
            {
                if (_FormData.WriteDebugDataToOutputFile != value)
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
                    _FormData.PlotFirstRandomField = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public REarthSoilProperties FirstRFPropertyToPlot
        {
            get { return _FormData.FirstRFPropertyToPlot; }
            set
            {
                if(_FormData.FirstRFPropertyToPlot != value)
                {
                    _FormData.FirstRFPropertyToPlot = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ProducePSPlotOfFirstFEM
        {
            get { return _FormData.ProducePSPlotOfFirstFEM; }
            set
            {
                if(_FormData.ProducePSPlotOfFirstFEM != value)
                {
                    _FormData.ProducePSPlotOfFirstFEM = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowMeshOnDisplacedMeshPlot
        {
            get { return _FormData.ShowMeshOnDisplacedMeshPlot; }
            set
            {
                if(_FormData.ShowMeshOnDisplacedMeshPlot != value)
                {
                    _FormData.ShowMeshOnDisplacedMeshPlot = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowRandomFieldOnDisplacedMesh
        {
            get { return _FormData.ShowRandomFieldOnDisplacedMesh; }
            set
            {
                if(_FormData.ShowRandomFieldOnDisplacedMesh != value)
                {
                    _FormData.ShowRandomFieldOnDisplacedMesh = value;
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
        public REarthSoilProperties DisplacedMeshPropertyToPlot
        {
            get { return _FormData.DisplacedMeshPropertyToPlot; }
            set
            {
                if(_FormData.DisplacedMeshPropertyToPlot != value)
                {
                    _FormData.DisplacedMeshPropertyToPlot = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double DisplacedMeshWidth
        {
            get { return _FormData.DisplacedMeshWidth; }
            set
            {
                if(_FormData.DisplacedMeshWidth != value)
                {
                    _FormData.DisplacedMeshWidth = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool StoreWallReactionSamples
        {
            get { return _FormData.StoreWallReactionSamples; }
            set
            {
                if(_FormData.StoreWallReactionSamples != value)
                {
                    _FormData.StoreWallReactionSamples = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool VirtuallySampleSoil
        {
            get { return _FormData.VirtuallySampleSoil; }
            set
            {
                if(_FormData.VirtuallySampleSoil != value)
                {
                    _FormData.VirtuallySampleSoil = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public ObservableCollection<SampleLocation> SampleLocations
        {
            get { return _FormData.SampleLocations; }
            set
            {
                if(_FormData.SampleLocations != value)
                {
                    _FormData.SampleLocations = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool OutputSampledSoilProperties
        {
            get { return _FormData.OutputSampledSoilProperties; }
            set
            {
                if(_FormData.OutputSampledSoilProperties != value)
                {
                    _FormData.OutputSampledSoilProperties = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NElementsInXDir
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

        public int NElementsInYDir
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
        public double ElementSizeInXDir
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
        public double ElementSizeInYDir
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
        public int WallExtension
        {
            get { return _FormData.WallExtension; }
            set
            {
                if(_FormData.WallExtension != value)
                {
                    _FormData.WallExtension = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool RoughWallSurface
        {
            get { return _FormData.RoughWallSurface; }
            set
            {
                if(_FormData.RoughWallSurface != value)
                {
                    _FormData.RoughWallSurface = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double DisplacementIncrement
        {
            get { return _FormData.DisplacementIncrement; }
            set
            {
                if(_FormData.DisplacementIncrement != value)
                {
                    _FormData.DisplacementIncrement = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double PlasticTol
        {
            get { return _FormData.PlasticTol; }
            set
            {
                if (_FormData.PlasticTol != value)
                {
                    _FormData.PlasticTol = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double StressTol
        {
            get { return _FormData.StressTol; }
            set
            {
                if(_FormData.StressTol != value)
                {
                    _FormData.StressTol = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int MaxNumSteps
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
        public int MaxNumIterations
        {
            get { return _FormData.MaxNumIterations; }
            set
            {
                if (_FormData.MaxNumIterations != value)
                {
                    _FormData.MaxNumIterations = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NumSimulation
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
        public int GeneratorSeed
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
        public REarthDistributionInfo Cohesion
        {
            get { return _FormData.Cohesion; }
            set
            {
                if(_FormData.Cohesion != value)
                {
                    _FormData.Cohesion = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public DistributionInfo FrictionAngle
        {
            get { return _FormData.FrictionAngle; }
            set
            {
                if(_FormData.FrictionAngle != value)
                {
                    _FormData.FrictionAngle = value;
                    NotifyPropertyChanged();
                }
            }
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
        public REarthDistributionInfo DilationAngle
        {
            get { return _FormData.DilationAngle; }
            set
            {
                if(_FormData.DilationAngle != value)
                {
                    _FormData.DilationAngle = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public REarthDistributionInfo ElasticModulus
        {
            get { return _FormData.ElasticModulus; }
            set
            {
                if(_FormData.ElasticModulus != value)
                {
                    _FormData.ElasticModulus = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public REarthDistributionInfo PoissonsRatio
        {
            get { return _FormData.PoissonsRatio; }
            set
            {
                if(_FormData.PoissonsRatio != value)
                {
                    _FormData.PoissonsRatio = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public REarthDistributionInfo UnitWeight
        {
            get { return _FormData.UnitWeight; }
            set
            {
                if(_FormData.UnitWeight != value)
                {
                    _FormData.UnitWeight = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public REarthDistributionInfo PressureCoefficient
        {
            get { return _FormData.PressureCoefficient; }
            set
            {
                if(_FormData.PressureCoefficient != value)
                {
                    _FormData.PressureCoefficient = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double[,] CorrelationMatrix
        {
            get { return _FormData.CorrelationMatrix; }
            set
            {
                if(_FormData.CorrelationMatrix != value)
                {
                    _FormData.CorrelationMatrix = value;
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
                    if (string.IsNullOrEmpty(_FormData.JobTitle) || _FormData.JobTitle.Trim() == "")
                        validationMessage = "Job title must have a value.";
                    break;
                case "BaseName":
                    if (string.IsNullOrEmpty(_FormData.BaseName) || _FormData.BaseName.Trim() == "")
                        validationMessage = "Base name must have a value.";
                    break;
                case "DisplacedMeshWidth":

                    if (DisplacedMeshWidth < 0 & ProducePSPlotOfFirstFEM)
                        validationMessage = "Displaced mesh plot must be a positive value.";
                    break;
                case "NumElementsInXDir":
                    if (_FormData.NElementsInXDir < 0)
                        validationMessage = "Number of elements must be a positive integer.";
                    break;
                case "NumElementsInYDir":
                    if (_FormData.NElementsInYDir < 0)
                        validationMessage = "Number of elements must be a positive integer.";
                    break;
                case "ElementSizeInXDir":
                    if (_FormData.ElementSizeInXDir < 0)
                        validationMessage = "Element size must be a positive integer.";
                    break;
                case "ElementSizeInYDir":
                    if (_FormData.ElementSizeInYDir < 0)
                        validationMessage = "Element size must be a positive integer.";
                    break;
                case "WallExtension":
                    if (_FormData.WallExtension < 0)
                        validationMessage = "Wall extension must be a positive integer.";
                    break;
                case "MaxNumSteps":
                    if (_FormData.MaxNumSteps < 0)
                        validationMessage = "Maximum number of steps must be a positive integer.";
                    break;
                case "MaxNumIterations":
                    if (_FormData.MaxNumIterations < 0)
                        if (_FormData.MaxNumIterations < 0)
                            validationMessage = "Maximum number of iterations must be a positive integer.";
                    break;
                case "NumSimulation":
                    if (_FormData.NumberOfRealizations < 1)
                        validationMessage = "Number of simulations must be an integer greater than zero";
                    break;
                case "GeneratorSeed":
                    if (_FormData.GeneratorSeed < 0)
                        validationMessage = "Generator seed must be a positive integer";
                    break;
                case "CorrelationLengthInXDir":
                    if (_FormData.CorrelationLengthInXDir < 0)
                        validationMessage = "Correlation Length must be a positive integer.";
                    break;
                case "CorrelationLengthInYDir":
                    if (_FormData.CorrelationLengthInYDir < 0)
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
                return _FormData.AppDataFileLocation;
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
        private string FieldFilePath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RFEM_Software\\" +
                    _FormData.BaseName + ".fld";
            }
        }
        private string MeshFilePath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RFEM_Software\\" +
                    _FormData.BaseName + ".dis";
            }
        }
        public bool CanDisplaySummaryStats
        {
            get { return _FormData.CanDisplaySummaryStats; }
        }


        public Program Type
        {
            get
            {
                return Program.REarth2D;
            }
        }

        
        public event PropertyChangedEventHandler PropertyChanged;

        public REarth2DViewModel()
        {
            _FormData = new REarth2D();
        }
        public REarth2DViewModel(REarth2D formData)
        {
            _FormData = formData;
        }
      
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            _ChangesHaveBeenMade = true;

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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
        public void ShowMesh()
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
            pInfo.Arguments = MeshFilePath;
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
    }
}
