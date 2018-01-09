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
    partial class JPKViewModel : INotifyPropertyChanged, Ijpk
    {
         public event PropertyChangedEventHandler PropertyChanged;
        RelayCommand _ConnectCommand;
        public ICommand ConnectCommand
        {
            get { return _ConnectCommand; }
        }

        RelayCommand _GenerateJPK;
        public ICommand GenerateJPK
        {
            get { return _GenerateJPK; }
        }

        RelayCommand _PreviousMonth;
        public ICommand PreviousMonth
        {
            get { return _PreviousMonth; }
        }

        RelayCommand _NextMonth;
        public ICommand NextMonth
        {
            get { return _NextMonth; }
        }


        public JPKViewModel()
        {
            _ConnectCommand = new RelayCommand((r) => TryToConnect(), (r) => { return true; });
            _GenerateJPK = new RelayCommand((r) => GenerujJPK(), (r) => {
                bool ret = (IFXMessage.Equals("Success") && DataOd < DataDo);
                return ret;
            });

            _PreviousMonth = new RelayCommand((r) => CalcPreviousMonth(), (r) => { return true; } );
            _NextMonth = new RelayCommand((r) => CalcNextMonth(), (r) => { return true; });

            ConnectionString = "Dsn=PowerLine";
            IFXMessage = "Not Connected";
            InicjacjaJPK(this);

        }

        private void CalcPreviousMonth()
        {
            DateTime dt =  DataOd.AddMonths(-1);
            DataOd = new DateTime(dt.Year, dt.Month, 1);
            DataDo = new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));
        }

        private void CalcNextMonth()
        {
            DateTime dt = DataOd.AddMonths(1);
            DataOd = new DateTime(dt.Year, dt.Month, 1);
            DataDo = new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));

        }

        private void GenerujJPK()
        {
            CsvCreator csv = new CsvCreator();
            csv.CreateCSvFile(this);    
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
            set
            {
                if(SetProperty(ref _IFXMessage, value)) GenerateJPK.CanExecute(new object());
            }
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


        string _KodFormularza;
        public string KodFormularza
        {
            get { return _KodFormularza; }
            set { SetProperty(ref _KodFormularza, value); }
        }

        string _kodSystemowy;
        public string kodSystemowy
        {
            get { return _kodSystemowy; }
            set { SetProperty(ref _kodSystemowy, value); }

        }

        string _wersjaSchemy;
        public string wersjaSchemy
        {
            get { return _wersjaSchemy; }
            set { SetProperty(ref _wersjaSchemy, value); }


        }

        string _WariantFormularza;
        public string WariantFormularza
        {
            get { return _WariantFormularza; }
            set { SetProperty(ref _WariantFormularza, value); }
        }

        string _CelZlozenia;
        public string CelZlozenia
        {
            get { return _CelZlozenia; }
            set { SetProperty(ref _CelZlozenia, value); }

        }


        DateTime _DataWytworzeniaJPK;
        public DateTime DataWytworzeniaJPK
        {
            get { return _DataWytworzeniaJPK; }
            set { SetProperty(ref _DataWytworzeniaJPK, value);}
        }

        DateTime _DataOd;
        public DateTime DataOd
        {
            get { return _DataOd; }
            set
            {
                if(SetProperty(ref _DataOd, value)) GenerateJPK.CanExecute(new object());

            }

        }

        DateTime _DataDo;
        public DateTime DataDo
        {
            get { return _DataDo; }
            set
            {
                if(SetProperty(ref _DataDo, value)) GenerateJPK.CanExecute(new object());

            }

        }

        string _DomyslnyKodWaluty;
        public string DomyslnyKodWaluty
        {
            get { return _DomyslnyKodWaluty; }
            set { SetProperty(ref _DomyslnyKodWaluty, value); }

        }

        string _KodUrzedu;
        public string KodUrzedu
        {
            get { return _KodUrzedu; }
            set { SetProperty(ref _KodUrzedu, value); }

        }

        string _NIP;
        public string NIP
        {
            get { return _NIP; }
            set { SetProperty(ref _NIP, value); }

        }


        string _PelnaNazwa;
        public string PelnaNazwa
        {
            get { return _PelnaNazwa; }
            set { SetProperty(ref _PelnaNazwa, value); }

        }

        string _REGON;
        public string REGON
        {
            get { return _REGON; }
            set { SetProperty(ref _REGON, value); }

        }


        string _KodKraju;
        public string KodKraju
        {
            get { return _KodKraju; }
            set { SetProperty(ref _KodKraju, value); }
        }

        string _Wojewodztwo;
        public string Wojewodztwo
        {
            get { return _Wojewodztwo; }
            set { SetProperty(ref _Wojewodztwo, value); }
        }

        string _Powiat;
        public string Powiat
        {
            get { return _Powiat; }
            set { SetProperty(ref _Powiat, value); }

        }

        string _Gmina;
        public string Gmina
        {
            get { return _Gmina; }
            set { SetProperty(ref _Gmina, value); }

        }


        string _Ulica;
        public string Ulica
        {
            get { return _Ulica; }
            set { SetProperty(ref _Ulica, value); }

        }

        string _NrDomu;
        string Ijpk.NrDomu
        {
            get { return _NrDomu; }
            set { SetProperty(ref _NrDomu, value); }
        }


        string _NrLokalu;
        public string NrLokalu
        {
            get { return _NrLokalu; }
            set { SetProperty(ref _NrLokalu, value); }
        }

        string _Miejscowosc;
        public string Miejscowosc
        {
            get { return _Miejscowosc; }
            set { SetProperty(ref _Miejscowosc, value); }
        }

        string _KodPocztowy;
        public string KodPocztowy
        {
            get { return _KodPocztowy; }
            set { SetProperty(ref _KodPocztowy, value); }

        }

        string _Poczta;
        public string Poczta
        {
            get { return _Poczta; }
            set { SetProperty(ref _Poczta,value); }
        }

        string _JPKMessage;
        public string JPKMessage
        {
            get { return _JPKMessage; }
            set { SetProperty(ref _JPKMessage, value); }

        }
    }
}
