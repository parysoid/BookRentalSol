CREATE TABLE [dbo].[Records]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [RecordNumber] INT NOT NULL, 
    [BookId] INT NOT NULL FOREIGN KEY REFERENCES Book([Id]), 
    [RentStart] NVARCHAR(50) NOT NULL, 
    [RentEnd] NVARCHAR(50) NULL, 
    [Invoiced] TINYINT NOT NULL DEFAULT 0, 
    [InvoicedDate] NVARCHAR(50) NULL
)
