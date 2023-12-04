namespace Store_App.Models.AccountModel
{
    public interface IAccountValidator
    {
        public void ValidateAccount(string email, string password);
        public void ValidateEmail(string email);
        public void ValidatePassword(string password);
    }
}