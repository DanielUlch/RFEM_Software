using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEMSoftware.Simulation.Infrastructure.Wrappers
{
    public static class HistogramExeWrapper
    {
        public static void Run(string filePath, string options, Program type)
        {
            string appFileName = Environment.GetCommandLineArgs()[0];
            string directory = System.IO.Path.GetDirectoryName(appFileName);
            var pInfo = new ProcessStartInfo();
            Process p;

            switch (type)
            {
                case Program.RBear2D:
                    directory += "\\Executables\\hist_bear2d.exe";
                    break;
                case Program.RDam2D:
                    directory += "\\Executables\\hist_dam2d.exe";
                    break;
                case Program.REarth2D:
                    directory += "\\Executables\\hist_earth2d.exe";
                    break;
                case Program.RFlow2D:
                    directory += "\\Executables\\hist_flow2d.exe";
                    break;
                case Program.RFlow3D:
                    directory += "\\Executables\\hist_flow3d.exe";
                    break;
                case Program.RPill2D:
                    directory += "\\Executables\\hist_pill2d.exe";
                    break;
                case Program.RPill3D:
                    directory += "\\Executables\\hist_pill3d.exe";
                    break;
                case Program.RSetl2D:
                    directory += "\\Executables\\hist_setl2d.exe";
                    break;
                case Program.RSetl3D:
                    directory += "\\Executables\\hist_setl3d.exe";
                    break;
                default:
                    throw new NotImplementedException();
            }
            
            directory = "\"" + directory + "\"";

            pInfo.FileName = directory;
            pInfo.Arguments = filePath + " " + options;
            pInfo.CreateNoWindow = true;
            pInfo.RedirectStandardOutput = true;
            pInfo.UseShellExecute = false;
            pInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(filePath);

            p = new Process() { StartInfo = pInfo };

            p.Start();
            string debugline = "";

            using (var reader = p.StandardOutput)
            {
                while (!reader.EndOfStream)
                {
                    debugline += reader.ReadToEnd();
                }
            }
                    p.WaitForExit();
        }
    }
}
