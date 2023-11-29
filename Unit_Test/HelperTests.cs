using Store_App.Helpers;

namespace Unit_Test
{
    [TestClass]
    public class HelperTests
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