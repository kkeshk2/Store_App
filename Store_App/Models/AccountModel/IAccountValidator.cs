using System.Text.RegularExpressions;

namespace Store_App.Models.AccountModel
{
    public interface IAccountValidator
    {
        public bool Validate(string accountEmail, string accountPassword, string accountName);
        public bool ValidateEmail(string accountEmail);
        public bool ValidateName(string accountName);
        public bool ValidatePassword(string accountPassword);
    }
}
