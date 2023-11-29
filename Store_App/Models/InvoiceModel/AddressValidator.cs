using System.Text.RegularExpressions;

namespace Store_App.Models.InvoiceModel
{
    public class AddressValidator : IAddressValidator
    {
        public bool ValidateAddress(string name, string line1, string line2, string city, string state, string postalCode)
        {
            bool valid = true;
            valid = valid && ValidateName(name);
            valid = valid && ValidateLine1(line1);
            valid = valid && ValidateLine2(line2);
            valid = valid && ValidateCity(city);
            valid = valid && ValidateState(state);
            valid = valid && ValidatePostalCode(postalCode);
            return valid;
        }

        public bool ValidateAddress(string name, string line1, string city, string state, string postalCode)
        {
            bool valid = true;
            valid = valid && ValidateName(name);
            valid = valid && ValidateLine1(line1);
            valid = valid && ValidateCity(city);
            valid = valid && ValidateState(state);
            valid = valid && ValidatePostalCode(postalCode);
            return valid;
        }

        private bool ValidateName(string name)
        {
            bool valid = true;
            if (!string.IsNullOrEmpty(name))
            {
                valid = Regex.Match(name, "^[A-Za-z][A-Za-z\\.\\-\\x20]{0,127}$").Success;
            }
            return valid;
        }

        private bool ValidateLine1(string line1)
        {
            bool valid = true;
            if (!string.IsNullOrEmpty(line1))
            {
                valid = Regex.Match(line1, "^[A-Za-z0-9][A-Za-z0-9\\.\\-\\&\\x20]{0,127}$").Success;
            }
            return valid;
        }

        private bool ValidateLine2(string line2)
        {
            bool valid = true;
            if (!string.IsNullOrEmpty(line2))
            {
                valid = Regex.Match(line2, "^[A-Za-z0-9]?[A-Za-z0-9\\.\\-\\&\\x20]{0,127}$").Success;
            }
            return valid;
        }

        private bool ValidateCity(string city)
        {
            bool valid = true;
            if (!string.IsNullOrEmpty(city))
            {
                valid = Regex.Match(city, "^[A-Za-z0-9][A-Za-z0-9\\.\\-\\&\\x20]{0,127}$").Success;
            }
            return valid;
        }

        private bool ValidateState(string state)
        {
            bool valid = true;
            if (!string.IsNullOrEmpty(state))
            {
                valid = Regex.Match(state, "^[A-Z]{2,2}$").Success;
            }
            return valid;
        }

        private bool ValidatePostalCode(string postalCode)
        {
            bool valid = true;
            if (!string.IsNullOrEmpty(postalCode))
            {
                valid = Regex.Match(postalCode, "^[0-9]{5,5}$").Success;
            }
            return valid;
        }
    }
}
