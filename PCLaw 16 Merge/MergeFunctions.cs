using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace PCLaw_16_Merge
{
    public static class MergeFunctions
    {
        public static string RunSQLCommand(string sQuery, string conString)
        {
            int iResult = 0;
            string sError = "";
            try
            {

                SqlConnection Conn = new SqlConnection(conString);
                Conn.Open();
                SqlCommand Cmd = new SqlCommand(sQuery, Conn);
                iResult = Cmd.ExecuteNonQuery();
                Conn.Dispose();
                Conn = null;
            }
            catch (Exception objError)
            {
                sError = objError.ToString();
            }
            return sError;
        }

        public static string GetMaxID(string sQuery, string conString)
        {
            string maxId = "";
            SqlConnection myConnection = new SqlConnection(conString);
            using (SqlCommand myCommand = myConnection.CreateCommand())
            {
                myConnection.Open();
                myCommand.CommandText = sQuery;
                maxId = myCommand.ExecuteScalar().ToString();
            }
            myConnection.Dispose();
            myConnection = null;
            return maxId;
        }
    }
}
