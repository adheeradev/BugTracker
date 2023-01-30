CREATE PROCEDURE [dbo].[Get_Bug]
	@BugId INT
AS
	SELECT b.Id,
				   b.AssignedToUserId,
				   b.Title,
				   b.[Description],
				   b.OpenedDate,
				   b.StatusId,
				   wfs.[Status],
				   usr.[Name] AS AssignedToUserName
			FROM [Bug] b
			LEFT OUTER JOIN [WorkFlowStatus] wfs
			ON b.StatusId = wfs.Id
			LEFT OUTER JOIN [User] usr
			ON b.AssignedToUserId = usr.Id
			WHERE b.Id = @BugId
GO
