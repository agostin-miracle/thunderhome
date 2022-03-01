using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ThunderFire.Connector
{
    public static class ConnectionFactory
    {
        private static IDbConnection _conn;

        public static string ConnectionString { get; set; } = string.Empty;

        public static string ConnectionSource { get; set; } = string.Empty;


        internal static string GetConnectionString(string context = "DBACTIVECON")
        {
            try
            {
                return ConfigurationManager.ConnectionStrings[context].ConnectionString;
            }
            catch { }
            return "";
        }

        internal static string GetConnectionSource()
        {
            try
            {
                return ConfigurationManager.AppSettings["CONSRC"].ToString();
            }
            catch { }
            return "";
        }

        public static IDbConnection GetConnection()
        {
            string reference = GetConnectionSource();
            if (!string.IsNullOrWhiteSpace(reference))
            {
                ConnectionString = GetConnectionString(reference);
                return GetConnection(ConnectionString);
            }
            return null;
        }


        public static IDbConnection GetConnection(string cs)
        {
            if (_conn == null || string.IsNullOrEmpty(_conn?.ConnectionString))
            {
                _conn = new SqlConnection(cs);
            }
            return _conn;
        }

    }

}
