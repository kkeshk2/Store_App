namespace Store_App.Models.AddressModel
{
    public interface IAddressFactory
    {
        public void AccessAddress(int addressId);
        public IAddress Create();
        public void SetAddress(string addressString);
    }
}
