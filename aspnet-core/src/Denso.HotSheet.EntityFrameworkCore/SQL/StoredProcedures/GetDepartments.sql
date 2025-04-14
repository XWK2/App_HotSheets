
-- exec GetDepartments @IsActive = null

CREATE OR ALTER PROCEDURE [dbo].[GetDepartments]	
	@IsActive			BIT = NULL
AS
BEGIN
	SELECT
		d.Id,
		d.Name,
		d.IsActive,
		COUNT(du.UserId) AS TotalUsers,
		CAST(d.Id AS VARCHAR(20)) + ' - ' + d.Name AS FullName
	FROM DensoDepartments d
		LEFT OUTER JOIN DensoDepartmentUsers du ON du.DepartmentId = d.Id
	WHERE @IsActive IS NULL OR d.IsActive = @IsActive
	GROUP BY d.Id, d.Name, d.IsActive
END
GO
