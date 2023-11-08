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

        // Error messages array
        public List<string> Errors { get; set; } = new List<string>();
        
        // Success boolean field
        public bool Success { get; set; } = true;

        public List<Product> GetAll()
        {
            // List of Products from the Database
            List<Product> product_list = new List<Product>();
            DataSet userDataset = new DataSet();

            string connectionString = "Data Source=DESKTOP-PUP0614\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;";
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
                            case "ProductId":
                                product.ProductId = (int)cellValue;
                                break;
                            case "ProductName":
                                product.ProductName = cellValue.ToString();
                                break;
                            case "ProductPrice":
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
                                // Handle other fields similarly
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
            Product product = new();
            product.ProductId = id;
            Success = true;
        
            string query = $"SELECT * FROM Products WHERE ProductId = {id}";
            string connectionString = "Data Source=TONY-DEV\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;";
            using (SqlConnection connection = new(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    try
                    {
                        Console.WriteLine("Opening Connection ...");
                        connection.Open();
                        Object obj = cmd.ExecuteScalar();
                        product = (Product) obj;
                    }
                    catch (Exception e)
                    {
                        Success = false;
                        Errors.Add("Error retrieving product: " + e.Message);
                    }
                    connection.Close();
                }
            }
            return product;
        }

        public Product Save(Product model)
        {
            // Add logic to save a new product
            // If save is successful, return true and set Success to true
            // If there are errors or the save fails, set Errors and Success accordingly
            return new Product();// Placeholder return
        }

        public Product Update(Product model)
        {
            // Add logic to update an existing product
            // If update is successful, return true and set Success to true
            // If there are errors or the update fails, set Errors and Success accordingly
            return new Product(); ; // Placeholder return
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
