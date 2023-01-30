CREATE PROCEDURE [dbo].[Update_Bug]
	@BugId INT,
	@Title NVARCHAR(255),
	@Description NVARCHAR(MAX) = NULL,
	@AssignedUserId INT = NULL,
	@StatusId INT = NULL,
	@OpenedDate DATETIME = NULL
AS	
	UPDATE [Bug]
	SET Title = @Title,
		[Description] = @Description,
		AssignedToUserId = @AssignedUserId,
		StatusId = @StatusId,
		OpenedDate = @OpenedDate
	WHERE Id = @BugId;

	SELECT Id,
		   Title,
		   [Description],
		   [AssignedToUserId],
		   [StatusId],
		   [OpenedDate]
	FROM [Bug]
	WHERE Id = @BugId;
GO