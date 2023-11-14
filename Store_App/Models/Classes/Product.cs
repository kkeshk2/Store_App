using Store_App.Models.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;


namespace Store_App.Models.Classes
{
    public class Product : IProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductManufacturer { get; set; }
        public decimal ProductRating { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }
        public decimal ProductLength { get; set; }
        public decimal ProductWidth { get; set; }
        public decimal ProductHeight { get; set; }
        public decimal ProductWeight { get; set; }
        public string ProductSKU { get; set; }
        public string ProductImageLocation { get; set; }

        // Error messages array
        public List<string> Errors { get; set; } = new List<string>();
        
        // Success boolean field
        public bool Success { get; set; } = true;

        public List<Product> GetAll()
        {
            // List of Products from the Database
            List<Product> product_list = new List<Product>();
            DataSet userDataset = new DataSet();

            string connectionString = ConnectionString.getConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    Console.WriteLine("Opening Connection ...");
                    connection.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }

                SqlDataAdapter myDataAdapter = new SqlDataAdapter("SELECT * FROM Product", connection);
                myDataAdapter.Fill(userDataset);
                connection.Close();
            }

            foreach (DataTable table in userDataset.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    Product product = new Product();
                    foreach (DataColumn column in table.Columns)
                    {
                        string columnName = column.ColumnName;
                        object cellValue = row[column];

                        switch (columnName)
                        {
                            case "productId":
                                product.ProductId = (int)cellValue;
                                break;
                            case "productName":
                                product.ProductName = cellValue.ToString();
                                break;
                            case "productPrice":
                                product.ProductPrice = (decimal)cellValue;
                                break;
                            case "productManufacturer":
                                product.ProductManufacturer = cellValue.ToString();
                                break;
                            case "productRating":
                                product.ProductRating = (decimal)cellValue;
                                break;
                            case "productDescription":
                                product.ProductDescription = cellValue.ToString();
                                break;
                            case "productCategory":
                                product.ProductCategory = cellValue.ToString();
                                break;
                            case "productLength":
                                product.ProductLength = (decimal)cellValue;
                                break;
                            case "productWidth":
                                product.ProductWidth = (decimal)cellValue;
                                break;
                            case "productHeight":
                                product.ProductHeight = (decimal)cellValue;
                                break;
                            case "productWeight":
                                product.ProductWeight = (decimal)cellValue;
                                break;
                            case "productSKU":
                                product.ProductSKU = cellValue.ToString();
                                break;
                            case "productImageLocation":
                                product.ProductImageLocation = cellValue.ToString();
                                break;
                        }

                    }
                    product_list.Add(product);
                }
            }

            // Add logic to retrieve all products from your data source
            // For example, you can query a database or another data store
            // Create and return a collection of Product instances
            // If there are errors or the retrieval fails, set Errors and Success accordingly
            return product_list; // Placeholder return
        }
        
       public Product GetOne(int id)
       {
           Product product = new Product();
           DataSet userDataset = new DataSet();
           string connectionString = ConnectionString.getConnectionString();
           using (SqlConnection connection = new(connectionString))
           {
               try
               {
                   Console.WriteLine("Opening Connection ...");
                   connection.Open();
                    
               }
               catch (Exception e)
               {
                   Console.WriteLine("Error: " + e.Message);
               }
                   SqlCommand command = new SqlCommand("SELECT * FROM Product WHERE productId = @id", connection);
                   command.Parameters.AddWithValue("@id", id);

                   SqlDataAdapter myDataAdapter = new SqlDataAdapter();
                   myDataAdapter.SelectCommand = command;
                   myDataAdapter.Fill(userDataset);
                   connection.Close();
           }
           foreach (DataTable table in userDataset.Tables)
           {
               foreach (DataRow row in table.Rows)
               {
                   foreach (DataColumn column in table.Columns)
                   {
                       string columnName = column.ColumnName;
                       object cellValue = row[column];

                       switch (columnName)
                       {
                           case "productId":
                               product.ProductId = (int)cellValue;
                               break;
                           case "productName":
                               product.ProductName = cellValue.ToString();
                               break;
                           case "productPrice":
                               product.ProductPrice = (decimal)cellValue;
                               break;
                           case "productManufacturer":
                               product.ProductManufacturer = cellValue.ToString();
                               break;
                           case "productRating":
                               product.ProductRating = (decimal)cellValue;
                               break;
                           case "productDescription":
                               product.ProductDescription = cellValue.ToString();
                               break;
                           case "productCategory":
                               product.ProductCategory = cellValue.ToString();
                               break;
                           case "productLength":
                               product.ProductLength = (decimal)cellValue;
                               break;
                           case "productWidth":
                               product.ProductWidth = (decimal)cellValue;
                               break;
                           case "productHeight":
                               product.ProductHeight = (decimal)cellValue;
                               break;
                           case "productWeight":
                               product.ProductWeight = (decimal)cellValue;
                               break;
                           case "productSKU":
                               product.ProductSKU = cellValue.ToString();
                               break;
                           case "productImageLocation":
                               product.ProductImageLocation = cellValue.ToString();
                               break;
                        }
                    }
                }
            }
            return product;
        }

        public Product Save(Product model)
        {
            DataSet userDataset = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    Console.WriteLine("Opening Connection ...");
                    connection.Open();
                    string query = "INSERT INTO Product (productId, productName, productPrice, productManufacturer, " +
                                   "productRating, productDescription, productCategory, productLength, " +
                                   "productWidth, productHeight, productWeight, productSKU) " +
                                   "VALUES (@ProductId, @ProductName, @ProductPrice, @ProductManufacturer, " +
                                   "@ProductRating, @ProductDescription, @ProductCategory, " +
                                   "@ProductLength, @ProductWidth, @ProductHeight, @ProductWeight, @ProductSKU);";
                                   
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", model.ProductId);
                        command.Parameters.AddWithValue("@ProductName", model.ProductName);
                        command.Parameters.AddWithValue("@ProductPrice", model.ProductPrice);
                        command.Parameters.AddWithValue("@ProductManufacturer", model.ProductManufacturer);
                        command.Parameters.AddWithValue("@ProductRating", model.ProductRating);
                        command.Parameters.AddWithValue("@ProductDescription", model.ProductDescription);
                        command.Parameters.AddWithValue("@ProductCategory", model.ProductCategory);
                        command.Parameters.AddWithValue("@ProductLength", model.ProductLength);
                        command.Parameters.AddWithValue("@ProductWidth", model.ProductWidth);
                        command.Parameters.AddWithValue("@ProductHeight", model.ProductHeight);
                        command.Parameters.AddWithValue("@ProductWeight", model.ProductWeight);
                        command.Parameters.AddWithValue("@ProductSKU", model.ProductSKU);
                        command.ExecuteNonQuery();
                        Console.WriteLine("Inserted new product . . .");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error saving product . . .");
                }
                SqlDataAdapter myDataAdapter = new SqlDataAdapter();
                myDataAdapter.SelectCommand = command;
                myDataAdapter.Fill(userDataset);
                connection.Close();
            }
            return model;
        }

        public Product Update(int id, Product updatedProduct)
        {
            DataSet userDataset = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConnectionString.getConnectionString()))
            {
                try
                {
                    Console.WriteLine("Opening connection...");
                    connection.Open();

                    string query = "UPDATE Product SET " +
                                   "ProductName = @ProductName, " +
                                   "ProductPrice = @ProductPrice, " +
                                   "ProductManufacturer = @ProductManufacturer, " +
                                   "ProductRating = @ProductRating, " +
                                   "ProductDescription = @ProductDescription, " +
                                   "ProductCategory = @ProductCategory, " +
                                   "ProductLength = @ProductLength, " +
                                   "ProductWidth = @ProductWidth, " +
                                   "ProductHeight = @ProductHeight, " +
                                   "ProductWeight = @ProductWeight, " +
                                   "ProductSKU = @ProductSKU " +
                                   "WHERE ProductId = @ProductId;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", id);
                        command.Parameters.AddWithValue("@ProductName", updatedProduct.ProductName);
                        command.Parameters.AddWithValue("@ProductPrice", updatedProduct.ProductPrice);
                        command.Parameters.AddWithValue("@ProductManufacturer", updatedProduct.ProductManufacturer);
                        command.Parameters.AddWithValue("@ProductRating", updatedProduct.ProductRating);
                        command.Parameters.AddWithValue("@ProductDescription", updatedProduct.ProductDescription);
                        command.Parameters.AddWithValue("@ProductCategory", updatedProduct.ProductCategory);
                        command.Parameters.AddWithValue("@ProductLength", updatedProduct.ProductLength);
                        command.Parameters.AddWithValue("@ProductWidth", updatedProduct.ProductWidth);
                        command.Parameters.AddWithValue("@ProductHeight", updatedProduct.ProductHeight);
                        command.Parameters.AddWithValue("@ProductWeight", updatedProduct.ProductWeight);
                        command.Parameters.AddWithValue("@ProductSKU", updatedProduct.ProductSKU);

                        int updatedLines = command.ExecuteNonQuery();

                        if (updatedLines > 0)
                        {
                            Console.WriteLine($"Product {id} updated.");
                        }
                        else
                        {
                            Console.WriteLine($"Product {id} not found.");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error updating product: " + e.Message);
                }
                SqlDataAdapter myDataAdapter = new SqlDataAdapter();
                myDataAdapter.SelectCommand = command;
                myDataAdapter.Fill(userDataset);
                connection.Close();
            }
            return updatedProduct;
        }

        public override string ToString()
        {
            return $"Product ID: {ProductId}, " +
                   $"Product Name: {ProductName}, " +
                   $"Product Price: {ProductPrice:C}, " +
                   $"Product Manufacturer: {ProductManufacturer}, " +
                   $"Product Rating: {ProductRating}, " +
                   $"Product Description: {ProductDescription}, " +
                   $"Product Category: {ProductCategory}, " +
                   $"Product Length: {ProductLength} cm, " +
                   $"Product Width: {ProductWidth} cm, " +
                   $"Product Height: {ProductHeight} cm, " +
                   $"Product Weight: {ProductWeight} kg, " +
                   $"Product SKU: {ProductSKU}";
        }
    }
}
