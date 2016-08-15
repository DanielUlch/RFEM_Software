using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEMSoftware.Simulation.Infrastructure.Wrappers
{
    public class GhostViewWrapper
    {
        private string _GhostViewPath;
        public GhostViewWrapper(string ghostViewPath)
        {
            _GhostViewPath = ghostViewPath;
        }
        public void Show(string filePath)
        {
            var pInfo = new ProcessStartInfo();
            pInfo.UseShellExecute = true;
            pInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                        "\\RFEM_Software";
            pInfo.FileName = "\"" + _GhostViewPath + "\"";
            pInfo.Arguments = "\"" + filePath + "\"";
            pInfo.CreateNoWindow = false;
            
            var p = new Process { StartInfo = pInfo };
           
            p.Start();
        }
    }
}
