using Store_App.Models.Interfaces;
using System;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;
using System.Data;
using Microsoft.Identity.Client;

namespace Store_App.Models.Classes
{
    public class Account : Models.Interfaces.IAccount
    {
        public int AccountId { get; }
        public string AccountEmail { get; set; }
        public string AccountName { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new List<string>();


        public Account(int accountId, string accountEmail, string accountName)
        {
            this.AccountId = accountId;
            this.AccountEmail = accountEmail;
            this.AccountName = accountName;
        }

        public Account()
        {
        }

        public static Account accessAccountByLogin(string accountEmail, string accountPassword)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.getConnectionString()))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }

                SqlCommand cmd = new SqlCommand("SELECT accountId, accountEmail, accountName FROM Account WHERE accountEmail = @accountEmail AND accountPassword = @accountPassword", connection);
                cmd.Parameters.AddWithValue("@accountEmail", accountEmail);
                cmd.Parameters.AddWithValue("@accountPassword", accountPassword);

                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                Account account = new Account(
                    reader.GetInt32("accountId"),
                    reader.GetString("accountEmail"),
                    reader.GetString("accountName")
                );

                reader.Close();
                connection.Close();

                return account;
            }
        }

        public static Account accessAccountById(Int32 accountId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.getConnectionString()))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }

                SqlCommand cmd = new SqlCommand("SELECT accountId, accountEmail, accountName FROM Account WHERE accountId = @accountId", connection);
                cmd.Parameters.AddWithValue("@accountId", accountId);

                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                Account account = new Account(
                    reader.GetInt32("accountId"),
                    reader.GetString("accountEmail"),
                    reader.GetString("accountName")
                );

                reader.Close();
                connection.Close();

                return account;
            }
        }

        public static Account createAccount(string accountEmail, string accountPassword, string accountName)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.getConnectionString()))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }

                SqlCommand cmd1 = new SqlCommand("INSERT INTO Account (accountEmail, accountPassword, accountName) VALUES (@accountEmail, @accountPassword, @accountName)", connection);
                cmd1.Parameters.AddWithValue("@accountEmail", accountEmail);
                cmd1.Parameters.AddWithValue("@accountPassword", accountPassword);
                cmd1.Parameters.AddWithValue("@accountName", accountName);

                cmd1.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("SELECT accountId, accountEmail, accountName FROM Account WHERE accountEmail = @accountEmail AND accountPassword = @accountPassword", connection);
                cmd2.Parameters.AddWithValue("@accountEmail", accountEmail);
                cmd2.Parameters.AddWithValue("@accountPassword", accountPassword);

                SqlDataReader reader = cmd2.ExecuteReader();

                reader.Read();
                Account account = new Account(
                    reader.GetInt32("accountId"),
                    reader.GetString("accountEmail"),
                    reader.GetString("accountName")
                );

                reader.Close();
                connection.Close();

                return account;
            }
        }

        public void updateAccount(string accountEmail, string accountName)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.getConnectionString()))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }

                SqlCommand cmd1 = new SqlCommand("UPDATE Account SET accountEmail = @accountEmail, accountName = @accountName WHERE accountId = @accountId", connection);
                cmd1.Parameters.AddWithValue("@accountEmail", accountEmail);
                cmd1.Parameters.AddWithValue("@accountName", accountName);
                cmd1.Parameters.AddWithValue("@accountId", this.AccountId);

                cmd1.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("SELECT accountId, accountEmail, accountName FROM Account WHERE accountId = @accountId", connection);
                cmd2.Parameters.AddWithValue("@accountId", this.AccountId);

                SqlDataReader reader = cmd2.ExecuteReader();

                reader.Read();
                this.AccountEmail = reader.GetString("accountEmail");
                this.AccountName = reader.GetString("accountName");

                reader.Close();
                connection.Close();
            }
        }

        public void updateAccountPassword(string accountPassword)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.getConnectionString()))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }

                SqlCommand cmd = new SqlCommand("UPDATE Account SET accountPassword = @accountPassword WHERE accountId = @accountId", connection);
                cmd.Parameters.AddWithValue("@accountPassword", accountPassword);
                cmd.Parameters.AddWithValue("@accountId", this.AccountId);

                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }
        public Account accountLogin(string accountName, string accountPassword)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.getConnectionString()))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
                SqlCommand cmd = new SqlCommand("SELECT * from accountName, accountPassword, accountEmail WHERE accountName = @accountName AND accountPassword = @accountPassword", connection);
                cmd.Parameters.AddWithValue("@accountName", accountName);
                cmd.Parameters.AddWithValue("@accountPassword", accountPassword);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No Account found");
                        Account account = new Account();
                        account.Success = false;
                        account.Errors.Add("Account not found");
                        return account;
                    }
                    else
                    {
                        Account account = new Account(
                            reader.GetInt32("accountId"),
                            reader.GetString("accountEmail"),
                            reader.GetString("accountName")
                        );
                        return account;
                    }
                    reader.Close();
                }
                Account notFound = new Account();
                notFound.Success = false;
                return notFound;
                connection.Close();
            }
        }
    }
}
