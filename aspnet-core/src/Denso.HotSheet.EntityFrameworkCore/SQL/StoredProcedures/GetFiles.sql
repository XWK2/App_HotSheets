
-- EXEC GetFiles 'HotSheet', '150022'
CREATE OR ALTER PROCEDURE [dbo].[GetFiles]
	@EntityType	VARCHAR(100),
	@EntityIds	VARCHAR(MAX)
AS
BEGIN	
	SELECT f.*,
		u.Name + ' ' + IsNull(u.Surname, '') As UploadedBy
	FROM DensoFiles f
		INNER JOIN AbpUsers u ON u.Id = f.CreatorUserId
	WHERE f.EntityType = @EntityType
		AND f.EntityId IN (SELECT CAST(value AS BIGINT)
						FROM STRING_SPLIT(@EntityIds, ',') WHERE value IS NOT NULL)
END
GO
