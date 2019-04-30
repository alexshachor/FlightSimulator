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

        #region Constructor
        private Client()
        {
            client = new TcpClient();
            commandLocker = new object();
        }
        #endregion

        #region Private functions
        private IPEndPoint GetSimulatorAddress()
        {
            ISettingsModel appSettings = ApplicationSettingsModel.Instance;
            IPAddress simulatorServerIP = IPAddress.Parse(appSettings.FlightServerIP);
            int simulatorPort = appSettings.FlightCommandPort;
            return new IPEndPoint(simulatorServerIP, simulatorPort);
        }
        #endregion

        #region Public functions 
        public bool ConnectToServer()
        {
            IPEndPoint serverAddress = GetSimulatorAddress();
            try
            {
                while (!client.Connected)
                {
                    client.Connect(serverAddress);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return client.Connected;
        }

        public void CloseConnection()
        {
            client.Close();
        }

        public void SendCommand(string command)
        {
            if (!client.Connected)
            {
                return;
            }
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
            Console.WriteLine(command);
        }

        public void SendCommands(List<string> commands, int commandsLatency = 0)
        {
            NetworkStream clientStream = client.GetStream();

            foreach (string command in commands)
            {
                lock (commandLocker)
                {
                    SendCommand(command);
                }
                Thread.Sleep(commandsLatency);
            }
        }
        public void SendCommandsThread(List<string> commands)
        {
            NetworkStream clientStream = client.GetStream();
            int commandsLatency = 2000;

            Thread thread = new Thread(() => SendCommands(commands, commandsLatency));
            thread.Start();
        }
        #endregion
    }
}
