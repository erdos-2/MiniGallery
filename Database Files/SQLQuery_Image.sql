CREATE DATABASE ImageStorageDB

USE ImageStorageDB

CREATE TABLE Pictures (
	[ID] INT PRIMARY KEY IDENTITY,
	[FilePath] NVARCHAR(100),
	[Image] IMAGE
);

SELECT * FROM Pictures