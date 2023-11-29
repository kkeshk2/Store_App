using Store_App.Helpers;

namespace Store_App.Models.InvoiceModel
{
    public interface IAddress
    {
        public void AccessAddress(int addressId);
        public void AccessAddress(string name, string line1, string line2, string city, string state, string postalCode);
        public void AccessAddress(string name, string line1, string city, string state, string postalCode);
        public void AddAddress(string addressString);
        public void AddAddress(string name, string line1, string line2, string city, string state, string postalCode);
        public void AddAddress(string name, string line1, string city, string state, string postalCode);
        public int GetAddressId();
    }
}
