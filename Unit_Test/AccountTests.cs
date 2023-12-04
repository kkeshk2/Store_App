using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store_App.Models.Classes;

namespace Unit_Test
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void TestAccessAccountByLogin()
        {
            // Arrange
            var accountEmail = "user1@example.com";
            var accountPassword = "pass1";

            // Act
            var account = Account.accessAccountByLogin(accountEmail, accountPassword);

            // Assert
            Assert.IsNotNull(account);
            Assert.AreEqual(accountEmail, account.AccountEmail);
        }

        [TestMethod]
        public void TestAccessAccountByLogin_isValid()
        {
            var accountEmail = "user1@example.com";
            var accountPassword = "pass1";
            Assert.ThrowsException<ArgumentNullException>(() => Account.accessAccountByLogin(null, accountPassword));
            Assert.ThrowsException<ArgumentNullException>(() => Account.accessAccountByLogin("", accountPassword));
            Assert.ThrowsException<ArgumentNullException>(() => Account.accessAccountByLogin(accountEmail, ""));
            Assert.ThrowsException<ArgumentNullException>(() => Account.accessAccountByLogin(accountEmail, null));
        }

        [TestMethod]
        public void TestAccessAccountByEmail()
        {
            // Arrange
            var accountEmail = "user1@example.com";

            // Act
            var count = Account.accessAccountByEmail(accountEmail);

            // Assert
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void TestAccessAccountByEmail_isValid()
        {
            Assert.ThrowsException<ArgumentNullException>(() => Account.accessAccountByEmail(null));
            Assert.ThrowsException<ArgumentNullException>(() => Account.accessAccountByEmail(""));
        }
        
        [TestMethod]
        public void TestCreateAccount()
        {
            // Arrange
            var accountEmail = "test1@example.com";
            var accountPassword = "testpass1";
            var accountName = "user10";

            // Act
            var createdAccount = Account.createAccount(accountEmail, accountPassword, accountName);

            // Assert
            Assert.IsNotNull(createdAccount);
            Assert.AreEqual(accountEmail, createdAccount.AccountEmail);
        }

        [TestMethod]
        public void TestUpdateAccount()
        {
            var test_Data = new Account(1, "user1@example.com", "User 1");

            // Arrange
            var accountEmail = "updated@example.com";
            var accountName = "updatedUser";

            // Act
            var account = new Account(test_Data.AccountId, test_Data.AccountEmail, test_Data.AccountName);
            account.updateAccount(accountEmail, accountName);

            // Assert
            Assert.AreEqual(accountEmail, account.AccountEmail);
            Assert.AreEqual(accountName, account.AccountName);

            //resets test_data to avoid database confliction
            account = test_Data;
        }
    }
}
