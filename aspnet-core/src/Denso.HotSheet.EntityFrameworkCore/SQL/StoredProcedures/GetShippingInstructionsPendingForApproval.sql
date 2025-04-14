
-- exec GetHotSheetPendingForApproval @UserId=7

CREATE OR ALTER PROCEDURE [dbo].[GetHotSheetPendingForApproval]
	@UserId		BIGINT
AS
BEGIN
	SELECT vsi.*,
		CASE
			WHEN vsi.CreatorUserId = @UserId THEN 1
			ELSE 0
		END As IsOwner,
		dbo.GetLastHistoryDateBy(vsi.HotSheetShiptId, 'EmailNotificationSent') AS LastNotificationDate,
		DATEDIFF(day, ISNULL(dbo.GetLastHistoryDateBy(vsi.HotSheetShiptId, 'ApprovalRequested'), GETDATE()), GETDATE()) AS DaysLateApproval,
		dbo.GetLastHistoryDateBy(vsi.HotSheetShiptId, 'ApprovalRequested')AS SentForApprovalDate
	FROM vwHotSheet vsi -- View
	WHERE ((vsi.ManagerApprovalId = @UserId AND ISNULL(vsi.ManagerIsApproved, 0) != 1) -- Manager Approver			
			OR (vsi.AccountingApproverUserId = @UserId AND ISNULL(vsi.AccountingIsApproved, 0) != 1 AND vsi.ManagerIsApproved = 1) -- Accounting Staff			
			OR (vsi.IEStaffApproverUserId = @UserId AND ISNULL(vsi.IEStaffIsApproved, 0) != 1 
				AND ((vsi.ManagerIsApproved = 1 AND vsi.AccountingIsApproved = 1)
						OR (vsi.ManagerIsApproved = 1 AND vsi.AccountingApprovalId IS NULL))) -- Impo/Expo Staff
		)
		AND vsi.StatusId = 5 -- Pending for Approval
	ORDER BY vsi.HotSheetShiptId DESC
END
GO
