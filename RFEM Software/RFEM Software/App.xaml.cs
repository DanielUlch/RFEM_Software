using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RFEM_Software
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
     
     
    }
    public class EntryPoint
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var app = new App();
            app.InitializeComponent();

            if (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 1)
            {
                //Windows 7 textboxes need to be large for some reason.
                app.Resources["TBHeight"] = (double)22;
            }
            else
            {
                app.Resources["TBHeight"] = (double)18;
            }
            app.Run();
        }
    }

    
}
