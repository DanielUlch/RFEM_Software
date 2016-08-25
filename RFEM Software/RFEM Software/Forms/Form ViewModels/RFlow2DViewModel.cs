using RFEMSoftware.Simulation.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFEMSoftware.Simulation.Infrastructure;
using System.Windows;
using RFEMSoftware.Simulation.Infrastructure.Persistence;
using RFEMSoftware.Simulation.Desktop.CustomControls;
using System.Windows.Input;
using RFEMSoftware.Simulation.Infrastructure.Wrappers;

namespace RFEMSoftware.Simulation.Desktop.Forms
{
    public class RFlow2DViewModel : INotifyPropertyChanged, IDataErrorInfo, ISimViewModel
    {
        private bool _ChangesHaveBeenMade;

        private RFlow2D _Model;

        private RFlow2DForm _View;

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

        public RFlow2DViewModel(CommandBindingCollection commandBindings, double width,
                               RoutedEventHandler closeTopTab,
                               RoutedEventHandler closeAllTopTabs)
        {
            _Model = new RFlow2D();

            _View = new RFlow2DForm(this);

            _MasterTab = new TopLevelTabItem(commandBindings, width, this, closeTopTab, closeAllTopTabs);

            FileInfo = new FileManager(null);

            _Model.OutputDirectory = FileInfo.OutputDirectory;

            FileInfo.PropertyChanged += OutputDirectoryChanged;
        }

        public RFlow2DViewModel(CommandBindingCollection commandBindings, double width, RFlow2D model,
                               RoutedEventHandler closeTopTab,
                               RoutedEventHandler closeAllTopTabs)
        {
            _Model = model;

            _View = new RFlow2DForm(this);

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
            NotifyPropertyChanged("CanShowFlownet");
            NotifyPropertyChanged("CanShowField");
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

        public bool ProduceDisplayOfFirstLogConductivityField
        {
            get { return _Model.ProduceDisplayOfFirstLogConductivityField; }
            set
            {
                if (_Model.ProduceDisplayOfFirstLogConductivityField != value)
                {
                    _Model.ProduceDisplayOfFirstLogConductivityField = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ProducePSPlotOfFirstFlownet
        {
            get { return _Model.ProducePSPlotOfFirstFlownet; }
            set
            {
                if (_Model.ProducePSPlotOfFirstFlownet != value)
                {
                    _Model.ProducePSPlotOfFirstFlownet = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ProduceDisplayOfTotalHeadMeanAndStdDev
        {
            get { return _Model.ProduceDisplayOfTotalHeadMeanAndStdDev; }
            set
            {
                if (_Model.ProduceDisplayOfTotalHeadMeanAndStdDev != value)
                {
                    _Model.ProduceDisplayOfTotalHeadMeanAndStdDev = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool OutputFlowRateExitGradientUpliftForce
        {
            get { return _Model.OutputFlowRateExitGradientUpliftForce; }
            set
            {
                _Model.OutputFlowRateExitGradientUpliftForce = value;
                NotifyPropertyChanged();
            }
        }
        public bool OutputDetailedExitGradientInfo
        {
            get { return _Model.OutputDetailedExitGradientInfo; }
            set
            {
                if (_Model.OutputDetailedExitGradientInfo != value)
                {
                    _Model.OutputDetailedExitGradientInfo = value;
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
                if (_Model.NumberOfWalls != value)
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
        public int NElementsBetweenWall
        {
            get { return _Model.NElementsBetweenWall; }
            set
            {
                if (_Model.NElementsBetweenWall != value)
                {
                    _Model.NElementsBetweenWall = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NElementsToTheRightOfRightWall
        {
            get { return _Model.NElementsToTheRightOfRightWall; }
            set
            {
                if (_Model.NElementsToTheRightOfRightWall != value)
                {
                    _Model.NElementsToTheRightOfRightWall = value;
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
        public double ElementDimensionHorrizontal
        {
            get { return _Model.ElementDimensionHorrizontal; }
            set
            {
                if (_Model.ElementDimensionHorrizontal != value)
                {
                    _Model.ElementDimensionHorrizontal = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double ElementDimensionVertical
        {
            get { return _Model.ElementDimensionVertical; }
            set
            {
                if (_Model.ElementDimensionVertical != value)
                {
                    _Model.ElementDimensionVertical = value;
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
        public CovarianceFunction CovFunc
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
        public int NContoursForMeanTotalHead
        {
            get { return _Model.NContoursForMeanTotalHead; }
            set
            {
                if (_Model.NContoursForMeanTotalHead != value)
                {
                    _Model.NContoursForMeanTotalHead = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NContoursForStdDevTotalHead
        {
            get { return _Model.NContoursForStdDevTotalHead; }
            set
            {
                if (_Model.NContoursForStdDevTotalHead != value)
                {
                    _Model.NContoursForStdDevTotalHead = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NEquipotentialDrops
        {
            get { return _Model.NEquipotentialDrops; }
            set
            {
                if (_Model.NEquipotentialDrops != value)
                {
                    _Model.NEquipotentialDrops = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowLogConductivityFieldOnFlownet
        {
            get { return _Model.ShowLogConductivityFieldOnFlownet; }
            set
            {
                if (_Model.ShowLogConductivityFieldOnFlownet != value)
                {
                    _Model.ShowLogConductivityFieldOnFlownet = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowSoilMassDimensionsOnFlownet
        {
            get { return _Model.ShowSoilMassDimensionsOnFlownet; }
            set
            {
                if (_Model.ShowSoilMassDimensionsOnFlownet != value)
                {
                    _Model.ShowSoilMassDimensionsOnFlownet = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowTitlesOnFlownet
        {
            get { return _Model.ShowTitlesOnFlownet; }
            set
            {
                if (_Model.ShowTitlesOnFlownet != value)
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
                if (_Model.FlownetWidth != value)
                {
                    _Model.FlownetWidth = value;
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
        private string FlownetPath
        {
            get
            {
                return FileInfo.OutputDirectory + "\\" + _Model.BaseName + ".net";
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
        public bool CanShowFlownet
        {
            get
            {
                return _Model.CanDisplayFlownet && System.IO.File.Exists(FlownetPath);
            }
        }
        public bool CanShowField
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
                return Program.RFlow2D;
            }
        }

        
        



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
        public void ShowFlownet()
        {
            GhostViewWrapper gView = new GhostViewWrapper("\"" + (string)Properties.Settings.Default["GhostViewPath"] + "\"");

            gView.Show(FlownetPath);
        }
    }
}
