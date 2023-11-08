using Store_App.Models.Classes;
using System.Data.SqlClient;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Account acc1 = new Account(1, "user1@example.com", "User 1");
            Account acc2 = Account.accessAccountByLogin("user1@example.com", "pass1");
            Assert.AreEqual(acc1.AccountId, acc2.AccountId);
            Assert.AreEqual(acc1.AccountEmail, acc2.AccountEmail);
            Assert.AreEqual(acc1.AccountName, acc2.AccountName);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Account acc1 = new Account(1, "user1@example.com", "User 1");
            Account acc2 = Account.accessAccountById(1);
            Assert.AreEqual(acc1.AccountId, acc2.AccountId);
            Assert.AreEqual(acc1.AccountEmail, acc2.AccountEmail);
            Assert.AreEqual(acc1.AccountName, acc2.AccountName);
        }

        [TestMethod]
        public void TestMethod3()
        {
            Account acc1 = new Account(3, "user3@example.com", "User 3");
            Account acc2 = Account.createAccount("user3@example.com", "pass3", "User 3");
            Assert.AreEqual(acc1.AccountEmail, acc2.AccountEmail);
            Assert.AreEqual(acc1.AccountName, acc2.AccountName);
        }

        [TestMethod]
        public void TestMethod4()
        {
            Account acc1 = new Account(3, "user4@example.com", "User 4");
            Account acc2 = Account.accessAccountByLogin("user3@example.com", "pass3");
            acc2.updateAccount("user4@example.com", "User 4");
            acc2.updateAccountPassword("pass4");
            Assert.AreEqual(acc1.AccountEmail, acc2.AccountEmail);
            Assert.AreEqual(acc1.AccountName, acc2.AccountName);
        }

        [TestMethod]
        public void TestMethod5()
        {
            Action action = () => {Account acc = Account.createAccount("user4@example.com", "pass4", "User 4"); };
            Assert.ThrowsException<SqlException>(action);
        }
    }
}