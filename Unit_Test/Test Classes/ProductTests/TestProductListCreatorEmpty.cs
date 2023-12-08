using Store_App.Models.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Test.Test_Classes.ProductTests
{
    public class TestProductListCreatorEmpty : IProductListCreator
    {
        public IProductList GetProductList()
        {
            return new ProductList(new TestProductDataContextEmpty(), new TestProductCreator());
        }
    }
}
