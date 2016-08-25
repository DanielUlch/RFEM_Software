using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RFEMSoftware.Simulation.Infrastructure.Models
{
    public interface ISimModel
    {
        //string DataFileLocation();

        //string AppDataFileLocation { get; }

        string OutputDirectory { get; set; }
        string DataLocation { get; }

        string BaseName { get; }

        string RunSim(IProgress<int> simIteration, IProgress<string> currentOp, CancellationToken token);

        int NumberOfRealizations { get; }
        Program Type { get; }
    }
}
