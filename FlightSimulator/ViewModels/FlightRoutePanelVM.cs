using FlightSimulator.Model;
using FlightSimulator.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class FlightRoutePanelVM
    {
        #region Private members
        private ICommand settingsCommand;
        #endregion

        #region Private functions
        private void OnSettingsClicked()
        {
            //open settings window
            Settings s = new Settings();
            s.ShowDialog();
        }
        #endregion

        #region Properties
        public ICommand SettingsCommand
        {
            get
            {
                return settingsCommand ?? (settingsCommand = new CommandHandler(() => OnSettingsClicked()));
            }
        }

        #endregion
    }
}
