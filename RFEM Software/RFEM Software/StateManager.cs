using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFEM_Infrastructure;
using System.ComponentModel;
using RFEM_Software.Custom_Controls;
using System.Windows;
using RFEM_Software.Forms;
using System.Collections.ObjectModel;

namespace RFEM_Software
{
    public class StateManager: INotifyPropertyChanged 
    {

        private RFEMTabItem _ActiveTab;

        private SimManager _SimManager;

        private ISimViewModel _ActiveScreenViewModel;

        public StateManager(SimManager manager)
        {
            _SimManager = manager;
            _SimManager.PropertyChanged += SimManagerPropertyChanged;
            _SimManager.SimCompleted += SimCompleted;
            _SimManager.SimQueueChanged += SimQueueChanged;
        }
        public ISimViewModel ActiveScreenViewModel
        {
            get { return _ActiveScreenViewModel; }
        }


        public Program ActiveProgram
        {
            get
            {
                if (_ActiveScreenViewModel != null)
                    return _ActiveScreenViewModel.Type;
                else
                    return Program.None;
            }
        }

       


        #region Ribbon Visibility and Enabled Binders

        public RunSimButtonState RunButtonState
        {
            get
            {
                if(_SimManager.State == SimManagerState.Idle)
                {
                    return RunSimButtonState.ReadyToRun;
                }
                else if(_SimManager.ActiveSim == _ActiveScreenViewModel.Model)
                {
                    return RunSimButtonState.ReadyToCancel;
                }
                else if (_SimManager.QueueContainsSim(_ActiveScreenViewModel.Model))
                {
                    return RunSimButtonState.ReadyToRemoveFromQueue;
                }
                else
                {
                    return RunSimButtonState.ReadyToQueue;
                }

            }
        }

        public bool SummaryStatsEnabled
        {
            get
            {
                return ActiveProgram != Program.None &&
                       _ActiveScreenViewModel.CanDisplaySummaryStats;
            }
        }

        //RBear Bindings
        public Visibility RBear2DButtonVisibility
        {
            get
            {
                if (ActiveProgram == Program.RBear2D)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }
        public bool CanDisplayRBear2DMesh
        {
            get
            {
                return (ActiveProgram == Program.RBear2D &&
                        ((RBear2DViewModel)_ActiveScreenViewModel).CanDisplayMesh);
            }
        }
        public bool CanDisplayRBear2DField
        {
            get
            {
                return (ActiveProgram == Program.RBear2D &&
                        ((RBear2DViewModel)_ActiveScreenViewModel).CanDisplayField);
            }
        }
        public bool CanDisplayRBear2DHist
        {
            get
            {
                return (ActiveProgram == Program.RBear2D &&
                        ((RBear2DViewModel)_ActiveScreenViewModel).CanDisplayBearingHist);
            }
        }


        //RDam Bindings
        public Visibility RDam2DButtonVisibility
        {
            get
            {
                if (ActiveProgram == Program.RDam2D)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public bool CanDisplayRDam2DFlownet
        {
            get
            {
                return (ActiveProgram == Program.RDam2D &&
                        ((RDam2DViewModel)_ActiveScreenViewModel).CanDisplayFlownet);
            }
        }
        
        public bool CanDisplayRDam2DField
        {
            get
            {
                return (ActiveProgram == Program.RDam2D &&
                        ((RDam2DViewModel)_ActiveScreenViewModel).CanDisplayField);
            }
        }

        public bool CanDisplayRDam2DMeanFields
        {
            get
            {
                return (ActiveProgram == Program.RDam2D &&
                        ((RDam2DViewModel)_ActiveScreenViewModel).CanDisplayGradientMeanAndStdDevFields);
            }
        }

        public bool CanDisplayRDam2DFlowHist
        {
            get
            {
                return (ActiveProgram == Program.RDam2D &&
                        ((RDam2DViewModel)_ActiveScreenViewModel).CanDisplayFlowRateHist);
            }
        }
        public bool CanDisplayRDam2DConductivityHist
        {
            get
            {
                return (ActiveProgram == Program.RDam2D &&
                        ((RDam2DViewModel)_ActiveScreenViewModel).CanDisplayEffectiveConductivityHist);
            }
        }

        public bool CanDisplayRDam2DNodalGradientHist
        {
            get
            {
                return (ActiveProgram == Program.RDam2D &&
                        ((RDam2DViewModel)_ActiveScreenViewModel).CanDisplayNodeGradientHist);
            }
        }

        //REarth Bindings

        public Visibility REarth2DButtonVisibility
        {
            get
            {
                if (ActiveProgram == Program.REarth2D)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public bool CanDisplayREarth2DField
        {
            get
            {
                return (ActiveProgram == Program.REarth2D &&
                        ((REarth2D)_ActiveScreenViewModel.Model).CanDisplayField);
            }
        }
        public bool CanDisplayREarth2DMesh
        {
            get
            {
                return (ActiveProgram == Program.REarth2D &&
                        ((REarth2D)_ActiveScreenViewModel.Model).CanDisplayMesh);
            }
        }
        #endregion







        public event PropertyChangedEventHandler PropertyChanged;




        public void SetActiveScreen(ISimViewModel activeScreenViewModel)
        {
            _ActiveScreenViewModel = activeScreenViewModel;

            NotifyAllChanged();
        }
        private void NotifyAllChanged()
        {
            NotifyPropertyChanged("ActiveProgram");
            NotifyPropertyChanged("SummaryStatsEnabled");

            NotifyPropertyChanged("RBear2DButtonVisibility");
            NotifyPropertyChanged("CanDisplayRBear2DMesh");
            NotifyPropertyChanged("CanDisplayRBear2DField");
            NotifyPropertyChanged("CanDisplayRBear2DHist");

            NotifyPropertyChanged("RDam2DButtonVisibility");
            NotifyPropertyChanged("CanDisplayRDam2DFlownet");
            NotifyPropertyChanged("CanDisplayRDam2DField");
            NotifyPropertyChanged("CanDisplayRDam2DMeanFields");
            NotifyPropertyChanged("CanDisplayRDam2DFlowHist");
            NotifyPropertyChanged("CanDisplayRDam2DConductivityHist");
            NotifyPropertyChanged("CanDisplayRDam2DNodalGradientHist");

            NotifyPropertyChanged("REarth2DButtonVisibility");
            NotifyPropertyChanged("CanDisplayREarth2DField");
            NotifyPropertyChanged("CanDisplayREarth2DMesh");

        }
        private void UpdateEnabledState()
        {
            NotifyPropertyChanged("SummaryStatsEnabled");

            switch (ActiveProgram)
            {
                case Program.None:
                    return;
                case Program.RBear2D:
                    NotifyPropertyChanged("CanDisplayRBear2DMesh");
                    NotifyPropertyChanged("CanDisplayRBear2DField");
                    NotifyPropertyChanged("CanDisplayRBear2DHist");
                    return;
                case Program.RDam2D:
                    NotifyPropertyChanged("CanDisplayRDam2DFlownet");
                    NotifyPropertyChanged("CanDisplayRDam2DField");
                    NotifyPropertyChanged("CanDisplayRDam2DMeanFields");
                    NotifyPropertyChanged("CanDisplayRDam2DFlowHist");
                    NotifyPropertyChanged("CanDisplayRDam2DConductivityHist");
                    NotifyPropertyChanged("CanDisplayRDam2DNodalGradientHist");
                    return;
                case Program.REarth2D:
                    NotifyPropertyChanged("CanDisplayREarth2DField");
                    NotifyPropertyChanged("CanDisplayREarth2DMesh");
                    return;
                default:
                    throw new NotImplementedException();
            }
        }
        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void SimManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged("RunButtonState");
        }

        private void SimCompleted()
        {
            UpdateEnabledState();
            NotifyPropertyChanged("RunButtonState");
        }
        private void SimQueueChanged()
        {
            NotifyPropertyChanged("RunButtonState");
        }


        
    }
    public enum RunSimButtonState
    {
        ReadyToRun,
        ReadyToQueue,
        ReadyToCancel,
        ReadyToRemoveFromQueue
    }
    public enum RunSimButtonCommand
    {
        Run,
        AddToQueue,
        CancelRun,
        RemoveFromQueue
    }
}
