﻿using System;
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
            jpk.kodSystemowy = "JPK_VAT (2)";
            jpk.wersjaSchemy = "1/1/2000";
            jpk.WariantFormularza = "2";
            jpk.CelZlozenia = "1";
            DateTime lastdayofPreviousMoth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);
            jpk.DataOd =  new DateTime(lastdayofPreviousMoth.Year, lastdayofPreviousMoth.Month, 1);
            jpk.DataDo = lastdayofPreviousMoth;

            jpk.DomyslnyKodWaluty = "PLN";
            jpk.KodUrzedu="US GLIWICE";
            jpk.NIP="";
            jpk.PelnaNazwa="DigiSys Technologies";
            jpk.REGON="121313";
            jpk.KodKraju ="PL";
            jpk.Wojewodztwo ="SLASKIE";
            jpk.Powiat = "GLIWICE";
            jpk.Gmina = "GLIWICE";
            jpk.Ulica = "Strzeleckiego";
            jpk.NrDomu = "32";
            jpk.NrLokalu = "";
            jpk.Miejscowosc = "GLIWICE";
            jpk.KodPocztowy = "44-100";
            jpk.Poczta = "GLIWICE";

        }

    }
}