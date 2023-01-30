CREATE PROCEDURE [dbo].[Update_User]
	@UserId INT,
	@Name VARCHAR(255)
AS
	UPDATE [User]
	SET [Name] = @Name
	WHERE Id = @UserId;

	SELECT Id, [Name]
	FROM [User]
	WHERE Id = @UserId;
GO
