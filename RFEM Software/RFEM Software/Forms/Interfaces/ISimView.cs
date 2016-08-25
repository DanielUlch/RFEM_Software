using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEMSoftware.Simulation.Desktop.Forms
{
    public interface ISimView
    {
        //ISimViewModel ViewModel { get; }

        /// <summary>
        /// When a user clicks F1, the main application window will request a help file 
        /// topic via this method.
        /// </summary>
        /// <returns>
        /// The topic of the help file pertaining to the element that is currently hovered, 
        /// or the default help file for this form
        /// </returns>
        string GetHoveredHelpTopic();


    }
}
