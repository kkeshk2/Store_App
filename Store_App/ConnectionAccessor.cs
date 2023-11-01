using System.Data.SqlClient;

namespace Store_App
{
    class ConnectionAccessor
    {

        public static void TestDatabaseConnection(string connectionStr)
        {
            string connectionString = connectionStr;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    Console.WriteLine("Opening Connection ...");
                    connection.Open();
                    Console.WriteLine("Connection successful!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
                connection.Close();
                // You can now use the connection to interact with the SQL Server database.
            }
        }
    }
}