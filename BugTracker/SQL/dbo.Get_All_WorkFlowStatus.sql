CREATE PROCEDURE [dbo].[Get_All_WorkFlowStatus]
AS
	SELECT Id, [Status]
	FROM [WorkFlowStatus]