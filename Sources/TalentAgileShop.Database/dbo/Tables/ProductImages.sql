CREATE TABLE [dbo].[ProductImages] (
    [Id]   INT             IDENTITY (1, 1) NOT NULL,
    [Data] VARBINARY (MAX) NULL,
    CONSTRAINT [PK_dbo.ProductImages] PRIMARY KEY CLUSTERED ([Id] ASC)
);

