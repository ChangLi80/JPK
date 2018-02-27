using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPKVat.Models
{
    partial class JPKViewModel : INotifyPropertyChanged, Ijpk
    {
        private void InicjacjaJPK(Ijpk jpk)
        {
            jpk.KodFormularza = "JPK_VAT";
            jpk.kodSystemowy = "JPK_VAT (3)";
            jpk.wersjaSchemy = "1-1";
            jpk.WariantFormularza = "3";
            jpk.CelZlozenia = "1";
            DateTime lastdayofPreviousMoth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);
            jpk.DataOd =  new DateTime(lastdayofPreviousMoth.Year, lastdayofPreviousMoth.Month, 1);
            jpk.DataDo = lastdayofPreviousMoth;
            jpk.DataWytworzeniaJPK = DateTime.Now;
            jpk.DomyslnyKodWaluty = "PLN";
            jpk.KodUrzedu= "2413";
            jpk.NIP= "6310202474";
            jpk.PelnaNazwa= "DIGITAL SYSTEMS TECHNOLOGIES";
            jpk.REGON= "ast@digisys-technologies.com";
            jpk.KodKraju ="PL";
            jpk.Wojewodztwo = "ŚLĄSKIE";
            jpk.Powiat = "GLIWICE";
            jpk.Gmina = "GLIWICE";
            jpk.Ulica = "Strzeleckiego";
            jpk.NrDomu = "32";
            jpk.NrLokalu = "";
            jpk.Miejscowosc = "GLIWICE";
            jpk.KodPocztowy = "44-105";
            jpk.Poczta = "GLIWICE";

        }

    }
}
