using Store_App.Helpers;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
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