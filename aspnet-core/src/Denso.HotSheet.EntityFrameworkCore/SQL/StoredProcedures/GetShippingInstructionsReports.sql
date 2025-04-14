
-- exec GetHotSheetReports @PlantId = 1

CREATE OR ALTER PROCEDURE [dbo].[GetHotSheetReports]	
	@PlantId			INT = NULL,	
	@DepartmentId		INT = NULL,
	@AuthorId			BIGINT = NULL,
	@CarrierId			BIGINT = NULL,
	@ServiceId			BIGINT = NULL,
	@CustomerId			BIGINT = NULL,
	@HotSheetTermId		BIGINT = NULL,
	@StatusId			INT = NULL,
	@StartDate			DATE = NULL,
	@EndDate			DATE = NULL
AS
BEGIN
	SELECT vsi.*,
		CASE
			WHEN vsi.CreatorUserId = @AuthorId THEN 1
			ELSE 0
		END As IsOwner,
		dbo.GetLastHistoryDateBy(vsi.HotSheetShiptId, 'EmailNotificationSent') AS LastNotificationDate,
		DATEDIFF(day, ISNULL(dbo.GetLastHistoryDateBy(vsi.HotSheetShiptId, 'ApprovalRequested'), GETDATE()), GETDATE()) AS DaysLateApproval,
		dbo.GetLastHistoryDateBy(vsi.HotSheetShiptId, 'ApprovalRequested') AS SentForApprovalDate		
	FROM vwHotSheet vsi		
	WHERE (@PlantId IS NULL OR vsi.PlantId = @PlantId)		
		AND (@DepartmentId IS NULL OR vsi.DepartmentId = @DepartmentId)		
		AND (@AuthorId IS NULL OR vsi.CreatorUserId = @AuthorId)
		AND (@CarrierId IS NULL OR vsi.CarrierId = @CarrierId)
		AND (@ServiceId IS NULL OR vsi.ServiceId = @ServiceId)
		AND (@CustomerId IS NULL OR vsi.CustomerId = @CustomerId)
		AND (@HotSheetTermId IS NULL OR vsi.HotSheetTermId = @HotSheetTermId)
		AND (@StatusId IS NULL OR vsi.StatusId = @StatusId)
		AND (@StartDate IS NULL OR (CAST(vsi.CreationDate AS DATE) BETWEEN @StartDate AND @EndDate))
	ORDER BY vsi.HotSheetShiptId DESC
END
GO
