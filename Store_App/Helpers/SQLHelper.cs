using System.Data.SqlClient;

namespace Store_App.Helpers
{
    public class SqlHelper : IDisposable
    {
        private SqlConnection SqlConnection { get; set; }
        private SqlCommand SqlCommand { get; set; }

        public SqlHelper(string command) {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("appsettings.json");
            var config = configBuilder.Build();

            string? connectionString = config.GetConnectionString("AlexConnection");
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

        public SqlDataReader ExecuteReader()
        {
            return SqlCommand.ExecuteReader();
        }
    }
}
