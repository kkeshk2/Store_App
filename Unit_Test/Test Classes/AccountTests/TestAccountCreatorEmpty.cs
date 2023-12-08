using Store_App.Helpers;
using Store_App.Models.AccountModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Test.Test_Classes.AccountTests
{
    public class TestAccountCreatorEmpty : IAccountCreator
    {
        public IAccount GetAccount()
        {
            return new Account(new AccountValidator(), new TestAccountDataContextEmpty(), new JsonWebTokenHelper());
        }

        public IAccount GetAccount(int id, string? email, string? password)
        {
            return new Account(id, email, password, new AccountValidator(), new TestAccountDataContextEmpty(), new JsonWebTokenHelper());
        }
    }
}
