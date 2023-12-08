using Store_App.Helpers;
using Store_App.Models.AccountModel;

namespace Unit_Test.Test_Classes.AccountTests
{
    public class TestAccountCreator : IAccountCreator
    {
        public IAccount GetAccount()
        {
            return new Account(new AccountValidator(), new TestAccountDataContext(), new JsonWebTokenHelper());
        }

        public IAccount GetAccount(int id, string? email, string? password)
        {
            return new Account(id, email, password, new AccountValidator(), new TestAccountDataContext(), new JsonWebTokenHelper());
        }
    }
}
