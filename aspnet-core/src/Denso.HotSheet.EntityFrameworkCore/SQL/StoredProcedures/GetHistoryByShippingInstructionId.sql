
CREATE OR ALTER PROCEDURE [dbo].[GetHistoryByHotSheetShiptId]	
	@HotSheetShiptId	BIGINT
AS
BEGIN	
	SELECT h.Id,
		h.HotSheetShiptId,
		h.HistoryType,
		h.Comments,
		h.CreationTime,
		h.CreatorUserId,
		u.Name + ' ' + u.Surname as CreatorUserName
	FROM DensoHotSheetHistory h
		INNER JOIN DensoHotSheet si ON si.Id = h.HotSheetShiptId
		LEFT OUTER JOIN AbpUsers u ON u.Id = h.CreatorUserId
	WHERE h.HotSheetShiptId = @HotSheetShiptId
END
GO
