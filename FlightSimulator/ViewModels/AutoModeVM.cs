using FlightSimulator.Model;
using System;
using System.Linq;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class AutoModeVM : BaseNotify
    {
        #region Private members
        private bool isNotWriting;
        private string commandsString;
        private ICommand okCommand;
        private ICommand clearCommand;
        #endregion

        #region Constructor
        public AutoModeVM()
        {
            isNotWriting = true;
        }
        #endregion

        #region Properties
        public bool IsNotWriting
        {
            get { return isNotWriting; }
            set
            {
                isNotWriting = value;
                NotifyPropertyChanged("IsNotWriting");
            }
        }

        public string CommandsString
        {
            get { return commandsString; }
            set
            {
                commandsString = value;
                NotifyPropertyChanged("CommandsString");
                //if the textbox empty, then IsNotWriting = true. otherwise false.
                IsNotWriting = commandsString.Equals(String.Empty);
            }
        }

        public ICommand OkCommand
        {
            get
            {
                return okCommand ?? (okCommand = new CommandHandler(() => OnOkClicked()));
            }
        }

        public ICommand ClearCommand
        {
            get
            {
                return clearCommand ?? (clearCommand = new CommandHandler(() => OnClearClicked()));
            }
        }
        #endregion

        #region Private functions
        private void OnOkClicked()
        {
            if (!ConnectionManager.IsConnected)
            {
                return;
            }

            //split the string text into array of lines
            string[] lines = CommandsString.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            //send the commands list
            Client.Instance.SendCommandsThread(lines.ToList());
            IsNotWriting = true;
        }

        private void OnClearClicked()
        {
            IsNotWriting = true;
            CommandsString = String.Empty;
        }
        #endregion
    }
}
