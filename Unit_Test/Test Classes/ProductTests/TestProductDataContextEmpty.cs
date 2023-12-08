using Store_App.Helpers;
using Store_App.Models.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Test.Test_Classes.ProductTests
{
    public class TestProductDataContextEmpty : IDataContext
    {
        public ISqlHelper GetConnection(string command)
        {
            return new TestProductSqlHelperEmpty(command);
        }
    }
}
