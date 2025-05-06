CREATE TABLE [dbo].[Transaction]
(
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [Description] NVARCHAR(MAX) NULL,
    [Type] INT NOT NULL,
    [Amount] DECIMAL(18,2) NOT NULL,
    [PaymentMethod] INT NOT NULL
);

DROP TABLE [Transaction]

SELECT * FROM [Transaction]