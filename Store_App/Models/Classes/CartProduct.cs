using Store_App.Models.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Store_App.Models.Classes
{
    public class CartProduct : ICartProduct
    {
        public int CartProductId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }

        // Additional properties related to the relationship between the product and the cart
        public int Quantity { get; set; }
        // public decimal Price { get; set; }

        // Error messages array
        public List<string> Errors { get; set; } = new List<string>();

        // Success boolean field
        public bool Success { get; set; } = true;

        /*
        // Navigation properties for relationships
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        */

        public CartProduct GetOne(int cartId, int productId)
        {
            CartProduct retrievedCartProduct = new CartProduct();
            DataSet userDataset = new DataSet();

            string connectionString = ConnectionString.getConnectionString("KareemConnection");
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

                // Get the Cart that is related to the current Account and create a Cart instance
                SqlCommand command = new SqlCommand("SELECT * FROM CartProduct WHERE cartId = @cartId AND productId = @productId", connection);
                command.Parameters.AddWithValue("@cartId", cartId);
                command.Parameters.AddWithValue("@productId", productId);

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
                            case "cartProductId":
                                retrievedCartProduct.CartProductId = (int)cellValue;
                                break;
                            case "cartId":
                                retrievedCartProduct.CartId = (int)cellValue;
                                break;
                            case "productId":
                                retrievedCartProduct.ProductId = (int)cellValue;
                                break;
                            case "cartProductQuantity":
                                retrievedCartProduct.Quantity = (int)cellValue;
                                break;
                        }
                    }
                }
            }

            return retrievedCartProduct;
        }
        public List<CartProduct> GetCartProductsBasedOnCart(int cartId)
        {
            // TODO: Implement this
            List<CartProduct> cart_product_list = new List<CartProduct>();
            DataSet userDataset = new DataSet();

            string connectionString = ConnectionString.getConnectionString("KareemConnection");
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

                // Get the Cart that is related to the current Account and create a Cart instance
                SqlCommand command = new SqlCommand("SELECT * FROM CartProduct WHERE cartId = @cartId", connection);
                command.Parameters.AddWithValue("@cartId", cartId);

                SqlDataAdapter myDataAdapter = new SqlDataAdapter();
                myDataAdapter.SelectCommand = command;
                myDataAdapter.Fill(userDataset);
                connection.Close();
            }

            foreach (DataTable table in userDataset.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    CartProduct cartProduct = new CartProduct();
                    foreach (DataColumn column in table.Columns)
                    {
                        string columnName = column.ColumnName;
                        object cellValue = row[column];

                        switch (columnName)
                        {
                            case "cartProductId":
                                cartProduct.CartProductId = (int)cellValue;
                                break;
                            case "cartId":
                                cartProduct.CartId = (int)cellValue;
                                break;
                            case "productId":
                                cartProduct.ProductId = (int)cellValue;
                                break;
                            case "cartProductQuantity":
                                cartProduct.Quantity = (int)cellValue;
                                break;
                        }
                    }
                    cart_product_list.Add(cartProduct);
                }
            }

            return cart_product_list;
        }

        public void AddOneToCartProductDatabase(int cartId, int productId, int quantity)
        {
            SqlConnection connection;
            SqlCommand command;
            string connectionString = ConnectionString.getConnectionString("KareemConnection");
            using (connection = new SqlConnection(connectionString))
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
                string query = "INSERT INTO CartProduct (cartId, productId, cartProductQuantity) VALUES(@cartId, @productId, @cartProductQuantity)";

                command = new SqlCommand(query, connection);
                //Pass values to Parameters
                command.Parameters.AddWithValue("@cartId", cartId);
                command.Parameters.AddWithValue("@productId", productId);
                command.Parameters.AddWithValue("@cartProductQuantity", quantity);

                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Records Inserted Successfully");
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
