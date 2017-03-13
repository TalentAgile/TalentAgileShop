CREATE TABLE [dbo].[Products] (
    [Id]          NVARCHAR (128)  NOT NULL,
    [Name]        NVARCHAR (MAX)  NULL,
    [Size]        INT             NOT NULL,
    [Price]       DECIMAL (18, 2) NOT NULL,
    [Description] NVARCHAR (MAX)  NULL,
    [Category_Id] NVARCHAR (128)  NULL,
    [Image_Id]    INT             NULL,
    [Origin_Id]   NVARCHAR (128)  NULL,
    CONSTRAINT [PK_dbo.Products] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Products_dbo.Categories_Category_Id] FOREIGN KEY ([Category_Id]) REFERENCES [dbo].[Categories] ([Id]),
    CONSTRAINT [FK_dbo.Products_dbo.Countries_Origin_Id] FOREIGN KEY ([Origin_Id]) REFERENCES [dbo].[Countries] ([Id]),
    CONSTRAINT [FK_dbo.Products_dbo.ProductImages_Image_Id] FOREIGN KEY ([Image_Id]) REFERENCES [dbo].[ProductImages] ([Id])
);

