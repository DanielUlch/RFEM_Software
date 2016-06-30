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
                MessageBox.Show("Windows 7");
                app.Resources["TBHeight"] = (double)22;
            }
            else
            {
                MessageBox.Show("Not windows 7");
                app.Resources["TBHeight"] = (double)18;
            }
            MessageBox.Show(Environment.OSVersion.ToString());
            
            
            app.Run();
        }
    }

    
}
