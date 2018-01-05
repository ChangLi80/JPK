using JPKVat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPKVat.Models
{
    public class CsvCreator
    {
        public readonly string header = "KodFormularza;kodSystemowy;wersjaSchemy;WariantFormularza;CelZlozenia;DataWytworzeniaJPK;DataOd;DataDo;DomyslnyKodWaluty;KodUrzedu;NIP;PelnaNazwa;REGON;KodKraju;Wojewodztwo;Powiat;Gmina;Ulica;NrDomu;NrLokalu;Miejscowosc;KodPocztowy;Poczta;typSprzedazy;LpSprzedazy;NrKontrahenta;NazwaKontrahenta;AdresKontrahenta;DowodSprzedazy;DataWystawienia;DataSprzedazy;K_10;K_11;K_12;K_13;K_14;K_15;K_16;K_17;K_18;K_19;K_20;K_21;K_22;K_23;K_24;K_25;K_26;K_27;K_28;K_29;K_30;K_31;K_32;K_33;K_34;K_35;K_36;K_37;K_38;K_39;LiczbaWierszySprzedazy;PodatekNalezny;typZakupu;LpZakupu;NrDostawcy;NazwaDostawcy;AdresDostawcy;DowodZakupu;DataZakupu;DataWplywu;K_43;K_44;K_45;K_46;K_47;K_48;K_49;K_50;LiczbaWierszyZakupow;PodatekNaliczony";

        public bool CreateCSvFile(Ijpk jpk)
        {
            string jpkPath = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"JPK","jpk.csv");
            if(!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(jpkPath)))
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(jpkPath));

            try
            {
                using (System.IO.TextWriter writeFile = new System.IO.StreamWriter(jpkPath))
                {
                    CsvHelper.CsvWriter csv = new CsvHelper.CsvWriter(writeFile);
                    csv.Configuration.Delimiter = ";";
                    var fields = header.Split(";".ToCharArray());

                    foreach (string field in fields) csv.WriteField(field);
                    csv.NextRecord();

                    csv.WriteField(jpk.KodFormularza);
                    csv.WriteField(jpk.kodSystemowy);
                    csv.WriteField(jpk.wersjaSchemy);
                    csv.WriteField(jpk.WariantFormularza);
                    csv.WriteField(jpk.CelZlozenia);
                    csv.WriteField(jpk.DataWytworzeniaJPK.ToShortDateString());
                    csv.WriteField(jpk.DataOd.ToShortDateString());
                    csv.WriteField(jpk.DataDo.ToShortDateString());
                    csv.WriteField(jpk.DomyslnyKodWaluty);
                    csv.WriteField(jpk.KodUrzedu);
                    for (int i = 0; i < fields.Count() - fields.ToList().IndexOf("KodUrzedu"); i++) csv.WriteField("");
                    csv.NextRecord();

                    for (int i = 0; i < fields.ToList().IndexOf("NIP"); i++) csv.WriteField("");
                    csv.WriteField(jpk.NIP);
                    csv.WriteField(jpk.PelnaNazwa);
                    csv.WriteField(jpk.REGON);
                    for (int i = 0; i < fields.Count() - fields.ToList().IndexOf("REGON"); i++) csv.WriteField("");
                    csv.NextRecord();

                    for (int i = 0; i < fields.ToList().IndexOf("KodKraju"); i++) csv.WriteField("");
                    csv.WriteField(jpk.KodKraju);
                    csv.WriteField(jpk.Wojewodztwo);
                    csv.WriteField(jpk.Powiat);
                    csv.WriteField(jpk.Gmina);
                    csv.WriteField(jpk.Ulica);
                    csv.WriteField(jpk.NrDomu);
                    csv.WriteField(jpk.NrLokalu);
                    csv.WriteField(jpk.Miejscowosc);
                    csv.WriteField(jpk.KodPocztowy);
                    csv.WriteField(jpk.Poczta);
                    for (int i = 0; i < fields.Count() - fields.ToList().IndexOf("Poczta"); i++) csv.WriteField("");
                    csv.NextRecord();
                    for (int i = 0; i < fields.ToList().IndexOf("typSprzedazy"); i++) csv.WriteField("");
                    FkRejSprzedazy(jpk, csv);

                }
                jpk.JPKMessage = (new StringBuilder()).AppendLine("Wygenerowano JPK csv").AppendLine(jpkPath).ToString();
                return true;
            }
            catch(Exception exp)
            {
                jpk.JPKMessage = exp.Message;
            }
            return false;
        }



        public void FkRejSprzedazy(Ijpk jpk,CsvHelper.CsvWriter csv)
        {
            csv.WriteField("G");
            csv.NextRecord();
        }

        public void FkRejZakupow(Ijpk jpk,CsvHelper.CsvWriter csv)
        {

        }

    }
}
