DROP TABLE IF EXISTS Sale;
DROP TABLE IF EXISTS InvoiceProduct;
DROP TABLE IF EXISTS Invoice;
DROP TABLE IF EXISTS Cart;
DROP TABLE IF EXISTS Address;
DROP TABLE IF EXISTS Account;
DROP TABLE IF EXISTS Product;

CREATE TABLE Product(
	productId int PRIMARY KEY IDENTITY(1, 1),
	name varchar(128) NOT NULL,
	price decimal(19, 2) NOT NULL,
	manufacturer varchar(128) NOT NULL,
	rating decimal(9, 2) NOT NULL,
	description varchar(1024) NOT NULL,
	category varchar(32) NOT NULL,
	length decimal(19, 2) NOT NULL,
	width decimal(19, 2) NOT NULL,
	height decimal(19, 2) NOT NULL,
	weight decimal(19, 2) NOT NULL,
	sku varchar(32) NOT NULL,
	imageLocation varchar(256) NOT NULL 
);


CREATE TABLE Account(
	accountId int PRIMARY KEY IDENTITY(1, 1),
	email varchar(128) NOT NULL UNIQUE,
	password varchar(128) NOT NULL,
);

CREATE TABLE Address(
	addressId int PRIMARY KEY IDENTITY(1, 1),
	name varchar(128) NOT NULL,
	line1 varchar(128) NOT NULL,
	line2 varchar(128),
	city varchar(128) NOT NULL,
	state char(2) NOT NULL,
	postal char(5) NOT NULL,
	UNIQUE (name, line1, line2, city, state, postal)
);

CREATE TABLE Cart(
	cartId int PRIMARY KEY IDENTITY(1, 1),
	accountId int NOT NULL,
	productId int NOT NULL,
	quantity int NOT NULL,
	FOREIGN KEY (accountId) REFERENCES Account(accountId),
	FOREIGN KEY (productId) REFERENCES Product(productId)
);

CREATE TABLE Invoice(
	invoiceId int PRIMARY KEY IDENTITY(1, 1),
	accountId int,
	size int NOT NULL,
	total decimal(19,2) NOT NULL,
	date dateTime NOT NULL,
	creditCard char(4) NOT NULL,
	billingAddressId int NOT NULL,
	shippingAddressId int NOT NULL,
	trackingNumber char(20) NOT NULL,
	FOREIGN KEY (accountId) REFERENCES Account(AccountId),
	FOREIGN KEY (billingAddressID) REFERENCES Address(addressId),
	FOREIGN KEY (shippingAddressID) REFERENCES Address(addressId)
);

CREATE TABLE InvoiceProduct(
	invoiceProductId int PRIMARY KEY IDENTITY(1, 1),
	invoiceId int NOT NULL,
	productId int NOT NULL,
	quantity int NOT NULL,
	price DECIMAL(19, 2) NOT NULL, 
	FOREIGN KEY (invoiceId) REFERENCES Invoice(invoiceId),
	FOREIGN KEY (productId) REFERENCES Product(productId)
);

CREATE TABLE Sale(
	saleId int NOT NULL,
	productId int NOT NULL,
	amount decimal(19,2) NOT NULL,
	startDate date NOT NULL,
	endDate date NOT NULL
);

-- Insert data into the Product table
INSERT INTO Product (name, price, manufacturer, rating, description, category, length, width, height, weight, sku, imageLocation)
VALUES
  ('NVIDIA GeForce RTX 2070', 499.99, 'NVIDIA', 4.8, 'High-performance graphics card.', 'Graphics Card', 10.0, 4.5, 1.5, 1.0, 'RTX2070', '/rtx2070_image.jpg'),
  ('Intel Core i7-9700K', 349.99, 'Intel', 4.7, 'Powerful processor for gaming and multitasking.', 'Processor', 5.0, 5.0, 1.5, 0.8, 'i79700k', '/i79700k_image.jpg'),
  ('NZXT Kraken X63 RGB', 149.99, 'NZXT', 4.6, 'Advanced liquid cooling solution with customizable RGB lighting.', 'Cooling', 15.0, 7.0, 2.0, 1.5, 'KrakenX63', '/kraken_x63_image.jpg');

-- Insert data into the Account table
INSERT INTO Account (email, password)
VALUES
  ('user1@example.com', 'pass1'),
  ('user2@example.com', 'pass2');
  -- Add more accounts here

-- Insert data into the Address table
INSERT INTO Address (name, line1, line2, city, state, postal)
VALUES
  ('Home Address', '123 Main St', 'Apt 4B', 'City A', 'CA', '12345'),
  ('Work Address', '456 Elm St', NULL, 'City B', 'NY', '54321');
  -- Add more addresses here

-- Insert data into the Cart table
INSERT INTO Cart (accountId, productId, quantity)
VALUES
  (1, 1, 2),
  (1, 2, 1),
  (2, 1, 3);
  -- Add more cart items here

-- Insert data into the Invoice table
INSERT INTO Invoice (accountId, size, total, date, creditCard, billingAddressId, shippingAddressId, trackingNumber)
VALUES
  (1, 3, 1349.99, '2023-10-30 12:31:23', '1234', 1, 1, 'ABCDEFGHIJ0123456789'),
  (2, 3, 1499.97, '2023-10-30 01:22:45', '5678', 2, 1, '9876543210JIHGFEDCBA');
  -- Add more invoices here

-- Insert data into the InvoiceItem table
INSERT INTO InvoiceProduct (invoiceId, productId, quantity, price)
VALUES
  (1, 1, 2, 499.99),
  (1, 2, 1, 349.99),
  (2, 1, 3, 499.99);
  -- Add more invoice items here

-- Insert data into the SaleProduct table
INSERT INTO Sale (saleId, productId, amount, startDate, endDate)
VALUES
  (1, 1, 100.00, '20231101', '20231231'),
  (2, 2, 50.00, '20231101', '20231231');
  -- Add more sale products here