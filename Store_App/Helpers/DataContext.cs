namespace Store_App.Helpers
{
    public class DataContext : IDataContext
    {
        public ISqlHelper GetConnection(string command)
        {
            return new SqlHelper(command);
        }
    }
}
