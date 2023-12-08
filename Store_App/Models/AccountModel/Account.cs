using System.Data;
using Store_App.Helpers;
using Newtonsoft.Json;
using System.Data.SqlClient;
using Store_App.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Store_App.Models.AccountModel
{
    public class Account : IAccount
    {
        [JsonIgnore] private readonly IAccountValidator AccountValidator;
        [JsonIgnore] private readonly IDataContext DataContext;
        [JsonIgnore] private readonly IJsonWebTokenHelper JsonWebTokenHelper;

        [JsonIgnore] private int AccountId;
        [JsonProperty] private string? Email;
        [JsonIgnore] private string? Password;

        public Account(IAccountValidator validator, IDataContext dataContext, IJsonWebTokenHelper helper) { 
            AccountValidator = validator;     
            DataContext = dataContext;
            JsonWebTokenHelper = helper;
        }

        public Account(int accountId, string? email, string? password, IAccountValidator validator, IDataContext dataContext, IJsonWebTokenHelper helper)
        {
            AccountId = accountId;
            Email = email;
            Password = password;
            AccountValidator = validator;
            DataContext = dataContext;
            JsonWebTokenHelper = helper;
        }

        public void AccessAccount(string email, string password)
        {
            using (ISqlHelper helper = DataContext.GetConnection("SELECT * FROM Account WHERE email = @email AND password = @password"))
            {
                helper.AddParameter("@email", email);
                helper.AddParameter("@password", password);
                AccessAccount(helper);
            }
        }

        public void AccessAccount(int accountId)
        {
            using (ISqlHelper helper = DataContext.GetConnection("SELECT * FROM Account WHERE accountId = @accountId"))
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

        private void AccessAccount(DbDataReader reader)
        {
            if (!reader.Read())
            {
                throw new AccountNotFoundException("The requested account was not found.");
            }

            AccountId = reader.GetInt32("accountId");
            Email = reader.GetString("email");
            Password = reader.GetString("password");
        }


        private void CheckIfEmailIsTaken(string email)
        {
            using (ISqlHelper helper = DataContext.GetConnection("SELECT * FROM Account WHERE email = @email"))
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

        private void CheckIfEmailIsTaken(DbDataReader reader)
        {
            if (reader.Read())
            {
                throw new EmailTakenException("That email is already taken.");
            }
        }

        public void CreateAccount(string email, string password)
        {
            AccountValidator.ValidateAccount(email, password);
            CheckIfEmailIsTaken(email);
            using (ISqlHelper helper = DataContext.GetConnection("INSERT INTO Account (email, password) VALUES (@email, @password)"))
            {
                helper.AddParameter("@email", email);
                helper.AddParameter("@password", password);
                helper.ExecuteNonQuery();
            }
            AccessAccount(email, password);
        }

        public string GenerateToken()
        {
            return JsonWebTokenHelper.GetToken(AccountId);
        }

        public void UpdateEmail(string email)
        {          
            AccountValidator.ValidateEmail(email);
            CheckIfEmailIsTaken(email);
            using (ISqlHelper helper = DataContext.GetConnection("UPDATE Account SET email = @email WHERE accountId = @accountId"))
            {
                helper.AddParameter("@email", email);
                helper.AddParameter("@accountId", AccountId);
                helper.ExecuteNonQuery();
            }
            AccessAccount(AccountId);
        }

        public void UpdatePassword(string password)
        {
            AccountValidator.ValidatePassword(password);
            using (ISqlHelper helper = DataContext.GetConnection("UPDATE Account SET password = @password WHERE accountId = @accountId"))
            {
                helper.AddParameter("@password", password);
                helper.AddParameter("@accountId", AccountId);
                helper.ExecuteNonQuery();
            }
            AccessAccount(AccountId);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not null && obj is Account account)
            {
                bool equals = true;
                equals = equals && account.AccountId == AccountId;
                equals = equals && account.Email == Email;
                equals = equals && account.Password == Password;
                return equals;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return AccountId.GetHashCode();   
        }
    }
}