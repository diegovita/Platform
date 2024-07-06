USE master;
GO

IF NOT EXISTS (
    SELECT [name] FROM sys.databases WHERE [name] = N'BloggingPlatform'
)
BEGIN
    CREATE DATABASE BloggingPlatform;
END
GO

USE BloggingPlatform;
GO

IF NOT EXISTS (
    SELECT * FROM sys.tables WHERE [name] = N'Users'
)
BEGIN
    CREATE TABLE Users (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(100) NOT NULL,
        Password NVARCHAR(100) NOT NULL
    );
	
	INSERT INTO Users (Username, Password) VALUES ('Blogging', 'Platform');
END
GO
