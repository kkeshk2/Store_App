using Store_App.Exceptions;
using System.Text.RegularExpressions;

namespace Store_App.Models.AccountModel
{
    public class AccountValidator : IAccountValidator
    {
        private static readonly IDictionary<string, string> RegexDictionary = CreateRegexDictionary();
        
        private static IDictionary<string, string> CreateRegexDictionary()
        {
            IDictionary<string, string> RegexDictionary = new Dictionary<string, string>
            {
                { "email", "^[^@\\s@]+@[^@\\s]+\\.[^@\\s]+$" },
                { "password", "^[^\\s]{8,128}$" }
            };
            return RegexDictionary;
        }

        public void ValidateAccount(string email, string password)
        {
            ValidateEmail(email);
            ValidatePassword(password);
        }

        public void ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new InvalidInputException("Email is invalid.");
            }

            if (!Regex.IsMatch(email, RegexDictionary["email"])) 
            {
                throw new InvalidInputException("Email is invalid.");
            }
        }

        public void ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new InvalidInputException("Password is invalid.");
            }

            if (!Regex.IsMatch(password, RegexDictionary["password"]))
            {
                throw new InvalidInputException("Password is invalid.");
            }
        }
    }
}