using RFEMSoftware.Simulation.Desktop.CustomControls;
using RFEMSoftware.Simulation.Infrastructure;
using RFEMSoftware.Simulation.Infrastructure.Models;
using RFEMSoftware.Simulation.Infrastructure.Persistence;
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
    public class RFlow3DViewModel : INotifyPropertyChanged, IDataErrorInfo, ISimViewModel
    {
        private bool _ChangesHaveBeenMade;

        private RFlow3D _Model;

        private RFlow3DForm _View;

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

        public RFlow3DViewModel(CommandBindingCollection commandBindings, double width,
                               RoutedEventHandler closeTopTab,
                               RoutedEventHandler closeAllTopTabs)
        {
            _Model = new RFlow3D();

            _View = new RFlow3DForm(this);

            _MasterTab = new TopLevelTabItem(commandBindings, width, this, closeTopTab, closeAllTopTabs);

            FileInfo = new FileManager(null);

            _Model.OutputDirectory = FileInfo.OutputDirectory;

            FileInfo.PropertyChanged += OutputDirectoryChanged;
        }
        public RFlow3DViewModel(CommandBindingCollection commandBindings, double width, RFlow3D model,
                               RoutedEventHandler closeTopTab,
                               RoutedEventHandler closeAllTopTabs)
        {
            _Model = model;

            _View = new RFlow3DForm(this);

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
        public bool OutputFlowRateExitGradientUpliftForce
        {
            get { return _Model.OutputFlowRateExitGradientUpliftForce; }
            set
            {
                if (_Model.OutputFlowRateExitGradientUpliftForce != value)
                {
                    _Model.OutputFlowRateExitGradientUpliftForce = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool OutputBlockHydraulicConductivities
        {
            get { return _Model.OutputBlockHydraulicConductivities; }
            set
            {
                if (_Model.OutputBlockHydraulicConductivities != value)
                {
                    _Model.OutputBlockHydraulicConductivities = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool OutputArithmeticGeometricHarmonicHydraulicConductivityAvgs
        {
            get { return _Model.OutputArithmeticGeometricHarmonicHydraulicConductivityAvgs; }
            set
            {
                if (_Model.OutputArithmeticGeometricHarmonicHydraulicConductivityAvgs != value)
                {
                    _Model.OutputArithmeticGeometricHarmonicHydraulicConductivityAvgs = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool GenerateUniformConductivityField
        {
            get { return _Model.GenerateUniformConductivityField; }
            set
            {
                if (_Model.GenerateUniformConductivityField != value)
                {
                    _Model.GenerateUniformConductivityField = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NumberOfWalls
        {
            get { return _Model.NumberOfWalls; }
            set
            {
                if(_Model.NumberOfWalls != value)
                {
                    _Model.NumberOfWalls = value;
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
        public int NElementsLeftOfWall
        {
            get { return _Model.NElementsLeftOfWall; }
            set
            {
                if (_Model.NElementsLeftOfWall != value)
                {
                    _Model.NElementsLeftOfWall = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NElementsRightOfWall
        {
            get { return _Model.NElementsRightOfWall; }
            set
            {
                if (_Model.NElementsRightOfWall != value)
                {
                    _Model.NElementsRightOfWall = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int DepthOfWall
        {
            get { return _Model.DepthOfWall; }
            set
            {
                if (_Model.DepthOfWall != value)
                {
                    _Model.DepthOfWall = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NElementsLeftOfLeftWall
        {
            get { return _Model.NElementsLeftOfLeftWall; }
            set
            {
                if (_Model.NElementsLeftOfLeftWall != value)
                {
                    _Model.NElementsLeftOfLeftWall = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NElementsBetweenWalls
        {
            get { return _Model.NElementsBetweenWalls; }
            set
            {
                if (_Model.NElementsBetweenWalls != value)
                {
                    _Model.NElementsBetweenWalls = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NElementsRightOfRightWall
        {
            get { return _Model.NElementsRightOfRightWall; }
            set
            {
                if (_Model.NElementsRightOfRightWall != value)
                {
                    _Model.NElementsRightOfRightWall = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int DepthOfLeftWall
        {
            get { return _Model.DepthOfLeftWall; }
            set
            {
                if (_Model.DepthOfLeftWall != value)
                {
                    _Model.DepthOfLeftWall = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int DepthOfRightWall
        {
            get { return _Model.DepthOfRightWall; }
            set
            {
                if (_Model.DepthOfRightWall != value)
                {
                    _Model.DepthOfRightWall = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double ElementDimensionXDir
        {
            get { return _Model.ElementDimensionXDir; }
            set
            {
                if (_Model.ElementDimensionXDir != value)
                {
                    _Model.ElementDimensionXDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double ElementDimensionYDir
        {
            get { return _Model.ElementDimensionYDir; }
            set
            {
                if (_Model.ElementDimensionYDir != value)
                {
                    _Model.ElementDimensionYDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double ElementDimensionZDir
        {
            get { return _Model.ElementDimensionZDir; }
            set
            {
                if (_Model.ElementDimensionZDir != value)
                {
                    _Model.ElementDimensionZDir = value;
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
        public int CorrelationLengthXDir
        {
            get { return _Model.CorrelationLengthXDir; }
            set
            {
                if (_Model.CorrelationLengthXDir != value)
                {
                    _Model.CorrelationLengthXDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int CorrelationLengthYDir
        {
            get { return _Model.CorrelationLengthYDir; }
            set
            {
                if (_Model.CorrelationLengthYDir != value)
                {
                    _Model.CorrelationLengthYDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int CorrelationLenghtZDir
        {
            get { return _Model.CorrelationLenghtZDir; }
            set
            {
                if (_Model.CorrelationLenghtZDir != value)
                {
                    _Model.CorrelationLenghtZDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double HydraulicConductivityMean
        {
            get { return _Model.HydraulicConductivityMean; }
            set
            {
                if (_Model.HydraulicConductivityMean != value)
                {
                    _Model.HydraulicConductivityMean = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double HydraulicConductivityStdDev
        {
            get { return _Model.HydraulicConductivityStdDev; }
            set
            {
                if (_Model.HydraulicConductivityStdDev != value)
                {
                    _Model.HydraulicConductivityStdDev = value;
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

        public bool CanDisplaySummaryStats
        {
            get
            {
                return _Model.CanDisplaySummaryStats && System.IO.File.Exists(SummaryFilePath);
            }
        }


        public Program Type
        {
            get
            {
                return Program.RFlow3D;
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
    }
}
