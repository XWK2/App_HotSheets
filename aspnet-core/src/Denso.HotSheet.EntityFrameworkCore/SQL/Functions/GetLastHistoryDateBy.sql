
CREATE OR ALTER FUNCTION dbo.GetLastHistoryDateBy(
   @ShippingInstuctionId BIGINT,
   @HistoryType VARCHAR(100)
)
RETURNS Datetime
AS
BEGIN
	DECLARE @LastHIstoryDate DATETIME
	DECLARE @HotSheetsHistory TABLE(HistoryDate DATETIME, RowNumber INT)

	INSERT INTO @HotSheetsHistory
	SELECT CreationTime, ROW_NUMBER() OVER(PARTITION BY h.HotSheetShiptId ORDER BY CreationTime DESC) AS RowNumber
	FROM DensoHotSheetHistory h WITH (NOLOCK)
	WHERE HotSheetShiptId = @ShippingInstuctionId
		AND HistoryType = @HistoryType

	SELECT @LastHIstoryDate = HistoryDate
	FROM @HotSheetsHistory
	WHERE RowNumber = 1

	RETURN @LastHIstoryDate
END
GO
