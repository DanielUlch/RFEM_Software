using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFEM_Infrastructure;
using RFEM_Software.Forms;

namespace RFEM_Software
{
    public interface IHistViewModel
    {
        HistogramType Type { get; }
        string BaseName { get; }
        string FilePath { get; }
        HistogramCoreViewModel HistogramCore { get; }
        HistogramHost GenerateHistogram();
        void PopOutHistogram();
    }
}
