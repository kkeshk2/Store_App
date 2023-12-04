using Microsoft.IdentityModel.Tokens;
using Store_App.Exceptions;
using System.Text.RegularExpressions;

namespace Store_App.Models.AddressModel
{
    public class AddressValidator : IAddressValidator
    {
        private static readonly IDictionary<string, string> RegexDictionary = GenerateRegexDictionary();

        private static IDictionary<string, string> GenerateRegexDictionary()
        {
            IDictionary<string, string> RegexDictionary = new Dictionary<string, string>
            {
                { "name", "^[A-Za-z][A-Za-z\\.\\-\\x20]{0,127}$" },
                { "line1", "^[0-9][A-Za-z0-9\\.\\-\\&\\x20]{0,127}$" },
                { "line2", "^$|^[A-Za-z0-9][A-Za-z0-9\\.\\-\\&\\x20]{0,127}$" },
                { "city", "^[A-Za-z0-9][A-Za-z0-9\\.\\-\\&\\x20]{0,127}$" },
                { "state", "^[A-Z]{2,2}$" },
                { "postal", "^[0-9]{5,5}$" }
            };
            return RegexDictionary;
        }

        public void ValidateAddress(string name, string line1, string? line2, string city, string state, string postal)
        {
            ValidateName(name);
            ValidateLine1(line1);
            ValidateLine2(line2);
            ValidateCity(city);
            ValidateState(state);
            ValidatePostal(postal);
        }

        private void ValidateName(string name)
        {
            if (name.IsNullOrEmpty())
            {
                throw new InvalidInputException("Address name is invalid.");
            }

            if (!Regex.IsMatch(name, RegexDictionary["name"]))
            {
                throw new InvalidInputException("Address name is invalid.");
            }
        }

        private void ValidateLine1(string line1)
        {
            if (line1.IsNullOrEmpty())
            {
                throw new InvalidInputException("Address line 1 is invalid.");
            }

            if (!Regex.IsMatch(line1, RegexDictionary["line1"]))
            {
                throw new InvalidInputException("Address line 1 is invalid.");
            }
        }

        private void ValidateLine2(string? line2)
        {
            if (line2 is null)
            {
                return;
            }

            if (line2 == string.Empty)
            {
                throw new InvalidInputException("Address line 2 is invalid.");
            }

            if (!Regex.IsMatch(line2, RegexDictionary["line2"]))
            {
                throw new InvalidInputException("Address line 2 is invalid.");
            }
        }

        private void ValidateCity(string city)
        {
            if (city.IsNullOrEmpty())
            {
                throw new InvalidInputException("Address city is invalid.");
            }

            if (!Regex.IsMatch(city, RegexDictionary["city"]))
            {
                throw new InvalidInputException("Address city is invalid.");
            }
        }

        private void ValidateState(string state)
        {
            if (state.IsNullOrEmpty())
            {
                throw new InvalidInputException("Address state is invalid.");
            }

            if (!Regex.IsMatch(state, RegexDictionary["state"]))
            {
                throw new InvalidInputException("Address state is invalid.");
            }
        }

        private void ValidatePostal(string postal)
        {
            if (postal.IsNullOrEmpty())
            {
                throw new InvalidInputException("Address postal code is invalid.");
            }

            if (!Regex.IsMatch(postal, RegexDictionary["postal"]))
            {
                throw new InvalidInputException("Address postal code is invalid.");
            }
        }
    }
}