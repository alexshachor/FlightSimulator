using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class ConnectionManager
    {
        #region Private members
        private static bool isConnected;
        private Client client;
        #endregion

        #region Constructor
        public ConnectionManager()
        {
            isConnected = false;
            client = Client.Instance;
        }
        #endregion

        #region Public functions
        public void ConnectToServer()
        {
            isConnected = client.ConnectToServer();
        }
        public void CloseConnection()
        {
            client.CloseConnection();
        }
        #endregion

        #region Properties
        static public bool IsConnected
        {
            get { return isConnected; }
        }
        #endregion
    }
}
