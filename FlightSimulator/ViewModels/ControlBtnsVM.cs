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
    class ControlBtnsVM
    {
        #region Private members
        private ICommand settingsCommand;
        private ICommand connectCommand;
        private static bool isConnected;
        #endregion

        #region Constructor
        public ControlBtnsVM()
        {
            isConnected = false;
        }
        #endregion

        #region Private functions
        private void OnConnectClicked()
        {
            Server server = new Server();
            server.StartServer();

            Client client = new Client();
            IsConnected = client.ConnectToServer();
        }

        private void OnSettingsClicked()
        {
            Settings s = new Settings();
            s.ShowDialog();
        }
        #endregion

        #region Properties
        public ICommand ConnectCommand
        {
            get
            {
                return connectCommand ?? (connectCommand = new CommandHandler(() => OnConnectClicked()));
            }
        }

        public ICommand SettingsCommand
        {
            get
            {
                return settingsCommand ?? (settingsCommand = new CommandHandler(() => OnSettingsClicked()));
            }
        }

        static public bool IsConnected
        {
            get { return isConnected; }
            set
            {
                isConnected = value;
            }
        }
        #endregion
    }
}

