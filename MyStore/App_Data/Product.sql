CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [OwnerId] BIGINT NULL, 
    [UserId] BIGINT NULL, 
    [Title] NVARCHAR(50) NOT NULL, 
    [ShortDescription] NVARCHAR(500) NOT NULL, 
    [LongDescription] NVARCHAR(4000) NOT NULL, 
    [Date] SMALLDATETIME NOT NULL, 
    [Price] DECIMAL NOT NULL, 
    [Picture1] IMAGE NULL, 
    [Picture2] IMAGE NULL, 
    [Picture3] IMAGE NULL, 
    [State] INT NOT NULL
)
