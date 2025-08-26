CREATE DATABASE SalesDB;
GO

USE SalesDB;

CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY,          -- Auto-incrementing product ID
    Name NVARCHAR(100) NOT NULL,          -- Product name
    Price DECIMAL(10,2) NOT NULL,         -- Unit price
    Stock INT NOT NULL                    -- Current stock quantity
);

CREATE TABLE Sales (
    Id INT PRIMARY KEY IDENTITY,           -- Auto-incrementing sale ID
    ProductId INT NOT NULL,                -- FK to Products
    Quantity INT NOT NULL,                 -- Quantity sold
    SaleDate DATETIME NOT NULL DEFAULT GETDATE(), -- Timestamp of sale
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);