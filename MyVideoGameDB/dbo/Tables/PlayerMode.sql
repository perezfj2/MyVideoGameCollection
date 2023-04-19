CREATE TABLE [dbo].[PlayerMode] (
    [PlayerModeId]   INT           IDENTITY (1, 1) NOT NULL,
    [PlayerModeName] VARCHAR (200) NULL,
    CONSTRAINT [PK_PlayerModeTable] PRIMARY KEY CLUSTERED ([PlayerModeId] ASC)
);

