namespace Store_App.Models.InvoiceModel
{
    public interface ICreditCardValidator
    {
        public string GetLast4(string creditCard);
        public bool ValidateCreditCardString(string creditCard);
        public bool ValidateCreditCard(string creditCard, string expMonth, string expYear, string cardVC);
    }
}
