CREATE TABLE [dbo].[Language] (
    [LanguageId]   INT           IDENTITY (1, 1) NOT NULL,
    [LanguageName] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_LanguageTable] PRIMARY KEY CLUSTERED ([LanguageId] ASC)
);



