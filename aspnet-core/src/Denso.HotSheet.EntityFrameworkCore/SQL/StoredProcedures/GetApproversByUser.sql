
-- EXEC GetApproversByUser 3
CREATE OR ALTER PROCEDURE [dbo].[GetApproversByUser]
	@UserId	BIGINT
AS
BEGIN
	DECLARE @DepartmentsByUser TABLE(DepartmentId INT)
	DECLARE @UsersByDepartments TABLE(DepartmentId INT, UserId BIGINT)

	INSERT INTO @DepartmentsByUser(DepartmentId)
	SELECT DepartmentId
	FROM DensoDepartmentUsers WITH (NOLOCK)
	WHERE UserId = @UserId

	INSERT INTO @UsersByDepartments (DepartmentId, UserId)
	SELECT du.DepartmentId, du.UserId
	FROM DensoDepartmentUsers du WITH (NOLOCK)
		INNER JOIN DensoDepartments d ON d.Id = du.DepartmentId AND d.IsActive = 1
	WHERE du.DepartmentId IN (SELECT DepartmentId FROM @DepartmentsByUser)
		AND du.IsSupervisor = 1

	SELECT u.Id As UserId,
		u.Name + ' ' + IsNull(u.Surname, '') As ApproverName,
		u.Name + ' ' + IsNull(u.Surname, '') As FullName,
		ud.DepartmentId,
		CASE
			WHEN u.EmployeeId IS NOT NULL THEN
				CAST(e.DensoEmployeeId AS VARCHAR(100)) + ' - ' + u.Name + ' ' + IsNull(u.Surname, '') 
			ELSE + u.Name + ' ' + IsNull(u.Surname, '')
		END AS DensoFullName
	FROM AbpUsers u WITH (NOLOCK)
		INNER JOIN @UsersByDepartments ud ON ud.UserId = u.Id AND u.IsActive = 1		
		LEFT OUTER JOIN DensoEmployees e WITH (NOLOCK) ON e.Id = u.EmployeeId
END
GO
