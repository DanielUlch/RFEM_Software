
using RFEMSoftware.Simulation.Desktop.CustomControls;
using RFEMSoftware.Simulation.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEMSoftware.Simulation.Desktop
{
    public class FormRepository
    {
        private FormFactory _Factory;

        public FormRepository(FormFactory factory)
        {
            _Factory = factory;
        }

        public List<RFEMTabItem> LoadStoredTabs()
        {
            var list = new List<RFEMTabItem>();

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
        public void StoreOpenTabs(List<RFEMTabItem> openTabs)
        {
            string StorageString;
            Properties.Settings.Default.TabsOpenOnClose = new System.Collections.Specialized.StringCollection();

            //Iterate through each open tab
            foreach (RFEMTabItem tab in openTabs)
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
        private RFEMTabItem RecreateTab(string storageString)
        {
            string[] tabInfo;
            RFEMTabType type;
            string tabName;
            string filePath;

            //Split the string at every semi-colon
            tabInfo = storageString.Split(';');

            //Determine the tabtype
            type = (RFEMTabType)Enum.Parse(typeof(RFEMTabType), tabInfo[0]);

            //Determine the tab name
            tabName = tabInfo[1];

            //Depending on the tab type, read the appropriate information from the string and load the tab
            switch (type)
            {
                case RFEMTabType.DataInput:
                    Program formType;
                    formType = (Program)Enum.Parse(typeof(Program), tabInfo[3]);
                    filePath = tabInfo[2];

                    return _Factory.CreateForm(formType, filePath);

                case RFEMTabType.Settings:
                    return _Factory.CreateSettingsTab();

                case RFEMTabType.Results:
                    Results resultsType = (Results)Enum.Parse(typeof(Results), tabInfo[3]);
                    filePath = tabInfo[2];
                    switch (resultsType)
                    {
                        case Results.Statistics:

                            return _Factory.CreateSummaryForm(filePath, tabName, null);

                        case Results.Histogram:

                            //var ProgramType = (Program)Enum.Parse(typeof(Program), tabInfo[4]);
                            //int NSim = int.Parse(tabInfo[5]);
                            //int NFootings = int.Parse(tabInfo[6]);
                            //string BaseName = tabInfo[7];

                            //return new HistogramTab(RFEMTabType.Results,
                            //                        _CloseTabHandler,
                            //                        _CloseAllTabsHandler,
                            //                        _CommandBindings,
                            //                        _Width,
                            //                        ProgramType,
                            //                        NSim,
                            //                        NFootings,
                            //                        BaseName,
                            //                        filePath);;


                            //Disabled until I find a better way

                            throw new NotImplementedException();
                            

                    }
                    break;
            }
            throw new NotImplementedException();
        }

        private string CreateStorageString(RFEMTabItem tab)
        {
            string StorageString = "";
            switch (tab.Type)
            {
                case RFEMTabType.Settings:
                    StorageString = RFEMTabType.Settings.ToString() + ";Settings";
                    break;
                case RFEMTabType.DataInput:
                    StorageString = RFEMTabType.DataInput.ToString();
                    var vm = ((DataEntryTab)tab).ViewModel;
                    StorageString += ";" + vm.BaseName + ";" + vm.DataFilePath + ";" + vm.Type.ToString();
                    break;
                case RFEMTabType.Results:
                    if (tab.GetType() == typeof(ResultsTab))
                    {
                        ResultsTab rTab = (ResultsTab)tab;
                        StorageString = RFEMTabType.Results.ToString() + ";" +
                                    rTab.TabName + ";" +
                                    rTab.FilePath + ";" +
                                    Results.Statistics;
                    }
                    else if (tab.GetType() == typeof(HistogramTab))
                    {
                        //HistogramTab hTab = (HistogramTab)tab;
                        //IHistViewModel hVM = hTab.ViewModel;
                        //switch (hVM.Type)
                        //{
                        //    case HistogramType.RBear_Bearing:
                        //        RBear2DHistViewModel RBVM = (RBear2DHistViewModel)hVM;
                        //        StorageString = RFEMTabType.Results.ToString() + ";" +
                        //                    hTab.TabName + ";" +
                        //                    RBVM.FilePath + ";" +
                        //                    Results.Histogram.ToString() + ";" +
                        //                    hTab.ProgramType.ToString() + ";" +
                        //                    RBVM.NSim + ";" +
                        //                    RBVM.NFootings + ";" +
                        //                    RBVM.BaseName;
                        //        break;
                        //    default:
                        //        throw new NotImplementedException();
                        //}

                        //Disabled for now
                        return "";

                    }
                    break;
            }
            return StorageString;
        }
    }
}
