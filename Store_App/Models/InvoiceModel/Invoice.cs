using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Store_App.Helpers;
using Store_App.Models.CartModel;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Store_App.Models.InvoiceModel
{
    public class Invoice : IInvoice
    {
        [JsonProperty] private int InvoiceId;
        [JsonIgnore] private int InvoiceAccountId;
        [JsonProperty] private int InvoiceSize;
        [JsonProperty] private decimal InvoiceTotal;
        [JsonProperty] private List<ICartProduct> InvoiceProducts = new();
        [JsonProperty] private DateTime InvoiceDate;
        [JsonProperty] private string? InvoiceCreditCardLast4;
        [JsonProperty] private IAddress InvoiceBillingAddress = new Address();
        [JsonProperty] private IAddress InvoiceShippingAddress = new Address();
        [JsonProperty] private string? InvoiceTrackingNumber;

        public void AccessInvoice(int invoiceId)
        {
            using (var helper = new SqlHelper("SELECT * FROM Invoice WHERE invoiceId = @invoiceId"))
            {
                helper.AddParameter("@invoiceId", invoiceId);
                using (var reader = helper.ExecuteReader())
                {
                    reader.Read();
                    AccessInvoice(reader);
                    reader.Close();
                }
            }
            AccessInvoiceProducts();
        }

        public void AccessInvoice(SqlDataReader reader)
        {
            InvoiceId = reader.GetInt32("invoiceId");
            InvoiceAccountId = reader.GetInt32("accountId");
            InvoiceSize = reader.GetInt32("invoiceSize");
            InvoiceTotal = reader.GetDecimal("invoiceTotal");
            InvoiceDate = reader.GetDateTime("invoiceDate");
            InvoiceCreditCardLast4 = reader.GetString("invoiceCreditCardLast4");
            int invoiceBillingAddressId = reader.GetInt32("billingAddressId");
            int invoiceShippingAddressId = reader.GetInt32("shippingAddressId");
            InvoiceTrackingNumber = reader.GetString("invoiceTrackingNumber");

            InvoiceBillingAddress.AccessAddress(invoiceBillingAddressId);
            InvoiceShippingAddress.AccessAddress(invoiceShippingAddressId);
        }

        public void AccessInvoiceProducts()
        {
            using (var helper = new SqlHelper("SELECT * FROM InvoiceProduct WHERE invoiceId = @invoiceId"))
            {
                helper.AddParameter("@invoiceId", InvoiceId);
                using (var reader = helper.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var productId = reader.GetInt32("productId");
                        var productQuantity = reader.GetInt32("InvoiceProductQuantity");
                        var productPrice = reader.GetDecimal("InvoiceProductPrice");
                        InvoiceProducts.Add(new CartProduct(productId, productQuantity, productPrice));
                    }
                }
            }
        }

        public void AccessFirstInvoice(int accountId)
        {
            using (var helper = new SqlHelper("SELECT * FROM Invoice WHERE accountId = @accountId"))
            {
                helper.AddParameter("@accountId", accountId);
                using (var reader = helper.ExecuteReader())
                {
                    reader.Read();
                    AccessInvoice(reader);
                    reader.Close();
                }
            }
            AccessInvoiceProducts();
        }

        public void AccessLastInvoice(int accountId) {
            using (var helper = new SqlHelper("SELECT * FROM Invoice WHERE accountId = @accountId ORDER BY invoiceId DESC"))
            {
                helper.AddParameter("@accountId", accountId);
                using (var reader = helper.ExecuteReader())
                {
                    reader.Read();
                    AccessInvoice(reader);
                    reader.Close();
                }
            }
            AccessInvoiceProducts();
        }

        public void CreateInvoice(int accountId, string invoiceCreditCardLast4, IAddress billingAddress, IAddress shippingAddress)
        {
            ICart cart = new Cart();
            cart.AccessCart(accountId);

            List<ICartProduct> products = cart.GetProductList();
            if (products.Count == 0) return;

            string trackingNumber = GenerateTrackingNumber();

            CreateInvoice(accountId, invoiceCreditCardLast4, trackingNumber, billingAddress, shippingAddress, products);
            AccessLastInvoice(accountId);
            cart.ClearCart();
        }

        private void CreateInvoice(int accountId, string invoiceCreditCardLast4, string trackingNumber, IAddress billingAddress, IAddress shippingAddress, List<ICartProduct> products)
        {
            using (var helper = new SqlHelper("INSERT INTO Invoice (accountId, invoiceSize, invoiceTotal, invoiceDate, invoiceCreditCardLast4, billingAddressId, shippingAddressId, invoiceTrackingNumber) VALUES (@accountId, @invoiceSize, @invoiceTotal, @invoiceDate, @invoiceCreditCardLast4, @billingAddressId, @shippingAddressId, @invoiceTrackingNumber)"))
            {
                helper.AddParameter("@accountId", accountId);
                helper.AddParameter("@invoiceSize", products.Sum(p => p.GetQuantity()));
                helper.AddParameter("@invoiceTotal", products.Sum(p => p.GetQuantity() * p.GetUnitPrice()));
                helper.AddParameter("@invoiceDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                helper.AddParameter("@invoiceCreditCardLast4", invoiceCreditCardLast4);
                helper.AddParameter("@billingAddressId", billingAddress.GetAddressId());
                helper.AddParameter("@shippingAddressId", shippingAddress.GetAddressId());
                helper.AddParameter("@invoiceTrackingNumber", trackingNumber);
                helper.ExecuteNonQuery();
            }
            AccessLastInvoice(accountId);
            products.ForEach(CreateInvoiceProduct);
        }

        private void CreateInvoiceProduct(ICartProduct product)
        {
            using (var helper = new SqlHelper("INSERT INTO InvoiceProduct (invoiceId, productId, invoiceProductQuantity, invoiceProductPrice) VALUES (@invoiceId, @productId, @invoiceProductQuantity, @invoiceProductPrice)"))
            {
                helper.AddParameter("@invoiceId", InvoiceId);
                helper.AddParameter("@productId", product.GetProductId());
                helper.AddParameter("@invoiceProductQuantity", product.GetQuantity());
                helper.AddParameter("@invoiceProductPrice", product.GetUnitPrice());
                helper.ExecuteNonQuery();
            }
        }

        private static string GenerateTrackingNumber()
        {
            string characterSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] trackingNumber = new char[20];

            for (int i = 0; i < trackingNumber.Length; i++)
            {
                trackingNumber[i] = characterSet[RandomNumberGenerator.GetInt32(0, characterSet.Length)];
            }

            return new string(trackingNumber);
        }
    }
}
