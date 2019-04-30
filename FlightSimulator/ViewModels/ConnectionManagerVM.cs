using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class ConnectionManagerVM : BaseNotify
    {
        #region Private members
        private ICommand connectCommand;
        private ICommand disconnectCommand;
        private ConnectionManager connectionManager;
        private Server server;
        private bool isConnected;
        private bool isServerAlive;

        #endregion

        #region Constructor
        public ConnectionManagerVM()
        {
            connectionManager = new ConnectionManager();
            isConnected = false;
            isServerAlive = false;
        }
        #endregion

        #region Private functions
        private void OnConnectClicked()
        {
            //if server is not online
            if (!isServerAlive)
            {
                server = new Server();
                server.StartServerThread();
                isServerAlive = true;
            }
            //if the program is yet to be connected to the server
            if (!IsConnected)
            {
                connectionManager.ConnectToServer();
                IsConnected = ConnectionManager.IsConnected;
            }
        }
        private void OnDisconnectClicked()
        {
            //if the server is online => close it
            if (isServerAlive)
            {
                server?.CloseServer();
                isServerAlive = false;
            }
            //if connected to the simulator => close connection
            if (ConnectionManager.IsConnected)
            {
                connectionManager?.CloseConnection();
            }
            IsConnected = false;
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

        public ICommand DisconnectCommand
        {
            get
            {
                return disconnectCommand ?? (disconnectCommand = new CommandHandler(() => OnDisconnectClicked()));
            }
        }

        public bool IsConnected
        {
            get { return isConnected; }
            set
            {
                isConnected = value;
                NotifyPropertyChanged("IsConnected");
            }
        }
        #endregion
    }
}
