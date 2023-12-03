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
            var accountEmail = "user1@example.com";
            Assert.ThrowsException<ArgumentNullException>(() => Account.accessAccountByEmail(null));
        }

    }
}
