
-- exec GetHotSheetReasons @IsActive = null, @Id = null

CREATE OR ALTER PROCEDURE [dbo].[GetHotSheetReasons]	
	@IsActive	BIT = NULL,
	@Id			INT = NULL
AS
BEGIN
	SELECT
		sr.Id,
		sr.Description,
		sr.BNoticeRMARequired,
		sr.PictureTechnicalInfoMakerModelSerialNumber,
		sr.AttachPurchaseOrder,
		sr.TechnicalInfoPicture,
		sr.AccountingApprovalRequired,
		sr.ExcludeTermOfPayment,
		sr.Remittence,
		sr.NoPayment,
		sr.IsActive
	FROM DensoHotSheetReasons sr	
	WHERE (@IsActive IS NULL OR sr.IsActive = @IsActive)
		OR (@Id IS NULL OR sr.Id = @Id)
END
GO
