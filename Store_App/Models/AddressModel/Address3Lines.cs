﻿using Newtonsoft.Json;
using Store_App.Helpers;
using System.Data;
using System.Data.SqlClient;


namespace Store_App.Models.AddressModel
{
    public class Address3Lines : IAddress
    {
        [JsonIgnore] private int AddressId;
        [JsonProperty] private string Name;
        [JsonProperty] private string Line1;
        [JsonProperty] private string City;
        [JsonProperty] private string State;
        [JsonProperty] private string Postal;
        [JsonIgnore] private IAddressValidator Validator = new AddressValidator();

        public Address3Lines(int addressId, string name, string line1, string city, string state, string postal)
        {
            AddressId = addressId;
            Name = name;
            Line1 = line1;
            City = city;
            State = state;
            Postal = postal;
        }

        private bool AddressExists()
        {
            using (ISqlHelper helper = new SqlHelper("SELECT addressId FROM Address WHERE name = @name AND line1 = @line1 AND line2 IS NULL AND city = @city AND state = @state AND postal = @postal"))
            {
                helper.AddParameter("@name", Name);
                helper.AddParameter("@line1", Line1);
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
            Validator.ValidateAddress(Name, Line1, null, City, State, Postal);
            if (AddressExists()) return;
            using (ISqlHelper helper = new SqlHelper("INSERT INTO Address (name, line1, city, state, postal) VALUES (@name, @line1, @city, @state, @postal) SELECT addressId = SCOPE_IDENTITY()"))
            {
                helper.AddParameter("@name", Name);
                helper.AddParameter("@line1", Line1);
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
    }
}
