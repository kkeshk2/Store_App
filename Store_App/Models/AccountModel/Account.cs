using System;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;
using System.Data;
using Microsoft.Identity.Client;
using Store_App.Helpers;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace Store_App.Models.AccountModel
{
    public class Account : IAccount
    {

        [JsonIgnore] private int AccountId = -1;
        [JsonProperty] private string? AccountEmail;
        [JsonProperty] private string? AccountName;
        [JsonIgnore] private readonly IAccountValidator _validator = new AccountValidator();

        public void AccessAccount(string accountEmail)
        {
            using (var helper = new SqlHelper("SELECT accountId, accountEmail, accountName FROM Account WHERE accountEmail = @accountEmail"))
            {
                helper.AddParameter("@accountEmail", accountEmail);

                using (var reader = helper.ExecuteReader())
                {
                    AccessAccount(reader);
                }
            }
        }

        public void AccessAccount(string accountEmail, string accountPassword)
        {
            using (var helper = new SqlHelper("SELECT accountId, accountEmail, accountName FROM Account WHERE accountEmail = @accountEmail AND accountPassword = @accountPassword"))
            {
                helper.AddParameter("@accountEmail", accountEmail);
                helper.AddParameter("@accountPassword", accountPassword);

                using (var reader = helper.ExecuteReader())
                {
                    AccessAccount(reader);
                }
            }
        }

        public void AccessAccount(int accountId)
        {
            using (var helper = new SqlHelper("SELECT accountId, accountEmail, accountName FROM Account WHERE accountId = @accountId"))
            {
                helper.AddParameter("@accountId", accountId);

                using (var reader = helper.ExecuteReader())
                {
                    AccessAccount(reader);
                }
            }
        }

        private void AccessAccount(SqlDataReader reader)
        {
            reader.Read();
            AccountId = reader.GetInt32("accountId");
            AccountEmail = reader.GetString("accountEmail");
            AccountName = reader.GetString("accountName");
            reader.Close();
        }

        private static bool AccountExists(string accountEmail)
        {
            using (var helper = new SqlHelper("SELECT * FROM Account WHERE accountEmail = @accountEmail"))
            {
                helper.AddParameter("@accountEmail", accountEmail);

                using (var reader = helper.ExecuteReader())
                {
                    var result = reader.Read();
                    reader.Close();
                    return result;
                }
            }
        }

        public void CreateAccount(string accountEmail, string accountPassword, string accountName)
        {
            if (!_validator.Validate(accountEmail, accountPassword, accountName)) throw new ArgumentException();
            if (AccountExists(accountEmail)) throw new InvalidOperationException();
            using (var helper = new SqlHelper("INSERT INTO Account (accountEmail, accountPassword, accountName) VALUES (@accountEmail, @accountPassword, @accountName)"))
            {
                helper.AddParameter("@accountEmail", accountEmail);
                helper.AddParameter("@accountPassword", accountPassword);
                helper.AddParameter("@accountName", accountName);
                helper.ExecuteNonQuery();
            }
            AccessAccount(accountEmail, accountPassword);
        }

        public string GenerateToken()
        {
            if (AccountId == -1) throw new InvalidOperationException();
            return JWTHelper.GetToken(AccountId);
        }

        public void UpdateAccountEmail(string accountEmail)
        {
            if (!_validator.ValidateEmail(accountEmail)) throw new ArgumentException();
            if (AccountExists(accountEmail)) throw new InvalidOperationException();
            using (var helper = new SqlHelper("UPDATE Account SET accountEmail = @accountEmail WHERE accountId = @accountId"))
            {
                helper.AddParameter("@accountEmail", accountEmail);
                helper.AddParameter("@accountId", AccountId);
                helper.ExecuteNonQuery();
            }
            AccessAccount(AccountId);
        }

        public void UpdateAccountName(string accountName)
        {
            if (!_validator.ValidateName(accountName)) throw new InvalidOperationException();
            using (var helper = new SqlHelper("UPDATE Account SET accountName = @accountName WHERE accountId = @accountId"))
            {
                helper.AddParameter("@accountName", accountName);
                helper.AddParameter("@accountId", AccountId);
                helper.ExecuteNonQuery();
                
            }
            AccessAccount(AccountId);
        }

        public void UpdateAccountPassword(string accountPassword)
        {
            if (!_validator.ValidatePassword(accountPassword)) throw new InvalidOperationException();
            using (var helper = new SqlHelper("UPDATE Account SET accountPassword = @accountPassword WHERE accountId = @accountId"))
            {
                helper.AddParameter("@accountPassword", accountPassword);
                helper.AddParameter("@accountId", AccountId);
                helper.ExecuteNonQuery();
            }
        }
    }
}