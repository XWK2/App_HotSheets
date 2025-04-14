
CREATE OR ALTER PROCEDURE [dbo].[GetDepartmentsByUser]
	@UserId	BIGINT
AS
BEGIN
	SELECT d.Id As DepartmentId,
		d.Name As DepartmentName,
		du.UserId,
		du.IsSupervisor
	FROM DensoDepartments d WITH (NOLOCK)
		INNER JOIN DensoDepartmentUsers du WITH (NOLOCK) on du.DepartmentId = d.Id
		INNER JOIN AbpUsers u WITH (NOLOCK) on u.Id = du.UserId AND u.IsActive = 1
	WHERE d.IsActive = 1
		AND u.Id = @UserId
END
GO
