using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEM_Infrastructure
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
