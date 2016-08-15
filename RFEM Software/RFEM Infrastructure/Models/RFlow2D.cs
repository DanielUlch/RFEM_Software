using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RFEMSoftware.Simulation.Infrastructure.Models
{
    public class RFlow2D : ISimModel, INotifyPropertyChanged
    {
        private bool _CanDisplaySummaryStats;
        private bool _CanDisplayFlownet;
        private bool _CanDisplayField;

        

        #region Form Properties

        public string JobTitle { get; set; }
        public string BaseName { get; set; }
        public bool EchoInputDataToOutputFile { get; set; }
        public bool OutputDebugInfo { get; set; }
        public bool ProduceDisplayOfFirstLogConductivityField { get; set; }
        public bool ProducePSPlotOfFirstFlownet { get; set; }
        public bool ProduceDisplayOfTotalHeadMeanAndStdDev { get; set; }
        public bool OutputFlowRateExitGradientUpliftForce { get; set; }
        public bool OutputDetailedExitGradientInfo { get; set; }
        public bool OutputBlockHydraulicConductivities { get; set; }
        public bool OutputArithmeticGeometricHarmonicHydraulicConductivityAvgs { get; set; }
        public bool GenerateUniformConductivityField { get; set; }
        public int NumberOfWalls { get; set; }

        public int NElementsInXDir { get; set; }
        public int NElementsInYDir { get; set; }

        public int NElementsLeftOfWall { get; set; }
        public int NElementsRightOfWall { get; set; }
        public int DepthOfWall { get; set; }

        public int NElementsLeftOfLeftWall { get; set; }
        public int NElementsBetweenWall { get; set; }
        public int NElementsToTheRightOfRightWall { get; set; }
        public int DepthOfLeftWall { get; set; }
        public int DepthOfRightWall { get; set; }

        public double ElementDimensionHorrizontal { get; set; }
        public double ElementDimensionVertical { get; set; }

        public int NumberOfRealizations { get; set; }
        public int GeneratorSeed { get; set; }
        
        public int CorrelationLengthInXDir { get; set; }
        public int CorrelationLengthInYDir { get; set; }

        public double HydraulicConductivityMean { get; set; }
        public double HydraulicConductivityStdDev { get; set; }

        public CovarianceFunction CovFunc { get; set; }

        public int NContoursForMeanTotalHead { get; set; }
        public int NContoursForStdDevTotalHead { get; set; }
        public int NEquipotentialDrops { get; set; }
        public bool ShowLogConductivityFieldOnFlownet { get; set; }
        public bool ShowSoilMassDimensionsOnFlownet { get; set; }
        public bool ShowTitlesOnFlownet { get; set; }
        public double FlownetWidth { get; set; }

        #endregion

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
        public bool CanDisplayFlownet
        {
            get { return _CanDisplayFlownet; }
            set
            {
                if(_CanDisplayFlownet != value)
                {
                    _CanDisplayFlownet = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool CanDisplayField
        {
            get { return _CanDisplayField; }
            set
            {
                if(_CanDisplayField != value)
                {
                    _CanDisplayField = value;
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

        public Program Type
        {
            get
            {
                return Program.RFlow2D;
            }
        }

        

        public string RunSim(IProgress<int> simIteration, IProgress<string> currentOp, CancellationToken token)
        {
            currentOp.Report("Initializing");

            var ctr = default(CancellationTokenRegistration);

            if (token.CanBeCanceled)
            {
                ctr = token.Register(() =>
                {
                    currentOp.Report("Canceled");
                    //throw new OperationCanceledException();
                });
            }

            var pInfo = new ProcessStartInfo();
            Process p;
            string Line = "";
            string ProgramOutput = "";
            string appFileName = Environment.GetCommandLineArgs()[0];
            string directory = System.IO.Path.GetDirectoryName(appFileName);
            char ch;
            int progress = 0;
            string partialLine = "";

            directory += "\\Executables\\rflow2d.exe";

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

                    if (Line == "Analyzing realization:")
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
}
