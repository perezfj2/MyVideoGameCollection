/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
SET IDENTITY_INSERT [dbo].[Console] ON 
GO
INSERT [dbo].[Console] ([ConsoleId], [ConsoleName], [ConsoleImageURL]) VALUES (1, N'Testing', NULL)
GO
INSERT [dbo].[Console] ([ConsoleId], [ConsoleName], [ConsoleImageURL]) VALUES (2, N'Wii', NULL)
GO
INSERT [dbo].[Console] ([ConsoleId], [ConsoleName], [ConsoleImageURL]) VALUES (1002, N'Wii', N'https://www.pwnedgames.co.za/images/stories/virtuemart/product/nintendo_wii_console_family_edition_black.png')
GO
SET IDENTITY_INSERT [dbo].[Console] OFF
GO
SET IDENTITY_INSERT [dbo].[Publisher] ON 
GO
INSERT [dbo].[Publisher] ([PublisherId], [PublisherName], [PublisherBio], [PublisherLogoURL], [PublisherWebsite]) VALUES (1, N'Sega', NULL, N'https://1000logos.net/wp-content/uploads/2021/05/Sega-logo.png', N'https://www.sega.com/')
GO
SET IDENTITY_INSERT [dbo].[Publisher] OFF
GO
