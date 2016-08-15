using RFEMSoftware.Simulation.Infrastructure.Models;
using RFEMSoftware.Simulation.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RFEMSoftware.Simulation.Infrastructure
{
    public class SimWorker: INotifyPropertyChanged
    {
        private int _ProgressPercentage;
        private string _CurrentOperation;
        private string _ProgressDetails;

        private ISimModel _ActiveSim;

        private SimWorkerState _State;

        public event PropertyChangedEventHandler PropertyChanged;

        public int ProgressPercentage
        {
            get { return _ProgressPercentage; }
            private set
            {
                if(_ProgressPercentage != value)
                {
                    _ProgressPercentage = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string CurrentOperation
        {
            get { return _CurrentOperation; }
            private set
            {
                if(_CurrentOperation != value)
                {
                    _CurrentOperation = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string CurrentSimBaseName
        {
            get { return _ActiveSim == null ? "" : _ActiveSim.BaseName; }
        }
        public ISimModel ActiveSim
        {
            get { return _ActiveSim; }
            private set
            {
                if(_ActiveSim != value)
                {
                    _ActiveSim = value;
                    NotifyPropertyChanged("CurrentSimBaseName");
                }
            }
        }
        public string ProgressDetails
        {
            get { return _ProgressDetails; }
            private set
            {
                if(_ProgressDetails != value)
                {
                    _ProgressDetails = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public SimWorkerState State
        {
            get { return _State; }
            private set
            {
                if(_State != value)
                {
                    _State = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public async Task<string> ExecuteSimAsync(ISimModel sim, CancellationToken token)
        {
            State = SimWorkerState.Operating;
            ActiveSim = sim;

            string Output = "";
            try
            {
                FileWriter.Write(_ActiveSim);

                int nSim = _ActiveSim.NumberOfRealizations;
                //Run the simulation asynchronously
                Output = await Task<string>.Run(() => _ActiveSim.RunSim(
                            new Progress<int>(p =>
                            {
                                ProgressPercentage = p * 100 / nSim;
                                ProgressDetails = string.Format("Realization {0}/{1}", p, nSim);
                            }),
                            new Progress<string>(ps =>
                            {
                                CurrentOperation = ps;
                            }), token));


                ProgressPercentage = 0;
                ProgressDetails = "";

                //If the simulation was not cancelled ping
                if (!token.IsCancellationRequested)
                {
                    System.Media.SystemSounds.Asterisk.Play();
                }
            }
            catch (OperationCanceledException oex)
            {
                ProgressDetails = "Run Canceled";
                ProgressPercentage = 0;
            }

            State = SimWorkerState.Idle;
            return Output;
            
        }

        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
