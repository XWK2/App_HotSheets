
-- EXEC GetUsersByCurrentUser 3
CREATE OR ALTER PROCEDURE [dbo].[GetUsersByCurrentUser]
	@UserId	BIGINT
AS
BEGIN
	DECLARE @DepartmentsByUser TABLE(DepartmentId INT)
	DECLARE @UsersByDepartments TABLE(DepartmentId INT, UserId BIGINT)

	INSERT INTO @DepartmentsByUser(DepartmentId)
	SELECT DepartmentId
	FROM DensoDepartmentUsers
	WHERE UserId = @UserId

	INSERT INTO @UsersByDepartments (DepartmentId, UserId)
	SELECT du.DepartmentId, du.UserId
	FROM DensoDepartmentUsers du
		INNER JOIN DensoDepartments d ON d.Id = du.DepartmentId AND d.IsActive = 1
	WHERE du.DepartmentId IN (SELECT DepartmentId FROM @DepartmentsByUser)

	SELECT u.Id As UserId,
		CASE 
			WHEN e.DensoEmployeeId IS NULL THEN u.Name + ' ' + IsNull(u.Surname, '')
			ELSE CAST(e.DensoEmployeeId AS VARCHAR(50)) + ' - ' + u.Name + ' ' + IsNull(u.Surname, '')
		END  As FullName,
		STRING_AGG( ISNULL(d.Id, ''), ',') As DepartmentIds
	FROM AbpUsers u
		INNER JOIN @UsersByDepartments ud ON ud.UserId = u.Id AND u.IsActive = 1
		INNER JOIN DensoDepartments d ON d.Id = ud.DepartmentId AND d.IsActive = 1
		LEFT OUTER JOIN DensoEmployees e ON e.Id = u.EmployeeId
	GROUP BY u.Id, u.Name, u.Surname, e.DensoEmployeeId
END
GO
