using Newtonsoft.Json;
using Store_App.Helpers;
using Store_App.Models.AddressModel;
using Store_App.Models.CartModel;
using System.Security.Cryptography;

namespace Store_App.Models.InvoiceModel
{
    public class InvoiceCreator : IInvoiceCreator
    {
        [JsonProperty] private int InvoiceId;
        [JsonIgnore] private int AccountId;
        [JsonProperty] private int Size;
        [JsonProperty] private decimal Total;
        [JsonProperty] private List<ICartProduct> Products;
        [JsonProperty] private string CreditCard;
        [JsonProperty] private IAddress BillingAddress;
        [JsonProperty] private IAddress ShippingAddress;

        public InvoiceCreator()
        {
            Products = new List<ICartProduct>();
            CreditCard = string.Empty;
            BillingAddress = new AddressFactory().Create();
            ShippingAddress = new AddressFactory().Create();
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
            IInvoice invoice = new Invoice();
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
            using (ISqlHelper helper = new SqlHelper("INSERT INTO Invoice (accountId, size, total, date, creditCard, billingAddressId, shippingAddressId, trackingNumber) VALUES (@accountId, @size, @total, @date, @creditCard, @billingAddressId, @shippingAddressId, @trackingNumber) SELECT invoiceId = SCOPE_IDENTITY()"))
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
            using (ISqlHelper helper = new SqlHelper("INSERT INTO InvoiceProduct (invoiceId, productId, quantity, price) VALUES (@invoiceId, @productId, @quantity, @price)"))
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
            ICart cart = new Cart();
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
            IAddressFactory addressFactory = new AddressFactory();
            addressFactory.SetAddress(addressString);
            IAddress address = addressFactory.Create();
            address.AddAddress();
            return address;
        }

        private string ProcessCreditCart(string creditCardString)
        {
            ICreditCardValidator validator = new CreditCardValidator();
            string creditCard = validator.ValidateCreditCardReturnLast4(creditCardString);
            return creditCard;
        }
    }
}