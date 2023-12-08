using Store_App.Helpers;

namespace Store_App.Models.AccountModel
{
    public class AccountCreator : IAccountCreator
    {
        public IAccount GetAccount()
        {
            return new Account(new AccountValidator(), new DataContext(), new JsonWebTokenHelper());
        }

        public IAccount GetAccount(int id, string email, string password)
        {
            return new Account(id, email, password, new AccountValidator(), new DataContext(), new JsonWebTokenHelper());
        }
    }
}
