﻿using Store_App.Exceptions;
using Store_App.Helpers;
using System.Data;
using System.Data.SqlClient;

namespace Store_App.Models.AddressModel
{
    public class AddressFactory : IAddressFactory
    {
        private int AddressId;
        private string Name;
        private string Line1;
        private string? Line2;
        private string City;
        private string State;
        private string Postal;

        public AddressFactory()
        {
            Name = string.Empty;
            Line1 = string.Empty;
            City = string.Empty;
            State = string.Empty;
            Postal = string.Empty;
        }

        public void AccessAddress(int addressId)
        {
            using (ISqlHelper helper = new SqlHelper("SELECT * FROM Address WHERE addressId = @addressId"))
            {
                helper.AddParameter("@addressId", addressId);
                AccessAddress(helper);
            }
        }

        private void AccessAddress(ISqlHelper helper)
        {
            using (var reader = helper.ExecuteReader())
            {
                AccessAddress(reader);
                reader.Close();
            }
        }

        private void AccessAddress(SqlDataReader reader)
        {
            if (!reader.Read())
            {
                throw new AddressNotFoundException("The requested address was not found");
            }

            AddressId = reader.GetInt32("addressId");
            Name = reader.GetString("name");
            Line1 = reader.GetString("line1");
            Line2 = reader.IsDBNull("line2") ? null : reader.GetString("line2");
            City = reader.GetString("city");
            State = reader.GetString("state");
            Postal = reader.GetString("postal");
        }

        public IAddress Create()
        {
            if (Line2 == null)
            {
                return new Address3Lines(AddressId, Name, Line1, City, State, Postal);
            }

            return new Address4Lines(AddressId, Name, Line1, Line2, City, State, Postal);
        }

        public void SetAddress(string addressString)
        {
            List<string> address = addressString.Split("\t", 6).ToList();
            Name = address[0];
            Line1 = address[1];
            Line2 = address[2] == "undefined" || address[2] == "" ? null : address[2];
            City = address[3];
            State = address[4];
            Postal = address[5];
        }
    }
}