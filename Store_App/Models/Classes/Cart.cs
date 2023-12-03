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

        // List of Products in each Cart
        public List<Product> Products { get; set; } = new List<Product>();

        // Error messages array
        public List<string> Errors { get; set; } = new List<string>();

        // Success boolean field
        public bool Success { get; set; } = true;

        public static Cart GetOneBasedOnAccountId(int? userAccountId)
        {
            if (userAccountId <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
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
            List<CartProduct> newCartProdList = CartProduct.GetCartProductsBasedOnCart(cart.CartId);
            cart.CartProducts.AddRange(newCartProdList);

            for (int i = 0; i < newCartProdList.Count(); i++)
            {
                cart.Products.Add(Product.GetOne(newCartProdList[i].ProductId));
            }

            return cart; // Placeholder return
        }

        public static void AddToCart(Cart? cart, int productId, int quantity)
        {
            if (cart == null)
            {
                throw new ArgumentNullException();
            }

            if (productId <= 0 || quantity <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            CartProduct.AddOneToCartProductDatabase(cart.CartId, productId, quantity);
        }

        public static void DeleteFromCart(int cartId, int productId)
        {
            if (cartId <= 0 || productId <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            CartProduct cartProd = CartProduct.GetOne(cartId, productId);
            CartProduct.DeleteFromCartProductDatabase(cartProd.CartProductId);
        }

        public static double GetTotalPrice(int? accountId)
        {
            Cart currentCart = GetOneBasedOnAccountId(accountId);
            CartProduct currentCartProduct = new CartProduct();

            double totalPrice = 0;
            for (int i = 0; i < currentCart.Products.Count(); i++)
            {
                currentCartProduct = CartProduct.GetOne(currentCart.CartId, currentCart.Products[i].ProductId);
                totalPrice += ((double)currentCart.Products[i].ProductPrice) * (currentCartProduct.Quantity);
            }

            return totalPrice;
        }

        public static void DeleteAllFromCart(int? accountId)
        {
            if (accountId <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            Cart currentCart = GetOneBasedOnAccountId(accountId);
            CartProduct.DeleteCartProductsForOneCart(currentCart.CartId);

        }
    }
}

