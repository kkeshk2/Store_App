using System.Data;
using Store_App.Helpers;
using Newtonsoft.Json;
using System.Data.SqlClient;
using Store_App.Exceptions;

namespace Store_App.Models.AccountModel
{
    public class Account : IAccount
    {

        [JsonIgnore] private int AccountId;
        [JsonProperty] private string? Email;
        [JsonIgnore] private readonly IAccountValidator Validator = new AccountValidator();

        public void AccessAccount(string email, string password)
        {
            using (ISqlHelper helper = new SqlHelper("SELECT * FROM Account WHERE email = @email AND password = @password"))
            {
                helper.AddParameter("@email", email);
                helper.AddParameter("@password", password);
                AccessAccount(helper);
            }
        }

        public void AccessAccount(int accountId)
        {
            using (ISqlHelper helper = new SqlHelper("SELECT * FROM Account WHERE accountId = @accountId"))
            {
                helper.AddParameter("@accountId", accountId);
                AccessAccount(helper);
            }
        }

        private void AccessAccount(ISqlHelper helper)
        {
            using (var reader = helper.ExecuteReader())
            {
                AccessAccount(reader);
                reader.Close();
            }
        }

        private void AccessAccount(SqlDataReader reader)
        {
            if (!reader.Read())
            {
                throw new AccountNotFoundException("The requested account was not found.");
            }

            AccountId = reader.GetInt32("accountId");
            Email = reader.GetString("email");
        }


        private void CheckIfEmailIsTaken(string email)
        {
            using (ISqlHelper helper = new SqlHelper("SELECT * FROM Account WHERE email = @email"))
            {
                helper.AddParameter("@email", email);
                CheckIfEmailIsTaken(helper);
            }
        }

        private void CheckIfEmailIsTaken(ISqlHelper helper)
        {
            using (var reader = helper.ExecuteReader())
            {
                CheckIfEmailIsTaken(reader);
            }
        }

        private void CheckIfEmailIsTaken(SqlDataReader reader)
        {
            if (reader.Read())
            {
                throw new EmailTakenException("That email is already taken.");
            }
        }

        public void CreateAccount(string email, string password)
        {
            Validator.ValidateAccount(email, password);
            CheckIfEmailIsTaken(email);
            using (ISqlHelper helper = new SqlHelper("INSERT INTO Account (email, password) VALUES (@email, @password)"))
            {
                helper.AddParameter("@email", email);
                helper.AddParameter("@password", password);
                helper.ExecuteNonQuery();
            }
            AccessAccount(email, password);
        }

        public string GenerateToken()
        {
            IJWTHelper helper = new JWTHelper();
            return helper.GetToken(AccountId);
        }

        public void UpdateEmail(string email)
        {          
            Validator.ValidateEmail(email);
            CheckIfEmailIsTaken(email);
            using (ISqlHelper helper = new SqlHelper("UPDATE Account SET email = @email WHERE accountId = @accountId"))
            {
                helper.AddParameter("@email", email);
                helper.AddParameter("@accountId", AccountId);
                helper.ExecuteNonQuery();
            }
            AccessAccount(AccountId);
        }

        public void UpdatePassword(string password)
        {
            Validator.ValidatePassword(password);
            using (ISqlHelper helper = new SqlHelper("UPDATE Account SET password = @password WHERE accountId = @accountId"))
            {
                helper.AddParameter("@password", password);
                helper.AddParameter("@accountId", AccountId);
                helper.ExecuteNonQuery();
            }
        }
    }
}