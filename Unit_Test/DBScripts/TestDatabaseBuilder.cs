using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Test.DBScripts
{
    internal class TestDatabaseBuilder : ITestDatabaseBuilder
    {
        private SqlConnection SqlConnection { get; set; }

        public TestDatabaseBuilder()
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("appsettings.json");
            var config = configBuilder.Build();

            string? connectionString = config.GetConnectionString("TestConnection");
            SqlConnection = new SqlConnection(connectionString);
            SqlConnection.Open();
        }

        public void ClearDB()
        {
            using (var command = SqlConnection.CreateCommand())
            {
                command.CommandText = "DROP TABLE IF EXISTS Sale;";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS InvoiceProduct";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS Invoice";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS Cart";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS Address";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS Account";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS Product";
                command.ExecuteNonQuery();
            }
        }

        public void CreateDB()
        {
            using (var command = SqlConnection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE Product (productId int PRIMARY KEY IDENTITY(1, 1), name varchar(128) NOT NULL, price decimal(19, 2) NOT NULL, manufacturer varchar(128) NOT NULL, rating decimal(9, 2) NOT NULL, description varchar(1024) NOT NULL, category varchar(32) NOT NULL, length decimal(19, 2) NOT NULL, width decimal(19, 2) NOT NULL, height decimal(19, 2) NOT NULL, weight decimal(19, 2) NOT NULL, sku varchar(32) NOT NULL, imageLocation varchar(256) NOT NULL);";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE Account (accountId int PRIMARY KEY IDENTITY(1, 1), email varchar(128) NOT NULL UNIQUE, password varchar(128) NOT NULL);";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE Address (addressId int PRIMARY KEY IDENTITY(1, 1), name varchar(128) NOT NULL, line1 varchar(128) NOT NULL, line2 varchar(128), city varchar(128) NOT NULL, state char(2) NOT NULL, postal char(5) NOT NULL, UNIQUE (name, line1, line2, city, state, postal));";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE Cart (cartId int PRIMARY KEY IDENTITY(1, 1), accountId int NOT NULL, productId int NOT NULL, quantity int NOT NULL, FOREIGN KEY (accountId) REFERENCES Account(accountId), FOREIGN KEY (productId) REFERENCES Product(productId));";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE Invoice (invoiceId int PRIMARY KEY IDENTITY(1, 1), accountId int, size int NOT NULL, total decimal(19,2) NOT NULL, date dateTime NOT NULL, creditCard char(4) NOT NULL, billingAddressId int NOT NULL, shippingAddressId int NOT NULL, trackingNumber char(20) NOT NULL, FOREIGN KEY (accountId) REFERENCES Account(AccountId), FOREIGN KEY (billingAddressID) REFERENCES Address(addressId), FOREIGN KEY (shippingAddressID) REFERENCES Address(addressId));";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE InvoiceProduct (invoiceProductId int PRIMARY KEY IDENTITY(1, 1), invoiceId int NOT NULL, productId int NOT NULL, quantity int NOT NULL, price DECIMAL(19, 2) NOT NULL, FOREIGN KEY (invoiceId) REFERENCES Invoice(invoiceId), FOREIGN KEY (productId) REFERENCES Product(productId));";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE Sale (saleId int NOT NULL, productId int NOT NULL, amount decimal(19,2) NOT NULL, startDate date NOT NULL, endDate date NOT NULL);";
                command.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            SqlConnection.Close();
            SqlConnection.Dispose();
        }

        public void PopulateDB()
        {
            using (var command = SqlConnection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Product (name, price, manufacturer, rating, description, category, length, width, height, weight, sku, imageLocation) VALUES ('NVIDIA GeForce RTX 2070', 499.99, 'NVIDIA', 4.8, 'High-performance graphics card.', 'Graphics Card', 10.0, 4.5, 1.5, 1.0, 'RTX2070', '/rtx2070_image.jpg'), ('Intel Core i7-9700K', 349.99, 'Intel', 4.7, 'Powerful processor for gaming and multitasking.', 'Processor', 5.0, 5.0, 1.5, 0.8, 'i79700k', '/i79700k_image.jpg'), ('NZXT Kraken X63 RGB', 149.99, 'NZXT', 4.6, 'Advanced liquid cooling solution with customizable RGB lighting.', 'Cooling', 15.0, 7.0, 2.0, 1.5, 'KrakenX63', '/kraken_x63_image.jpg');";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO Account (email, password) VALUES ('user1@example.com', 'pass1'), ('user2@example.com', 'pass2');";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO Address (name, line1, line2, city, state, postal) VALUES ('Home Address', '123 Main St', 'Apt 4B', 'City A', 'CA', '12345'), ('Work Address', '456 Elm St', NULL, 'City B', 'NY', '54321');";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO Cart (accountId, productId, quantity) VALUES (1, 1, 2), (1, 2, 1), (2, 1, 3);";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO Invoice (accountId, size, total, date, creditCard, billingAddressId, shippingAddressId, trackingNumber) VALUES (1, 3, 1349.99, '2023-10-30 12:31:23', '1234', 1, 1, 'ABCDEFGHIJ0123456789'), (2, 3, 1499.97, '2023-10-30 01:22:45', '5678', 2, 1, '9876543210JIHGFEDCBA');";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO InvoiceProduct (invoiceId, productId, quantity, price) VALUES (1, 1, 2, 499.99), (1, 2, 1, 349.99), (2, 1, 3, 499.99);";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO Sale (saleId, productId, amount, startDate, endDate) VALUES (1, 1, 100.00, '20231101', '20231231'), (2, 2, 50.00, '20231101', '20231231');";
                command.ExecuteNonQuery();
            }
        }
    }
}
