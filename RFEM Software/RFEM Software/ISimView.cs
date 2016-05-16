using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEM_Software
{
    public interface ISimView
    {
        ISimViewModel ViewModel { get; }

        /// <summary>
        /// When a user clicks F1, the main application window will request a help file 
        /// location via this method
        /// </summary>
        /// <returns>
        /// The location of the help file pertaining to the element that is currently hovered, 
        /// or the default help file for this form
        /// </returns>
        string hoveredHelpDocLocation();
        /// <summary>
        /// This method is called when a user clicks the help item on the content menu of a control
        /// The control is passed as a command parameter, and then is passed to this method from the main window
        /// This method will return the location of the help file pertaining to the passed control, or
        /// a default help file for the form
        /// </summary>
        /// <param name="F">
        /// The control that the help command originated from
        /// </param>
        /// <returns>
        /// Help file location for the given control
        /// </returns>
        string HelpLocation(System.Windows.FrameworkElement F);

    }
}
