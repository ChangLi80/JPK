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
    partial class JPKViewModel : INotifyPropertyChanged
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
            ConnectionString = "Dsn=PowerLine";
            IFXMessage = "Not Connected";
        }

        private void TryToConnect()
        {
            IFXMessage = Informix.IFXHelper.TryToConnect(ConnectionString);
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }


        string _IFXMessage;
        public string IFXMessage
        {
            get { return _IFXMessage; }
            set { SetProperty(ref _IFXMessage, value); }
        }

        string _ConnectionString;
        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { SetProperty(ref _ConnectionString, value); }
        }

        string _JPKName;
        public string JPKName
        {
            get { return _JPKName; }
            set { SetProperty(ref _JPKName, value); }
        }


    }
}
