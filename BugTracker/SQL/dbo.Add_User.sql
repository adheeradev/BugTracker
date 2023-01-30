CREATE PROCEDURE [dbo].[Add_User]
	@Name VARCHAR(255)
AS
	DECLARE @NewId INT;
	INSERT [User]([Name]) VALUES(@Name)
	SELECT @NewId = SCOPE_IDENTITY();

	SELECT Id, [Name]
	FROM [User]
	WHERE Id = @NewId;
GO
