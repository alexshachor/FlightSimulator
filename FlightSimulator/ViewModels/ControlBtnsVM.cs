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
        public ICommand SettingsCommand
        {
            get
            {
                return settingsCommand ?? (settingsCommand = new CommandHandler(() => SettingsClick()));
            }
        }

        private void SettingsClick()
        {
            Settings s = new Settings();
            s.ShowDialog();
        }

        private ICommand connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return connectCommand ?? (connectCommand = new CommandHandler(() => ConnectClick()));
            }
        }

        static private bool is_connect = false;
        static public bool Is_connect
        {
            get { return is_connect; }
            set
            {
                is_connect = value;
            }
        }

        private void ConnectClick()
        {
            is_connect = true;
            TCPServer server = new TCPServer();
            TCPClient client = new TCPClient();
        }


        private ICommand disConnectCommand;
        public ICommand DisConnectCommand
        {
            get
            {
                return disConnectCommand ?? (disConnectCommand = new CommandHandler(() => DisConnectClick()));
            }
        }

        private void DisConnectClick()
        {
            // TO-DO!!!
            // call model to dis - connect
        }

    }
}

