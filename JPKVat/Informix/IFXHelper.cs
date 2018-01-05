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
            catch(OdbcException ex)
            {
                return ex.Message;
            } 
        }

        
        public static DataTable GetRejSprzedazy(string connectionstring)
        {
                OdbcConnection DbConnection = new OdbcConnection(connectionstring);
                DbConnection.Open();
                DataSet ds = new DataSet();
                OdbcDataAdapter da = new OdbcDataAdapter("SELECT h.idfakt,k.netto,k.vat,s.stawka,h.netto,k.dataew FROM fkx_vatz h, fkx_vatz_kwoty k, fkx_stawkivat s WHERE k.stawka = s.idstawki AND h.idvat = k.idvat ORDER BY h.idfakt ", DbConnection);
                da.Fill(ds);
                DbConnection.Close();
                return ds.Tables[0];

        }



    }
}
