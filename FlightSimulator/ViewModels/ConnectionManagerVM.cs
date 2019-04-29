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
        private bool isAlive;
        private bool isServerAlive;

        #endregion

        #region Constructor
        public ConnectionManagerVM()
        {
            connectionManager = new ConnectionManager();
            isAlive = false;
            isServerAlive = false;
        }
        #endregion

        #region Private functions
        private void OnConnectClicked()
        {
            if (!isServerAlive)
            {
                server = new Server();
                isServerAlive = server.StartServer();
            }
            if (!IsAlive)
            {
                connectionManager.ConnectToServer();
                IsAlive = ConnectionManager.IsConnected;
            }
        }
        private void OnDisconnectClicked()
        {
            if (isServerAlive)
            {
                server?.CloseServer();
                isServerAlive = false;
            }
            if (ConnectionManager.IsConnected)
            {
                connectionManager?.CloseConnection();
            }
            IsAlive = false;
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
            set
            {
                isAlive = value;
                NotifyPropertyChanged("IsAlive");
            }
        }
        #endregion
    }
}
