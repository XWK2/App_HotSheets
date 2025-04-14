
-- exec GetHotSheetById @HotSheetShiptId=150022, @UserId = 2

CREATE OR ALTER PROCEDURE [dbo].[GetHotSheetById]	
	@HotSheetShiptId	BIGINT,
	@UserId					BIGINT = NULL
AS
BEGIN
	DECLARE @TenantId INT = 1
	DECLARE @AdminRoleId INT, @UserIsAdmin BIT = 0
	SELECT @AdminRoleId = Id FROM AbpRoles WITH (NOLOCK) WHERE TenantId = @TenantId AND Name = 'Admin'

	IF EXISTS(
		SELECT * FROM AbpUserRoles WITH (NOLOCK)
		WHERE UserId = @UserId AND TenantId = @TenantId
			AND RoleId = @AdminRoleId)
	BEGIN
		SET @UserIsAdmin = 1
	END

	SELECT vsi.*,
		CASE
			WHEN vsi.CreatorUserId = @UserId THEN 1
			ELSE 0
		END As IsOwner,
		CASE
			WHEN vsi.CreatorUserId = @UserId
				OR vsi.ManagerApprovalId = @UserId
				OR vsi.IEStaffApproverUserId = @UserId
				OR vsi.AccountingApproverUserId = @UserId
				OR @UserIsAdmin = 1 
			THEN 1
			ELSE 0
		END As CanUploadFiles		
	FROM vwHotSheet vsi -- View
	WHERE vsi.HotSheetShiptId = @HotSheetShiptId
END
GO
