using Newtonsoft.Json;
using Store_App.Helpers;
using System.Data;
using System.Data.SqlClient;

namespace Store_App.Models.AddressModel
{
    public class Address4Lines : IAddress
    {
        [JsonIgnore] private int AddressId;
        [JsonProperty] private string Name;
        [JsonProperty] private string Line1;
        [JsonProperty] private string Line2;
        [JsonProperty] private string City;
        [JsonProperty] private string State;
        [JsonProperty] private string Postal;
        [JsonIgnore] private IAddressValidator Validator = new AddressValidator();

        public Address4Lines(int addressId, string name, string line1, string line2, string city, string state, string postal)
        {
            AddressId = addressId;
            Name = name;
            Line1 = line1;
            Line2 = line2;
            City = city;
            State = state;
            Postal = postal;
        }

        private bool AddressExists()
        {
            using (ISqlHelper helper = new SqlHelper("SELECT addressId FROM Address WHERE name = @name AND line1 = @line1 AND line2 = @line2 AND city = @city AND state = @state AND postal = @postal"))
            {
                helper.AddParameter("@name", Name);
                helper.AddParameter("@line1", Line1);
                helper.AddParameter("@line2", Line2);
                helper.AddParameter("@city", City);
                helper.AddParameter("@state", State);
                helper.AddParameter("@postal", Postal);
                return AddressExists(helper);
            }
        }

        private bool AddressExists(ISqlHelper helper)
        {
            using (var reader = helper.ExecuteReader())
            {
                bool exists = AddressExists(reader);
                reader.Close();
                return exists;
            }
        }

        private bool AddressExists(SqlDataReader reader)
        {
            if (!reader.Read()) return false;
            AddressId = reader.GetInt32("addressId");
            return true;
        }

        public void AddAddress()
        {
            Validator.ValidateAddress(Name, Line1, Line2, City, State, Postal);
            if (AddressExists()) return;
            using (ISqlHelper helper = new SqlHelper("INSERT INTO Address (name, line1, line2, city, state, postal) VALUES (@name, @line1, @line2, @city, @state, @postal) SELECT addressId = SCOPE_IDENTITY()"))
            {
                helper.AddParameter("@name", Name);
                helper.AddParameter("@line1", Line1);
                helper.AddParameter("@line2", Line2);
                helper.AddParameter("@city", City);
                helper.AddParameter("@state", State);
                helper.AddParameter("@postal", Postal);
                AddressId = Convert.ToInt32(helper.ExecuteScalar());
            }
        }

        public int GetAddressId()
        {
            return AddressId;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not null && obj is Address4Lines address)
            {
                bool equals = true;
                equals = equals && address.AddressId == AddressId;
                equals = equals && address.Name == Name;
                equals = equals && address.Line1 == Line1;
                equals = equals && address.Line2 == Line2;
                equals = equals && address.City == City;
                equals = equals && address.State == State;
                equals = equals && address.Postal == Postal;
                return equals;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
