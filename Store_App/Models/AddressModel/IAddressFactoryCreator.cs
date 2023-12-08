using Store_App.Helpers;

namespace Store_App.Models.AddressModel
{
    public interface IAddressFactoryCreator
    {
        public IAddressFactory GetAddressFactory();
        public IAddressFactory GetAddressFactory(int addressId, string name, string line1, string? line2, string city, string state, string postal);
    }
}
