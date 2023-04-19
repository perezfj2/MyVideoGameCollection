CREATE TABLE [dbo].[VideoGame] (
    [GameId]          INT            IDENTITY (1, 1) NOT NULL,
    [GameTitle]       VARCHAR (250)  NOT NULL,
    [GameRating]      DECIMAL (18)   NOT NULL,
    [GameImageURL]    VARCHAR (200)  NULL,
    [LanguageId]      INT            NOT NULL,
    [PublisherId]     INT            NOT NULL,
    [GameSummary]     VARCHAR (2000) NOT NULL,
    [ReleaseDate]     DATETIME       NOT NULL,
    [PlayerModeId]    INT            NOT NULL,
    [ContentRatingId] INT            NOT NULL,
    CONSTRAINT [PK_VideoGameTable] PRIMARY KEY CLUSTERED ([GameId] ASC),
    CONSTRAINT [FK_VideoGame_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([LanguageId]),
    CONSTRAINT [FK_VideoGameTable_PlayerModeTable] FOREIGN KEY ([PlayerModeId]) REFERENCES [dbo].[PlayerMode] ([PlayerModeId]),
    CONSTRAINT [FK_VideoGameTable_PublisherTable] FOREIGN KEY ([PublisherId]) REFERENCES [dbo].[Publisher] ([PublisherId]) ON DELETE CASCADE
);







