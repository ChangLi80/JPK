using JPKVat.Extentions;
using JPKVat.Informix;
using JPKVat.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPKVat.Models
{
    public class CsvCreator
    {
        //public readonly string header = "KodFormularza;kodSystemowy;wersjaSchemy;WariantFormularza;CelZlozenia;DataWytworzeniaJPK;DataOd;DataDo;DomyslnyKodWaluty;KodUrzedu;NIP;PelnaNazwa;REGON;KodKraju;Wojewodztwo;Powiat;Gmina;Ulica;NrDomu;NrLokalu;Miejscowosc;KodPocztowy;Poczta;typSprzedazy;LpSprzedazy;NrKontrahenta;NazwaKontrahenta;AdresKontrahenta;DowodSprzedazy;DataWystawienia;DataSprzedazy;K_10;K_11;K_12;K_13;K_14;K_15;K_16;K_17;K_18;K_19;K_20;K_21;K_22;K_23;K_24;K_25;K_26;K_27;K_28;K_29;K_30;K_31;K_32;K_33;K_34;K_35;K_36;K_37;K_38;K_39;LiczbaWierszySprzedazy;PodatekNalezny;typZakupu;LpZakupu;NrDostawcy;NazwaDostawcy;AdresDostawcy;DowodZakupu;DataZakupu;DataWplywu;K_43;K_44;K_45;K_46;K_47;K_48;K_49;K_50;LiczbaWierszyZakupow;PodatekNaliczony";

        public readonly string header = "KodFormularza;kodSystemowy;wersjaSchemy;WariantFormularza;CelZlozenia;DataWytworzeniaJPK;DataOd;DataDo;NazwaSystemu;NIP;PelnaNazwa;Email;LpSprzedazy;NrKontrahenta;NazwaKontrahenta;AdresKontrahenta;DowodSprzedazy;DataWystawienia;DataSprzedazy;K_10;K_11;K_12;K_13;K_14;K_15;K_16;K_17;K_18;K_19;K_20;K_21;K_22;K_23;K_24;K_25;K_26;K_27;K_28;K_29;K_30;K_31;K_32;K_33;K_34;K_35;K_36;K_37;K_38;K_39;LiczbaWierszySprzedazy;PodatekNalezny;LpZakupu;NrDostawcy;NazwaDostawcy;AdresDostawcy;DowodZakupu;DataZakupu;DataWplywu;K_43;K_44;K_45;K_46;K_47;K_48;K_49;K_50;LiczbaWierszyZakupow;PodatekNaliczony";

        public bool CreateCSvFile(Ijpk jpk)
        {
            string jpkPath = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "JPK", "jpk.csv");
            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(jpkPath)))
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
                    csv.WriteField(jpk.DataWytworzeniaJPK.ToJPKDataUtworzenia());
                    csv.WriteField(jpk.DataOd.ToJPK());
                    csv.WriteField(jpk.DataDo.ToJPK());
                    csv.WriteField("My JPK");
                    //csv.WriteField(jpk.KodUrzedu);
                    for (int i = 0; i < fields.Count() - fields.ToList().IndexOf("NazwaSystemu"); i++) csv.WriteField("");
                    csv.NextRecord();

                    for (int i = 0; i < fields.ToList().IndexOf("NIP"); i++) csv.WriteField("");
                    csv.WriteField(jpk.NIP);
                    csv.WriteField(jpk.PelnaNazwa);
                    csv.WriteField(jpk.REGON);
                    for (int i = 0; i < fields.Count() - fields.ToList().IndexOf("Email"); i++) csv.WriteField("");
                    csv.NextRecord();

                    //for (int i = 0; i < fields.ToList().IndexOf("KodKraju"); i++) csv.WriteField("");
                    //csv.WriteField(jpk.KodKraju);
                    //csv.WriteField(jpk.Wojewodztwo);
                    //csv.WriteField(jpk.Powiat);
                    //csv.WriteField(jpk.Gmina);
                    //csv.WriteField(jpk.Ulica);
                    //csv.WriteField(jpk.NrDomu);
                    //csv.WriteField(jpk.NrLokalu);
                    //csv.WriteField(jpk.Miejscowosc);
                    //csv.WriteField(jpk.KodPocztowy);
                    //csv.WriteField(jpk.Poczta);
                    //for (int i = 0; i < fields.Count() - fields.ToList().IndexOf("Poczta"); i++) csv.WriteField("");
                    //csv.NextRecord();
                    FkRejSprzedazy(jpk, csv);
                    FkRejZakupow(jpk, csv);
                }
                jpk.JPKMessage = (new StringBuilder()).AppendLine("Wygenerowano JPK csv").AppendLine(jpkPath).ToString();
                return true;
            }
            catch (Exception exp)
            {
                jpk.JPKMessage = exp.Message;
            }
            return false;
        }




        public void FkRejSprzedazy(Ijpk jpk, CsvHelper.CsvWriter csv)
        {
            //typSprzedazy	NrKontrahenta	NazwaKontrahenta	AdresKontrahenta	DowodSprzedazy	DataWystawienia	DataSprzedazy	K_10	K_11	K_12	K_13	K_14	K_15	K_16	K_17	K_18	K_19	K_20	K_21	K_22	K_23	K_24	K_25	K_26	K_27	K_28	K_29	K_30	K_31	K_32	K_33	K_34	K_35	K_36	K_37	K_38	K_39
            //LpSprzedazy   NrKontrahenta;NazwaKontrahenta;AdresKontrahenta;DowodSprzedazy;DataWystawienia;DataSprzedazy;           K_10;K_11;K_12;K_13;K_14;K_15;K_16;K_17;K_18;K_19;K_20;K_21;K_22;K_23;K_24;K_25;K_26;K_27;K_28;K_29;K_30;K_31;K_32;K_33;K_34;K_35;K_36;K_37;K_38;K_39;


            var fields = header.Split(";".ToCharArray());
            IFXHelper ifx = new IFXHelper();
            DataTable dt = ifx.GetRejSprzedazy(jpk.ConnectionString, jpk.DataOd, jpk.DataDo);

            int ICount = 0;
            double vat = 0.0;
            bool bFirst = true;
            foreach (DataRow dr in dt.Rows)
            {
                ICount += 1;
                for (int first = 0; first < (bFirst ? 2 : 1); first++)
                {
                    for (int i = 0; i < fields.ToList().IndexOf("LpSprzedazy"); i++) csv.WriteField("");
                    //csv.WriteField("G");
                    csv.WriteField(ICount);
                    csv.WriteField(dr["nip"].ToString().Trim().Length == 0 ? "Brak" : dr["nip"].ToString().Trim());
                    csv.WriteField(dr["nazwa"].ToString().Trim());
                    StringBuilder cbAddress = new StringBuilder();
                    cbAddress.Append(dr["miasto"]).Append(" ").Append(dr["ulica"]).Append(" ").Append(dr["nr"]);
                    csv.WriteField(cbAddress.ToString());
                    csv.WriteField(dr["nrksiegowy"].ToString().Trim());
                    csv.WriteField(Convert.ToDateTime(dr["datawyst"]).ToJPK());
                    csv.WriteField(Convert.ToDateTime(dr["datasprzed"]).ToJPK());
                    csv.WriteField("");
                    csv.WriteField(string.IsNullOrEmpty(dr["nip"].ToString().Trim()) ? dr["netto"] : "");
                    PutEmpty(csv, "K_12", "K_19");
                    csv.WriteField(string.IsNullOrEmpty(dr["nip"].ToString().Trim()) ? "" : dr["netto"]);
                    csv.WriteField(string.IsNullOrEmpty(dr["nip"].ToString().Trim()) ? "" : dr["vat"]);
                    for (int i = 0; i < fields.Count() - fields.ToList().IndexOf("K_21"); i++) csv.WriteField("");
                    csv.NextRecord();
                }
                bFirst = false;
                vat += Convert.ToDouble(dr["vat"]);

            }
            for (int i = 0; i < fields.ToList().IndexOf("LiczbaWierszySprzedazy"); i++) csv.WriteField("");
            csv.WriteField(ICount);
            csv.WriteField(vat);
            for (int i = 0; i < fields.Count() - fields.ToList().IndexOf("PodatekNalezny"); i++) csv.WriteField("");
            csv.NextRecord();
        }

        public void FkRejZakupow(Ijpk jpk, CsvHelper.CsvWriter csv)
        {
            //LpZakupu;NrDostawcy;NazwaDostawcy;AdresDostawcy;DowodZakupu;DataZakupu;DataWplywu;K_43;K_44;K_45;K_46;K_47;K_48;K_49;K_50;LiczbaWierszyZakupow;PodatekNaliczony
            //LpZakupu;NrDostawcy;NazwaDostawcy;AdresDostawcy;DowodZakupu;DataZakupu;DataWplywu;K_43;K_44;K_45;K_46;K_47;K_48;K_49;K_50;LiczbaWierszyZakupow;PodatekNaliczony

            var fields = header.Split(";".ToCharArray());
            IFXHelper ifx = new IFXHelper();
            DataTable dt = ifx.GetRefZakupow(jpk.ConnectionString, jpk.DataOd, jpk.DataDo);
            int ICount = 0;
            double vat = 0.0;
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < fields.ToList().IndexOf("LpZakupu"); i++) csv.WriteField("");
                //csv.WriteField("G");
                csv.WriteField(++ICount);
                csv.WriteField(dr["nip"].ToString().Trim().Length == 0 ? "Brak" : dr["nip"].ToString().Trim());
                csv.WriteField(dr["nazwa"].ToString().Trim());
                StringBuilder cbAddress = new StringBuilder();
                cbAddress.Append(dr["miasto"].ToString().Trim()).Append(" ").Append(dr["ulica"].ToString().Trim()).Append(" ").Append(dr["nr"].ToString().Trim());
                csv.WriteField(cbAddress.ToString());
                csv.WriteField(dr["nrfaktury"].ToString());
                csv.WriteField(Convert.ToDateTime(dr["datawyst"]).ToJPK());
                csv.WriteField(Convert.ToDateTime(dr["datawpl"]).ToJPK());
                PutEmpty(csv, "K_43", "K_45");
                csv.WriteField(dr["netto"]);
                csv.WriteField(dr["vat"]);
                for (int i = 0; i < fields.Count() - fields.ToList().IndexOf("K_47"); i++) csv.WriteField("");
                vat += Convert.ToDouble(dr["vat"]);
                csv.NextRecord();
            }
            for (int i = 0; i < fields.ToList().IndexOf("LiczbaWierszyZakupow"); i++) csv.WriteField("");
            csv.WriteField(ICount);
            csv.WriteField(vat);
            csv.NextRecord();
        }

        public void PutEmpty(CsvHelper.CsvWriter csv, string from, string to)
        {
            var fields = header.Split(";".ToCharArray());
            for (int i = 0; i < fields.ToList().IndexOf(to) - fields.ToList().IndexOf(from); i++) csv.WriteField("");


        }

    }
}
