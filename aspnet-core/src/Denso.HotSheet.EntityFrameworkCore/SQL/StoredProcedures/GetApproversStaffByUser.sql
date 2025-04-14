
-- EXEC GetApproversStaffByUser 3
CREATE OR ALTER PROCEDURE [dbo].[GetApproversStaffByUser]
	@UserId	BIGINT
AS
BEGIN
	DECLARE @PlantsByUser TABLE(PlantId INT)
	DECLARE @UsersByPlants TABLE(PlantId INT, UserId BIGINT)

	INSERT INTO @PlantsByUser(PlantId)
	SELECT PlantId
	FROM DensoPlantUsers
	WHERE UserId = @UserId

	INSERT INTO @UsersByPlants (PlantId, UserId)
	SELECT pu.PlantId, pu.UserId
	FROM DensoPlantUsers pu
		INNER JOIN DensoPlants p ON p.Id = pu.PlantId AND p.IsActive = 1
	WHERE pu.PlantId IN (SELECT PlantId FROM @PlantsByUser)

	SELECT u.Id As UserId,
		u.Name + ' ' + IsNull(u.Surname, '') As ApproverName,
		u.Name + ' ' + IsNull(u.Surname, '') As FullName,
		ud.PlantId,
		p.Name As PlantName,
		ds.Type,
		ds.Id As StaffId,
		CASE
			WHEN u.EmployeeId IS NOT NULL THEN
				CAST(e.DensoEmployeeId AS VARCHAR(100)) + ' - ' + u.Name + ' ' + IsNull(u.Surname, '') 
			ELSE + u.Name + ' ' + IsNull(u.Surname, '')
		END AS DensoFullName
	FROM AbpUsers u
		INNER JOIN DensoStaff ds ON ds.UserId = u.Id AND ds.IsActive = 1
		INNER JOIN @UsersByPlants ud ON ud.UserId = u.Id AND u.IsActive = 1
		INNER JOIN DensoPlants p ON p.Id = ud.PlantId AND p.IsActive = 1
		LEFT OUTER JOIN DensoEmployees e ON e.Id = u.EmployeeId
END
GO
