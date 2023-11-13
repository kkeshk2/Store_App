using AuthApp.Helpers;
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
            var token = JWTHelper.GetToken(1);
            Assert.IsTrue(JWTHelper.ValidateToken(token));
            Assert.AreEqual(1, JWTHelper.GetUserId(token));
        }
    }
}