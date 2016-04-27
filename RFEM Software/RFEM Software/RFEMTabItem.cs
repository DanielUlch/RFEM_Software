using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RFEM_Software
{
    /// <summary>
    /// Adds a TabType field to tabs to determine which type of tab it is
    /// </summary>
    class RFEMTabItem : TabItem
    {
        public RFEMTabType TabType { get; set; }
    }
}
/// <summary>
/// Enumeration for different TabTypes
/// </summary>
public enum RFEMTabType
{
    DataInput,
    Help,
}