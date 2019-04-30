using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    class ManualModeVM
    {
        #region Private members

        private double aileron;
        private double rudder;
        private double elevator;
        private double throttle;
        private Dictionary<string, string> commandsMap;

        #endregion

        #region Constructor
        public ManualModeVM()
        {
            aileron = 0;
            rudder = 0;
            elevator = 0;
            throttle = 0;
            //maps the property and the matching message
            commandsMap = new Dictionary<string, string>
            {
                {"aileron", "set controls/flight/aileron "},
                {"rudder","set controls/flight/rudder " },
                {"elevator", "set controls/flight/elevator "},
                {"throttle", "set controls/engines/current-engine/throttle "}
            };
        }
        #endregion

        #region Private functions

        private void SendCommand(string command)
        {
            //if connected, send the command
            if (ConnectionManager.IsConnected)
            {
                Client.Instance.SendCommand(command);
            }
        }

        #endregion

        #region Properties

        public double Aileron
        {
            set
            {
                aileron = value;
                SendCommand(commandsMap["aileron"] + Aileron);
            }
            get
            {
                return aileron;
            }
        }

        public double Rudder
        {
            set
            {
                rudder = value;
                SendCommand(commandsMap["rudder"] + Rudder);
            }
            get
            { return rudder; }
        }

        public double Elevator
        {
            set
            {
                elevator = value;
                SendCommand(commandsMap["elevator"] + Elevator);
            }
            get
            {
                return elevator;
            }
        }

        public double Throttle
        {
            set
            {
                throttle = value;
                SendCommand(commandsMap["throttle"] + Throttle);
            }
            get
            {
                return throttle;
            }
        }

        #endregion
    }
}
