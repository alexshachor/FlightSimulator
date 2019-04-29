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
        #endregion

        #region Constructor
        public ConnectionManager()
        {
            isConnected = false;
        }
        #endregion

        #region Public functions
        public void ConnectToServer()
        {
            Client client = new Client();
            isConnected = client.ConnectToServer();
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
