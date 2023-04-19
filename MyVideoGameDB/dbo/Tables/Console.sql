CREATE TABLE [dbo].[Console] (
    [ConsoleId]       INT           IDENTITY (1, 1) NOT NULL,
    [ConsoleName]     VARCHAR (250) NOT NULL,
    [ConsoleImageURL] VARCHAR (250) NULL,
    CONSTRAINT [PK_ConsoleTable] PRIMARY KEY CLUSTERED ([ConsoleId] ASC)
);



