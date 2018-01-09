using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JPKVat.Informix
{
    class IFXHelper
    {
        public static string TryToConnect(string connectionstring)
        {
            try
            {
                OdbcConnection DbConnection = new OdbcConnection(connectionstring);
                DbConnection.Open();
                DataSet ds = new DataSet();
                OdbcDataAdapter da = new OdbcDataAdapter("SELECT h.idfakt,k.netto,k.vat,s.stawka,h.netto,k.dataew FROM fkx_vatz h, fkx_vatz_kwoty k, fkx_stawkivat s WHERE k.stawka = s.idstawki AND h.idvat = k.idvat ORDER BY h.idfakt ", DbConnection);
                da.Fill(ds);
                DbConnection.Close();
                return "Success";
            }
            catch (OdbcException ex)
            {
                return ex.Message;
            }
        }


        public DataTable GetRejSprzedazy(string connectionstring, DateTime from, DateTime to)
        {

            OdbcConnection DbConnection = new OdbcConnection(connectionstring);
            DbConnection.Open();
            DataSet ds = new DataSet();
            using (OdbcCommand cmd = new OdbcCommand())
            {
                cmd.Connection = DbConnection;
                cmd.CommandText = "SELECT r.idfakt,r.nrksiegowy,r.datawyst,r.datasprzed,f.nazwa,f.nip,r.kwotabr,SUM(v.netto) AS netto,SUM(v.vat) AS vat,TRIM(o.opis),TRIM(a.miasto) AS miasto,TRIM(a.ulica) AS ulica,TRIM(a.nr) AS nr " +
                                  "FROM fkx_vats z,fkx_vats_kwoty v,rej_sprzed r,adres_firmy a, firmy f,fkx_stawkivat s,outer opis_fs o WHERE  r.data_ks <= ? AND r.data_ks >= ? " +
                                  "AND v.idvats=z.idvats and z.idfakt=r.idfakt and r.idfirmy=a.id_adresu and a.idfirmy=f.idfirmy and r.idfakt=o.idfakt and v.idstawki=s.idstawki " +
                                  "GROUP BY  r.idfakt,r.nrksiegowy,r.datawyst,r.datasprzed,f.nazwa,f.nip,r.kwotabr,o.opis,a.miasto,a.ulica,a.nr ORDER BY r.datasprzed";
                cmd.Parameters.AddWithValue("@do", to);
                cmd.Parameters.AddWithValue("@od", from);

                OdbcDataAdapter da = new OdbcDataAdapter(cmd);
                da.Fill(ds);
                DbConnection.Close();
                return ds.Tables[0];
            }

        }


        public DataTable GetRefZakupow(string connectionstring, DateTime from, DateTime to)
        {
            OdbcConnection DbConnection = new OdbcConnection(connectionstring);
            DbConnection.Open();
            DataSet ds = new DataSet();
            using (OdbcCommand cmd = new OdbcCommand())
            {
                cmd.Connection = DbConnection;
                cmd.CommandText = "SELECT 1 tr,r.idfakt,v.rodzaj[3,4],r.nrksiegowy,r.nrfaktury,r.datawyst,r.datawpl,"+
                                  "glb_IsDifMYData(r.data_ks, v.dataew),f.nazwa,f.nip,SUM(v.netto + v.vat) AS brutto,SUM(v.netto) AS netto,SUM(v.vat) AS vat,o.opis,a.miasto,a.ulica,a.nr "+
                                  "FROM fkx_vatz z, fkx_vatz_kwoty v,rejfaktur r, adres_firmy a, firmy f, fkx_stawkivat s,outer opis_fz o "+
                                  "WHERE v.dataew <= ? AND v.dataew >= ? and v.idvat = z.idvat and z.idfakt = r.idfakt and r.idfirmy = f.idfirmy " +
                                  "AND r.id_adrfirmy = a.id_adresu and r.idfakt = o.idfakt and v.stawka = s.idstawki and s.stawka <> 0 "+
                                  "GROUP BY 1,2,3,4,5,6,7,8,9,10,14,15,16,17";
                cmd.Parameters.AddWithValue("@do", to);
                cmd.Parameters.AddWithValue("@od", from);

                OdbcDataAdapter da = new OdbcDataAdapter(cmd);
                da.Fill(ds);
                DbConnection.Close();
                return ds.Tables[0];
            }
        }

    }
}
