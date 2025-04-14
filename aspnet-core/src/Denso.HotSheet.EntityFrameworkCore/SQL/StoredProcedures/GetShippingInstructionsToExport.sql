
-- exec GetHotSheetToExport @HotSheetShiptId=120034

CREATE OR ALTER   PROCEDURE [dbo].[GetHotSheetToExport]	
	@HotSheetShiptId	BIGINT
AS
BEGIN
	DECLARE @TotalContainers INT, @TotalNetWeight DECIMAL(10, 3), @TotalGrossWeight DECIMAL(10, 3)

	SELECT @TotalContainers = SUM(BoxQuantity),
		@TotalNetWeight = SUM(NetWeight),
		@TotalGrossWeight = SUM(GrossWeight)
	FROM DensoHotSheetShipPackaging
	WHERE HotSheetShiptId = @HotSheetShiptId

	SELECT si.Id AS SHFOL,	
		CAST(c.Name AS VARCHAR(30)) AS SHCAR1, -- Carrier Name
		CAST(st.Name AS VARCHAR(25)) AS SHTES1, -- Termino de Emarque
		CAST(pt.Name AS VARCHAR(25)) AS SHTEP1, -- Termino de Pago
		CAST(ISNULL(contact.ContactName, '') AS VARCHAR(39)) AS SHATTO, -- Contacto Person
		FORMAT(cust.DensoCustomerId, '00000000') AS SHSUNO, -- Company ID (SOLD TO)

		SUBSTRING(si.AdditionalExplanation, 1, 60) AS SHINF11, -- Reason part 1
		SUBSTRING(si.AdditionalExplanation, 61, 60) AS SHINF12, -- Reason part 2
		SUBSTRING(si.AdditionalExplanation, 121, 60) AS SHINF13, -- Reason part 3

		CAST(p.AddressLine1 AS VARCHAR(39)) AS SHDMX1, -- Configuraci�n Planta
		CAST(p.AddressLine2 AS VARCHAR(39)) AS SHDMX2,
		CAST(ISNULL(p.AddressLine3,'') AS VARCHAR(39)) AS SHDMX3,		
		'R.F.C. ' + CAST(ISNULL(p.RFC, '') AS VARCHAR(39)) AS SHDMX4,		
		
		CAST(ISNULL(p.AddressLine4,'') AS VARCHAR(39)) AS SHDMXR,		

		--FORMAT(cust.Id, '00') AS SHSHP,		
		FORMAT(custPlant.ShipToNumber, '00') AS SHSHP,
		CAST(cust.Name AS VARCHAR(39)) AS SHSOT1,

		CAST(cust.AddressLine1 AS VARCHAR(39)) AS SHSOT2,
		CAST(cust.AddressLine2 AS VARCHAR(39)) AS SHSOT3,
		CAST(cust.AddressLine3 AS VARCHAR(39)) AS SHSOT4,
		CAST(ISNULL(cust.TaxId, '') AS VARCHAR(39)) AS SHSOT5,
		
		CAST(custPlant.Name AS VARCHAR(39)) AS SHSHT1,
		CAST(custPlant.AddressLine1 AS VARCHAR(39)) AS SHSHT2,
		CAST(custPlant.AddressLine2 AS VARCHAR(39)) AS SHSHT3,
		CAST(custPlant.AddressLine3 AS VARCHAR(39)) AS SHSHT4,
		CAST(ISNULL(custPlant.TaxId, '') AS VARCHAR(39)) AS SHSHT5,
	
		@TotalContainers AS SHTOPA, -- Cantidad de contenedores
		@TotalNetWeight AS SHNEWT, -- Peso total Neto (todos los contenedores) decimal 10.3
		@TotalGrossWeight AS SHGRWT, -- Peso total Bruto (todos los contenedores) decimal 10.3

		CAST(DC.DensoCode AS VARCHAR(30)) AS SHAUX0,
		si.DocumentTypeId AS SHAUX2,

		UPPER( CAST ((RTRIM(u.Name) + ' ' + LTRIM(u.Surname)) AS VARCHAR(30) )) AS SHAUX3,				
		
		FORMAT(GETDATE(),'HHmmss') AS SHTIME,
		FORMAT(GETDATE(), 'yyyyMMdd') AS SHDATE,

		--CAST(ISNULL(RMANumber,'') as varchar(21)) as RMA,
		--CAST(ISNULL(BNotice,'') as  varchar(10)) as BNOTICE

		'RMA:' + CAST(ISNULL(RMANumber,'') as varchar(21)) + ' ' + 'BN:' + CAST(ISNULL(BNotice,'') as  varchar(10)) as SHDMX5
	FROM DensoHotSheet si		
		INNER JOIN AbpUsers u ON u.Id = si.CreatorUserId
		INNER JOIN DensoCarriers c ON c.Id = si.CarrierId
		INNER JOIN DensoServices s ON s.Id = si.ServiceId
		INNER JOIN DensoPlants p ON p.Id = si.PlantId
		INNER JOIN DensoCustomers cust ON cust.Id = si.CustomerId
		INNER JOIN DensoCustomerPlants custPlant ON custPlant.Id = si.CustomerPlantId		
		INNER JOIN DensoPaymentTerms pt ON pt.Id = si.PaymentTermId
		INNER JOIN DensoHotSheetTerms st ON st.Id = si.HotSheetTermId
		INNER JOIN DensoDepartments depto ON depto.Id = si.DepartmentId
		INNER JOIN DensoHotSheetReasons reason ON reason.Id = si.HotSheetReasonId		
		INNER JOIN DensoCurrencies DC ON DC.Code = si.Currency
		LEFT OUTER JOIN DensoCustomerPlantContacts contact ON contact.Id = si.CustomerPlantContactId		

	WHERE si.Id = @HotSheetShiptId
END
