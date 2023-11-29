using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Store_App.Helpers;
using System.Data;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Store_App.Models.InvoiceModel
{
    public class Address : IAddress
    {
        [JsonIgnore] private int AddressId;
        [JsonProperty] private string? Name;
        [JsonProperty] private string? Line1;
        [JsonProperty] private string? Line2;
        [JsonProperty] private string? City;
        [JsonProperty] private string? State;
        [JsonProperty] private string? PostalCode;
        [JsonIgnore] private IAddressValidator _validator = new AddressValidator();

        public void AccessAddress(int addressId)
        {
            using (var helper = new SqlHelper("SELECT * FROM Address WHERE addressId = @addressId"))
            {
                helper.AddParameter("@addressId", addressId);
                using (var reader = helper.ExecuteReader())
                {
                    AccessAddress(reader);
                }
            }
        }

        public void AccessAddress(string name, string line1, string line2, string city, string state, string postalCode)
        {
            using (var helper = new SqlHelper("SELECT * FROM Address WHERE addressName = @addressName AND addressLine1 = @addressLine1 AND addressLine2 = @addressLine2 AND addressCity = @addressCity AND addressState = @addressState AND addressZip = @addressZip"))
            {
                helper.AddParameter("@addressName", name);
                helper.AddParameter("@addressLine1", line1);
                helper.AddParameter("@addressLine2", line2);
                helper.AddParameter("@addressCity", city);
                helper.AddParameter("@addressState", state);
                helper.AddParameter("@addressZip", postalCode);
                using (var reader = helper.ExecuteReader())
                {
                    AccessAddress(reader);
                }
            }
        }

        public void AccessAddress(string name, string line1, string city, string state, string postalCode)
        {
            using (var helper = new SqlHelper("SELECT * FROM Address WHERE addressName = @addressName AND addressLine1 = @addressLine1 AND addressLine2 IS NULL AND addressCity = @addressCity AND addressState = @addressState AND addressZip = @addressZip"))
            {
                helper.AddParameter("@addressName", name);
                helper.AddParameter("@addressLine1", line1);
                helper.AddParameter("@addressCity", city);
                helper.AddParameter("@addressState", state);
                helper.AddParameter("@addressZip", postalCode);
                using (var reader = helper.ExecuteReader())
                {
                    AccessAddress(reader);
                }
            }
        }

        private void AccessAddress(SqlDataReader reader)
        {
            reader.Read();
            AddressId = reader.GetInt32("addressId");
            Name = reader.GetString("addressName");
            Line1 = reader.GetString("addressLine1");
            Line2 = reader.IsDBNull("addressLine2") ? null : reader.GetString("addressLine2");
            City = reader.GetString("addressCity");
            State = reader.GetString("addressState");
            PostalCode = reader.GetString("addressZip");
        }

        public static bool AddressExists(string name, string line1, string line2, string city, string state, string postalCode)
        {
            using (var helper = new SqlHelper("SELECT * FROM Address WHERE addressName = @addressName AND addressLine1 = @addressLine1 AND addressLine2 = @addressLine2 AND addressCity = @addressCity AND addressState = @addressState AND addressZip = @addressZip"))
            {
                helper.AddParameter("@addressName", name);
                helper.AddParameter("@addressLine1", line1);
                helper.AddParameter("@addressLine2", line2);
                helper.AddParameter("@addressCity", city);
                helper.AddParameter("@addressState", state);
                helper.AddParameter("@addressZip", postalCode);
                using (var reader = helper.ExecuteReader())
                {
                    var result = reader.Read();
                    reader.Close();
                    return result;
                }
            }
        }

        public static bool AddressExists(string name, string line1, string city, string state, string postalCode)
        {
            using (var helper = new SqlHelper("SELECT * FROM Address WHERE addressName = @addressName AND addressLine1 = @addressLine1 AND addressLine2 IS NULL AND addressCity = @addressCity AND addressState = @addressState AND addressZip = @addressZip"))
            {
                helper.AddParameter("@addressName", name);
                helper.AddParameter("@addressLine1", line1);
                helper.AddParameter("@addressCity", city);
                helper.AddParameter("@addressState", state);
                helper.AddParameter("@addressZip", postalCode);
                using (var reader = helper.ExecuteReader())
                {
                    var result = reader.Read();
                    reader.Close();
                    return result;
                }
            }
        }

        public void AddAddress(string addressString)
        {
            List<string> address = addressString.Split((char) 31).ToList();
            address.Remove("undefined");
            if (address.Count == 5)
            {
                AddAddress(address[0], address[1], address[2], address[3], address[4]);
                AccessAddress(address[0], address[1], address[2], address[3], address[4]);
            }
            else if (address.Count == 6)
            {
                AddAddress(address[0], address[1], address[2], address[3], address[4], address[5]);
                AccessAddress(address[0], address[1], address[2], address[3], address[4], address[5]);
            }
        }

        public void AddAddress(string name, string line1, string line2, string city, string state, string postalCode)
        {
            if (!_validator.ValidateAddress(name, line1, line2, city, state, postalCode)) throw new ArgumentException();
            if (AddressExists(name, line1, line2, city, state, postalCode)) return;
            using (var helper = new SqlHelper("INSERT INTO Address (addressName, addressLine1, addressLine2, addressCity, addressState, addressZip) VALUES (@addressName, @addressLine1, @addressLine2, @addressCity, @addressState, @addressZip)"))
            {
                helper.AddParameter("@addressName", name);
                helper.AddParameter("@addressLine1", line1);
                helper.AddParameter("@addressLine2", line2);
                helper.AddParameter("@addressCity", city);
                helper.AddParameter("@addressState", state);
                helper.AddParameter("@addressZip", postalCode);
                helper.ExecuteNonQuery();
            }
        }

        public void AddAddress(string name, string line1, string city, string state, string postalCode)
        {
            if (!_validator.ValidateAddress(name, line1, city, state, postalCode)) throw new ArgumentException();
            if (AddressExists(name, line1, city, state, postalCode)) return;
            using (var helper = new SqlHelper("INSERT INTO Address (addressName, addressLine1, addressCity, addressState, addressZip) VALUES (@addressName, @addressLine1, @addressCity, @addressState, @addressZip)"))
            {
                helper.AddParameter("@addressName", name);
                helper.AddParameter("@addressLine1", line1);
                helper.AddParameter("@addressCity", city);
                helper.AddParameter("@addressState", state);
                helper.AddParameter("@addressZip", postalCode);
                helper.ExecuteNonQuery();
            }
        }

        public int GetAddressId()
        {
            return AddressId;
        }
    }
}
