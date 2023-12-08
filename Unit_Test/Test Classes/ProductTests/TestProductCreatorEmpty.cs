using Store_App.Helpers;
using Store_App.Models.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Test.Test_Classes.ProductTests
{
    public class TestProductCreatorEmpty : IProductCreator
    {
        public IProduct GetProduct()
        {
            return new Product(new TestProductDataContextEmpty());
        }

        public IProduct GetProduct(int productId, string? name, decimal price, decimal sale, decimal rating, string? manufacturer, string? description, string? category, decimal length, decimal width, decimal height, decimal weight, string? sku, string? imageLocation)
        {
            return new Product(productId, name, price, sale, rating, manufacturer, description, category, length, width, height, weight, sku, imageLocation, new TestProductDataContext());
        }
    }
}
