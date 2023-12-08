namespace Store_App.Helpers
{
    public interface IDataContext
    {
        public ISqlHelper GetConnection(string connection);
    }
}
