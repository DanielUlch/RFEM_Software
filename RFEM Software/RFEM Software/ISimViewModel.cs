using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RFEM_Software
{
    public interface ISimViewModel
    {
        string CurrentOperation { get; set; }
        int ProgressPercentage { get; set; }
        string ProgressDetails { get; set; }

        Task<string> RunSimAsync(CancellationToken token);

        bool CanDisplaySummaryStats { get; }
        bool CanDisplayMesh { get; }
        bool CanDisplayField { get; }
        bool CanDisplayBearingHist { get; }

        string MeshFilePath { get; }
        string FieldFilePath { get; }
        string SummaryFilePath { get; }
    }
}
