namespace Store_App.Models.InvoiceModel
{
    public interface ICreditCardValidator
    {
        public string ValidateCreditCardReturnLast4(string creditCard);
        public void ValidateCreditCard(string creditCard, string expMonth, string expYear, string cardVC);
    }
}
