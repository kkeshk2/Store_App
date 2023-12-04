namespace Store_App.Models.AddressModel
{
    public interface IAddressValidator
    {
        public void ValidateAddress(string name, string line1, string? line2, string city, string state, string postalCode);
    }
}
