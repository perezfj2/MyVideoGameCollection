CREATE TABLE [dbo].[Publisher] (
    [PublisherId]      INT            IDENTITY (1, 1) NOT NULL,
    [PublisherName]    VARCHAR (150)  NOT NULL,
    [PublisherBio]     VARCHAR (1000) NULL,
    [PublisherLogoURL] VARCHAR (150)  NULL,
    [PublisherWebsite] VARCHAR (200)  NULL,
    CONSTRAINT [PK_Publisher] PRIMARY KEY CLUSTERED ([PublisherId] ASC)
);



