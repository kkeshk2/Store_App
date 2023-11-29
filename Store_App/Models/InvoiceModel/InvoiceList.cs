using Newtonsoft.Json;
using Store_App.Helpers;
using Store_App.Models.ProductModel;

namespace Store_App.Models.InvoiceModel
{
    public class InvoiceList : IInvoiceList
    {
        [JsonProperty] private List<IInvoice> Invoices = new();

        public void AccessInvoiceList(int accountId)
        {
            using (var helper = new SqlHelper("SELECT * FROM Invoice WHERE accountId = @accountId"))
            {
                helper.AddParameter("@accountId", accountId);
                using (var reader = helper.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IInvoice invoice = new Invoice();
                        invoice.AccessInvoice(reader);
                        Invoices.Add(invoice);
                    }
                }
            }
            Invoices.ForEach(p => p.AccessInvoiceProducts());
        }
    }
}
