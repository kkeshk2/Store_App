namespace Store_App.Models.AccountModel
{
    public interface IAccount
    {
        public void AccessAccount(string accountEmail);
        public void AccessAccount(string accountEmail, string accountPassword);
        public void AccessAccount(int accountId);
        public void CreateAccount(string accountEmail, string accountPassword, string accountName);
        public string GenerateToken();
        public void UpdateAccountEmail(string accountEmail);
        public void UpdateAccountName(string accountName);
        public void UpdateAccountPassword(string accountName);
    }
}
