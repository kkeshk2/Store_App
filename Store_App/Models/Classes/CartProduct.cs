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


        /*
        // Navigation properties for relationships
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        */

        public List<CartProduct> GetCartProductsBasedOnCart(int cartId)
        {
            // TODO: Implement this
            List<CartProduct> cart_product_list = new List<CartProduct>();
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
    }
}
