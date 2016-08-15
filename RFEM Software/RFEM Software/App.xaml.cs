using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace RFEMSoftware.Simulation.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const int MinSplashTime = 1500;
        private const int SplashFadeTime = 500;
        protected override void OnStartup(StartupEventArgs e)
        {
            var splashScreen = new SplashScreen("Images/SplashScreen2.png");
            splashScreen.Show(false, true);

            var timer = new Stopwatch();
            timer.Start();

            base.OnStartup(e);
            var MainWindow = new MainWindow();

            timer.Stop();

            int remainingTime = MinSplashTime - (int)timer.ElapsedMilliseconds;
            if (remainingTime > 0)
                Thread.Sleep(remainingTime);

            splashScreen.Close(TimeSpan.FromMilliseconds(SplashFadeTime));

            MainWindow.Show();
        }

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

            try
            {
                app.Run();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }

    
}
