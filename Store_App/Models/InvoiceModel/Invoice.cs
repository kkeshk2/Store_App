using Newtonsoft.Json;
using Store_App.Exceptions;
using Store_App.Helpers;
using Store_App.Models.AddressModel;
using Store_App.Models.CartModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;


namespace Store_App.Models.InvoiceModel
{
    public class Invoice : IInvoice
    {
        [JsonIgnore] private IAddressFactoryCreator AddressFactoryCreator;
        [JsonIgnore] private ICartProductCreator CartProductCreator;
        [JsonIgnore] private IDataContext DataContext;
        
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

        public Invoice(IAddressFactoryCreator addressFactoryCreator, ICartProductCreator cartProductCreator, IDataContext dataContext)
        {
            AddressFactoryCreator = addressFactoryCreator;
            CartProductCreator = cartProductCreator;
            DataContext = dataContext;
            Products = new List<ICartProduct>();
            Date = DateTime.MinValue;
            CreditCard = string.Empty;
            BillingAddress = AddressFactoryCreator.GetAddressFactory().Create();
            ShippingAddress = AddressFactoryCreator.GetAddressFactory().Create();
            TrackingNumber = string.Empty;
        }

        public Invoice(int invoiceId, int accountId, int size, decimal total, List<ICartProduct> products, DateTime dateTime, string creditCard, IAddress billingAddress, IAddress shippingAddress, string trackingNumber, IAddressFactoryCreator addressFactoryCreator, ICartProductCreator cartProductCreator, IDataContext dataContext) 
        {
            InvoiceId = invoiceId;
            AccountId = accountId;
            Size = size;
            Total = total;
            Products = products;
            Date = dateTime;
            CreditCard = creditCard;
            BillingAddress = billingAddress;
            ShippingAddress = shippingAddress;
            TrackingNumber = trackingNumber;
            AddressFactoryCreator = addressFactoryCreator;
            CartProductCreator = cartProductCreator;
            DataContext = dataContext;
        }

        public void AccessInvoice(int invoiceId,int accountId)
        {
            using (ISqlHelper helper = DataContext.GetConnection("SELECT * FROM Invoice WHERE invoiceId = @invoiceId AND accountId = @accountID"))
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

        private void AccessInvoice(DbDataReader reader)
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
            IAddressFactory factory = AddressFactoryCreator.GetAddressFactory();
            factory.AccessAddress(billingAddressId);
            BillingAddress = factory.Create();
            factory = AddressFactoryCreator.GetAddressFactory();
            factory.AccessAddress(shippingAddressId);
            ShippingAddress = factory.Create();
        }

        private void AccessInvoiceProducts()
        {
            using (ISqlHelper helper = DataContext.GetConnection("SELECT * FROM InvoiceProduct WHERE invoiceId = @invoiceId"))
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

        private void AccessInvoiceProducts(DbDataReader reader)
        {
            while (reader.Read())
            {
                var productId = reader.GetInt32("productId");
                var quantity = reader.GetInt32("quantity");
                var price = reader.GetDecimal("price");
                Products.Add(CartProductCreator.GetCartProduct(productId, quantity, price));
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is not null && obj is Invoice creator)
            {
                bool equals = true;
                equals = equals && creator.InvoiceId == InvoiceId;
                equals = equals && creator.AccountId == AccountId;
                equals = equals && creator.Size == Size;
                equals = equals && creator.Total == Total;
                equals = equals && creator.Products.SequenceEqual(Products);
                equals = equals && creator.Date.Equals(Date);
                equals = equals && creator.CreditCard == CreditCard;
                equals = equals && creator.BillingAddress.Equals(BillingAddress);
                equals = equals && creator.ShippingAddress.Equals(ShippingAddress);
                equals = equals && creator.TrackingNumber == TrackingNumber;
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