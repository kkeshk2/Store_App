using Store_App.Exceptions;
using System.Text.RegularExpressions;

namespace Store_App.Models.InvoiceModel
{
    public class CreditCardValidator : ICreditCardValidator
    {
        private static IDictionary<string, string> RegexDictionary = GenerateRegexDictionary();

        private static IDictionary<string, string> GenerateRegexDictionary()
        {
            IDictionary<string, string> RegexDictionary = new Dictionary<string, string>
            {
                { "creditCardNumber", "^[0-9]{16,16}$" },
                { "verificationCode", "^[0-9]{3,4}$" },
                { "expirationMonth", "^[1][012]$|^[0]?[1-9]$" },
                { "expirationYear", "^[0-9]{4,4}$" }
            };
            return RegexDictionary;
        }

        public string ValidateCreditCardReturnLast4 (string creditCard)
        {
            string[] creditCardArray = creditCard.Split("\t");
            ValidateCreditCard(creditCardArray[0], creditCardArray[1], creditCardArray[2], creditCardArray[3]);
            return creditCardArray[0][12..16];
        }

        public void ValidateCreditCard(string cardNumber, string expirationMonth, string expirationYear, string verificationCode)
        {
            ValidateCreditCardNumber(cardNumber);
            ValidateVerificationCode(verificationCode);
            ValidateExpirationMonth(expirationMonth);
            ValidateExpirationYear(expirationYear);
            int expirationYearInteger = Convert.ToInt32(expirationYear);
            int expirationMonthInteger = Convert.ToInt32(expirationMonth);
            ValidateExpirationDate(expirationYearInteger, expirationMonthInteger);
        }

        private void ValidateCreditCardNumber(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
            {
                throw new InvalidInputException("Credit card number is invalid.");
            }

            if (!Regex.IsMatch(cardNumber, RegexDictionary["creditCardNumber"]))
            {
                throw new InvalidInputException("Credit card number is invalid.");
            }
        }

        private void ValidateVerificationCode(string verificationCode)
        {
            if (string.IsNullOrEmpty(verificationCode))
            {
                throw new InvalidInputException("Verification code is invalid.");
            }

            if (!Regex.IsMatch(verificationCode, RegexDictionary["verificationCode"]))
            {
                throw new InvalidInputException("Verification code is invalid.");
            }
        }

        private void ValidateExpirationDate(int expirationYear, int expirationMonth)
        {
            DateTime expirationDate = new DateTime();
            expirationDate = expirationDate.AddYears(expirationYear);
            expirationDate = expirationDate.AddMonths(expirationMonth + 1);

            if (expirationDate <= DateTime.Now)
            {
                throw new InvalidInputException("Credit card is expired.");
            }
        }

        private void ValidateExpirationMonth(string expirationMonth)
        {
            if (string.IsNullOrEmpty(expirationMonth))
            {
                throw new InvalidInputException("Verification code is invalid.");
            }

            if (!Regex.IsMatch(expirationMonth, RegexDictionary["expirationMonth"]))
            {
                throw new InvalidInputException("Verification code is invalid.");
            }
        }

        private void ValidateExpirationYear(string expirationYear)
        {
            if (string.IsNullOrEmpty(expirationYear))
            {
                throw new InvalidInputException("Verification code is invalid.");
            }

            if (!Regex.IsMatch(expirationYear, RegexDictionary["expirationYear"]))
            {
                throw new InvalidInputException("Verification code is invalid.");
            }
        }
    }
}