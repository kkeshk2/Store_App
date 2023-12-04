using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Store_App.Helpers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Store_App.Exceptions;

namespace Store_App.Models.ProductModel
{
    public class Product : IProduct
    {
        [JsonProperty] private int ProductId;
        [JsonProperty] private string? Name;
        [JsonProperty] private decimal Price;
        [JsonProperty] private decimal Sale;
        [JsonProperty] private decimal Rating;
        [JsonProperty] private string? Manufacturer;
        [JsonProperty] private string? Description;
        [JsonProperty] private string? Category;
        [JsonProperty] private decimal Length;
        [JsonProperty] private decimal Width;
        [JsonProperty] private decimal Height;
        [JsonProperty] private decimal Weight;
        [JsonProperty] private string? SKU;
        [JsonProperty] private string? ImageLocation;

        public Product() {}

        public void AccessProduct(int productId)
        {
            using (ISqlHelper helper = new SqlHelper("SELECT * FROM Product WHERE productId = @productId"))
            {
                AccessProduct(helper, productId);
            }
            AccessProductSale();
        }

        private void AccessProduct(ISqlHelper helper, int productId)
        {
            helper.AddParameter("@productId", productId);
            using (var reader = helper.ExecuteReader())
            {
                AccessProduct(reader);
                reader.Close();
            }
        }

        private void AccessProduct(SqlDataReader reader)
        {
            if (!reader.Read())
            {
                throw new ProductNotFoundException("Product not found.");
            }

            ProductId = reader.GetInt32("productId");
            Name = reader.GetString("name");
            Price = reader.GetDecimal("price");
            AccessProductDetails(reader);
            AccessProductDimensions(reader);
        }

        private void AccessProductDetails(SqlDataReader reader)
        {
            Manufacturer = reader.GetString("manufacturer");
            Rating = reader.GetDecimal("rating");
            Description = reader.GetString("description");
            Category = reader.GetString("category");
            SKU = reader.GetString("sku");
            ImageLocation = reader.GetString("imageLocation");
        }

        private void AccessProductDimensions(SqlDataReader reader)
        {
            Length = reader.GetDecimal("Length");
            Width = reader.GetDecimal("Width");
            Height = reader.GetDecimal("Height");
            Weight = reader.GetDecimal("Weight");
        }

        public void AccessProductSale()
        {
            using (ISqlHelper helper = new SqlHelper("SELECT amount FROM Sale WHERE productId = @productId AND startDate <= @currentDate AND endDate >= @currentDate"))
            {
                AccessProductSale(helper);
            }
        }

        private void AccessProductSale(ISqlHelper helper)
        {
            var currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            helper.AddParameter("@currentDate", currentDate);
            helper.AddParameter("@productId", ProductId);
            using (var reader = helper.ExecuteReader())
            {
                AccessProductSale(reader);
                reader.Close();
            }
        }

        private void AccessProductSale(SqlDataReader reader)
        {
            decimal saleAmount = 0;
            if (reader.Read())
            {
                saleAmount = reader.GetDecimal("Amount");
            }
            Sale = saleAmount;
        }

        public decimal GetPrice()
        {
            return Price - Sale;
        }

        public int GetProductId()
        {
            return ProductId;
        } 
    }
}