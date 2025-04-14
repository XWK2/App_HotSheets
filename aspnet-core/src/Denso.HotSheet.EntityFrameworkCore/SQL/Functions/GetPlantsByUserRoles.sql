
CREATE OR ALTER FUNCTION dbo.GetPlantsByUserRoles(
   @UserId BIGINT   
)
RETURNS @PlantsByUser TABLE
(
  PlantId INT
)
AS
BEGIN
	DECLARE @TenantId INT = 1
	DECLARE @AdminRoleId INT, @ApproversRoleId INT, @StaffImpoExpoRoleId INT, @StaffAccountingRoleId INT

	SELECT @AdminRoleId = Id FROM AbpRoles WHERE TenantId = @TenantId AND Name = 'Admin'
	SELECT @StaffImpoExpoRoleId = Id FROM AbpRoles WHERE TenantId = @TenantId AND Name = 'StaffImpoExpo'
	SELECT @StaffAccountingRoleId = Id FROM AbpRoles WHERE TenantId = @TenantId AND Name = 'StaffAccounting'

	IF EXISTS(
		SELECT * FROM AbpUserRoles
		WHERE UserId = @UserId AND TenantId = @TenantId
			AND RoleId IN (@AdminRoleId, @StaffImpoExpoRoleId, @StaffAccountingRoleId)
	)
	BEGIN
		INSERT INTO @PlantsByUser(PlantId)
		SELECT PlantId
		FROM DensoPlantUsers
		WHERE UserId = @UserId AND IsSupervisor = 1
	END

	RETURN;
END
GO
