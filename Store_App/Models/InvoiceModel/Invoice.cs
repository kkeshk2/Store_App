using Newtonsoft.Json;
using Store_App.Exceptions;
using Store_App.Helpers;
using Store_App.Models.AddressModel;
using Store_App.Models.CartModel;
using System.Data;
using System.Data.SqlClient;


namespace Store_App.Models.InvoiceModel
{
    public class Invoice : IInvoice
    {
        [JsonProperty] private int InvoiceId;
        [JsonIgnore] private int AccountId;
        [JsonProperty] private int Size;
        [JsonProperty] private decimal Total;
        [JsonProperty] private List<ICartProduct> Products;
        [JsonProperty] private DateTime Date;
        [JsonProperty] private string CreditCard;
        [JsonProperty] private IAddress BillingAddress;
        [JsonProperty] private IAddress ShippingAddress;
        [JsonProperty] private string TrackingNumber;

        public Invoice()
        {
            Products = new List<ICartProduct>();
            Date = DateTime.MinValue;
            CreditCard = string.Empty;
            BillingAddress = new AddressFactory().Create();
            ShippingAddress = new AddressFactory().Create();
            TrackingNumber = string.Empty;
        }

        public void AccessInvoice(int invoiceId,int accountId)
        {
            using (ISqlHelper helper = new SqlHelper("SELECT * FROM Invoice WHERE invoiceId = @invoiceId AND accountId = @accountID"))
            {
                helper.AddParameter("@invoiceId", invoiceId);
                helper.AddParameter("@accountID", accountId);
                AccessInvoice(helper);
            }
            AccessInvoiceProducts();
        }

        private void AccessInvoice(ISqlHelper helper)
        {
            using (var reader = helper.ExecuteReader())
            {          
                AccessInvoice(reader);
                reader.Close();
            }
        }

        private void AccessInvoice(SqlDataReader reader)
        {     
            if (!reader.Read())
            {
                throw new InvoiceNotFoundException("Invoice not found.");
            }

            InvoiceId = reader.GetInt32("invoiceId");
            AccountId = reader.GetInt32("accountId");
            Size = reader.GetInt32("size");
            Total = reader.GetDecimal("total");
            Date = reader.GetDateTime("date");
            CreditCard = reader.GetString("creditCard");
            int billingAddressId = (reader.GetInt32("billingAddressId"));
            int shippingAddressId = (reader.GetInt32("shippingAddressId"));
            TrackingNumber = reader.GetString("trackingNumber");
            AccessInvoiceAddresses(billingAddressId, shippingAddressId);
        }

        private void AccessInvoiceAddresses(int billingAddressId, int shippingAddressId)
        {
            IAddressFactory factory = new AddressFactory();
            factory.AccessAddress(billingAddressId);
            BillingAddress = factory.Create();
            factory = new AddressFactory();
            factory.AccessAddress(shippingAddressId);
            ShippingAddress = factory.Create();
        }

        private void AccessInvoiceProducts()
        {
            using (ISqlHelper helper = new SqlHelper("SELECT * FROM InvoiceProduct WHERE invoiceId = @invoiceId"))
            {
                helper.AddParameter("@invoiceId", InvoiceId);
                AccessInvoiceProducts(helper);
            }
        }

        private void AccessInvoiceProducts(ISqlHelper helper)
        {
            using (var reader = helper.ExecuteReader())
            {
                AccessInvoiceProducts(reader);
                reader.Close();
            }
        }

        private void AccessInvoiceProducts(SqlDataReader reader)
        {
            while (reader.Read())
            {
                var productId = reader.GetInt32("productId");
                var quantity = reader.GetInt32("quantity");
                var price = reader.GetDecimal("price");
                Products.Add(new CartProduct(productId, quantity, price));
            }
        }
    }
}