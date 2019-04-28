using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class Client
    {
        #region Singleton
        private static Client myInstance = null;
        public static Client Instance
        {
            get
            {
                if (myInstance == null)
                {
                    myInstance = new Client();
                }
                return myInstance;
            }
        }
        #endregion

        private TcpClient client;

        public Client()
        {
            client = new TcpClient();
        }

        public void ConnectToServer()
        {
            ISettingsModel appSettings = ApplicationSettingsModel.Instance;
            IPAddress serverIP = IPAddress.Parse(appSettings.FlightServerIP);
            int serverPort = appSettings.FlightInfoPort;

            try
            {
                IPEndPoint serverAddress = new IPEndPoint(serverIP, serverPort);
                while (!client.Connected)
                {
                    client.Connect(serverAddress);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SendCommand(string command)
        {
            int offset = 0;
            NetworkStream clientStream = client.GetStream();
            byte[] bytesMsg = Encoding.ASCII.GetBytes(command);
            //send message to the server
            clientStream.Write(bytesMsg, offset, bytesMsg.Length);
        }

        public void SendCommands(List<string> commands)
        {
            NetworkStream clientStream = client.GetStream();

            Thread thread = new Thread(() =>
            {
                object commandLocker = new object();
                foreach (string command in commands)
                {
                    lock (commandLocker)
                    {
                        SendCommand(command);
                    }
                    Thread.Sleep(2000);
                }
            });
            thread.Start();
        }
    }
}
