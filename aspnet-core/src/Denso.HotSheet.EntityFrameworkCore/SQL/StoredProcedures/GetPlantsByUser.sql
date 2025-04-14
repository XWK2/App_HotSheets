
-- EXEC GetPlantsByUser 2

CREATE OR ALTER PROCEDURE [dbo].[GetPlantsByUser]
	@UserId	BIGINT
AS
BEGIN
	SELECT p.Id As PlantId,
		p.Name As PlantName,
		pu.UserId,
		pu.IsSupervisor,
		p.AddressLine1,
		p.AddressLine2,
		p.AddressLine3,
		p.AddressLine4,
		p.RFC
	FROM DensoPlants p WITH (NOLOCK)
		INNER JOIN DensoPlantUsers pu WITH (NOLOCK) on pu.PlantId = p.Id
		INNER JOIN AbpUsers u WITH (NOLOCK) on u.Id = pu.UserId AND u.IsActive = 1
	WHERE p.IsActive = 1
		AND pu.UserId = @UserId
	ORDER BY p.Id
END
GO
