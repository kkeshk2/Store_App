using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store_App.Models.AccountModel;

namespace Unit_Test
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void TestAccountConstructorEqual()
        {
            IAccount account1 = new Account();
            IAccount account2 = new Account(0, null, null);
            Assert.AreEqual(account1, account2);
        }

        [TestMethod]
        public void TestAccountConstructorNotEqual()
        {
            IAccount account1 = new Account(1, "email", "password");
            IAccount account2 = new Account(1, "email", null);
            IAccount account3 = new Account(1, null, "password");
            IAccount account4 = new Account(1, null, null);
            Assert.AreNotEqual(account1, account2);
            Assert.AreNotEqual(account1, account3);
            Assert.AreNotEqual(account1, account4);
        }

        [TestMethod]
        public void TestAccessAccountEqual()
        {
            IAccount account1 = new Account();
            IAccount account2 = new Account(1, "user1@example.com", "pass1");
            account1.AccessAccount(1);
            Assert.AreEqual(account1, account2);
        }

        [TestMethod]
        public void TestAccessAccountNotEqual()
        {
            IAccount account1 = new Account();
            IAccount account2 = new Account(2, "user2@example.com", "pass2");
            account1.AccessAccount(1);
            Assert.AreNotEqual(account1, account2);
        }

        [TestMethod]
        public void TestAccountList()
        {
            List<IAccount> accounts1 = new List<IAccount>();
            List<IAccount> accounts2 = new List<IAccount>();
            IAccount account1 = new Account();
            IAccount account2 = new Account();
            accounts1.Add(account1);
            accounts2.Add(account2);
            Assert.IsTrue(accounts1.SequenceEqual(accounts2));
        }

        [TestMethod]
        public void TestAccountListNotEqual()
        {
            List<IAccount> accounts1 = new List<IAccount>();
            List<IAccount> accounts2 = new List<IAccount>();
            IAccount account1 = new Account();
            IAccount account2 = new Account();
            account1.AccessAccount(1);
            account2.AccessAccount(2);
            accounts1.Add(account1);
            accounts2.Add(account2);
            Assert.IsFalse(accounts1.SequenceEqual(accounts2));
        }
    }
}
