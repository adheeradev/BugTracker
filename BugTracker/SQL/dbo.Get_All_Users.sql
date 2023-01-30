CREATE PROCEDURE [dbo].[Get_All_Users]
AS
	SELECT Id, [Name]
	FROM [User]
GO
