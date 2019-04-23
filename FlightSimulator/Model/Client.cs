using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class Client
    {
        private static Client m_Instance = null;
        public static Client Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Client();
                }
                return m_Instance;
            }
        }

        TcpClient client;
        private ApplicationSettingsModel app;
        private NetworkStream stream;
        private IPEndPoint ep;

        public Client()
        {
            app = new ApplicationSettingsModel();
            client = new TcpClient();
            ep = new IPEndPoint(IPAddress.Parse(app.FlightServerIP), app.FlightCommandPort);
            client.Connect(ep);
            stream = client.GetStream();
        }

        public void Write(string command)
        {
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(command);
                writer.Flush();
            }
        }


    }
}
