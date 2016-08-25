
using RFEMSoftware.Simulation.Desktop.CustomControls;
using RFEMSoftware.Simulation.Desktop.Forms;
using RFEMSoftware.Simulation.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEMSoftware.Simulation.Desktop
{
    public class ViewModelRepository
    {
        private ViewModelFactory _Factory;

        public ViewModelRepository(ViewModelFactory factory)
        {
            _Factory = factory;
        }

        public List<ISimViewModel> LoadStoredTabs()
        {
            var list = new List<ISimViewModel>();

            //If the tab collection exists
            if (Properties.Settings.Default.TabsOpenOnClose != null)
            {

                //Iterate through each string in the collection
                foreach (string s in Properties.Settings.Default.TabsOpenOnClose)
                {
                    //Wrapped in a try block incase files have been misplaced and cannot be loaded
                    try
                    {
                        list.Add(RecreateTab(s));
                    }
                    catch (Exception ex)
                    {
                        //do nothing if tab cannot be loaded
                    }
                }
            }

            return list;
        }
        public void StoreOpenTabs(List<ISimViewModel> openTabs)
        {
            string StorageString;
            Properties.Settings.Default.TabsOpenOnClose = new System.Collections.Specialized.StringCollection();

            //Iterate through each open tab
            foreach (ISimViewModel tab in openTabs)
            {
                StorageString = "";

                StorageString = CreateStorageString(tab);

                //If there is a string, add it to the collection
                if (StorageString != "")
                {
                    Properties.Settings.Default.TabsOpenOnClose.Add(StorageString);
                }
            }

            //Save settings
            Properties.Settings.Default.Save();
        }
        private ISimViewModel RecreateTab(string storageString)
        {
            string[] tabInfo;
            Program type;
            string filePath;
            string InnerStorageString;

            //Split the string at every semi-colon
            tabInfo = storageString.Split(';');

            filePath = tabInfo[0];

            //Determine the tabtype
            type = (Program)Enum.Parse(typeof(Program), tabInfo[1]);

            InnerStorageString = tabInfo[2];

            ISimViewModel viewModel = _Factory.CreateViewModel(type, filePath);

            switch (type)
            {
                case Program.RBear2D:
                    RestoreRBear2DTab(InnerStorageString, (RBear2DViewModel)viewModel);
                    break;
                case Program.RDam2D:
                    RestoreRDam2DTab(InnerStorageString, (RDam2DViewModel)viewModel);
                    break;
            }

            return viewModel;
            
        }

        private string CreateStorageString(ISimViewModel viewModel)
        {
            string StorageString = viewModel.DataFilePath + ";" + viewModel.Type + ";" + viewModel.StorageString;
            
            return StorageString;
        }
        private void RestoreRBear2DTab(string storageString, RBear2DViewModel viewModel)
        {
            var splitString = storageString.Split(',');

            foreach (string s in splitString)
            {
                RBear2DItem item;
                if(Enum.TryParse(s, out item))
                {
                    switch (item)
                    {
                        case RBear2DItem.DataFile:
                            viewModel.ShowDataTab();
                            break;
                        case RBear2DItem.SummaryStats:
                            viewModel.ShowSummaryStats();
                            break;
                        case RBear2DItem.BearingHist:
                            viewModel.ShowBearingHistTab();
                            break;
                    }
                }
            }

        }
        private void RestoreRDam2DTab(string storageString, RDam2DViewModel viewModel)
        {
            var splitString = storageString.Split(',');

            foreach(string s in splitString)
            {
                RDam2DItem item;
                if(Enum.TryParse(s, out item))
                {
                    switch (item)
                    {
                        case RDam2DItem.DataFile:
                            viewModel.ShowDataTab();
                            break;
                        case RDam2DItem.SummaryStats:
                            viewModel.ShowSummaryStats();
                            break;
                        case RDam2DItem.ConductivityHist:
                            viewModel.ShowConductivityHist();
                            break;
                        case RDam2DItem.FlowRateHist:
                            viewModel.ShowFlowRateHist();
                            break;
                        case RDam2DItem.NodalGradientHist:
                            viewModel.ShowNodalGradientHist();
                            break;
                    }
                }

            }
        }
    }
}
