namespace Store_App.Models.AccountModel
{
    public interface IAccountCreator
    {
        public IAccount GetAccount();
        public IAccount GetAccount(int id, string email, string password);
    }
}
