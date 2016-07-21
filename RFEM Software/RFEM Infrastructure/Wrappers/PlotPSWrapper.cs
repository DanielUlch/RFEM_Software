using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEM_Infrastructure
{
    public static class PlotPSWrapper
    {
        public static void Run(string filePath)
        {
            string appFileName = Environment.GetCommandLineArgs()[0];
            ProcessStartInfo pInfo;
            Process p;

            pInfo = new ProcessStartInfo();
            pInfo.FileName = System.IO.Path.GetDirectoryName(appFileName) + "\\Executables\\plotps.exe";
            pInfo.Arguments = System.IO.Path.GetDirectoryName(filePath) + "\\hist.plotps";
            pInfo.CreateNoWindow = true;
            pInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(filePath);

            p = new Process() { StartInfo = pInfo };
            p.Start();
            p.WaitForExit();
        }
    }
}
