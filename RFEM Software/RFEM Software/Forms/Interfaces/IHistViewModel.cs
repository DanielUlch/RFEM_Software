using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFEMSoftware.Simulation.Infrastructure;

namespace RFEMSoftware.Simulation.Desktop.Forms
{
    public interface IHistViewModel
    {
        HistogramType Type { get; }
        //string BaseName { get; }
        //string FilePath { get; }
        HistogramCoreViewModel HistogramCore { get; }
        HistogramHost GenerateHistogram();
        void PopOutHistogram();
    }
}
