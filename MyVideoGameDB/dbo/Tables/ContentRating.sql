CREATE TABLE [dbo].[ContentRating] (
    [ContentRatingId]   INT           IDENTITY (1, 1) NOT NULL,
    [ContentRatingName] VARCHAR (250) NOT NULL,
    CONSTRAINT [PK_ContentRatingTable] PRIMARY KEY CLUSTERED ([ContentRatingId] ASC)
);

