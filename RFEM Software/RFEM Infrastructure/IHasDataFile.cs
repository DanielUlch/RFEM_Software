using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEM_Infrastructure
{
    public interface IHasDataFile
    {
        string DataFileString();
        string DataFileLocation();

        string AppDataFileLocation { get; }

        string BaseName { get; }
        
    }
}
