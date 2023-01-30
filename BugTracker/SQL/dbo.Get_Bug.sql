CREATE PROCEDURE [dbo].[Get_Bug]
	@BugId INT
AS
	SELECT b.Id,
				   b.AssignedToUserId,
				   b.Title,
				   b.[Description],
				   b.OpenedDate,
				   b.StatusId
			FROM [Bug] b
			WHERE b.Id = @BugId