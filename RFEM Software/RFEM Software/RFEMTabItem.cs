using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RFEM_Software
{
    class RFEMTabItem: TabItem
    {

        public RFEMTabType TabType { get; set; }
    }
}
public enum RFEMTabType
{
    DataInput,
    Help,
}