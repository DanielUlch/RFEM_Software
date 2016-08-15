using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RFEMSoftware.Simulation.Infrastructure.Models
{
    public class RSetl2D: ISimModel, INotifyPropertyChanged
    {
        private bool _CanDisplaySummaryStats;

        #region Form Properties


        public string JobTitle { get; set; }
        public string BaseName { get; set; }
        public bool EchoInputDataToOutputFile { get; set; }
        public bool OutputDebugInfo { get; set; }

        public bool ProduceDisplayOfFirstLogElasticModulusField { get; set; }
        public bool ProducePSPlotOfFirstDisplacedMesh { get; set; }
        public bool OverlayLogElasticModulusFieldOnDisplacedMesh { get; set; }
        public bool ShowProblemDimensionsOnDisplacedMesh { get; set; }
        public bool ShowTitlesOnDisplacedMesh { get; set; }

        public double DisplacementMagInXDir { get; set; }
        public double DisplacementMagInYDir { get; set; }
        public double DisplacedMeshWidth { get; set; }

        public bool UseKGSelectiveReducedIntegration { get; set; }
        public bool OutputSettlementSamplesAndSummaryStats { get; set; }
        public bool OutputBlockModulusSamplesAndSummaryStats { get; set; }
        public bool OutputFieldAveragedModulusSamplesAndSummaryStats { get; set; }
        public bool GenerateUniformRandomFields { get; set; }

        public int NElementsInXDir { get; set; }
        public int NElementsInYDir { get; set; }

        public double ElementSizeInXDir { get; set; }
        public double ElementSizeInYDir { get; set; }

        public int NumberOfRealizations { get; set; }
        public int GeneratorSeed { get; set; }

        public double PoissonsRatio { get; set; }
        public double ElasticModulusMean { get; set; }
        public double ElasticModulusStdDev { get; set; }

        public int CorrelationLengthInXDir { get; set; }
        public int CorrelationLengthInYDir { get; set; }

        public CovarianceFunction CovFunc { get; set; }

        public ObservableCollection<int> SettlementNodeList { get; set; } = new ObservableCollection<int>();
        public ObservableCollection<Tuple<int, int>> DifferentialNodePairList { get; set; } = 
            new ObservableCollection<Tuple<int, int>>();
        public ObservableCollection<RigidFootingLoad2D> RigidFootingLoads { get; set; } = 
            new ObservableCollection<RigidFootingLoad2D>();
        public ObservableCollection<UniformDistributedLoad2D> UniformLoads { get; set; } = 
            new ObservableCollection<UniformDistributedLoad2D>();
        public ObservableCollection<LineLoad2D> LineLoads { get; set; } = 
            new ObservableCollection<LineLoad2D>();

        #endregion

        public Program Type
        {
            get
            {
                return Program.RSetl2D;
            }
        }

        public bool CanDisplaySummaryStats
        {
            get { return _CanDisplaySummaryStats; }
            set
            {
                if (_CanDisplaySummaryStats != value)
                {
                    _CanDisplaySummaryStats = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string AppDataFileLocation
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RFEM_Software\\" + BaseName + ".dat";
            }
        }
        

        public string DataFileLocation()
        {
            string appFileName = Environment.GetCommandLineArgs()[0];
            string directory = System.IO.Path.GetDirectoryName(appFileName);

            return directory + "\\" + BaseName + ".dat";
        }

        public string RunSim(IProgress<int> simIteration, IProgress<string> currentOp, CancellationToken token)
        {
            var pInfo = new ProcessStartInfo();
            Process p;
            string Line = "";
            string ProgramOutput = "";
            string appFileName = Environment.GetCommandLineArgs()[0];
            string directory = System.IO.Path.GetDirectoryName(appFileName);
            char ch;
            int progress = 0;
            string partialLine = "";

            directory += "\\Executables\\rsetl2d.exe";

            directory = "\"" + directory + "\"";

            pInfo.FileName = directory;
            pInfo.RedirectStandardOutput = true;
            pInfo.Arguments = "\"" + AppDataFileLocation + "\"";
            pInfo.UseShellExecute = false;
            pInfo.CreateNoWindow = true;
            p = new Process() { StartInfo = pInfo };

            p.Start();

            currentOp.Report("Running Simulation");

            using (var reader = p.StandardOutput)
            {
                while (!reader.EndOfStream && !token.IsCancellationRequested)
                {
                    Line = reader.ReadLine();

                    if (Line.Length >= 12 && Line.Substring(0, 12).Trim() == "Realization:")
                    {
                        while (progress < (int)NumberOfRealizations && !token.IsCancellationRequested)
                        {
                            ch = (char)reader.Read();
                            if (ch == ' ' || ch == '\r')
                            {
                                if (partialLine.Length != 0)
                                {
                                    bool result = Int32.TryParse(partialLine, out progress);
                                    if (result)
                                    {
                                        simIteration.Report(progress);
                                    }
                                }
                                partialLine = "";
                            }
                            else
                            {
                                partialLine += ch;
                            }
                            ProgramOutput += ch;
                        }

                    }
                    ProgramOutput += Line + Environment.NewLine;
                }
            }

            CanDisplaySummaryStats = true;


            currentOp.Report("Finished");


            return ProgramOutput;
        }

        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion


    }
    public struct RigidFootingLoad2D:INotifyPropertyChanged
    {
        private int _LeftNode;
        private int _RightNode;
        private double _Load;
        private bool _RoughInterface;

        public int LeftNode
        {
            get { return _LeftNode; }
            set
            {
                if(_LeftNode != value)
                {
                    _LeftNode = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int RightNode
        {
            get { return _RightNode; }
            set
            {
                if(_RightNode != value)
                {
                    _RightNode = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double Load
        {
            get { return _Load; }
            set
            {
                if(_Load != value)
                {
                    _Load = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool RoughInterface
        {
            get { return _RoughInterface; }
            set
            {
                if(_RoughInterface != value)
                {
                    _RoughInterface = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public struct UniformDistributedLoad2D : INotifyPropertyChanged
    {
        private int _LeftNode;
        private int _RightNode;
        private double _Load;

        public int LeftNode
        {
            get { return _LeftNode; }
            set
            {
                if (_LeftNode != value)
                {
                    _LeftNode = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int RightNode
        {
            get { return _RightNode; }
            set
            {
                if (_RightNode != value)
                {
                    _RightNode = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double Load
        {
            get { return _Load; }
            set
            {
                if (_Load != value)
                {
                    _Load = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
    }
    public struct LineLoad2D : INotifyPropertyChanged
    {
        private int _Node;
        private double _Load;

        public int Node
        {
            get { return _Node; }
            set
            {
                if (_Node != value)
                {
                    _Node = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double Load
        {
            get { return _Load; }
            set
            {
                if (_Load != value)
                {
                    _Load = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
