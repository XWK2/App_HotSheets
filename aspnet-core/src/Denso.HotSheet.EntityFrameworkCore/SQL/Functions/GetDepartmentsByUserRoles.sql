
CREATE OR ALTER FUNCTION dbo.GetDepartmentsByUserRoles(
   @UserId	BIGINT
)
RETURNS @DepartmentsByUser TABLE
(
  DepartmentId INT
)
AS
BEGIN
	DECLARE @TenantId INT = 1
	DECLARE @ApproversRoleId INT

	SELECT @ApproversRoleId = Id FROM AbpRoles WHERE TenantId = @TenantId AND Name = 'Approvers'

	IF EXISTS(
		SELECT * FROM AbpUserRoles
		WHERE UserId = @UserId AND TenantId = @TenantId
			AND RoleId IN (@ApproversRoleId)
	)
	BEGIN
		INSERT INTO @DepartmentsByUser(DepartmentId)
		SELECT DepartmentId
		FROM DensoDepartmentUsers
		WHERE UserId = @UserId AND IsSupervisor = 1
	END

	RETURN;
END
GO
