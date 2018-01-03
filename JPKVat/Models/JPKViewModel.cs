using JPKVat.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JPKVat.Models
{
    class JPKViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        RelayCommand _ConnectCommand;
        public ICommand ConnectCommand
        {
            get { return _ConnectCommand; }
        }

        public JPKViewModel()
        {
            _ConnectCommand = new RelayCommand((r) => TryToConnect(), (r) => { return true; });
            ConnectionString = "Dsn='';Driver={INFORMIX 3.30 32 BIT};Host=192.168.1.124;Server=ol_aleksander;Service = 1526; Protocol = onsoctcp; Database = powerline; Uid = informix;Pwd = informix;";
            IFXMessage = "Not Connected";
        }

        private void TryToConnect()
        {
            IFXMessage = "Huj"; //Informix.IFXHelper.TryToConnect(ConnectionString);
        }

        private bool SetProperty<T>(ref T oldvalue, T newvalue, [CallerMemberName]string propname = "")
        {

            if (oldvalue != null && oldvalue.Equals(newvalue)) return false;
            oldvalue = newvalue;
            OnPropertyChange(propname);
            return true;

        }

        private void OnPropertyChange(string info)
        {
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(info));
        }

        bool _Connected;
        public bool Connected
        {
            get { return _Connected; }
            set { SetProperty<bool>(ref _Connected, value); }
        }

        string _IFXMessage;
        public string IFXMessage
        {
            get { return _IFXMessage; }
            set { SetProperty<string>(ref _IFXMessage, value); }
        }

        string _ConnectionString;
        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { SetProperty<string>(ref _ConnectionString, value); }
        }


    }
}
