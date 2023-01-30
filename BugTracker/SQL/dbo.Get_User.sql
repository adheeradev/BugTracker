CREATE PROCEDURE [dbo].[Get_User]
	@UserId INT	
AS
	SELECT Id, [Name]
	FROM [User]
	WHERE Id = @UserId;
GO
