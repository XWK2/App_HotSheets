
-- exec GetPartNumberPricesInternal @IsActive = null

CREATE OR ALTER PROCEDURE [dbo].[GetPartNumberPricesInternal]	
	@IsActive			BIT = NULL
AS
BEGIN
	SELECT
		pnpi.Id,
		pnpi.CustomerId,
		pnpi.PartNumberInternalId,
		pnpi.UnitPrice,
		pnpi.Currency,
		pnpi.PublishDate,
		pnpi.IsActive
	FROM DensoPartNumberPricesInternal pnpi
	WHERE @IsActive IS NULL OR pnpi.IsActive = @IsActive
END
GO
