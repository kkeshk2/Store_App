using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Store_App.Helpers;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Store_App.Models.InvoiceModel
{
    public class InvoiceList : IInvoiceList
    {
        [JsonIgnore] private IDataContext DataContext;
        [JsonIgnore] private IInvoiceCreator InvoiceCreator;
        [JsonProperty] private List<IInvoice> Invoices = new();

        public InvoiceList(IDataContext dataContext, IInvoiceCreator invoiceCreator)
        {
            DataContext = dataContext;
            InvoiceCreator = invoiceCreator;
        }

        public void AccessInvoiceList(int accountId)
        {
            using (ISqlHelper helper = DataContext.GetConnection("SELECT invoiceId FROM Invoice WHERE accountId = @accountId"))
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

        private void AccessInvoiceList(DbDataReader reader, int accountId)
        {
            while (reader.Read())
            {
                IInvoice invoice = InvoiceCreator.GetInvoice();
                var invoiceId = reader.GetInt32("invoiceId");
                invoice.AccessInvoice(invoiceId, accountId);
                Invoices.Add(invoice);
            }
        }
    }
}
