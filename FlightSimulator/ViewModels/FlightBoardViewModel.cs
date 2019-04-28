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
        #region Singleton

        private static FlightBoardViewModel myInstance = null;
        public static FlightBoardViewModel Instance
        {
            get
            {
                if (myInstance == null)
                {
                    myInstance = new FlightBoardViewModel();
                }
                return myInstance;
            }
        }
        #endregion

        private double lon;
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