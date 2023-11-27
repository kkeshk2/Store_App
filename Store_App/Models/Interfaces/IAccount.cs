using Store_App.Models.Classes;

namespace Store_App.Models.Interfaces
{
    public interface IAccount
    {
        public int AccountId { get; }
        public string AccountEmail { get; set; }
        public string AccountName { get; set; }

        public static abstract Account accessAccountByLogin(string accountEmail, string accountPassword);
        public static abstract Account accessAccountById(int accountId);
        public static abstract Account createAccount(string accountEmail, string accountPassword, string accountName);
        public void updateAccount(string accountEmail, string accountName);
        public void updateAccountPassword(string accountPassword);
    }
}
