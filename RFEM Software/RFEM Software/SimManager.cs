using RFEM_Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace RFEM_Software
{
    public class SimManager: INotifyPropertyChanged 
    {
        private List<ISimModel> _SimQueue = new List<ISimModel>();

        private ISimModel _ActiveSim;
        private CancellationTokenSource _TokenSource;

        private int _NumberOfLogicalProcessors;

        private string _CurrentOperation;
        private int _ProgressPercentage;
        private string _ProgressDetails;

        private string _LastOutput;

        private delegate void SimAddedHandler();
        public delegate void SimQueueChangedHandler();
        public delegate void SimCompletedHandler();

        private event SimAddedHandler SimAdded;
        public event SimQueueChangedHandler SimQueueChanged;
        public event SimCompletedHandler SimCompleted;

        public event PropertyChangedEventHandler PropertyChanged;

        public string CurrentOperation
        {
            get { return _CurrentOperation; }
        }
        public int ProgressPercentage
        {
            get { return _ProgressPercentage; }
        }
        public string ProgressDetails
        {
            get { return _ProgressDetails; }
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
        }

        public bool QueueContainsSim(ISimModel sim)
        {
            return _SimQueue.Contains(sim);
        }

        public ISimModel ActiveSim
        {
            get
            {
                return _ActiveSim;
            }
        }
        public int QueueLength
        {
            get { return _SimQueue.Count; }
        }
        public SimManagerState State
        {
            get
            {
                if(ActiveSim == null)
                {
                    return SimManagerState.Idle;
                }
                else
                {
                    return SimManagerState.Operating;
                }
            }
        }


        private async void HandleNewSimAsync()
        {
            if(_ActiveSim != null)
            {
                return;
            }
            else
            {
                LoadSim();

                _LastOutput = await RunLoadedSim();

                _ActiveSim = null;
                _TokenSource = null;

                SimCompleted();

            }

        }
        private async void HandleCompletedSim()
        {
            if(_SimQueue.Count > 0)
            {
                LoadSim();

                _LastOutput = await RunLoadedSim();

                _ActiveSim = null;
                _TokenSource = null;

                SimCompleted();
            }
            else
            {
                return;
            }
        }

        private void LoadSim()
        {
            _ActiveSim = _SimQueue.First();
            _SimQueue.Remove(_ActiveSim);

            NotifyAllChanged();
        }

        public void AddSimToQueue(ISimModel simModel)
        {
            _SimQueue.Add(simModel);

            NotifyAllChanged();

            SimAdded();
        }
        public void CancelSim(ISimModel simModel)
        {
            if (_ActiveSim == simModel)
                _TokenSource.Cancel();
            else if (_SimQueue.Contains(simModel))
            {
                _SimQueue.Remove(simModel);
                SimQueueChanged();
            }
            else
                return;
        }

        private async Task<string> RunLoadedSim()
        {

            _TokenSource = new CancellationTokenSource();
            var token = _TokenSource.Token;

            string Output = string.Empty;

            try
            {

                if (false /*check for errors*/)
                {
                    MessageBox.Show("Please correct errors in form before running the simulation.");
                    return "";
                }

                FileWriter.Write(_ActiveSim);

                int nSim = _ActiveSim.NumberOfRealizations;
                    //Run the simulation asynchronously
                    Output = await Task<string>.Run(() => _ActiveSim.RunSim(
                                new Progress<int>(p =>
                                {
                                    _ProgressPercentage = p * 100 / nSim;
                                    _ProgressDetails = string.Format("Realization {0}/{1}", p, nSim);
                                    NotifyPropertyChanged("ProgressPercentage");
                                    NotifyPropertyChanged("ProgressDetails");
                                }),
                                new Progress<string>(ps =>
                                {
                                    _CurrentOperation = ps;
                                    NotifyPropertyChanged("CurrentOperation");
                                }), token));


                _ProgressPercentage = 0;
                _ProgressDetails = "";
                NotifyPropertyChanged("ProgressPercentage");
                NotifyPropertyChanged("ProgressDetails");

                //If the simulation was not cancelled ping
                if (!token.IsCancellationRequested)
                {
                    System.Media.SystemSounds.Asterisk.Play();
                }

            }
            catch (OperationCanceledException oex)
            {
                _ProgressDetails = "Run Canceled";
                _ProgressPercentage = 0;
                NotifyPropertyChanged("ProgressPercentage");
                NotifyPropertyChanged("ProgressDetails");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return Output;
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
            NotifyPropertyChanged("ActiveSim");
            NotifyPropertyChanged("QueueLength");
            NotifyPropertyChanged("State");
        }


    }

    public enum SimManagerState
    {
        Idle,
        Operating
    }

}
