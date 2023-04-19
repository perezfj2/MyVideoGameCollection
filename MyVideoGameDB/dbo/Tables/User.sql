CREATE TABLE [dbo].[User] (
    [UserId]              INT           IDENTITY (1, 1) NOT NULL,
    [Email]               VARCHAR (150) NOT NULL,
    [FirstName]           VARCHAR (150) NOT NULL,
    [LastName]            VARCHAR (200) NOT NULL,
    [UserProfileImageURL] VARCHAR (300) NULL,
    [DateJoined]          DATETIME      NOT NULL,
    [Password]            VARCHAR (300) NOT NULL,
    [Salt]                VARCHAR (15)  NOT NULL,
    CONSTRAINT [PK_UserTable] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_UserTable_UserGamesTable] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserGames] ([UserId])
);

