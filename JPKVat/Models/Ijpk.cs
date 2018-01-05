using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPKVat.Models
{
    public interface Ijpk
    {
        string KodFormularza { get; set; }
        string kodSystemowy { get; set; }
        string wersjaSchemy { get; set; }
        string WariantFormularza { get; set; }

        string CelZlozenia { get; set; }
        DateTime DataWytworzeniaJPK { get; set; }
        DateTime DataOd { get; set; }
        DateTime DataDo { get; set; }
        string DomyslnyKodWaluty { get; set; }
        string KodUrzedu { get; set; }
        string NIP { get; set; }
        string PelnaNazwa { get; set; }
        string REGON { get; set; }
        string KodKraju { get; set; }
        string Wojewodztwo { get; set; }
        string Powiat { get; set; }
        string Gmina { get; set; }
        string Ulica { get; set; }
        string NrDomu { get; set; }
        string NrLokalu { get; set; }
        string Miejscowosc { get; set; }
        string KodPocztowy { get; set; }
        string Poczta { get; set; }

        string JPKMessage { get; set; }
    }
}
