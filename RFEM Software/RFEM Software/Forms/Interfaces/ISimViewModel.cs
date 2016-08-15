using RFEMSoftware.Simulation.Infrastructure;
using RFEMSoftware.Simulation.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RFEMSoftware.Simulation.Desktop.Forms
{
    public interface ISimViewModel
    {
        bool ChangesHaveBeenMade { get; }

        string DataFilePath { get; }
        string SummaryFilePath { get; }
        string BaseName { get; }

        Program Type { get; }

        void Save();
        void SaveAs(string filePath);

        bool CanDisplaySummaryStats { get; }

        ISimModel Model { get; }
    }
}
