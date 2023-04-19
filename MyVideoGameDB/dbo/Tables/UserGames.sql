CREATE TABLE [dbo].[UserGames] (
    [UserId] INT IDENTITY (1, 1) NOT NULL,
    [GameId] INT NOT NULL,
    CONSTRAINT [PK_UserGamesTable] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_UserGamesTable_VideoGameTable] FOREIGN KEY ([GameId]) REFERENCES [dbo].[VideoGame] ([GameId])
);

