using Store_App.Helpers;

namespace Store_App.Models.AddressModel
{
    public class AddressFactoryCreator : IAddressFactoryCreator
    {
        public IAddressFactory GetAddressFactory()
        {
            return new AddressFactory(new DataContext(), new AddressCreator());
        }

        public IAddressFactory GetAddressFactory(int id, string name, string line1, string? line2, string city, string state, string postal)
        {
            return new AddressFactory(id, name, line1, line2, city, state, postal, new DataContext(), new AddressCreator());
        } 
    }
}
