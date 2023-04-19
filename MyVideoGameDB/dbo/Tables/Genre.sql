CREATE TABLE [dbo].[Genre] (
    [GenreId]   INT           IDENTITY (1, 1) NOT NULL,
    [GenreName] VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_GenreTable] PRIMARY KEY CLUSTERED ([GenreId] ASC)
);

