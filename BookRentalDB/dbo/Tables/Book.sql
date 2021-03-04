CREATE TABLE [dbo].[Book](
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [BookId] INT NOT NULL, 
    [Title] NVARCHAR(250) NOT NULL,   
    [Author] NVARCHAR(150) NOT NULL ,
    [PricePerDay] INT NOT NULL, 
    [Availability] INT NOT NULL DEFAULT 1,
)
