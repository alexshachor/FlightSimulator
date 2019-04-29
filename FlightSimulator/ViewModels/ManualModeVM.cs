﻿using FlightSimulator.Model;
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

        #endregion

        #region Constructor
        public ManualModeVM()
        {
            aileron = 0;
            rudder = 0;
            elevator = 0;
            throttle = 0;
        }
        #endregion

        #region Private functions

        private void SendCommand(string command)
        {
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
                string command = "set controls/flight/aileron " + Aileron;
                SendCommand(command);
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
                string command = "set controls/flight/rudder " + Rudder;
                SendCommand(command);
            }
            get
            { return rudder; }
        }

        public double Elevator
        {
            set
            {
                elevator = value;
                string command = "set controls/flight/elevator " + Elevator;
                SendCommand(command);
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
                string command = "set controls/engines/current-engine/throttle " + Throttle;
                SendCommand(command);
            }
            get
            {
                return throttle;
            }
        }

        #endregion
    }
}
