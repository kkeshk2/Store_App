using System.Text.RegularExpressions;

namespace Store_App.Models.InvoiceModel
{
    public class CreditCardValidator : ICreditCardValidator
    {
        public string GetLast4 (string creditCard)
        {
            if (!ValidateCreditCardString(creditCard)) throw new ArgumentException();
            var cardNumber = creditCard.Split((char)31)[0];
            var last4 = cardNumber[12..16];
            return last4;
        }

        public bool ValidateCreditCardString(string creditCard)
        {
            string[] creditCardArray = creditCard.Split((char)31);
            bool valid = true;
            valid = valid && ValidateCreditCard(creditCardArray[0]);
            valid = valid && ValidateCardVC(creditCardArray[1]);
            valid = valid && ValidateExpMonth(creditCardArray[2]);
            valid = valid && ValidateExpYear(creditCardArray[3]);
            return valid;
        }

        public bool ValidateCreditCard(string creditCard, string expMonth, string expYear, string cardVC)
        {
            bool valid = true;
            valid = valid && ValidateCreditCard(creditCard);
            valid = valid && ValidateCardVC(cardVC);
            valid = valid && ValidateExpMonth(expMonth);
            valid = valid && ValidateExpYear(expYear);
            return valid;
        }

        private bool ValidateCreditCard(string creditCard)
        {
            bool valid = true;
            if (!string.IsNullOrEmpty(creditCard))
            {
                valid = Regex.Match(creditCard, "^[0-9]{16,16}$").Success;
            }
            return valid;
        }

        private bool ValidateCardVC(string cardVC)
        {
            bool valid = true;
            if (!string.IsNullOrEmpty(cardVC))
            {
                valid = Regex.Match(cardVC, "^[0-9]{3,4}$").Success;
            }
            return valid;
        }

        private bool ValidateExpMonth(string expMonth)
        {
            bool valid = true;
            if (!string.IsNullOrEmpty(expMonth))
            {
                valid = Regex.Match(expMonth, "^[0-9]{2,2}$").Success;
            }
            return valid;
        }

        private bool ValidateExpYear(string expYear)
        {
            bool valid = true;
            if (!string.IsNullOrEmpty(expYear))
            {
                valid = Regex.Match(expYear, "^[0-9]{4,4}$").Success;
            }
            return valid;
        }
    }
}
