using System.Text.RegularExpressions;

namespace Store_App.Models.AccountModel
{
    public class AccountValidator : IAccountValidator
    {
        public bool Validate(string accountEmail, string accountPassword, string accountName)
        {
            bool valid = true;
            valid = valid && ValidateEmail(accountEmail);
            valid = valid && ValidatePassword(accountPassword);
            valid = valid && ValidateName(accountName);
            return valid;
        }

        public bool ValidateEmail(string accountEmail)
        {
            bool valid = true;
            if (!string.IsNullOrEmpty(accountEmail))
            {
                valid = Regex.Match(accountEmail, "^[^@\\s@]+@[^@\\s]+\\.[^@\\s]+$").Success;
            }
            return valid;
        }

        public bool ValidateName(string accountName)
        {
            bool valid = true;
            if (!string.IsNullOrEmpty(accountName))
            {
                valid = Regex.Match(accountName, "^[A-Za-z][A-Za-z\\.\\-\\x20]{0,127}$").Success;
            }
            return valid;
        }

        public bool ValidatePassword(string accountPassword)
        {
            bool valid = true;
            if (!string.IsNullOrEmpty(accountPassword))
            {
                valid = Regex.Match(accountPassword, "^[^\\s]{8,128}$").Success;
            }
            return valid;
        }
    }
}
