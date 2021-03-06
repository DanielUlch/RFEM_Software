﻿using System.Windows.Input;

namespace RFEMSoftware.Simulation.Desktop.Commands
{
    /// <summary>
    /// Custom HelpClick command that allows differentiation between an F1 press and a context menu
    /// help command. This command is for the context menu item click.
    /// </summary>
    public static class CustomCommands
    {
        public static readonly RoutedUICommand NewHelpClick = new RoutedUICommand("Help", "NewHelpClick", typeof(CustomCommands));
    }
}
