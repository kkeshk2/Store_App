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
    public class TestAddressSqlHelper : ISqlHelper
    {
        private static DataSet DataSet = CreateDataSet();

        public TestAddressSqlHelper(string command)
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
            table.Rows.Add(1, "John Smith", "100 Main Street", "Apt 101", "Lincoln", "NE", "68500");

            dataSet.Tables.Add(table);
            dataSet.CreateDataReader();

            return dataSet;
        }
    }
}
