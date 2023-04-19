CREATE TABLE [dbo].[VideoGameConsole] (
    [GameId]    INT NOT NULL,
    [ConsoleId] INT NOT NULL,
    CONSTRAINT [FK_VideoGameConsole_VideoGame] FOREIGN KEY ([GameId]) REFERENCES [dbo].[VideoGame] ([GameId]) ON DELETE CASCADE,
    CONSTRAINT [FK_VideoGameConsoleTable_ConsoleTable] FOREIGN KEY ([ConsoleId]) REFERENCES [dbo].[Console] ([ConsoleId])
);







