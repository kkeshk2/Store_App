using System.Data.Common;
using System.Data.SqlClient;

namespace Store_App.Helpers
{
    public interface ISqlHelper : IDisposable
    {
        public void AddParameter(string parameterName, object value);
        public void ExecuteNonQuery();
        public DbDataReader ExecuteReader();
        public object ExecuteScalar();
    }
}
