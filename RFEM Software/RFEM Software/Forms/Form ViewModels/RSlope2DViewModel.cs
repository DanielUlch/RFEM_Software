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

namespace RFEMSoftware.Simulation.Desktop.Forms
{
    class RSlope2DViewModel : INotifyPropertyChanged, IDataErrorInfo, ISimViewModel
    {
        private bool _ChangesHaveBeenMade;

        private RSlope2D _Model;

        private List<string> _Errors = new List<string>();

        public RSlope2DViewModel()
        {
            _Model = new RSlope2D();
        }
        public RSlope2DViewModel(RSlope2D model)
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
        public bool PlotARandomField
        {
            get { return _Model.PlotARandomField; }
            set
            {
                if (_Model.PlotARandomField != value)
                {
                    _Model.PlotARandomField = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public RSlopeSoilProperty RFPropertyToPlot
        {
            get { return _Model.RFPropertyToPlot; }
            set
            {
                if (_Model.RFPropertyToPlot != value)
                {
                    _Model.RFPropertyToPlot = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ProducePSPlotOfDisplacedMesh
        {
            get { return _Model.ProducePSPlotOfDisplacedMesh; }
            set
            {
                if (_Model.ProducePSPlotOfDisplacedMesh != value)
                {
                    _Model.ProducePSPlotOfDisplacedMesh = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowMeshOnDisplacedMesh
        {
            get { return _Model.ShowMeshOnDisplacedMesh; }
            set
            {
                if (_Model.ShowMeshOnDisplacedMesh != value)
                {
                    _Model.ShowMeshOnDisplacedMesh = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowRFOnDisplacedMesh
        {
            get { return _Model.ShowRFOnDisplacedMesh; }
            set
            {
                if (_Model.ShowRFOnDisplacedMesh != value)
                {
                    _Model.ShowRFOnDisplacedMesh = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool ShowLogRFOnDisplacedMesh
        {
            get { return _Model.ShowLogRFOnDisplacedMesh; }
            set
            {
                if (_Model.ShowLogRFOnDisplacedMesh != value)
                {
                    _Model.ShowLogRFOnDisplacedMesh = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public RSlopeSoilProperty DisplacedMeshPropertyToPlot
        {
            get { return _Model.DisplacedMeshPropertyToPlot; }
            set
            {
                if (_Model.DisplacedMeshPropertyToPlot != value)
                {
                    _Model.DisplacedMeshPropertyToPlot = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double DisplacedMeshPlotWidth
        {
            get { return _Model.DisplacedMeshPlotWidth; }
            set
            {
                if (_Model.DisplacedMeshPlotWidth != value)
                {
                    _Model.DisplacedMeshPlotWidth = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NElementsLeftOfEmbark
        {
            get { return _Model.NElementsLeftOfEmbark; }
            set
            {
                if (_Model.NElementsLeftOfEmbark != value)
                {
                    _Model.NElementsLeftOfEmbark = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NElementsRightOfEmbark
        {
            get { return _Model.NElementsRightOfEmbark; }
            set
            {
                if (_Model.NElementsRightOfEmbark != value)
                {
                    _Model.NElementsRightOfEmbark = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NElementsInEmbark
        {
            get { return _Model.NElementsInEmbark; }
            set
            {
                if (_Model.NElementsInEmbark != value)
                {
                    _Model.NElementsInEmbark = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NElementsInFoundations
        {
            get { return _Model.NElementsInFoundations; }
            set
            {
                if (_Model.NElementsInFoundations != value)
                {
                    _Model.NElementsInFoundations = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double SlopeGradient
        {
            get { return _Model.SlopeGradient; }
            set
            {
                if (_Model.SlopeGradient != value)
                {
                    _Model.SlopeGradient = value;
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
        public double ConvergenceTolerance
        {
            get { return _Model.ConvergenceTolerance; }
            set
            {
                if (_Model.ConvergenceTolerance != value)
                {
                    _Model.ConvergenceTolerance = value;
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
        public List<double> StrengthReductionFactors
        {
            get { return _Model.StrengthReductionFactors; }
            set
            {
                if (_Model.StrengthReductionFactors != value)
                {
                    _Model.StrengthReductionFactors = value;
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
        public DistributionInfo UnitWeight
        {
            get { return _Model.UnitWeight; }
            set
            {
                if (_Model.UnitWeight != value)
                {
                    _Model.UnitWeight = value;
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
                return Program.RSlope2D;
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
