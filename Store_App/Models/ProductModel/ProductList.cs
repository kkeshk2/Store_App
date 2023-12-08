using Newtonsoft.Json;
using Store_App.Helpers;
using Store_App.Models.CartModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Store_App.Models.ProductModel
{
    public class ProductList : IProductList
    {
        [JsonIgnore] private IDataContext DataContext;
        [JsonIgnore] private IProductCreator ProductCreator;

        [JsonProperty] private List<IProduct> Products = new List<IProduct>();

        public ProductList(IDataContext dataContext, IProductCreator productCreator)
        {
            DataContext = dataContext;
            ProductCreator = productCreator;
        }

        public void AccessProductList()
        {
            using (ISqlHelper helper = DataContext.GetConnection("SELECT productId FROM Product"))
            {
                AccessProductList(helper);
            }
        }

        private void AccessProductList(ISqlHelper helper)
        {
            using (var reader = helper.ExecuteReader())
            {
                AccessProductList(reader);
                reader.Close();
            }
        }

        private void AccessProductList(DbDataReader reader)
        {
            while (reader.Read())
            {
                int productId = reader.GetInt32("productId");
                IProduct product = ProductCreator.GetProduct();
                product.AccessProduct(productId);
                Products.Add(product);
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is not null && obj is ProductList product_list)
            {
                bool equals = true;
                equals = equals && product_list.Products.SequenceEqual(Products);
                return equals;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
