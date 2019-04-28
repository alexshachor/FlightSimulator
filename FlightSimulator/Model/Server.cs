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
        public void StartServer()
        {
            ISettingsModel appSettings = ApplicationSettingsModel.Instance;
            IPAddress serverIP = IPAddress.Parse(appSettings.FlightServerIP);
            int serverPort = appSettings.FlightInfoPort;
            TcpListener tcpListener = new TcpListener(serverIP, serverPort);

            Thread thread = new Thread(() =>
            {
                tcpListener.Start();
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
                        flightBoardVM.Lon = Convert.ToDouble(separatedParam[0]);
                        flightBoardVM.Lat = Convert.ToDouble(separatedParam[1]);
                    }
                }
                client.Close();
            });
            thread.Start();
        }
    }
}