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
    class AutoModeVM : BaseNotify
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
            if (!ControlBtnsVM.IsConnected)
            {
                return;
            }
            Sending = true;
           

            string[] lines = setComendText.Split(new[] { "\r\n", "\r", "\n" },StringSplitOptions.None);
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] += Environment.NewLine;
            }

            Client client = Client.Instance;
            client.SendCommands(lines.ToList());
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
