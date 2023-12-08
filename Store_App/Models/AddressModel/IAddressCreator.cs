namespace Store_App.Models.AddressModel
{
    public interface IAddressCreator
    {
        public IAddress GetAddress(int id, string name, string line1, string city, string state, string postal);
        public IAddress GetAddress(int id, string name, string line1, string line2, string city, string state, string postal);      
    }
}
