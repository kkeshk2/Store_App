namespace Store_App.Models.InvoiceModel
{
    public interface IAddressValidator
    {
        public bool ValidateAddress(string name, string line1, string line2, string city, string state, string postalCode);
        public bool ValidateAddress(string name, string line1, string city, string state, string postalCode);
    }
}
