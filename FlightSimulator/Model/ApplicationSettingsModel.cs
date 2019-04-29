using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    public class ApplicationSettingsModel : ISettingsModel
    {
        #region Singleton
        private static ISettingsModel myInstance = null;
        public static ISettingsModel Instance
        {
            get
            {
                if (myInstance == null)
                {
                    myInstance = new ApplicationSettingsModel();
                }
                return myInstance;
            }
        }
        #endregion

        #region Public functions
        public string FlightServerIP
        {
            get { return Properties.Settings.Default.FlightServerIP; }
            set { Properties.Settings.Default.FlightServerIP = value; }
        }
        public int FlightCommandPort
        {
            get { return Properties.Settings.Default.FlightCommandPort; }
            set { Properties.Settings.Default.FlightCommandPort = value; }
        }

        public int FlightInfoPort
        {
            get { return Properties.Settings.Default.FlightInfoPort; }
            set { Properties.Settings.Default.FlightInfoPort = value; }
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }

        public void ReloadSettings()
        {
            Properties.Settings.Default.Reload();
        }
        #endregion
    }
}
