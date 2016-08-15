
using RFEMSoftware.Simulation.Infrastructure.Models;
using RFEMSoftware.Simulation.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace RFEMSoftware.Simulation.Infrastructure
{
    public class SimManager: INotifyPropertyChanged 
    {
        private List<ISimModel> _SimQueue = new List<ISimModel>();
        private List<SimWorker> _SimWorkers;
        private ObservableCollection<Tuple<string, DateTime>> _SimHistory = 
                                                    new ObservableCollection<Tuple<string, DateTime>>();

        private Dictionary<ISimModel, Tuple<SimWorker, CancellationTokenSource>> _ActiveSims =
                                                new Dictionary<ISimModel, Tuple<SimWorker, CancellationTokenSource>>();

        private SimWorker _FeaturedWorker;

        private int _NumberOfLogicalProcessors;

        private string _LastOutput;

        private delegate void SimAddedHandler();
        public delegate void SimQueueChangedHandler();
        public delegate void SimCompletedHandler();

        private event SimAddedHandler SimAdded;
        public event SimQueueChangedHandler SimQueueChanged;
        public event SimCompletedHandler SimCompleted;

        

        public event PropertyChangedEventHandler PropertyChanged;




        public List<string> SimQueue
        {
            get { return new List<string>(_SimQueue.Select(x => x.BaseName).ToList()); }
        }
        public List<SimWorker> SimWorkers
        {
            get { return _SimWorkers; }
        }


        public SimWorker FeaturedWorker
        {
            get { return _FeaturedWorker; }
            private set
            {
                if(_FeaturedWorker != value)
                {
                    _FeaturedWorker = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private void UpdateFeaturedWorker(SimWorker worker)
        {
            if(_FeaturedWorker == null || _FeaturedWorker.State == SimWorkerState.Idle)
            {
                var RunningWorkers = _SimWorkers.Where(x => x.State != SimWorkerState.Idle);
                if (RunningWorkers.Count() > 0)
                    FeaturedWorker = RunningWorkers.First();
            }
            if (_FeaturedWorker == null || _FeaturedWorker.State == SimWorkerState.Idle)
            { 
                FeaturedWorker = worker;
            }

        }
        private void UpdateFeaturedWorker()
        {
            if (_FeaturedWorker == null || _FeaturedWorker.State == SimWorkerState.Idle)
            {
                var RunningWorkers = _SimWorkers.Where(x => x.State != SimWorkerState.Idle);
                if (RunningWorkers.Count() > 0)
                    FeaturedWorker = RunningWorkers.First();
            }
        }

        public int NumberOfLogicalProcessors
        {
            get { return _NumberOfLogicalProcessors; }
        }


        public SimManager()
        {
            SimAdded += HandleNewSimAsync;
            SimCompleted += HandleCompletedSim;

            _NumberOfLogicalProcessors = Environment.ProcessorCount;

            //_SimWorkers = new List<SimWorker>(_NumberOfLogicalProcessors - 2);

            //for(int i=1;i<=_NumberOfLogicalProcessors - 2; i++)
            //{
            //    _SimWorkers.Add(new SimWorker());
            //}
            _SimWorkers = new List<SimWorker>(2);

            for (int i = 1; i <= 2; i++)
            {
                _SimWorkers.Add(new SimWorker());
            }
        }


        public bool QueueContainsSim(ISimModel sim)
        {
            return _SimQueue.Contains(sim);
        }
        


        public int QueueLength
        {
            get { return _SimQueue.Count; }
        }

        public ObservableCollection<Tuple<string, DateTime>> SimHistory
        {
            get { return _SimHistory; }
        }




        private async void HandleNewSimAsync()
        {
            var IdleWorkers = _SimWorkers.Where(x => x.State == SimWorkerState.Idle);

            if(IdleWorkers.Count() == 0)
            {
                return;
            }
            else
            {

                var SimToRun = _SimQueue.First();
                _SimQueue.Remove(SimToRun);
                var WorkerToUse = IdleWorkers.First();

                var TokenSource = new CancellationTokenSource();
                var token = TokenSource.Token;

                _ActiveSims.Add(SimToRun, new Tuple<SimWorker, CancellationTokenSource>(WorkerToUse, TokenSource));

                UpdateFeaturedWorker(WorkerToUse);
                NotifyAllChanged();


                _LastOutput = await IdleWorkers.First().ExecuteSimAsync(SimToRun, token);

                _ActiveSims.Remove(SimToRun);

                _SimHistory.Add(new Tuple<string, DateTime>(SimToRun.BaseName, DateTime.Now));

                SimCompleted();

            }

        }
        private async void HandleCompletedSim()
        {

            UpdateFeaturedWorker();

            if(_SimQueue.Count > 0)
            {
                var IdleWorkers = _SimWorkers.Where(x => x.State == SimWorkerState.Idle);

                if (IdleWorkers.Count() == 0)
                {
                    return;
                }
                else
                {

                    var SimToRun = _SimQueue.First();
                    _SimQueue.Remove(SimToRun);
                    var WorkerToUse = IdleWorkers.First();

                    var TokenSource = new CancellationTokenSource();
                    var token = TokenSource.Token;

                    _ActiveSims.Add(SimToRun, new Tuple<SimWorker, CancellationTokenSource>(WorkerToUse, TokenSource));

                    UpdateFeaturedWorker(WorkerToUse);
                    NotifyAllChanged();


                    _LastOutput = await IdleWorkers.First().ExecuteSimAsync(SimToRun, token);

                    _ActiveSims.Remove(SimToRun);

                    _SimHistory.Add(new Tuple<string, DateTime>(SimToRun.BaseName, DateTime.Now));

                    SimCompleted();

                }
            }
            else
            {
                return;
            }
        }
        

        public void AddSimToQueue(ISimModel simModel)
        {
            _SimQueue.Add(simModel);

            NotifyAllChanged();

            SimAdded();
        }
        public SimRunState QueryManager(ISimModel sim)
        {
            if (_ActiveSims.Keys.Contains(sim))
                return SimRunState.CanCancelSim;
            else if (_SimQueue.Contains(sim))
                return SimRunState.CanUnQueueSim;
            else if (_SimWorkers.Where(x => x.State == SimWorkerState.Idle).Count() > 0)
                return SimRunState.CanRunSim;
            else
                return SimRunState.CanQueueSim;
        }
        public void CancelSim(ISimModel simModel)
        {
            if (_ActiveSims.Keys.Contains(simModel))

                _ActiveSims[simModel].Item2.Cancel();

            else if (_SimQueue.Contains(simModel))
            {
                _SimQueue.Remove(simModel);
                NotifyAllChanged();
                SimQueueChanged();
            }
            else
                return;
        }




        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private void NotifyAllChanged()
        {
            NotifyPropertyChanged("SimQueue");
            NotifyPropertyChanged("QueueLength");
            NotifyPropertyChanged("State");
        }


    }

    public enum SimWorkerState
    {
        Idle,
        Operating
    }
    public enum SimRunState
    {
        CanRunSim,
        CanQueueSim,
        CanUnQueueSim,
        CanCancelSim
    }

}
