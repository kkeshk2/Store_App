using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Store_App.Helpers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace Store_App.Models.ProductModel
{
    public class Product : IProduct
    {
        [JsonProperty] private int ProductId;
        [JsonProperty] private string? ProductName;
        [JsonProperty] private decimal ProductPrice;
        [JsonProperty] private decimal ProductSale;
        [JsonProperty] private decimal ProductRating;
        [JsonProperty] private string? ProductManufacturer;
        [JsonProperty] private string? ProductDescription;
        [JsonProperty] private string? ProductCategory;
        [JsonProperty] private decimal ProductLength;
        [JsonProperty] private decimal ProductWidth;
        [JsonProperty] private decimal ProductHeight;
        [JsonProperty] private decimal ProductWeight;
        [JsonProperty] private string? ProductSKU;
        [JsonProperty] private string? ProductImageLocation;

        public int GetProductId()
        {
            return ProductId;
        }

        public decimal GetProductPrice()
        {
            return ProductPrice - ProductSale;
        }

        public void AccessProduct(int productId)
        {
            using (var helper = new SqlHelper("SELECT * FROM Product WHERE productId = @productId"))
            {
                helper.AddParameter("@productId", productId);
                using (var reader = helper.ExecuteReader())
                {
                    reader.Read();
                    AccessProduct(reader);
                    reader.Close();
                }
                AccessProductSale();
            }
        }

        public void AccessProduct(SqlDataReader reader)
        {
            
            ProductId = reader.GetInt32("productId");
            ProductName = reader.GetString("productName");
            ProductPrice = reader.GetDecimal("productPrice");
            AccessProductDetails(reader);
            AccessProductDimensions(reader);
            
        }

        private void AccessProductDetails(SqlDataReader reader)
        {
            ProductManufacturer = reader.GetString("productManufacturer");
            ProductRating = reader.GetDecimal("productRating");
            ProductDescription = reader.GetString("productDescription");
            ProductCategory = reader.GetString("productCategory");
            ProductSKU = reader.GetString("productSKU");
            ProductImageLocation = reader.GetString("productImageLocation");
        }

        private void AccessProductDimensions(SqlDataReader reader)
        {
            ProductLength = reader.GetDecimal("productLength");
            ProductWidth = reader.GetDecimal("productWidth");
            ProductHeight = reader.GetDecimal("productHeight");
            ProductWeight = reader.GetDecimal("productWeight");
        }

        public void AccessProductSale()
        {
            using (var helper = new SqlHelper("SELECT saleAmount FROM Sale WHERE productId = @productId AND saleStartDate <= @currentDate AND saleEndDate >= @currentDate"))
            {
                decimal saleAmount = 0;
                var currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                helper.AddParameter("@currentDate", currentDate);
                helper.AddParameter("@productId", ProductId);
                using (var reader = helper.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        saleAmount = reader.GetDecimal("saleAmount");
                    }
                    reader.Close();
                    ProductSale = saleAmount;
                }
            }
        }
    }
}
