using Store_App.Helpers;
using Store_App.Models.CartModel;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Store_App.Models.InvoiceModel
{
    public interface IInvoice
    {
        public void AccessInvoice(int invoiceId);
        public void AccessInvoice(SqlDataReader reader);
        public void AccessInvoiceProducts();
        public void AccessFirstInvoice(int accountId);
        public void AccessLastInvoice(int accountId);
        public void CreateInvoice(int accountId, string invoiceCreditCardLast4, IAddress billingAddress, IAddress shippingAddress);           
    }
}