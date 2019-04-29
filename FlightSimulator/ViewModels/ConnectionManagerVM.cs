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
        private ConnectionManager connectionManager;
        #endregion

        #region Constructor
        public ConnectionManagerVM()
        {
            connectionManager = new ConnectionManager();
        }
        #endregion

        #region Private functions
        private void OnConnectClicked()
        {
            Server server = new Server();
            server.StartServer();
            connectionManager.ConnectToServer();
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
        #endregion
    }
}
