using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store_App.Exceptions;
using Store_App.Models.AccountModel;
using Unit_Test.Test_Classes;
using Unit_Test.Test_Classes.AccountTests;

namespace Unit_Test
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void TestAccountConstructorEqual1()
        {   
            IAccount account1 = new TestAccountCreator().GetAccount(0, null, null);
            IAccount account2 = new TestAccountCreator().GetAccount(0, null, null);
            Assert.AreEqual(account1, account2);
        }

        [TestMethod]
        public void TestAccountConstructorEqual2()
        {
            IAccount account1 = new TestAccountCreator().GetAccount(0, "email", "password");
            IAccount account2 = new TestAccountCreator().GetAccount(0, "email", "password");
            Assert.AreEqual(account1, account2);
        }

        [TestMethod]
        public void TestAccountConstructorNotEqual1()
        {
            IAccount account1 = new TestAccountCreator().GetAccount(0, null, null);
            IAccount account2 = new TestAccountCreator().GetAccount(1, null, null);
            Assert.AreNotEqual(account1, account2);
        }

        [TestMethod]
        public void TestAccountConstructorNotEqual2()
        {
            IAccount account1 = new TestAccountCreator().GetAccount(0, null, null);
            IAccount account2 = new TestAccountCreator().GetAccount(0, "email", null);
            Assert.AreNotEqual(account1, account2);
        }

        [TestMethod]
        public void TestAccountConstructorNotEqual3()
        {
            IAccount account1 = new TestAccountCreator().GetAccount(0, null, null);
            IAccount account2 = new TestAccountCreator().GetAccount(0, null, "password");
            Assert.AreNotEqual(account1, account2);
        }

        [TestMethod]
        public void TestAccessAccountEqual1()
        {
            IAccount account1 = new TestAccountCreator().GetAccount(1, "user1@example.com", "pass1");
            IAccount account2 = new TestAccountCreator().GetAccount();
            account2.AccessAccount(1);
            Assert.AreEqual(account1, account2);
        }

        [TestMethod]
        public void TestAccessAccountNotEqual()
        {
            IAccount account1 = new TestAccountCreator().GetAccount(2, "user2@example.com", "pass2");
            IAccount account2 = new TestAccountCreator().GetAccount();
            account2.AccessAccount(1);
            Assert.AreNotEqual(account1, account2);
        }

        [TestMethod]
        public void TestCreateAccountThrowsInvalidInputException()
        {
            IAccount account1 = new TestAccountCreator().GetAccount();
            Assert.ThrowsException<InvalidInputException>(() => account1.CreateAccount("user3", "pass"));
        }

        [TestMethod]
        public void TestCreateAccountThrowsInvalidInputException2()
        {
            IAccount account1 = new TestAccountCreator().GetAccount();
            Assert.ThrowsException<InvalidInputException>(() => account1.CreateAccount("user3@example.com", "pass"));
        }

        [TestMethod]
        public void TestCreateAccountThrowsEmailTakenException()
        {
            IAccount account1 = new TestAccountCreator().GetAccount();
            Assert.ThrowsException<EmailTakenException>(() => account1.CreateAccount("user3@example.com", "password3"));
        }

        [TestMethod]
        public void TestUpdateEmailThrowsInvalidInputException()
        {
            IAccount account1 = new TestAccountCreator().GetAccount();
            Assert.ThrowsException<InvalidInputException>(() => account1.UpdateEmail("user3"));
        }

        [TestMethod]
        public void TestUpdateEmailThrowsEmailTakenException()
        {
            IAccount account1 = new TestAccountCreator().GetAccount();
            Assert.ThrowsException<EmailTakenException>(() => account1.UpdateEmail("user3@example.com"));
        }

        [TestMethod]
        public void TestUpdatePasswordEqual()
        {
            IAccount account1 = new TestAccountCreator().GetAccount(1, "user1@example.com", "pass1");
            IAccount account2 = new TestAccountCreator().GetAccount();
            account2.UpdatePassword("password");
            Assert.AreEqual(account1, account2);
        }

        [TestMethod]
        public void TestUpdatePasswordNotEqual()
        {
            IAccount account1 = new TestAccountCreator().GetAccount(1, "user1@example.com", "password");
            IAccount account2 = new TestAccountCreator().GetAccount();
            account2.UpdatePassword("password");
            Assert.AreNotEqual(account1, account2);
        }

        [TestMethod]
        public void TestUpdatePasswordThrowsInvalidInputException()
        {
            IAccount account1 = new TestAccountCreator().GetAccount();
            Assert.ThrowsException<InvalidInputException>(() => account1.UpdatePassword("pass"));
        }

        [TestMethod]
        public void TestAccessAccountThrowsException()
        {
            IAccount account1 = new TestAccountCreatorEmpty().GetAccount();
            Assert.ThrowsException<AccountNotFoundException>(() => account1.AccessAccount(1));
        }

        [TestMethod]
        public void TestCreateAccountThrowsException()
        {
            IAccount account1 = new TestAccountCreatorEmpty().GetAccount();
            Assert.ThrowsException<AccountNotFoundException>(() => account1.CreateAccount("user3@example.com", "password3"));
        }

        [TestMethod]
        public void TestUpdateEmailThrowsException()
        {
            IAccount account1 = new TestAccountCreatorEmpty().GetAccount();
            Assert.ThrowsException<AccountNotFoundException>(() => account1.UpdateEmail("user3@example.com"));
        }

        [TestMethod]
        public void TestUpdatePasswordThrowsException()
        {
            IAccount account1 = new TestAccountCreatorEmpty().GetAccount();
            Assert.ThrowsException<AccountNotFoundException>(() => account1.UpdatePassword("password3"));
        }
    }
}
