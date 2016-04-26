using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RFEM_Software.Forms;


namespace RFEM_Software
{
    /// <summary>
    /// Interaction logic for Rbear2d.xaml
    /// </summary>
    public partial class Rbear2d : UserControl, IHelpFiled
    {
        
        //Dictionary of controls and related help files
        private Dictionary<FrameworkElement, string> HelpLocations;
        private string DefaultHelp = "";

        public Rbear2d()
        {
            InitializeComponent();
            HelpLocations = InitializeHelpLocations();
            InitializeContextMenus();


        }
        private Dictionary<FrameworkElement, string > InitializeHelpLocations()
        {
            var dict = new Dictionary<FrameworkElement, string>();
            string Base = "RFEM_Software.Help_Files.RBear_Help_Files.";

            DefaultHelp = Base + "RBear2dHelp.xaml";


            dict.Add(chkEchoInput, Base + "EchoInputToOutputFile.xaml");
            dict.Add(chkShowRFOnPlot, Base + "ShowRFOnDisplacedMeshPlot.xaml");
            dict.Add(chkShowLogRF, Base + "ShowLogRF.xaml");
            dict.Add(cboPropertyToPlot, Base + "PropertyToPlot.xaml");




            return dict;       
        }
        private void InitializeContextMenus()
        {
            NameScope.SetNameScope(contextMenu, NameScope.GetNameScope(this));
            NameScope.SetNameScope(contextMenu1, NameScope.GetNameScope(this));
            NameScope.SetNameScope(contextMenu2, NameScope.GetNameScope(this));
            NameScope.SetNameScope(contextMenu3, NameScope.GetNameScope(this));
            NameScope.SetNameScope(contextMenu4, NameScope.GetNameScope(this));
        }

        public string hoveredHelpDocLocation()
        {
            foreach (FrameworkElement f in HelpLocations.Keys)
            {
                if (f.IsMouseOver)
                    return HelpLocations[f];
               
            }
            return DefaultHelp;
        }

        public string HelpLocation(FrameworkElement F)
        {

            if(HelpLocations[F] != null)
            {
                return HelpLocations[F];
            }
            else
            {
                return DefaultHelp;
            }
        }
        private void HelpClickStub(object sender, CanExecuteRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void HelpClickExeStub(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}
public static class extensions
{
    
}
