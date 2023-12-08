using Store_App.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Test.Test_Classes.CartTests
{
    public class TestCartSqlHelper : ISqlHelper
    {
        private static DataSet DataSet = CreateDataSet();

        public TestCartSqlHelper(string command)
        {

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void AddParameter(string parameterName, object value)
        {

        }

        public void ExecuteNonQuery()
        {

        }

        public DbDataReader ExecuteReader()
        {
            return DataSet.Tables[0].CreateDataReader();

        }

        public object ExecuteScalar()
        {
            return 1;
        }

        private static DataSet CreateDataSet()
        {
            DataSet dataSet = new DataSet();

            DataTable table = new DataTable();
            table.TableName = "Cart";
            table.Columns.Add("cartId", typeof(int));
            table.Columns.Add("accountId", typeof(int));
            table.Columns.Add("productId", typeof(int));
            table.Columns.Add("quantity", typeof(int));
            table.Rows.Add(1, 1, 1, 3);
            

            dataSet.Tables.Add(table);
            dataSet.CreateDataReader();

            return dataSet;
        }
    }
}
