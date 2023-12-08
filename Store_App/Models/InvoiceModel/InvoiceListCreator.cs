using Store_App.Helpers;

namespace Store_App.Models.InvoiceModel
{
    public class InvoiceListCreator : IInvoiceListCreator
    {
        public IInvoiceList GetInvoiceList()
        {
            return new InvoiceList(new DataContext(), new InvoiceCreator());
        }
    }
}
