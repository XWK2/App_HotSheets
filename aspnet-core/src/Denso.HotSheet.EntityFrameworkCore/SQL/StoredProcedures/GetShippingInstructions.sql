
-- exec GetHotSheet @UserId=2, @IsTemplate=null
-- exec GetHotSheet @UserId=3
-- exec GetHotSheet @UserId=7

CREATE OR ALTER   PROCEDURE [dbo].[GetHotSheet]
	@UserId			BIGINT,
	@PlantId		BIGINT = NULL,
	@StatusId		INT = NULL,
	@IsTemplate		BIT = NULL
AS
BEGIN
	SELECT vsi.*,
		CASE
			WHEN vsi.CreatorUserId = @UserId THEN 1
			ELSE 0
		END As IsOwner,
		dbo.GetLastHistoryDateBy(vsi.HotSheetShiptId, 'EmailNotificationSent') AS LastNotificationDate,
		DATEDIFF(day, ISNULL(dbo.GetLastHistoryDateBy(vsi.HotSheetShiptId, 'ApprovalRequested'), GETDATE()), GETDATE()) AS DaysLateApproval,
		dbo.GetLastHistoryDateBy(vsi.HotSheetShiptId, 'ApprovalRequested')AS SentForApprovalDate,

		CASE
			WHEN vsi.StatusId = 5 THEN
				CASE
					WHEN vsi.ManagerIsApproved = 0 THEN 'Manager - ' + vsi.ManagerName
					WHEN vsi.IEStaffIsApproved = 0 AND vsi.ManagerIsApproved = 1 THEN 'Impo/Expo - ' + vsi.IEStaffName
					WHEN vsi.AccountingIsApproved = 0 AND vsi.IEStaffIsApproved = 1 THEN 'Accounting - ' +vsi.AccountingName
					ELSE ''
				END
		ELSE '' END As PendingApprover
	FROM vwHotSheet vsi -- View
	WHERE (
			vsi.CreatorUserId = @UserId --- Owner
			OR (vsi.ManagerApprovalId = @UserId -- Manager Approver
			OR vsi.AccountingApproverUserId = @UserId -- Accounting Staff
			OR vsi.IEStaffApproverUserId = @UserId) -- Impo/Expo Staff			
			OR vsi.PlantId IN (SELECT PlantId FROM dbo.GetPlantsByUserRoles(@UserId) WHERE @PlantId IS NULL OR PlantId = @PlantId)
			OR vsi.DepartmentId IN (SELECT DepartmentId FROM dbo.GetDepartmentsByUserRoles(@UserId))
		)
		AND (@StatusId IS NULL OR vsi.StatusId = @StatusId)
		AND (@IsTemplate IS NULL OR vsi.IsTemplate = @IsTemplate)
	ORDER BY vsi.HotSheetShiptId DESC
END

GO
