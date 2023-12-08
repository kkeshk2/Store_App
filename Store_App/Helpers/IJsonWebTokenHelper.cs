namespace Store_App.Helpers
{
    public interface IJsonWebTokenHelper
    {
        public string GetToken(int accountId);
        public bool ValidateToken(string encodedToken);
        public int GetAccountId(string encodedToken);
    }
}
