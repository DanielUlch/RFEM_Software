using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEM_Software
{
    public interface IHistViewModel
    {
        int NSim { get; }
        int NFootings { get; }
        string BaseName { get; }
        string FilePath { get; }
    }
}
