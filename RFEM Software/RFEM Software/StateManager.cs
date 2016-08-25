using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Collections.ObjectModel;
using RFEMSoftware.Simulation.Desktop.CustomControls;
using RFEMSoftware.Simulation.Infrastructure;
using RFEMSoftware.Simulation.Desktop.Forms;
using RFEMSoftware.Simulation.Infrastructure.Models;
using System.Windows.Media.Imaging;

namespace RFEMSoftware.Simulation.Desktop
{
    public class StateManager: INotifyPropertyChanged 
    {

        private SimManager _SimManager;

        private RunSimButton _RunButton = new RunSimButton();

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

        public SimRunState RunButtonState
        {
            get
            {
                return _SimManager.QueryManager(_ActiveScreenViewModel.Model);

            }
        }
        public RunSimButton RunButton
        {
            get { return _RunButton; }
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
                        ((REarth2DViewModel)_ActiveScreenViewModel).CanDisplayField);
            }
        }
        public bool CanDisplayREarth2DMesh
        {
            get
            {
                return (ActiveProgram == Program.REarth2D &&
                        ((REarth2DViewModel)_ActiveScreenViewModel).CanDisplayMesh);
            }
        }

        //RFlow2D bidnings

        public Visibility RFlow2DButtonVisibility
        {
            get
            {
                if (ActiveProgram == Program.RFlow2D)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }
        public bool CanDisplayRFlow2DFlownet
        {
            get
            {
                return (ActiveProgram == Program.RFlow2D &&
                        ((RFlow2DViewModel)_ActiveScreenViewModel).CanShowFlownet);
            }
        }
        public bool CanDisplayRFlow2DField
        {
            get
            {
                return (ActiveProgram == Program.RFlow2D &&
                        ((RFlow2DViewModel)_ActiveScreenViewModel).CanShowField);
            }
        }

        //RFlow3D Bindings


        //RPill2D Bindings
        
        public Visibility RPill2DButtonVisibility
        {
            get
            {
                if(ActiveProgram == Program.RPill2D)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public bool CanDisplayRPill2DField
        {
            get
            {
                return (ActiveProgram == Program.RPill2D &&
                        ((RPill2DViewModel)_ActiveScreenViewModel).CanDisplayField);
            }
        }
        public bool CanDisplayRPill2DMesh
        {
            get
            {
                return (ActiveProgram == Program.RPill2D &&
                        ((RPill2DViewModel)_ActiveScreenViewModel).CanDisplayMesh);
            }
        }

        //RPill3D Bindings

        public Visibility RPill3DButtonVisibility
        {
            get
            {
                if (ActiveProgram == Program.RPill3D)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public bool CanDisplayRPill3DField
        {
            get
            {
                return (ActiveProgram == Program.RPill3D &&
                        ((RPill3DViewModel)_ActiveScreenViewModel).CanDisplayField);
            }
        }

        //RSetl2D Bindings

        public Visibility RSetl2DButtonVisibility
        {
            get
            {
                if (ActiveProgram == Program.RSetl2D)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public bool CanDisplayRSetl2DField
        {
            get
            {
                return (ActiveProgram == Program.RSetl2D &&
                        ((RSetl2DViewModel)_ActiveScreenViewModel).CanDisplayField);
            }
        }
        public bool CanDisplayRSetl2DMesh
        {
            get
            {
                return (ActiveProgram == Program.RSetl2D &&
                        ((RSetl2DViewModel)_ActiveScreenViewModel).CanDisplayMesh);
            }
        }

        //RSetl3D Bindings

        public Visibility RSetl3DButtonVisibility
        {
            get
            {
                if (ActiveProgram == Program.RSetl3D)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public bool CanDisplayRSetl3DField
        {
            get
            {
                return (ActiveProgram == Program.RSetl3D &&
                        ((RSetl3DViewModel)_ActiveScreenViewModel).CanDisplayField);
            }
        }
        public bool CanDisplayRSetl3DMesh
        {
            get
            {
                return (ActiveProgram == Program.RSetl3D &&
                        ((RSetl3DViewModel)_ActiveScreenViewModel).CanDisplayMesh);
            }
        }

        //RSlope2D Bindings

        public Visibility RSlope2DButtonVisibility
        {
            get
            {
                if (ActiveProgram == Program.RSlope2D)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public bool CanDisplayRSlope2DField
        {
            get
            {
                return (ActiveProgram == Program.RSlope2D &&
                        ((RSlope2DViewModel)_ActiveScreenViewModel).CanDisplayField);
            }
        }
        public bool CanDisplayRSlope2DMesh
        {
            get
            {
                return (ActiveProgram == Program.RSlope2D &&
                        ((RSlope2DViewModel)_ActiveScreenViewModel).CanDisplayMesh);
            }
        }

        #endregion







        public event PropertyChangedEventHandler PropertyChanged;




        public void SetActiveScreen(ISimViewModel activeScreenViewModel)
        {
            _ActiveScreenViewModel = activeScreenViewModel;

            if(_ActiveScreenViewModel != null && ActiveScreenViewModel.Model != null)
                _RunButton.State = _SimManager.QueryManager(_ActiveScreenViewModel.Model);

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


            NotifyPropertyChanged("RFlow2DButtonVisibility");
            NotifyPropertyChanged("CanDisplayRFlow2DFlownet");
            NotifyPropertyChanged("CanDisplayRFlow2DField");

            NotifyPropertyChanged("RPill2DButtonVisibility");
            NotifyPropertyChanged("CanDisplayRPill2DField");
            NotifyPropertyChanged("CanDisplayRPill2DMesh");
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
                case Program.RFlow2D:
                    NotifyPropertyChanged("CanDisplayRFlow2DFlownet");
                    NotifyPropertyChanged("CanDisplayRFlow2DField");
                    return;
                case Program.RFlow3D:
                    return;
                case Program.RPill2D:
                    NotifyPropertyChanged("CanDisplayRPill2DField");
                    NotifyPropertyChanged("CanDisplayRPill2DMesh");
                    return;
                case Program.RPill3D:
                case Program.RSetl2D:
                case Program.RSetl3D:
                case Program.RSlope2D:
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
            if(_ActiveScreenViewModel != null)
                _RunButton.State = _SimManager.QueryManager(_ActiveScreenViewModel.Model);
        }

        private void SimCompleted()
        {
            UpdateEnabledState();
            _RunButton.State = _SimManager.QueryManager(_ActiveScreenViewModel.Model);
        }
        private void SimQueueChanged()
        {
            _RunButton.State = _SimManager.QueryManager(_ActiveScreenViewModel.Model);
        }


        
    }
    public class RunSimButton: INotifyPropertyChanged
    {
        private BitmapImage _ButtonImage;
        private string _Label;
        private RunSimButtonCommand _Tag;

        public event PropertyChangedEventHandler PropertyChanged;

        public SimRunState State
        {
            set
            {
                switch (value)
                {
                    case SimRunState.CanRunSim:
                        ButtonImage = new BitmapImage(new Uri("Images/Start.png", UriKind.Relative));
                        Label = "Run Sim";
                        Tag = RunSimButtonCommand.Run;
                        break;
                    case SimRunState.CanCancelSim:
                        ButtonImage = new BitmapImage(new Uri("Images/Cancel.png", UriKind.Relative));
                        Label = "Cancel Run";
                        Tag = RunSimButtonCommand.CancelRun;
                        break;
                    case SimRunState.CanUnQueueSim:
                        ButtonImage = new BitmapImage(new Uri("Images/RemoveFromQueue.png", UriKind.Relative));
                        Label = "Remove From Queue";
                        Tag = RunSimButtonCommand.RemoveFromQueue;
                        break;
                    case SimRunState.CanQueueSim:
                        ButtonImage = new BitmapImage(new Uri("Images/AddToQueue.png", UriKind.Relative));
                        Label = "Add to Queue";
                        Tag = RunSimButtonCommand.AddToQueue;
                        break;
                }
            }
        }
        public BitmapImage ButtonImage
        {
            get { return _ButtonImage; }
            private set
            {
                if(_ButtonImage != value)
                {
                    _ButtonImage = value;
                    NotifyPropertyChanged();
                }

            }
        }
        public string Label
        {
            get { return _Label; }
            private set
            {
                if(_Label != value)
                {
                    _Label = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public RunSimButtonCommand Tag
        {
            get { return _Tag; }
            private set
            {
                if(_Tag != value)
                {
                    _Tag = value;
                    NotifyPropertyChanged();
                }
            }
        }



        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
    //public enum RunSimButtonState
    //{
    //    ReadyToRun,
    //    ReadyToQueue,
    //    ReadyToCancel,
    //    ReadyToRemoveFromQueue
    //}
    public enum RunSimButtonCommand
    {
        Run,
        AddToQueue,
        CancelRun,
        RemoveFromQueue
    }
}
