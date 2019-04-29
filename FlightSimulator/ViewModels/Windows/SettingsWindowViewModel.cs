using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlightSimulator.ViewModels.Windows
{
    public class SettingsWindowViewModel : BaseNotify
    {
        #region Private members
        private ISettingsModel model;
        private ICommand okCommand;
        private ICommand cancelCommand;
        private Action closeAction;
        #endregion

        #region Constructor
        public SettingsWindowViewModel(ISettingsModel settingsModel, Action closeActionParam)
        {
            model = settingsModel;
            closeAction = closeActionParam;
        }
        #endregion

        #region Properties
        public string FlightServerIP
        {
            get { return model.FlightServerIP; }
            set
            {
                model.FlightServerIP = value;
                NotifyPropertyChanged("FlightServerIP");
            }
        }

        public int FlightCommandPort
        {
            get { return model.FlightCommandPort; }
            set
            {
                model.FlightCommandPort = value;
                NotifyPropertyChanged("FlightCommandPort");
            }
        }

        public int FlightInfoPort
        {
            get { return model.FlightInfoPort; }
            set
            {
                model.FlightInfoPort = value;
                NotifyPropertyChanged("FlightInfoPort");
            }
        }

        public ICommand OkCommand => okCommand ?? (okCommand = new CommandHandler(() => OnOkClicked()));

        public ICommand CancelCommand => cancelCommand ?? (cancelCommand = new CommandHandler(() => OnCancelClicked()));
        #endregion

        #region Private functions
        private void OnOkClicked()
        {
            model.SaveSettings();
            closeAction?.Invoke();
        }

        private void OnCancelClicked()
        {
            model.ReloadSettings();
            closeAction?.Invoke();
        }
        #endregion
    }
}

