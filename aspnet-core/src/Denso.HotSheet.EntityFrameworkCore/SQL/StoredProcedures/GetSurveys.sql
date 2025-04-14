
CREATE OR ALTER PROCEDURE [dbo].[GetSurveys]
	@UserId			BIGINT = NULL,
	@ShippingCode	VARCHAR(100) = NULL,
	@CreationDate	DATE = NULL,
	@Qualification	VARCHAR(100) = NULL
AS
BEGIN
	SELECT s.*,
		u.Name + ' ' + IsNull(u.Surname, '') CreatorFullName,
		CAST(si.Id AS VARCHAR(30)) + '-' + ISNULL(p.Sufix, '') As HotSheetFolio
	FROM DensoSurveys s
		INNER JOIN AbpUsers u ON u.Id = s.CreatorUserId
		INNER JOIN DensoHotSheet si ON si.Id = s.HotSheetShiptId
		INNER JOIN DensoPlants p ON p.Id = si.PlantId
	WHERE (@UserId IS NULL OR s.CreatorUserId = @UserId)
		AND (@ShippingCode IS NULL OR s.Id LIKE '%' + RTRIM(LTRIM(@ShippingCode)) + '%')
		AND (@CreationDate IS NULL OR CAST(s.CreationTime AS DATE) = @CreationDate)
		AND (@Qualification IS NULL OR s.AnswerQuestion1 = @Qualification)
	ORDER BY s.Id DESC
END
GO
