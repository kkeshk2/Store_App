DROP TABLE IF EXISTS SaleProduct;
DROP TABLE IF EXISTS SaleCategory;
DROP TABLE IF EXISTS InvoiceItem;
DROP TABLE IF EXISTS Invoice;
DROP TABLE IF EXISTS CartProduct;
DROP TABLE IF EXISTS Cart;
DROP TABLE IF EXISTS Address;
DROP TABLE IF EXISTS Account;
DROP TABLE IF EXISTS ProductImage;
DROP TABLE IF EXISTS Product;

CREATE TABLE Product(
	productId int PRIMARY KEY IDENTITY(1, 1),
	productName varchar(128) NOT NULL,
	productPrice decimal(19, 2) NOT NULL,
	productManufacturer varchar(128) NOT NULL,
	productRating decimal(9, 2) NOT NULL,
	productDescription varchar(1024) NOT NULL,
	productCategory varchar(32) NOT NULL,
	productLength decimal(19, 2) NOT NULL,
	productWidth decimal(19, 2) NOT NULL,
	productHeight decimal(19, 2) NOT NULL,
	productWeight decimal(19, 2) NOT NULL,
	productSKU varchar(32) NOT NULL,
	productImageLocation varchar(256) NOT NULL 
);


CREATE TABLE Account(
	accountId int PRIMARY KEY IDENTITY(1, 1),
	accountEmail varchar(128) NOT NULL,
	accountPassword varchar(128) NOT NULL,
	accountName varchar(128) NOT NULL
);

CREATE TABLE Address(
	addressId int PRIMARY KEY IDENTITY(1, 1),
	addressName varchar(128) NOT NULL,
	addressLine1 varchar(128) NOT NULL,
	addressLine2 varchar(128),
	addressCity varchar(128) NOT NULL,
	addressState char(2) NOT NULL,
	addressZip int NOT NULL
);

CREATE TABLE Cart(
	cartId int PRIMARY KEY IDENTITY(1, 1),
	accountId int,
	FOREIGN KEY (accountId) REFERENCES Account(AccountId)
);

CREATE TABLE CartProduct(
	cartProductId int PRIMARY KEY IDENTITY(1, 1),
	cartId int NOT NULL,
	productId int NOT NULL,
	cartProductQuantity int NOT NULL,
	FOREIGN KEY (cartId) REFERENCES Cart(cartId),
	FOREIGN KEY (productId) REFERENCES Product(productId)
);

CREATE TABLE Invoice(
	invoiceId int PRIMARY KEY IDENTITY(1, 1),
	accountId int,
	billingAddressId int NOT NULL,
	shippingAddressId int NOT NULL,
	invoiceDate int NOT NULL,
	invoiceCreditCardLast4 int NOT NULL,
	FOREIGN KEY (accountId) REFERENCES Account(AccountId),
	FOREIGN KEY (billingAddressID) REFERENCES Address(addressId),
	FOREIGN KEY (shippingAddressID) REFERENCES Address(addressId)
);

CREATE TABLE InvoiceItem(
	invoiceItemId int PRIMARY KEY IDENTITY(1, 1),
	invoiceId int NOT NULL,
	productId int NOT NULL,
	InvoiceItemQuantity int NOT NULL,
	FOREIGN KEY (invoiceId) REFERENCES Invoice(invoiceId),
	FOREIGN KEY (productId) REFERENCES Product(productId)
);

CREATE TABLE SaleCategory(
	saleId int NOT NULL,
	productCategory varchar(32) NOT NULL,
	saleAmount decimal(9,2) NOT NULL,
	saleStartDate date NOT NULL,
	saleEndDate date NOT NULL
);

CREATE TABLE SaleProduct(
	saleId int NOT NULL,
	productId int NOT NULL,
	saleAmount decimal(19,2) NOT NULL,
	saleStartDate date NOT NULL,
	saleEndDate date NOT NULL
);

-- Insert data into the Product table
INSERT INTO Product (productName, productPrice, productManufacturer, productRating, productDescription, productCategory, productLength, productWidth, productHeight, productWeight, productSKU, productImageLocation)
VALUES
  ('NVIDIA GeForce RTX 2070', 499.99, 'NVIDIA', 4.8, 'High-performance graphics card.', 'Graphics Card', 10.0, 4.5, 1.5, 1.0, 'RTX2070', '/rtx2070_image.jpg'),
  ('Intel Core i7-9700K', 349.99, 'Intel', 4.7, 'Powerful processor for gaming and multitasking.', 'Processor', 5.0, 5.0, 1.5, 0.8, 'i79700k', '/i79700k_image.jpg'),
  ('NZXT Kraken X63 RGB', 149.99, 'NZXT', 4.6, 'Advanced liquid cooling solution with customizable RGB lighting.', 'Cooling', 15.0, 7.0, 2.0, 1.5, 'KrakenX63', '/kraken_x63_image.jpg');



-- Insert data into the Account table
INSERT INTO Account (accountEmail, accountPassword, accountName)
VALUES
  ('user1@example.com', 'pass1', 'User 1'),
  ('user2@example.com', 'pass2', 'User 2');
  -- Add more accounts here

-- Insert data into the Address table
INSERT INTO Address (addressName, addressLine1, addressLine2, addressCity, addressState, addressZip)
VALUES
  ('Home Address', '123 Main St', 'Apt 4B', 'City A', 'CA', 12345),
  ('Work Address', '456 Elm St', NULL, 'City B', 'NY', 54321);
  -- Add more addresses here

-- Insert data into the Cart table
INSERT INTO Cart (accountId)
VALUES
  (1),
  (2);
  -- Add more cart records here

-- Insert data into the CartProduct table
INSERT INTO CartProduct (cartId, productId, cartProductQuantity)
VALUES
  (1, 1, 2),
  (1, 2, 1),
  (2, 1, 3);
  -- Add more cart items here

-- Insert data into the Invoice table
INSERT INTO Invoice (accountId, billingAddressId, shippingAddressId, invoiceDate, invoiceCreditCardLast4)
VALUES
  (1, 1, 2, 20231030, 1234),
  (2, 2, 1, 20231030, 5678);
  -- Add more invoices here

-- Insert data into the InvoiceItem table
INSERT INTO InvoiceItem (invoiceId, productId, InvoiceItemQuantity)
VALUES
  (1, 1, 2),
  (1, 2, 1),
  (2, 1, 3);
  -- Add more invoice items here

-- Insert data into the SaleCategory table
INSERT INTO SaleCategory (saleId, productCategory, saleAmount, saleStartDate, saleEndDate)
VALUES
  (1, 'Category A', 0.10, '20231001', '20231015'),
  (2, 'Category B', 0.15, '20231005', '20231020');
  -- Add more sale categories here

-- Insert data into the SaleProduct table
INSERT INTO SaleProduct (saleId, productId, saleAmount, saleStartDate, saleEndDate)
VALUES
  (1, 1, 0.20, '20231002', '20231012'),
  (2, 2, 0.25, '20231006', '20231018');
  -- Add more sale products here