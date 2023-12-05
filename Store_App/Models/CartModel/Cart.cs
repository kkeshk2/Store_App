using System.Data;
using Store_App.Helpers;
using Newtonsoft.Json;
using System.Data.SqlClient;
using Store_App.Models.AccountModel;

namespace Store_App.Models.CartModel
{
    public class Cart : ICart
    {
        [JsonIgnore] private int AccountId;
        [JsonProperty] private int Size;
        [JsonProperty] private decimal Total;      
        [JsonProperty] private List<ICartProduct> Products = new();

        public void AccessCart(int accountId)
        {
            AccountId = accountId;
            AccessCartProducts();
        }

        private void AccessCartProducts()
        {
            using (ISqlHelper helper = new SqlHelper("SELECT * FROM Cart WHERE accountId = @accountId"))
            {
                helper.AddParameter("@accountId", AccountId);
                AccessCartProducts(helper);
                CalculateCartTotal();
            }
        }

        private void AccessCartProducts(ISqlHelper helper)
        {
            
            using (var reader = helper.ExecuteReader())
            {
                AccessCartProducts(reader);
                reader.Close();
            }
        }

        private void AccessCartProducts(SqlDataReader reader)
        {
            while (reader.Read())
            {
                var productId = reader.GetInt32("productId");
                var quantity = reader.GetInt32("quantity");
                Products.Add(new CartProduct(productId, quantity));
            }
        }

        public void AddToCart(int productId, int quantity)
        {
            if (Products.Count(p => p.GetProductId() == productId) != 0) return;
            using (ISqlHelper helper = new SqlHelper("INSERT INTO Cart (accountId, productId, quantity) VALUES (@accountId, @productId, @quantity)"))
            {
                helper.AddParameter("@accountId", AccountId);
                helper.AddParameter("@productId", productId);
                helper.AddParameter("@quantity", quantity);
                helper.ExecuteNonQuery();
            }
            AccessCart(AccountId);
        }

        private void CalculateCartTotal()
        {
            Size = 0;
            Total = 0;

            foreach (ICartProduct product in Products)
            {
                Size += product.GetQuantity();
                Total += product.GetQuantity() * product.GetPrice();
            }
        }

        public void ClearCart()
        {
            using (ISqlHelper helper = new SqlHelper("DELETE FROM Cart WHERE accountId = @accountId"))
            {
                helper.AddParameter("@accountId", AccountId);
                helper.ExecuteNonQuery();
            }
            AccessCart(AccountId);
        }

        public void DeleteItem(int productId)
        {
            using (ISqlHelper helper = new SqlHelper("DELETE FROM Cart WHERE accountId = @accountId AND productId = @productId"))
            {
                helper.AddParameter("@accountId", AccountId);
                helper.AddParameter("@productId", productId);
                helper.ExecuteNonQuery();
            }

            Products = new(); // Kareem Added
            AccessCart(AccountId);
        }

        public List<ICartProduct> GetCartProducts()
        {
            return Products;
        }

        public decimal GetTotal()
        {
            return Total;
        }

        public void UpdateCart(int productId, int quantity)
        {
            using (ISqlHelper helper = new SqlHelper("UPDATE Cart SET quantity = @quantity WHERE accountId = @accountId AND productId = @productId"))
            {
                helper.AddParameter("@quantity", quantity);
                helper.AddParameter("@accountId", AccountId);
                helper.AddParameter("@productId", productId);
                helper.ExecuteNonQuery();
            }
            AccessCart(AccountId);
        }

        // Kareem Added
        public override bool Equals(object? obj)
        {
            if (obj is not null && obj is Cart cart)
            {
                bool equals = true;
                equals = equals && cart.AccountId == AccountId;
                equals = equals && cart.Size == Size;
                equals = equals && cart.Total == Total;
                equals = equals && cart.Products.SequenceEqual(Products);
                return equals;
            }

            return false;
        }
    }
}