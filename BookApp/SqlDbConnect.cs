using System.Data.SqlClient;
using System.Configuration;

namespace BookApp
{
    public class SqlDbConnect
    {
        public static SqlConnection CreateConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings["bookDb"].ConnectionString;
            return new SqlConnection(connString);
        }

        public static SqlCommand CreateCommand()
        {
            return new SqlCommand();
        }
    }
}