using Newtonsoft.Json;
using Store_App.Helpers;

namespace Store_App.Models.ProductModel
{
    public class ProductList : IProductList
    {
        [JsonProperty] private List<IProduct> Products = new List<IProduct>();

        public void AccessProductList()
        {
            using (var helper = new SqlHelper("SELECT * FROM Product"))
            {
                using (var reader = helper.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IProduct product = new Product();
                        product.AccessProduct(reader);
                        product.AccessProductSale();
                        Products.Add(product);
                    }
                }
            }
        }
    }
}
