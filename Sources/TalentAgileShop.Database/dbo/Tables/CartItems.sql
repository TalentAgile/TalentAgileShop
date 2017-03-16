CREATE TABLE [dbo].[CartItems]
(
	[Cart_Id] varchar(50) NOT NULL,
	[Product_Id] NVARCHAR (128)  NOT NULL, 
	[Count] int NOT NULL
    CONSTRAINT [PK_Cart] PRIMARY KEY Clustered ([Cart_Id], [Product_Id]),
	CONSTRAINT [FK_dbo.Cart_dbo.Product_Id] FOREIGN KEY ([Product_Id]) REFERENCES [dbo].[Products] ([Id]),
)
