using Store_App.Models.Interfaces;
using System.Data.SqlClient;
using System.Data;


namespace Store_App.Models.Classes
{
    public class Cart : ICart
    {
        public int CartId { get; set; }
        public int AccountId { get; set; }

        // Define a list of CartProduct items
        public List<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

        // TODO: Create Product List

        // Error messages array
        public List<string> Errors { get; set; } = new List<string>();

        // Success boolean field
        public bool Success { get; set; } = true;

        public Cart GetOneBasedOnAccountId(int userAccountId)
        {
            Cart cart = new Cart();
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

                // Get the Cart that is related to the current Account and create a Cart instance
                SqlCommand command = new SqlCommand("SELECT * FROM Cart WHERE accountId = @userAccountId", connection);
                command.Parameters.AddWithValue("@userAccountId", userAccountId);

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
                            case "cartId":
                                cart.CartId = (int)cellValue;
                                break;
                            case "accountId":
                                cart.AccountId = (int)cellValue;
                                break;
                        }
                    }
                }
            }
            // Get all cart products for current cart and append them to CartProducts list
            CartProduct cartProd = new CartProduct();
            List<CartProduct> newCartProdList = cartProd.GetCartProductsBasedOnCart(cart.CartId);
            cart.CartProducts.AddRange(newCartProdList);

            // TODO: Fill in Product List using CartProducts list and passing in Product Id

            return cart; // Placeholder return
        }

        public void AddToCart(Cart cart, int productId, int quantity)
        {

            CartProduct cartProd = new CartProduct();
            cartProd.AddOneToCartProductDatabase(cart.CartId, productId, quantity);

            CartProduct newCartProd = new CartProduct();
            newCartProd = newCartProd.GetOne(cart.CartId, productId);
            cart.CartProducts.Add(newCartProd);
            
        }
    }
}

