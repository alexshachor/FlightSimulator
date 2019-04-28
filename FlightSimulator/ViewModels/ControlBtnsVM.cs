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
        private ICommand settingsCommand;
        private static bool isConnected = false;
        private ICommand connectCommand;


        private void OnSettingsClicked()
        {
            Settings s = new Settings();
            s.ShowDialog();
        }


        private void OnConnectClicked()
        {
            isConnected = true;
            Server server = new Server();
            Client client = new Client();
        }



        public ICommand SettingsCommand
        {
            get
            {
                return settingsCommand ?? (settingsCommand = new CommandHandler(() => OnSettingsClicked()));
            }
        }


        public ICommand ConnectCommand
        {
            get
            {
                return connectCommand ?? (connectCommand = new CommandHandler(() => OnConnectClicked()));
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

    }
}

