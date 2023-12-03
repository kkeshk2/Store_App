using Microsoft.Extensions.Configuration;
namespace Store_App
{
    public class ConnectionString
    {

        public static string getConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionstr = "KareemConnection";

            string? connectionString = config["ConnectionStrings:" + connectionstr];
            if (connectionString == null)
            {
                throw new NullReferenceException("Connection String doesn't exist");
            }
            return connectionString;
        }
    }
}
