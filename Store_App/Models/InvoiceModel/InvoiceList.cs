using Newtonsoft.Json;
using Store_App.Helpers;
using System.Data;
using System.Data.SqlClient;

namespace Store_App.Models.InvoiceModel
{
    public class InvoiceList : IInvoiceList
    {
        [JsonProperty] private List<IInvoice> Invoices = new();

        public void AccessInvoiceList(int accountId)
        {
            using (ISqlHelper helper = new SqlHelper("SELECT invoiceId FROM Invoice WHERE accountId = @accountId"))
            {
                AccessInvoiceList(helper, accountId);
            }
        }

        private void AccessInvoiceList(ISqlHelper helper, int accountId)
        {
            helper.AddParameter("@accountId", accountId);
            using (var reader = helper.ExecuteReader())
            {
                AccessInvoiceList(reader, accountId);
                reader.Close();
            }
        }

        private void AccessInvoiceList(SqlDataReader reader, int accountId)
        {
            while (reader.Read())
            {
                IInvoice invoice = new Invoice();
                var invoiceId = reader.GetInt32("invoiceId");
                invoice.AccessInvoice(invoiceId, accountId);
                Invoices.Add(invoice);
            }
        }
    }
}
