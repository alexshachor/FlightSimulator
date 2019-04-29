using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class ConnectionManagerVM
    {
        #region Private members
        private ICommand connectCommand;
        private ICommand disconnectCommand;
        private ConnectionManager connectionManager;
        private Server server;
        private bool isAlive;
        #endregion

        #region Constructor
        public ConnectionManagerVM()
        {
            connectionManager = new ConnectionManager();
            isAlive = false;
        }
        #endregion

        #region Private functions
        private void OnConnectClicked()
        {
            server = new Server();
            server.StartServer();
            connectionManager.ConnectToServer();
            isAlive = ConnectionManager.IsConnected;
        }
        private void OnDisconnectClicked()
        {
            server?.CloseServer();
            if (ConnectionManager.IsConnected)
            {
                connectionManager?.CloseConnection();
            }
            isAlive = false;
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

        public bool IsAlive
        {
            get { return isAlive; }
        }
        #endregion
    }
}
