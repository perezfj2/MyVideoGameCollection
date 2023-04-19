CREATE TABLE [dbo].[VideoGameGenre] (
    [GameId]  INT NOT NULL,
    [GenreId] INT NOT NULL,
    CONSTRAINT [FK_VideoGameGenre_VideoGame] FOREIGN KEY ([GameId]) REFERENCES [dbo].[VideoGame] ([GameId]) ON DELETE CASCADE,
    CONSTRAINT [FK_VideoGameGenreTable_GenreTable] FOREIGN KEY ([GenreId]) REFERENCES [dbo].[Genre] ([GenreId])
);







