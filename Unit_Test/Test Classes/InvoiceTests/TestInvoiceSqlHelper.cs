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
    public class TestInvoiceSqlHelper : ISqlHelper
    {
        private static DataSet DataSet = CreateDataSet();

        public TestInvoiceSqlHelper(string command)
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
            table.Columns.Add("date", typeof(DateTime));
            table.Columns.Add("creditCard", typeof(string));
            table.Columns.Add("billingAddressId", typeof(int));
            table.Columns.Add("shippingAddressId", typeof(int));
            table.Columns.Add("trackingNumber", typeof(string));
            table.Columns.Add("invoiceProductId", typeof(int));
            table.Columns.Add("productId", typeof(int));
            table.Columns.Add("quantity", typeof(int));
            table.Columns.Add("price", typeof(decimal));
            table.Rows.Add(1, 1, 1, 100M, new DateTime(2000, 1, 1), "1000", 1, 1, "1000", 1, 1, 1, 100M);


            dataSet.Tables.Add(table);
            dataSet.CreateDataReader();

            return dataSet;
        }
    }
}
