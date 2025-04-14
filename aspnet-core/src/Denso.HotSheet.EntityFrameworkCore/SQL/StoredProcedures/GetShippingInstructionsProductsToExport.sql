
-- exec GetHotSheetShipProductsToExport @HotSheetShiptId=170117
-- exec GetHotSheetShipProductsToExport @HotSheetShiptId=170113

CREATE OR ALTER PROCEDURE [dbo].[GetHotSheetShipProductsToExport]	
	@HotSheetShiptId	BIGINT
AS
BEGIN
	SELECT si.Id AS SDFOL,
		CASE 
			WHEN siProd.PartNumberId IS NOT NULL THEN CAST(partNum.Number AS VARCHAR(25))
			ELSE CAST(partNumInt.Number AS VARCHAR(25))
		END AS SDCUIN,
		SUBSTRING(siProd.Description, 1, 40) AS SDIDE1,
		SUBSTRING(siProd.Description, 41, 40) AS SDIDE2,
		SUBSTRING(siProd.DescriptionSpanish, 1, 40) AS SDIDS1,
		SUBSTRING(siProd.DescriptionSpanish, 41, 40) AS SDIDS2,
		CASE 
			WHEN siProd.PartNumberId IS NOT NULL THEN CAST(partNum.Number AS VARCHAR(15))
			ELSE CAST(partNumInt.Number AS VARCHAR(15))
		END AS SDPRTN,
		CAST(siProd.Quantity AS INT) AS SDSHQY, 
		CAST(siProd.UnitPrice AS DECIMAL(12, 4)) AS SDPRIC,
		ISNULL(CAST(um.DensoCode AS VARCHAR(2)), '') AS SDUNM,
		CAST(ISNULL(siProd.PoNumber,'') AS VARCHAR(10)) AS SDCUPO, 
		CASE 
			WHEN siProd.PartNumberId IS NOT NULL THEN ISNULL(CAST(pcSAT.Code AS VARCHAR(8)), '')
			ELSE ISNULL(CAST(pcSATInt.Code AS VARCHAR(8)), '')
		END AS SDSATC,
		FORMAT(GETDATE(), 'HHmmss') AS SDTIME,
		FORMAT(GETDATE(), 'yyyyMMdd') AS SDDATE,
		CASE 
			WHEN siProd.PartNumberId IS NOT NULL 
				THEN CAST(ISNULL(partNumCountry.Name, ISNULL(partNumCountryById.Name, '')) AS VARCHAR(15))
			ELSE CAST(ISNULL(partNumInt.OriginCountry, '') AS VARCHAR(15))
		END AS SDORIG
	FROM DensoHotSheetShipProducts siProd
		INNER JOIN DensoHotSheet si ON si.Id = siProd.HotSheetShiptId
		LEFT OUTER JOIN DensoPartNumbers partNum ON partNum.Id = siProd.PartNumberId
		LEFT OUTER JOIN DensoPartNumbersInternal partNumInt ON partNumInt.Id = siProd.PartNumberInternalId
		LEFT OUTER JOIN DensoUnitMeasures um ON um.Id = siProd.UnitMeasureId
		LEFT OUTER JOIN DensoProductCodesSAT pcSAT ON pcSAT.Id = partNum.ProductCodeSATId
		LEFT OUTER JOIN DensoProductCodesSAT pcSATInt ON pcSATInt.Id = partNumInt.ProductCodeSATId
		LEFT OUTER JOIN DensoCountries partNumCountry ON partNumCountry.SegroveCode = partNum.OriginCountry
		LEFT OUTER JOIN DensoCountries partNumCountryById ON partNumCountryById.Id = partNum.OriginCountryId
	WHERE si.Id = @HotSheetShiptId
END
