using RFEMSoftware.Simulation.Infrastructure;
using RFEMSoftware.Simulation.Infrastructure.Models;
using RFEMSoftware.Simulation.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RFEMSoftware.Simulation.Desktop.Forms
{
    class RSetl2DViewModel : INotifyPropertyChanged, IDataErrorInfo, ISimViewModel
    {
        private bool _ChangesHaveBeenMade;

        private RSetl2D _Model;

        private List<string> _Errors = new List<string>();

        public RSetl2DViewModel()
        {
            _Model = new RSetl2D();
        }

        public RSetl2DViewModel(RSetl2D model)
        {
            _Model = model;
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
        public bool ProduceDisplayOfFirstLogElasticModulusField
        {
            get { return _Model.ProduceDisplayOfFirstLogElasticModulusField; }
            set
            {
                if (_Model.ProduceDisplayOfFirstLogElasticModulusField != value)
                {
                    _Model.ProduceDisplayOfFirstLogElasticModulusField = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ProducePSPlotOfFirstDisplacedMesh
        {
            get { return _Model.ProducePSPlotOfFirstDisplacedMesh; }
            set
            {
                if (_Model.ProducePSPlotOfFirstDisplacedMesh != value)
                {
                    _Model.ProducePSPlotOfFirstDisplacedMesh = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool OverlayLogElasticModulusFieldOnDisplacedMesh
        {
            get { return _Model.OverlayLogElasticModulusFieldOnDisplacedMesh; }
            set
            {
                if (_Model.OverlayLogElasticModulusFieldOnDisplacedMesh != value)
                {
                    _Model.OverlayLogElasticModulusFieldOnDisplacedMesh = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowProblemDimensionsOnDisplacedMesh
        {
            get { return _Model.ShowProblemDimensionsOnDisplacedMesh; }
            set
            {
                if (_Model.ShowProblemDimensionsOnDisplacedMesh != value)
                {
                    _Model.ShowProblemDimensionsOnDisplacedMesh = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowTitlesOnDisplacedMesh
        {
            get { return _Model.ShowTitlesOnDisplacedMesh; }
            set
            {
                if (_Model.ShowTitlesOnDisplacedMesh != value)
                {
                    _Model.ShowTitlesOnDisplacedMesh = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double DisplacementMagInXDir
        {
            get { return _Model.DisplacementMagInXDir; }
            set
            {
                if (_Model.DisplacementMagInXDir != value)
                {
                    _Model.DisplacementMagInXDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double DisplacementMagInYDir
        {
            get { return _Model.DisplacementMagInYDir; }
            set
            {
                if (_Model.DisplacementMagInYDir != value)
                {
                    _Model.DisplacementMagInYDir = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double DisplacedMeshWidth
        {
            get { return _Model.DisplacedMeshWidth; }
            set
            {
                if (_Model.DisplacedMeshWidth != value)
                {
                    _Model.DisplacedMeshWidth = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool UseKGSelectiveReducedIntegration
        {
            get { return _Model.UseKGSelectiveReducedIntegration; }
            set
            {
                if (_Model.UseKGSelectiveReducedIntegration != value)
                {
                    _Model.UseKGSelectiveReducedIntegration = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool OutputSettlementSamplesAndSummaryStats
        {
            get { return _Model.OutputSettlementSamplesAndSummaryStats; }
            set
            {
                if (_Model.OutputSettlementSamplesAndSummaryStats != value)
                {
                    _Model.OutputSettlementSamplesAndSummaryStats = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool OutputBlockModulusSamplesAndSummaryStats
        {
            get { return _Model.OutputBlockModulusSamplesAndSummaryStats; }
            set
            {
                if (_Model.OutputBlockModulusSamplesAndSummaryStats != value)
                {
                    _Model.OutputBlockModulusSamplesAndSummaryStats = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool OutputFieldAveragedModulusSamplesAndSummaryStats
        {
            get { return _Model.OutputFieldAveragedModulusSamplesAndSummaryStats; }
            set
            {
                if (_Model.OutputFieldAveragedModulusSamplesAndSummaryStats != value)
                {
                    _Model.OutputFieldAveragedModulusSamplesAndSummaryStats = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool GenerateUniformRandomFields
        {
            get { return _Model.GenerateUniformRandomFields; }
            set
            {
                if (_Model.GenerateUniformRandomFields != value)
                {
                    _Model.GenerateUniformRandomFields = value;
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
        public double PoissonsRatio
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
        public double ElasticModulusMean
        {
            get { return _Model.ElasticModulusMean; }
            set
            {
                if (_Model.ElasticModulusMean != value)
                {
                    _Model.ElasticModulusMean = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double ElasticModulusStdDev
        {
            get { return _Model.ElasticModulusStdDev; }
            set
            {
                if (_Model.ElasticModulusStdDev != value)
                {
                    _Model.ElasticModulusStdDev = value;
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
        public ObservableCollection<int> SettlementNodeList
        {
            get { return _Model.SettlementNodeList; }
            set
            {
                if (_Model.SettlementNodeList != value)
                {
                    _Model.SettlementNodeList = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public ObservableCollection<Tuple<int, int>> DifferentialNodePairList
        {
            get { return _Model.DifferentialNodePairList; }
            set
            {
                if (_Model.DifferentialNodePairList != value)
                {
                    _Model.DifferentialNodePairList = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public ObservableCollection<RigidFootingLoad2D> RigidFootingLoads
        {
            get { return _Model.RigidFootingLoads; }
            set
            {
                if (_Model.RigidFootingLoads != value)
                {
                    _Model.RigidFootingLoads = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public ObservableCollection<UniformDistributedLoad2D> UniformLoads
        {
            get { return _Model.UniformLoads; }
            set
            {
                if(_Model.UniformLoads != value)
                {
                    _Model.UniformLoads = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public ObservableCollection<LineLoad2D> LineLoads
        {
            get { return _Model.LineLoads; }
            set
            {
                if (_Model.LineLoads != value)
                {
                    _Model.LineLoads = value;
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
                return _Model.DataFileLocation();
            }
        }

        public string SummaryFilePath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RFEM_Software\\" +
                    _Model.BaseName + ".stt";
            }
        }


        public Program Type
        {
            get
            {
                return Program.RSetl2D;
            }
        }

        public bool CanDisplaySummaryStats
        {
            get
            {
                return _Model.CanDisplaySummaryStats;
            }
        }

        public ISimModel Model
        {
            get
            {
                return _Model;
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
