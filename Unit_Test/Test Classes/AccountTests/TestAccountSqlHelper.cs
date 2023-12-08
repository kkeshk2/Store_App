using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Store_App.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Test.Test_Classes.AccountTests
{
    public class TestAccountSqlHelper : ISqlHelper
    {
        private static DataSet DataSet = CreateDataSet();

        public TestAccountSqlHelper(string command)
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
            table.TableName = "Account";
            table.Columns.Add("accountId", typeof(int));
            table.Columns.Add("email", typeof(string));
            table.Columns.Add("password", typeof(string));
            table.Rows.Add(1, "user1@example.com", "pass1");

            dataSet.Tables.Add(table);
            dataSet.CreateDataReader();

            return dataSet;
        }
    }
}
