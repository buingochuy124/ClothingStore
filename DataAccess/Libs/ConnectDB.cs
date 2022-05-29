using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Libs
{
    public class ConnectDB
    {
        public static SqlConnection GetSqlConnection()
        {
            SqlConnection sqlconn = null;
            try
            {
                var connectName = "Server=.;Database=ClothingStore;Trusted_Connection=True;";

                sqlconn = new SqlConnection(connectName);

                if (sqlconn.State == System.Data.ConnectionState.Open)
                {
                    sqlconn.Close();
                }

                sqlconn.Open();
            }
            catch (Exception)
            {
                throw;
            }

            return sqlconn;

        }
    }
}
