using Newtonsoft.Json;
using Store_App.Helpers;
using Store_App.Models.AddressModel;
using Store_App.Models.CartModel;
using System.Security.Cryptography;

namespace Store_App.Models.InvoiceModel
{
    public class InvoiceProcessor : IInvoiceProcessor
    {
        [JsonIgnore] private IAddressFactoryCreator AddressFactoryCreator;
        [JsonIgnore] private ICartCreator CartCreator;
        [JsonIgnore] private ICreditCardValidator Validator;
        [JsonIgnore] private IDataContext DataContext;
        [JsonIgnore] private IInvoiceCreator InvoiceCreator;
        
        [JsonProperty] private int InvoiceId;
        [JsonIgnore] private int AccountId;
        [JsonProperty] private int Size;
        [JsonProperty] private decimal Total;
        [JsonProperty] private List<ICartProduct> Products;
        [JsonProperty] private string CreditCard;
        [JsonProperty] private IAddress BillingAddress;
        [JsonProperty] private IAddress ShippingAddress;

        public InvoiceProcessor(IAddressFactoryCreator addressFactoryCreator, ICartCreator cartCreator, ICreditCardValidator validator, IDataContext dataContext, IInvoiceCreator invoiceCreator)
        {
            AddressFactoryCreator = addressFactoryCreator;
            CartCreator = cartCreator;
            Validator = validator;
            DataContext = dataContext;
            InvoiceCreator = invoiceCreator;
            Products = new List<ICartProduct>();
            CreditCard = string.Empty;
            BillingAddress = AddressFactoryCreator.GetAddressFactory().Create();
            ShippingAddress = AddressFactoryCreator.GetAddressFactory().Create();
        }

        public InvoiceProcessor(int invoiceId, int accountId, int size, decimal total, List<ICartProduct> products, string creditCard, IAddress billingAddress, IAddress shippingAddress, IAddressFactoryCreator addressFactoryCreator, ICartCreator cartCreator, ICreditCardValidator validator, IDataContext dataContext, IInvoiceCreator invoiceCreator)
        {
            InvoiceId = invoiceId;
            AccountId = accountId;
            Size = size;
            Total = total;
            Products = products;
            CreditCard = creditCard;
            BillingAddress = billingAddress;
            ShippingAddress = shippingAddress;
            AddressFactoryCreator = addressFactoryCreator;
            CartCreator = cartCreator;
            Validator = validator;
            DataContext = dataContext;
            InvoiceCreator = invoiceCreator;
        }

        private void CalculateInvoiceTotals()
        {
            Size = 0;
            Total = 0;

            foreach (ICartProduct product in Products)
            {
                Size += product.GetQuantity();
                Total += product.GetQuantity() * product.GetPrice();
            }
        }

        public IInvoice CreateInvoice(int accountId, string creditCardString, string billingAddressString, string shippingAddressString)
        {
            IAddress billingAddress = ProcessAddress(billingAddressString);
            IAddress shippingAddress = ProcessAddress(shippingAddressString);
            string creditCard = ProcessCreditCart(creditCardString);
            CreateInvoice(accountId, creditCard, billingAddress, shippingAddress);
            IInvoice invoice = InvoiceCreator.GetInvoice();
            invoice.AccessInvoice(InvoiceId, AccountId);
            return invoice;
        }

        private void CreateInvoice(int accountId, string invoiceCreditCardLast4, IAddress billingAddress, IAddress shippingAddress)
        {
            AccountId = accountId;
            GetItemsFromCart(accountId);
            CalculateInvoiceTotals();
            CreditCard = invoiceCreditCardLast4;
            BillingAddress = billingAddress;
            ShippingAddress = shippingAddress;
            CreateInvoice();
        }

        private void CreateInvoice()
        {
            using (ISqlHelper helper = DataContext.GetConnection("INSERT INTO Invoice (accountId, size, total, date, creditCard, billingAddressId, shippingAddressId, trackingNumber) VALUES (@accountId, @size, @total, @date, @creditCard, @billingAddressId, @shippingAddressId, @trackingNumber) SELECT invoiceId = SCOPE_IDENTITY()"))
            {
                helper.AddParameter("@accountId", AccountId);
                helper.AddParameter("@size", Size);
                helper.AddParameter("@total", Total);
                helper.AddParameter("@date", GetDate());
                helper.AddParameter("@creditCard", CreditCard);
                helper.AddParameter("@billingAddressId", BillingAddress.GetAddressId());
                helper.AddParameter("@shippingAddressId", ShippingAddress.GetAddressId());
                helper.AddParameter("@trackingNumber", GenerateTrackingNumber());
                InvoiceId = Convert.ToInt32(helper.ExecuteScalar());
            }
            Products.ForEach(CreateInvoice);
        }

        private void CreateInvoice(ICartProduct product)
        {
            using (ISqlHelper helper = DataContext.GetConnection("INSERT INTO InvoiceProduct (invoiceId, productId, quantity, price) VALUES (@invoiceId, @productId, @quantity, @price)"))
            {
                helper.AddParameter("@invoiceId", InvoiceId);
                helper.AddParameter("@productId", product.GetProductId());
                helper.AddParameter("@quantity", product.GetQuantity());
                helper.AddParameter("@price", product.GetPrice());
                helper.ExecuteNonQuery();
            }
        }

        private void GetItemsFromCart(int accountId)
        {
            ICart cart = CartCreator.GetCart();
            cart.AccessCart(accountId);
            Products.AddRange(cart.GetCartProducts());

            cart.ClearCart();
            CalculateInvoiceTotals();
        }

        private static string GenerateTrackingNumber()
        {
            string characterSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] trackingNumber = new char[20];

            foreach (int value in Enumerable.Range(0, 20))
            {
                var random = RandomNumberGenerator.GetInt32(0, characterSet.Length);
                trackingNumber[value] = characterSet[random];
            }

            return new string(trackingNumber);
        }

        private string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private IAddress ProcessAddress(string addressString)
        {
            IAddressFactory addressFactory = AddressFactoryCreator.GetAddressFactory();
            addressFactory.SetAddress(addressString);
            IAddress address = addressFactory.Create();
            address.AddAddress();
            return address;
        }

        private string ProcessCreditCart(string creditCardString)
        {
            string creditCard = Validator.ValidateCreditCardReturnLast4(creditCardString);
            return creditCard;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not null && obj is InvoiceProcessor creator)
            {
                bool equals = true;
                equals = equals && creator.InvoiceId == InvoiceId;
                equals = equals && creator.AccountId == AccountId;
                equals = equals && creator.Size == Size;
                equals = equals && creator.Total == Total;
                equals = equals && creator.Products.SequenceEqual(Products);
                equals = equals && creator.CreditCard == CreditCard;
                equals = equals && creator.BillingAddress.Equals(BillingAddress);
                equals = equals && creator.ShippingAddress.Equals(ShippingAddress);
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