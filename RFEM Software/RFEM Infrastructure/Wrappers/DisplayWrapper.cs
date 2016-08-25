using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEMSoftware.Simulation.Infrastructure.Wrappers
{
    public static class DisplayWrapper
    {
        public static void Run(string filePath)
        {
            var pInfo = new ProcessStartInfo();
            pInfo.UseShellExecute = false;
            pInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(filePath);
            string appFileDir = Environment.GetCommandLineArgs()[0];
            string displayFilePath = System.IO.Path.GetDirectoryName(appFileDir);
            displayFilePath += "\\Executables\\display.exe";
            pInfo.FileName = displayFilePath;
            pInfo.CreateNoWindow = true;
            pInfo.Arguments = filePath;
            var p = new Process { StartInfo = pInfo };


            p.Start();
            p.WaitForExit();
        }
    }
}
