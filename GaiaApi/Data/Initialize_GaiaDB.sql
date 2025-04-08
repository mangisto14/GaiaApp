Initialize_GaiaDB.sql

-- Create database
CREATE DATABASE GaiaDB;
GO

-- Use the new database
USE GaiaDB;
GO

-- Create Operations table
CREATE TABLE Operations (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FieldA FLOAT NOT NULL,
    FieldB FLOAT NOT NULL,
    Operation VARCHAR(50) NOT NULL,
    Result VARCHAR(255) NOT NULL,
    Timestamp DATETIME NOT NULL
);
GO

--גרסה עם בדיקה אם DB כבר קיים
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'GaiaDB')
BEGIN
    CREATE DATABASE GaiaDB;
END
GO

USE GaiaDB;
GO

IF OBJECT_ID(N'dbo.Operations', N'U') IS NULL
BEGIN
    CREATE TABLE Operations (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        FieldA FLOAT NOT NULL,
        FieldB FLOAT NOT NULL,
        Operation VARCHAR(50) NOT NULL,
        Result VARCHAR(255) NOT NULL,
        Timestamp DATETIME NOT NULL
    );
END
GO
