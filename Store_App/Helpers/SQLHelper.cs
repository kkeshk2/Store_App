using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace Store_App.Helpers
{
    public class SqlHelper : ISqlHelper
    {
        private SqlConnection SqlConnection { get; set; }
        private SqlCommand SqlCommand { get; set; }

        public SqlHelper(string command) {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("appsettings.json");
            var config = configBuilder.Build();

            string? connectionString = config.GetConnectionString("Connection");
            SqlConnection = new SqlConnection(connectionString);
            SqlConnection.Open();

            SqlCommand = new SqlCommand(command, SqlConnection);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            SqlCommand.Dispose();
            SqlConnection.Close();
            SqlConnection.Dispose();
        }

        public void AddParameter(string parameterName, object value)
        {
            SqlCommand.Parameters.AddWithValue(parameterName, value);
        }

        public void ExecuteNonQuery()
        {
            SqlCommand.ExecuteNonQuery();
        }

        public DbDataReader ExecuteReader()
        {
            return SqlCommand.ExecuteReader();
        }

        public object ExecuteScalar()
        {
            return SqlCommand.ExecuteScalar();
        }
    }
}
