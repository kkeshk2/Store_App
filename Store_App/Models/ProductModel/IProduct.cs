using System.Collections;
using System.Data.SqlClient;

namespace Store_App.Models.ProductModel
{
    public interface IProduct
    {
        public int GetProductId();
        public decimal GetProductPrice();
        public void AccessProduct(int productId);
        public void AccessProduct(SqlDataReader reader);
        public void AccessProductSale();
    }
}
