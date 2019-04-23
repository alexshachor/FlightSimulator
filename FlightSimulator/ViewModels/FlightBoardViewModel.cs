using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        private double lon;
        //in the model when you get a data from the simulator update the Lon (Of the plane)
        //by write in the model "Lon=..."
        public double Lon
        {
            get { return lon; }

            set
            {
                lon = value;
                NotifyPropertyChanged("Lon");
            }
        }

        private double lat;
        //in the model when you get a data from the simulator update the Lat (Of the plane)
        //by write in the model "Lat=..."
        public double Lat
        {
            get { return lat; }
            set
            {
                lat = value;
                NotifyPropertyChanged("Lat");
            }
        }
    }
}
