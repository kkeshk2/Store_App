using System.Collections;
using System.Data.SqlClient;

namespace Store_App.Models.ProductModel
{
    public interface IProduct
    {
        public int GetProductId();
        public decimal GetPrice();
        public void AccessProduct(int productId);
        public void AccessProductSale();
    }
}
