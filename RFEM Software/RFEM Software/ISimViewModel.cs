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
    }
}
