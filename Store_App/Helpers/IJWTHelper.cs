namespace Store_App.Helpers
{
    public interface IJWTHelper
    {
        public string GetToken(int accountId);
        public bool ValidateToken(string encodedToken);
        public int GetAccountId(string encodedToken);
    }
}
