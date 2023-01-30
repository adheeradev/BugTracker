CREATE PROCEDURE [dbo].[Get_All_Bugs]
	@Status VARCHAR(50)	= NULL
AS
	IF @Status IS NOT NULL
		BEGIN
			DECLARE @StatusId INT;

			SELECT @StatusId = Id 
			FROM [WorkFlowStatus]
			WHERE [Status] = @Status;

			SELECT b.Id,
				   b.AssignedToUserId,
				   b.Title,
				   b.[Description],
				   b.OpenedDate,
				   b.StatusId,
				   wfs.[Status]
			FROM [Bug] b
			INNER JOIN [WorkFlowStatus] wfs
			ON b.StatusId = wfs.Id
			WHERE b.StatusId = @StatusId;
		END
	ELSE
		BEGIN
			SELECT b.Id,
				   b.AssignedToUserId,
				   b.Title,
				   b.[Description],
				   b.OpenedDate,
				   b.StatusId,
				   wfs.[Status]
			FROM [Bug] b
			LEFT OUTER JOIN [WorkFlowStatus] wfs
			ON b.StatusId = wfs.Id
		END	
GO