using RFEMSoftware.Simulation.Desktop.CustomControls;
using RFEMSoftware.Simulation.Infrastructure;
using RFEMSoftware.Simulation.Infrastructure.Models;
using RFEMSoftware.Simulation.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RFEMSoftware.Simulation.Desktop.Forms
{
    abstract class ViewModelBase
    {
        protected  List<RFEMTabItem> _SubTabs;

        protected  DataEntryTab _DataTab;

        protected ISimModel _Model;

        protected ISimView _View;

        protected List<string> _Errors = new List<string>();

        protected bool _ChangesHaveBeenMade;

        protected TopLevelTabItem _MasterTab;

        public TopLevelTabItem MasterTab
        {
            get { return _MasterTab; }
        }
        public List<RFEMTabItem> SubTabs
        {
            get { return _SubTabs; }
        }

        public ISimModel Model
        {
            get { return _Model; }
        }
        public bool ChangesHaveBeenMade
        {
            get
            {
                return _ChangesHaveBeenMade;
            }
        }
        public Program Type
        {
            get { return _Model.Type; }
        }
        protected bool HasErrors
        {
            get
            {
                return (_Errors.Count > 0);
            }
        }

        public ViewModelBase(CommandBindingCollection commandBindings, double width, ISimModel model, ISimView view)
        {

            //_DataTab = new DataEntryTab(RFEMTabType.DataInput, 
            //                            CloseTab, 
            //                            CmdCloseAllTabs, 
            //                            view, 
            //                            this, 
            //                            commandBindings, 
            //                            (UserControl)view, model.BaseName, model.Type);

            _SubTabs = new List<RFEMTabItem>() { _DataTab };

            _Model = _DataTab.ViewModel.Model;


        }
        

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
