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
            //start listening to the socket
            tcpListener.Start();
            while (true)
            {
                //wait for a client (the simulator) to connect
                TcpClient client = tcpListener.AcceptTcpClient();
                while (client.Connected)
                {
                    NetworkStream clientStream = client.GetStream();
                    if (!clientStream.DataAvailable)
                    {
                        continue;
                    }

                    BinaryReader reader = new BinaryReader(clientStream);
                    StringBuilder data = new StringBuilder();
                    char c;
                    //each iteration read a char from the socket and add it to data 
                    while ((c = reader.ReadChar()) != '\n')
                    {
                        data.Append(c);
                    }
                    string[] separatedParam = data.ToString().Split(',');

                    object lonAndLatLocker = new object();
                    FlightBoardViewModel flightBoardVM = FlightBoardViewModel.Instance;
                    lock (lonAndLatLocker)
                    {
                        //update lon and lat values
                        flightBoardVM.Lon = Double.Parse(separatedParam[0]);
                        flightBoardVM.Lat = Double.Parse(separatedParam[1]);
                    }
                }
                client?.GetStream()?.Close();            
                client?.Close();
            }
        }

        public void CloseServer() => tcpListener?.Stop();
        #endregion
    }
}