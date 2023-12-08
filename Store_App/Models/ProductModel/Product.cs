using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Store_App.Helpers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Store_App.Exceptions;
using Store_App.Models.CartModel;
using System.Data.Common;

namespace Store_App.Models.ProductModel
{
    public class Product : IProduct
    {
        [JsonIgnore] private IDataContext DataContext;

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

        public Product(IDataContext context)
        {
            DataContext = context;
        }

        public Product(int productId, string? name, decimal price, decimal sale, decimal rating, string? manufacturer, string? description, string? category, decimal length, decimal width, decimal height, decimal weight, string? sku, string? imageLocation, IDataContext dataContext)
        {
            ProductId = productId;
            Name = name;
            Price = price;
            Sale = sale;
            Rating = rating;
            Manufacturer = manufacturer;
            Description = description;
            Category = category;
            Length = length;
            Width = width;
            Height = height;
            Weight = weight;
            SKU = sku;
            ImageLocation = imageLocation;
            DataContext = dataContext;
        }

        public void AccessProduct(int productId)
        {
            using (ISqlHelper helper = DataContext.GetConnection("SELECT * FROM Product WHERE productId = @productId"))
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

        private void AccessProduct(DbDataReader reader)
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

        private void AccessProductDetails(DbDataReader reader)
        {
            Manufacturer = reader.GetString("manufacturer");
            Rating = reader.GetDecimal("rating");
            Description = reader.GetString("description");
            Category = reader.GetString("category");
            SKU = reader.GetString("sku");
            ImageLocation = reader.GetString("imageLocation");
        }

        private void AccessProductDimensions(DbDataReader reader)
        {
            Length = reader.GetDecimal("Length");
            Width = reader.GetDecimal("Width");
            Height = reader.GetDecimal("Height");
            Weight = reader.GetDecimal("Weight");
        }

        public void AccessProductSale()
        {
            using (ISqlHelper helper = DataContext.GetConnection("SELECT amount FROM Sale WHERE productId = @productId AND startDate <= @currentDate AND endDate >= @currentDate"))
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

        private void AccessProductSale(DbDataReader reader)
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

        public override bool Equals(object? obj)
        {
            if (obj is not null && obj is Product product)
            {
                bool equals = true;
                equals = equals && product.ProductId == ProductId;
                equals = equals && product.Name == Name;
                equals = equals && product.Price == Price;
                equals = equals && product.Sale == Sale;
                equals = equals && product.Rating == Rating;
                equals = equals && product.Manufacturer == Manufacturer;
                equals = equals && product.Description == Description;
                equals = equals && product.Category == Category;
                equals = equals && product.Length == Length;
                equals = equals && product.Width == Width;
                equals = equals && product.Height == Height;
                equals = equals && product.Weight == Weight;
                equals = equals && product.SKU == SKU;
                equals = equals && product.ImageLocation == ImageLocation;
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