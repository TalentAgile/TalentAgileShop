CREATE TABLE [dbo].[Countries] (
    [Id]   NVARCHAR (128) NOT NULL,
    [Name] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Countries] PRIMARY KEY CLUSTERED ([Id] ASC)
);

