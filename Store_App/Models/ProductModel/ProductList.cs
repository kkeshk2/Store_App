using Newtonsoft.Json;
using Store_App.Helpers;
using System.Data;
using System.Data.SqlClient;

namespace Store_App.Models.ProductModel
{
    public class ProductList : IProductList
    {
        [JsonProperty] private List<IProduct> Products = new List<IProduct>();

        public void AccessProductList()
        {
            using (ISqlHelper helper = new SqlHelper("SELECT productId FROM Product"))
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

        private void AccessProductList(SqlDataReader reader)
        {
            while (reader.Read())
            {
                int productId = reader.GetInt32("productId");
                IProduct product = new Product();
                product.AccessProduct(productId);
                Products.Add(product);
            }
        }
    }
}
