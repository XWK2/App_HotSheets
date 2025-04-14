
-- EXEC GetPartNumbersForSelect null, null

CREATE OR ALTER PROCEDURE [dbo].[GetPartNumbersForSelect]
	@IsActive				BIT = NULL,
	@HotSheetShiptId	BIGINT = NULL
AS
BEGIN
	-- Update:2 Febrero 2024 Luis Hdez Hdez, show all part numbers.
	-- Change 2-26-2024: Rollback to original sp
	-- Change 17-12-2024: Union from DensoPartNumbers and DensoPartNumbersInternal no duplicates
	-- Change 30-01-2025: LHH: se agrega el valor 1 default isInternal en el 3 query donde esta el right join internal
	-- Change 20-02-2025: LHH; se agrega la busqueda sobre ParNumberInternal en el Where final para que busque tambien sobre partes internal de miselaneos.
    -------------------------------------------------------------------------------

	--QUERY ORIGINAL COMENTADO

	--SELECT pn.Id,
	--	pn.Number,
	--	pn.Description,
	--	pn.DescriptionSpanish,
	--	pn.Number + ' - ' + ISNUll(pn.Description, '') + ' / ' + ISNUll(pn.DescriptionSpanish, '') As FullNumber,
	--	pn.OriginCountryId,
	--	0 As IsInternal,
	--	CAST(pn.Id AS VARCHAR(100)) + '-0' AS NumberValue,
	--	pn.UnitMeasureId,
	--	pni.Price As UnitPriceInternal,
	--	pn.ProductCodeSATId,
	--	pcSat.Code + ' - ' + pcSat.Description AS ProductSATCode
	--FROM DensoPartNumbers pn WITH (NOLOCK)
	--	LEFT OUTER JOIN DensoPartNumbersInternal pni WITH (NOLOCK) ON pni.Number =  pn.Number
	--	LEFT OUTER JOIN DensoProductCodesSAT pcSat WITH (NOLOCK) ON pcSat.Id = pn.ProductCodeSATId
	--WHERE @IsActive IS NULL OR pn.IsActive = @IsActive
	--	AND (@HotSheetShiptId IS NULL
	--		OR pn.Id IN (SELECT prods.PartNumberId
	--					FROM DensoHotSheetShipProducts prods
	--					WHERE prods.HotSheetShiptId = @HotSheetShiptId))


	-- MODIFICACION: 17-12-2024
	-- NUEVO QUERY.
	---AHORA SIN DUPLICAR con los 3 join son 85,925 con inactivos y activos son 85,903	

	SELECT * FROM (
	--son 85,903 considerando los que tengan o no precios en internal.
	--son 83,177 registros activos  y con inactivos
	--son 83,199 registros solo los que no coinciden con los miselaneos  se van con 0 en precios.
	--con left join ahora los de DensoPartNumbers que no existan en DensoPartNumbersInternal (Miselaneos) por obias razones no tienen precio y se establece 0
	SELECT * FROM(
	SELECT 
		pn.Id,
		pn.Number,		
		pn.Description,
		pn.DescriptionSpanish,
		pn.Number + ' - ' + ISNUll(pn.Description, '') + ' / ' + ISNUll(pn.DescriptionSpanish, '') As FullNumber,
		pn.OriginCountryId,
		0 As IsInternal,
		CAST(pn.Id AS VARCHAR(100)) + '-0' AS NumberValue,
		pn.UnitMeasureId,
		0 As UnitPriceInternal,
		pn.ProductCodeSATId
		,pcSat.Code + ' - ' + pcSat.Description AS ProductSATCode
	FROM DensoPartNumbers pn WITH (NOLOCK)
		LEFT JOIN DensoPartNumbersInternal pni WITH (NOLOCK) ON pni.Number =  pn.Number
		INNER JOIN DensoProductCodesSAT pcSat WITH (NOLOCK) ON pcSat.Id = pn.ProductCodeSATId
	WHERE  pni.Number IS NULL  AND pn.IsActive = 1  --AND pn.Number = 'YAGI-LS9L'
	)QUERY 	
	GROUP BY Id, Number,Description, DescriptionSpanish, FullNumber, OriginCountryId, IsInternal, 
	NumberValue, UnitMeasureId,UnitPriceInternal,  ProductCodeSATId , ProductSATCode
	UNION ALL
	---con inner join 2059 registros sin duplicados solo los que existan en DensoPartNumbers.
	SELECT * FROM(
	SELECT 
		pn.Id,
		pn.Number,		
		pn.Description,
		pn.DescriptionSpanish,
		pn.Number + ' - ' + ISNUll(pn.Description, '') + ' / ' + ISNUll(pn.DescriptionSpanish, '') As FullNumber,
		pn.OriginCountryId,
		0 As IsInternal,
		CAST(pn.Id AS VARCHAR(100)) + '-0' AS NumberValue,
		pn.UnitMeasureId,
		pni.Price As UnitPriceInternal,
		pn.ProductCodeSATId,
		pcSat.Code + ' - ' + pcSat.Description AS ProductSATCode
	FROM DensoPartNumbers pn WITH (NOLOCK)
		INNER JOIN DensoPartNumbersInternal pni WITH (NOLOCK) ON pni.Number =  pn.Number
		INNER JOIN DensoProductCodesSAT pcSat WITH (NOLOCK) ON pcSat.Id = pn.ProductCodeSATId
	WHERE pn.IsActive = 1  --AND pn.Number = 'YAGI-LS9L'
	)QUERY 
	GROUP BY Id, Number,Description, DescriptionSpanish, FullNumber, OriginCountryId, IsInternal, 
	NumberValue, UnitMeasureId,UnitPriceInternal,  ProductCodeSATId, ProductSATCode
	UNION ALL
	--son 667 registros activos de miselaneos 
	----ahora los miselaneos DensoPartNumbersInternal que no existan en DensoPartNumbers
	SELECT * FROM(
	SELECT 
		pni.Id,
		RTRIM(LTRIM(pni.Number)) as Number,		
		pni.Description,
		pni.DescriptionSpanish,
		pni.Number + ' - ' + ISNUll(pni.Description, '') + ' / ' + ISNUll(pni.DescriptionSpanish, '') As FullNumber,
		pni.OriginCountryId,
		1 As IsInternal,
		CAST(pni.Id AS VARCHAR(100)) + '-1' AS NumberValue,
		pni.UnitMeasureId,
		pni.Price As UnitPriceInternal,
		pni.ProductCodeSATId
		,pcSat.Code + ' - ' + pcSat.Description AS ProductSATCode
	FROM DensoPartNumbers pn WITH (NOLOCK)
		RIGHT JOIN DensoPartNumbersInternal pni WITH (NOLOCK) ON pni.Number =  pn.Number
		INNER JOIN DensoProductCodesSAT pcSat WITH (NOLOCK) ON pcSat.Id = pni.ProductCodeSATId
	WHERE  pn.Number IS NULL --AND pni.IsActive = 1  --AND pn.Number = 'YAGI-LS9L'
	)QUERY 
	GROUP BY Id, Number,Description, DescriptionSpanish, FullNumber, OriginCountryId, IsInternal, 
	NumberValue, UnitMeasureId,UnitPriceInternal,  ProductCodeSATId , ProductSATCode
	)QUERY2
	--WHERE Number =  'YAGI-LS9L' --'G1YAHA04' ejemplo.
	WHERE (@HotSheetShiptId IS NULL
			OR Id IN (SELECT prods.PartNumberId
						FROM DensoHotSheetShipProducts prods
						WHERE prods.HotSheetShiptId = @HotSheetShiptId)
			OR Id IN (SELECT prodsInt.PartNumberInternalId
						FROM DensoHotSheetShipProducts prodsInt
						WHERE prodsInt.HotSheetShiptId = @HotSheetShiptId))

						
					



END
GO
