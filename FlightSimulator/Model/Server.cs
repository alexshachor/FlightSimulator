using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using FlightSimulator.ViewModels;
using FlightSimulator.Model.Interface;

namespace FlightSimulator.Model
{
    class Server
    {
        #region Private members
        private TcpListener tcpListener;
        #endregion

        public Server()
        {
            tcpListener = GetServerConfiguration();
        }

        #region Private functions
        private TcpListener GetServerConfiguration()
        {
            //get server IP and port
            ISettingsModel appSettings = ApplicationSettingsModel.Instance;
            IPAddress serverIP = IPAddress.Parse(appSettings.FlightServerIP);
            int serverPort = appSettings.FlightInfoPort;
            return new TcpListener(serverIP, serverPort);
        }

        #endregion

        #region Public functions
        public void StartServerThread()
        {
            Thread thread = new Thread(() => StartServer());
            thread.Start();
        }

        public void StartServer()
        {
            tcpListener.Start();
            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                while (client.Connected)
                {
                    BinaryReader reader = new BinaryReader(client.GetStream());
                    StringBuilder data = new StringBuilder();
                    char c;
                    while ((c = reader.ReadChar()) != '\n')
                    {
                        data.Append(c);
                    }
                    string[] separatedParam = data.ToString().Split(',');

                    object lonAndLatLocker = new object();
                    FlightBoardViewModel flightBoardVM = FlightBoardViewModel.Instance;
                    lock (lonAndLatLocker)
                    {
                        flightBoardVM.Lon = Double.Parse(separatedParam[0]);
                        flightBoardVM.Lat = Double.Parse(separatedParam[1]);
                    }
                }
                client.Close();
            }
        }

        public void CloseServer() => tcpListener?.Stop();
        #endregion
    }
}