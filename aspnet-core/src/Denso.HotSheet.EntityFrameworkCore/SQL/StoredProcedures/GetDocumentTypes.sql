
-- exec GetDocumentTypes @IsActive = null, @Id = null

CREATE OR ALTER PROCEDURE [dbo].[GetDocumentTypes]	
	@IsActive	BIT = NULL,
	@Id			INT = NULL
AS
BEGIN
	SELECT
		dt.Id,
		dt.Name,
		dt.IsActive,
		CAST(dt.Id AS VARCHAR(20)) + ' - ' + dt.Name AS FullName
	FROM DensoDocumentTypes dt		
	WHERE (@IsActive IS NULL OR dt.IsActive = @IsActive)
		OR (@Id IS NULL OR dt.Id = @Id)
END
GO
