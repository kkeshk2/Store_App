using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using Store_App.Helpers;

namespace Unit_Test.Test_Classes.InvoiceTests
{
    public class TestInvoiceSqlHelperEmpty : ISqlHelper
    {
        private static DataSet DataSet = CreateDataSet();

        public TestInvoiceSqlHelperEmpty(string command)
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
            table.TableName = "Invoice";
            table.Columns.Add("invoiceId", typeof(int));
            table.Columns.Add("accountId", typeof(int));
            table.Columns.Add("size", typeof(int));
            table.Columns.Add("total", typeof(decimal));
            table.Columns.Add("date", typeof(SqlDateTime));
            table.Columns.Add("creditCard", typeof(string));
            table.Columns.Add("billingAddressId", typeof(int));
            table.Columns.Add("shippingAddressId", typeof(int));
            table.Columns.Add("trackingNumber", typeof(string));
            table.Columns.Add("invoiceProductId", typeof(int));
            table.Columns.Add("productId", typeof(int));
            table.Columns.Add("quantity", typeof(int));
            table.Columns.Add("price", typeof(decimal));
           

            dataSet.Tables.Add(table);
            dataSet.CreateDataReader();

            return dataSet;
        }
    }
}
