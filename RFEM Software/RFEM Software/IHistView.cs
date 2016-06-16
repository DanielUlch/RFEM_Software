using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEM_Software
{
    public interface IHistView
    {
        IHistViewModel ViewModel { get; }
        string helpLocation(string topic);
    }
}
