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

        #region Private members
        private TcpClient client;
        private readonly object commandLocker;
        #endregion

        public Client()
        {
            client = new TcpClient();
            commandLocker = new object();
        }

        public bool ConnectToServer()
        {
            ISettingsModel appSettings = ApplicationSettingsModel.Instance;
            IPAddress serverIP = IPAddress.Parse(appSettings.FlightServerIP);
            int serverPort = appSettings.FlightInfoPort;
            bool isConnected = false;

            try
            {
                IPEndPoint serverAddress = new IPEndPoint(serverIP, serverPort);
                while (!client.Connected)
                {
                    client.Connect(serverAddress);
                }
                isConnected = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return isConnected;
        }

        public void SendCommand(string command)
        {
            string newLine = "\r\n";
            if (!command.EndsWith(newLine))
            {
                command += newLine;
            }
            int offset = 0;
            NetworkStream clientStream = client.GetStream();
            byte[] bytesMsg = Encoding.ASCII.GetBytes(command);
            //send message to the server
            clientStream.Write(bytesMsg, offset, bytesMsg.Length);
        }

        public void SendCommands(List<string> commands)
        {
            NetworkStream clientStream = client.GetStream();
            int sleepFor = 2000;

            Thread thread = new Thread(() =>
            {
                foreach (string command in commands)
                {
                    lock (commandLocker)
                    {
                        SendCommand(command);
                    }
                    Thread.Sleep(sleepFor);
                }
            });
            thread.Start();
        }
    }
}
