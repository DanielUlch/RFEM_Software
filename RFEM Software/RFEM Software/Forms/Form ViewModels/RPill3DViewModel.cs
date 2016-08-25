using RFEMSoftware.Simulation.Desktop.CustomControls;
using RFEMSoftware.Simulation.Infrastructure;
using RFEMSoftware.Simulation.Infrastructure.Models;
using RFEMSoftware.Simulation.Infrastructure.Persistence;
using RFEMSoftware.Simulation.Infrastructure.Wrappers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RFEMSoftware.Simulation.Desktop.Forms
{
    public class RPill3DViewModel : INotifyPropertyChanged, IDataErrorInfo, ISimViewModel
    {
        private bool _ChangesHaveBeenMade;

        private RPill3D _Model;

        private RPill3DForm _View;

        private TopLevelTabItem _MasterTab;

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
            get { throw new NotImplementedException(); }
        }
        public RPill3DViewModel(CommandBindingCollection commandBindings, double width,
                               RoutedEventHandler closeTopTab,
                               RoutedEventHandler closeAllTopTabs)
        {
            _Model = new RPill3D();

            _View = new RPill3DForm(this);

            _MasterTab = new TopLevelTabItem(commandBindings, width, this, closeTopTab, closeAllTopTabs);

            FileInfo = new FileManager(null);

            _Model.OutputDirectory = FileInfo.OutputDirectory;

            FileInfo.PropertyChanged += OutputDirectoryChanged;
        }
        public RPill3DViewModel(CommandBindingCollection commandBindings, double width, RPill3D model,
                               RoutedEventHandler closeTopTab,
                               RoutedEventHandler closeAllTopTabs)
        {
            _Model = model;

            _View = new RPill3DForm(this);

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
            NotifyPropertyChanged("CanDisplayField");
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
                if (_Model.EchoInputDataToOutputFile != value)
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
                if (_Model.OutputDebugInfo != value)
                {
                    _Model.OutputDebugInfo = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool PlotFirstRF
        {
            get { return _Model.PlotFirstRF; }
            set
            {
                if (_Model.PlotFirstRF != value)
                {
                    _Model.PlotFirstRF = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public SoilProperty FirstRFPropertyToPlot
        {
            get { return _Model.FirstRFPropertyToPlot; }
            set
            {
                if (_Model.FirstRFPropertyToPlot != value)
                {
                    _Model.FirstRFPropertyToPlot = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int FirstRFNodeIndexToPlot
        {
            get { return _Model.FirstRFNodeIndexToPlot; }
            set
            {
                if (_Model.FirstRFNodeIndexToPlot != value)
                {
                    _Model.FirstRFNodeIndexToPlot = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public Axis FirstRFPerpindicularToThisAxis
        {
            get { return _Model.FirstRFPerpindicularToThisAxis; }
            set
            {
                if (_Model.FirstRFPerpindicularToThisAxis != value)
                {
                    _Model.FirstRFPerpindicularToThisAxis = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool NormalizePillarCapacitySamples
        {
            get { return _Model.NormalizePillarCapacitySamples; }
            set
            {
                if (_Model.NormalizePillarCapacitySamples != value)
                {
                    _Model.NormalizePillarCapacitySamples = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool OutputCapacitySamples
        {
            get { return _Model.OutputCapacitySamples; }
            set
            {
                if (_Model.OutputCapacitySamples != value)
                {
                    _Model.OutputCapacitySamples = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int NElementsInXDir
        {
            get { return _Model.NElementsInXDir; }
            set
            {
                if (_Model.NElementsInXDir != value)
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
                if (_Model.NElementsInYDir != value)
                {
                    _Model.NElementsInYDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NElementsInZDir
        {
            get { return _Model.NElementsInZDir; }
            set
            {
                if (_Model.NElementsInZDir != value)
                {
                    _Model.NElementsInZDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double ElementSizeInXDir
        {
            get { return _Model.ElementSizeInXDir; }
            set
            {
                if (_Model.ElementSizeInXDir != value)
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
                if (_Model.ElementSizeInYDir != value)
                {
                    _Model.ElementSizeInYDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double ElementSizeInZDir
        {
            get { return _Model.ElementSizeInZDir; }
            set
            {
                if (_Model.ElementSizeInZDir != value)
                {
                    _Model.ElementSizeInZDir = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RPill3DElementType ElementType
        {
            get { return _Model.ElementType; }
            set
            {
                if(_Model.ElementType != value)
                {
                    _Model.ElementType = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double DisplacementInc
        {
            get { return _Model.DisplacementInc; }
            set
            {
                if (_Model.DisplacementInc != value)
                {
                    _Model.DisplacementInc = value;
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
        public double BearingTol
        {
            get { return _Model.BearingTol; }
            set
            {
                if (_Model.BearingTol != value)
                {
                    _Model.BearingTol = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int MaxNumSteps
        {
            get { return _Model.MaxNumSteps; }
            set
            {
                if (_Model.MaxNumSteps != value)
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
        public int NumberOfRealizations
        {
            get { return _Model.NumberOfRealizations; }
            set
            {
                if (_Model.NumberOfRealizations != value)
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
                if (_Model.GeneratorSeed != value)
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
                if (_Model.CorrelationLengthInXDir != value)
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
                if (_Model.CorrelationLengthInYDir != value)
                {
                    _Model.CorrelationLengthInYDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int CorrelationLengthInZDir
        {
            get { return _Model.CorrelationLengthInZDir; }
            set
            {
                if (_Model.CorrelationLengthInZDir != value)
                {
                    _Model.CorrelationLengthInZDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public CovarianceFunction3D CovFunc
        {
            get { return _Model.CovFunc; }
            set
            {
                if (_Model.CovFunc != value)
                {
                    _Model.CovFunc = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public DistributionInfo Cohesion
        {
            get { return _Model.Cohesion; }
            set
            {
                if (_Model.Cohesion != value)
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
                if (_Model.FrictionAngle != value)
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
                if (_Model.FrictionAngleType != value)
                {
                    _Model.FrictionAngleType = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public DistributionInfo DilationAngle
        {
            get { return _Model.DilationAngle; }
            set
            {
                if (_Model.DilationAngle != value)
                {
                    _Model.DilationAngle = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public DistributionInfo ElasticModulus
        {
            get { return _Model.ElasticModulus; }
            set
            {
                if (_Model.ElasticModulus != value)
                {
                    _Model.ElasticModulus = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public DistributionInfo PoissonsRatio
        {
            get { return _Model.PoissonsRatio; }
            set
            {
                if (_Model.PoissonsRatio != value)
                {
                    _Model.PoissonsRatio = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double[,] CorrelationMatrix
        {
            get { return _Model.CorrelationMatrix; }
            set
            {
                if (_Model.CorrelationMatrix != value)
                {
                    _Model.CorrelationMatrix = value;
                    NotifyPropertyChanged();
                }
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
        private string FieldPath
        {
            get
            {
                return FileInfo.OutputDirectory + "\\" + _Model.BaseName + ".fld";
            }
        }

        public bool CanDisplaySummaryStats
        {
            get
            {
                return _Model.CanDisplaySummaryStats && System.IO.File.Exists(SummaryFilePath);
            }
        }
        public bool CanDisplayField
        {
            get
            {
                return _Model.CanDisplayField && System.IO.File.Exists(FieldPath);
            }
        }

        public Program Type
        {
            get
            {
                return Program.RPill3D;
            }
        }
        


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

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            _ChangesHaveBeenMade = true;

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

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
            DisplayWrapper.Run(FieldPath);

            GhostViewWrapper gView = new GhostViewWrapper("\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"");

            gView.Show(FileInfo.OutputDirectory + "\\graph1.ps");
        }
    }
}
