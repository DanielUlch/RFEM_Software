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
            pInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(filePath);
            pInfo.FileName = "\"" + _GhostViewPath + "\"";
            pInfo.Arguments = "\"" + filePath + "\"";
            pInfo.CreateNoWindow = false;
            
            var p = new Process { StartInfo = pInfo };
           
            p.Start();
        }
    }
}
