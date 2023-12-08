using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store_App.Helpers;

namespace Unit_Test.Test_Classes.AddressTests
{
    internal class TestAddressSqlHelperEmpty : ISqlHelper
    {
        private static DataSet DataSet = CreateDataSet();

        public TestAddressSqlHelperEmpty(string command)
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
            table.TableName = "Address";
            table.Columns.Add("addressId", typeof(int));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("line1", typeof(string));
            table.Columns.Add("line2", typeof(string));
            table.Columns.Add("city", typeof(string));
            table.Columns.Add("state", typeof(string));
            table.Columns.Add("postal", typeof(string));

            dataSet.Tables.Add(table);
            dataSet.CreateDataReader();

            return dataSet;
        }
    }
}
