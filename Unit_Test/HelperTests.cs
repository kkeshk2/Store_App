using Store_App.Helpers;

namespace Unit_Test
{
    [TestClass]
    public class HelperTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            IJWTHelper helper = new JWTHelper();
            var token = helper.GetToken(1);
            Assert.AreEqual(1, helper.GetAccountId(token));
        }
    }
}