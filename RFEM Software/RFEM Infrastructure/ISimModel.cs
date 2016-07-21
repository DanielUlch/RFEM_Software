using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RFEM_Infrastructure
{
    public interface ISimModel
    {
        string GetDataFileString();
        string DataFileLocation();

        string AppDataFileLocation { get; }

        string BaseName { get; }

        string RunSim(IProgress<int> simIteration, IProgress<string> currentOp, CancellationToken token);

        int NumberOfRealizations { get; }
    }
}
