CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NCHAR(200) NOT NULL, 
    [Price] DECIMAL(10, 2) NOT NULL, 
    [Count] INT NOT NULL
)
