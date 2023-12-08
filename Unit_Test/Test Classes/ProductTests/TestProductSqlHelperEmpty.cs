using Store_App.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Test.Test_Classes.ProductTests
{
    public class TestProductSqlHelperEmpty : ISqlHelper
    {
        private static DataSet DataSet = CreateDataSet();

        public TestProductSqlHelperEmpty(string command)
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
            table.TableName = "Product";
            table.Columns.Add("productId", typeof(int));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("price", typeof(decimal));
            table.Columns.Add("manufacturer", typeof(string));
            table.Columns.Add("rating", typeof(decimal));
            table.Columns.Add("description", typeof(string));
            table.Columns.Add("category", typeof(string));
            table.Columns.Add("length", typeof(decimal));
            table.Columns.Add("width", typeof(decimal));
            table.Columns.Add("height", typeof(decimal));
            table.Columns.Add("weight", typeof(decimal));
            table.Columns.Add("sku", typeof(string));
            table.Columns.Add("imageLocation", typeof(string));
            table.Columns.Add("amount", typeof(decimal));

            dataSet.Tables.Add(table);
            dataSet.CreateDataReader();

            return dataSet;
        }
    }
}
