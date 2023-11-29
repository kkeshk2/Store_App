using System.Data;
using Store_App.Helpers;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using Store_App.Models.ProductModel;

namespace Store_App.Models.CartModel
{
    public class Cart : ICart
    {
        [JsonProperty] private int CartId;
        [JsonIgnore] private int AccountId;
        [JsonProperty] private List<ICartProduct> ProductList = new();

        public void AccessCart(int accountId)
        {
            if (!CartExists(accountId)) CreateCart(accountId);
            using (var helper = new SqlHelper("SELECT * FROM Cart WHERE accountId = @accountId"))
            {
                helper.AddParameter("accountId", accountId);
                using (var reader = helper.ExecuteReader())
                {
                    reader.Read();
                    CartId = reader.GetInt32("cartId");
                    AccountId = reader.GetInt32("accountId");
                    reader.Close();
                }    
            }
            AccessCartProducts();
        }

        private void AccessCartProducts()
        {
            using (var helper = new SqlHelper("SELECT * FROM CartProduct WHERE cartId = @cartId"))
            {
                helper.AddParameter("@cartId", CartId);
                using (var reader = helper.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var productId = reader.GetInt32("productId");
                        var quantity = reader.GetInt32("cartProductQuantity");
                        ProductList.Add(new CartProduct(productId, quantity));
                    }
                    reader.Close();
                }
            }
        }

        public void AddToCart(int productId, int quantity)
        {
            if (Contains(productId) != 0) return;
            using (var helper = new SqlHelper("INSERT INTO CartProduct (cartId, productId, cartProductQuantity) VALUES (@cartId, @productId, @cartProductQuantity)"))
            {
                helper.AddParameter("@cartId", CartId);
                helper.AddParameter("@productId", productId);
                helper.AddParameter("@cartProductQuantity", quantity);
                helper.ExecuteNonQuery();
            }
            AccessCart(AccountId);
        }

        private static bool CartExists(int accountId)
        {
            using (var helper = new SqlHelper("SELECT * FROM Cart WHERE accountId = @accountId"))
            {
                helper.AddParameter("accountId", accountId);
                using (var reader = helper.ExecuteReader())
                {
                    var result = reader.Read();                 
                    reader.Close();
                    return result;
                }
            }
        }

        public int Contains(int productId)
        {
            using (var helper = new SqlHelper("SELECT * FROM CartProduct WHERE cartId = @cartId AND productId = @productId"))
            {
                helper.AddParameter("@cartId", CartId);
                helper.AddParameter("@productId", productId);
                using (var reader = helper.ExecuteReader())
                {
                    var result = 0;
                    if (reader.Read())
                    {
                        result = reader.GetInt32("cartProductQuantity");
                    }    
                    reader.Close();
                    return result;
                }
            }
        }

        public void ClearCart()
        {
            using (var helper = new SqlHelper("DELETE FROM CartProduct WHERE cartId = @cartId"))
            {
                helper.AddParameter("@cartId", CartId);
                helper.ExecuteNonQuery();
            }
            AccessCart(AccountId);
        }

        private void CreateCart(int accountId)
        {
            if (CartExists(accountId)) throw new InvalidOperationException();
            using (var helper = new SqlHelper("INSERT INTO Cart (accountId) VALUES (@accountId)"))
            {
                helper.AddParameter("accountId", accountId);
                helper.ExecuteNonQuery();
            }
            AccessCart(AccountId);
        }

        public void DeleteItem(int productId)
        {
            using (var helper = new SqlHelper("DELETE FROM CartProduct WHERE cartId = @cartId AND productId = @productId"))
            {
                helper.AddParameter("@cartId", CartId);
                helper.AddParameter("@productId", productId);
                helper.ExecuteNonQuery();
            }
            AccessCart(AccountId);
        }

        public List<ICartProduct> GetProductList()
        {
            return ProductList;
        }

        public void UpdateCart(int productId, int quantity)
        {
            using (var helper = new SqlHelper("UPDATE CartProduct SET cartProductQuantity = @cartProductQuantity WHERE cartId = @cartId AND productId = @productId"))
            {
                helper.AddParameter("@cartProductQuantity", quantity);
                helper.AddParameter("@cartId", CartId);
                helper.AddParameter("@productId", productId);
                helper.ExecuteNonQuery();
            }
            AccessCart(AccountId);
        }
    }
}