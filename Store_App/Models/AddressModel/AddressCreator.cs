using Store_App.Helpers;

namespace Store_App.Models.AddressModel
{
    public class AddressCreator : IAddressCreator
    {
        public IAddress GetAddress(int id, string name, string line1, string city, string state, string postal)
        {
            return new Address3Lines(id, name, line1, city, state, postal, new DataContext(), new AddressValidator());
        }

        public IAddress GetAddress(int id, string name, string line1, string line2, string city, string state, string postal)
        {
            return new Address4Lines(id, name, line1, line2, city, state, postal, new DataContext(), new AddressValidator());
        }
    }
}
