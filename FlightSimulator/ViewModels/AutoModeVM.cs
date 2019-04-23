using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class AutoModeVM :BaseNotify
    {
        private bool sending = true;
        public bool Sending
        {
            get { return sending; }
            set
            {
                sending = value;
                NotifyPropertyChanged("Sending");
            }
        }


        private string setComendText;
        public string SetComendText
        {
            get { return setComendText; }
            set
            {
                setComendText = value;
                NotifyPropertyChanged("SetComendText");
                if (setComendText != "")
                {
                    Sending = false;
                }
                else
                {
                    Sending = true;
                }
            }
        }

        private ICommand okCommand;
        public ICommand OkCommand
        {
            get
            {
                return okCommand ?? (okCommand = new CommandHandler(() => OkClick()));
            }
        }

        private void OkClick()
        {
            if (!ControlBtnsVM.Is_connect)
            {
                return;
            }
            Sending = true;
            using (StringReader reader = new StringReader(setComendText))
            {
                string command;
                while ((command = reader.ReadLine()) != null)
                {
                    Client client = Client.Instance;
                    command = command + "\r\n"; // check if work
                    client.Write(command);
                }
            }
        }

        private ICommand clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                return clearCommand ?? (clearCommand = new CommandHandler(() => ClearClick()));
            }
        }

        private void ClearClick()
        {
            SetComendText = "";
            Sending = true;
        }
    }
}
