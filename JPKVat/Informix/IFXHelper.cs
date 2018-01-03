using System;
using System.Collections.Generic;
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
                DbConnection.Close();
                return "Success";
            }
            catch(OdbcException ex)
            {
                return ex.Message;
            }
            
        }
    }
}
