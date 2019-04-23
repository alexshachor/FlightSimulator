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
    class Server
    {
        TcpListener server;
        private ApplicationSettingsModel app;
        private NetworkStream stream;
        private IPEndPoint ep;

        public Server()
        {
            app = new ApplicationSettingsModel();
            ep = new IPEndPoint(IPAddress.Parse(app.FlightServerIP), app.FlightInfoPort);
            server = new TcpListener(ep);
        }

        public void start()
        {
            try
            {
                server.Start();
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Thread t = new Thread(() =>
            {
                string prop;
                int temp_lon, temp_lat;
                double[] numbers;
                while (true)
                {
                    try
                    {
                        TcpClient client = server.AcceptTcpClient();
                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            prop = Convert.ToString(reader.Read());
                            numbers = prop.Split(',').Select(n => double.Parse(n)).ToArray();
                            //temp_lon = numbers[LON_INDEX];
                            //temp_lat = numbers[LAT_INDEX];
                            //FlightBoardViewModel.lon = temp_lon;
                            //FlightBoardViewModel.lat = temp_lat;
                        }
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
            });
            t.Start();
        }
    }
}
