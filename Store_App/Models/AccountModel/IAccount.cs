namespace Store_App.Models.AccountModel
{
    public interface IAccount
    {
        public void AccessAccount(string email, string password);
        public void AccessAccount(int accountId);
        public void CreateAccount(string email, string password);
        public string GenerateToken();
        public void UpdateEmail(string email);
        public void UpdatePassword(string password);
    }
}
