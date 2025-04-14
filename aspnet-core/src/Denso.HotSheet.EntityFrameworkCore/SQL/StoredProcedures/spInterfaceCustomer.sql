
CREATE OR ALTER  PROCEDURE [dbo].[spInterfaceCustomer]
	@iIdUser INT,
	@CustomerXML XML = NULL
AS
BEGIN
 SET NOCOUNT ON
-------------------------------------------------------------------------------
 -- Creation by:    Luis Hdez Hdez
 -- Creation Date:  20 de Julio del 2023
 -- Purpose: Save Data of Customers, processed by the interface
 -------------------------------------------------------------------------------
	DECLARE @doc INT, @Counter INT, @CounterDel INT, @CounterUpd INT, @CounterIns INT
	EXEC sp_xml_preparedocument @doc OUTPUT, @CustomerXML

	DECLARE @Customers TABLE 
	(
		 NoCliente		varchar(100) 
		,Tipo			varchar(100) 
		,NombreCliente	varchar(600) 
		,Direccion1		varchar(200) 
		,Direccion2		varchar(200) 
		,Direccion3		varchar(200) 
		,Direccion4		varchar(200) 
		,RFC			nvarchar(40) 
		,Estado			varchar(100) 
		,Pais			varchar(100) 
		,CP				varchar(100) 
		,NoClienteFacturar varchar(100)
		,TaxID			varchar(100)
		,Payment		int
	)	 
	INSERT INTO @Customers
		   (
			 NoCliente		 
			,Tipo			 
			,NombreCliente	 
			,Direccion1		 
			,Direccion2		 
			,Direccion3		 
			,Direccion4		 
			,RFC				 
			,Estado			 
			,Pais			 
			,CP				 
			,NoClienteFacturar 
			,TaxID	
			,Payment
		   )
	SELECT
		     NoCliente		 
			,RTRIM(LTRIM(Tipo)) as Tipo			 
			,RTRIM(LTRIM(NombreCliente)) as NombreCliente
			,RTRIM(LTRIM(Direccion1)) as Direccion1
			,RTRIM(LTRIM(Direccion2)) as Direccion2
			,RTRIM(LTRIM(Direccion3)) as  Direccion3
			,RTRIM(LTRIM(Direccion4)) as Direccion4
			,RTRIM(LTRIM(RFC)) as RFC				 
			,RTRIM(LTRIM(Estado)) as Estado			 
			,RTRIM(LTRIM(Pais)) as Pais			 
			,RTRIM(LTRIM(CP)) as CP				 
			,RTRIM(LTRIM(NoClienteFacturar)) as NoClienteFacturar 
			,TaxID
			,Payment
	FROM OPENXML(@doc,N'ArrayOfAS400Customer/AS400Customer',2)
	WITH
		( 
			 NoCliente		varchar(100) 
			,Tipo			varchar(100) 
			,NombreCliente	varchar(600) 
			,Direccion1		varchar(200) 
			,Direccion2		varchar(200) 
			,Direccion3		varchar(200) 
			,Direccion4		varchar(200) 
			,RFC			nvarchar(40) 
			,Estado			varchar(100) 
			,Pais			varchar(100) 
			,CP				varchar(100) 
			,NoClienteFacturar varchar(100)
			,TaxID			varchar(100)
			,Payment		int
		)
	SET @Counter = @@ROWCOUNT 
	EXEC sp_xml_removedocument @doc

	--Update all records to inactive
	--Update P 
	--SET P.IsActive = 0 FROM DensoPlants AS P

	--Update the records that id exists with active
	UPDATE DC
	SET 
		DC.LastModifierUserId = @iIdUser,
		DC.LastModificationTime = GETDATE(),
		DC.[Name] =  RTRIM(LTRIM(CN.NombreCliente)),
		DC.[Contact] = '',
		DC.RFC = (CASE WHEN RTRIM(LTRIM(CN.RFC)) != '' 
					  THEN SUBSTRING(RTRIM(LTRIM(CN.RFC)), 1,  (CASE WHEN LEN(CN.RFC) >= 20 THEN 20 ELSE LEN(CN.RFC)-1 END))
				      ELSE '' END),
	    DC.[State] = CN.Estado,
		DC.Country = CN.Pais,
		DC.ZipCode = CN.CP,
		DC.[Type] = CN.Tipo,
		DC.AddressLine1 = (REPLACE(RTRIM(LTRIM(CN.Direccion1)) ,'''','')),
		DC.AddressLine2 = (REPLACE(RTRIM(LTRIM(CN.Direccion2)) ,'''','')),
		DC.AddressLine3 = (REPLACE(RTRIM(LTRIM(CN.Direccion3)) ,'''','')),
		DC.AddressLine4 = (REPLACE(RTRIM(LTRIM(CN.Direccion4)) ,'''','')),
		DC.CustomerIdBilling = CN.NoClienteFacturar,
		DC.TaxId = CN.TaxID,
		DC.IsActive = 1,
		DC.Payment = CN.Payment
	FROM DensoCustomers DC
	INNER JOIN @Customers CN ON CN.NoCliente = DC.DensoCustomerId AND CN.Payment = DC.Payment
	WHERE DC.DensoCustomerId = CN.NoCliente

	SET @CounterUpd = ISNULL(@@ROWCOUNT ,0 )

	--SET IDENTITY_INSERT dbo.DensoCustomers ON;
	--Insert Plant Record that are new
	INSERT INTO DensoCustomers
		(	
			DensoCustomerId,
			[Name],	
			[Contact],
			RFC,
			[State],
			Country,
			ZipCode,
			[Type],
			AddressLine1,
			AddressLine2,
			AddressLine3,
			AddressLine4,
			CustomerIdBilling,
			TaxID,
			Payment,
			IsActive,
			CreatorUserId,
			CreationTime,
			TenantId
		)
	SELECT 
		    CNEW.NoCliente	   
		   ,RTRIM(LTRIM(CNEW.NombreCliente))
		   ,''
		   ,(CASE WHEN RTRIM(LTRIM(CNEW.RFC)) != '' 
					  THEN SUBSTRING(RTRIM(LTRIM(CNEW.RFC)), 1,  (CASE WHEN LEN(CNEW.RFC) >= 20 THEN 20 ELSE LEN(CNEW.RFC)-1 END))
				      ELSE '' END)
		   ,CNEW.Estado
		   ,CNEW.Pais
		   ,CNEW.CP
		   ,CNEW.Tipo
		   ,(REPLACE(RTRIM(LTRIM(CNEW.Direccion1)) ,'''',''))
		   ,(REPLACE(RTRIM(LTRIM(CNEW.Direccion2)) ,'''',''))
		   ,(REPLACE(RTRIM(LTRIM(CNEW.Direccion3)) ,'''',''))
		   ,(REPLACE(RTRIM(LTRIM(CNEW.Direccion4)) ,'''',''))
		   ,CNEW.NoClienteFacturar
		   ,CNEW.TaxID
		   ,CNEW.Payment
           ,1
           ,@iIdUser
		   ,GETDATE()
		   ,1
	FROM @Customers aS CNEW 
	LEFT JOIN DensoCustomers AS C ON C.DensoCustomerId = CNEW.NoCliente AND C.Payment = CNEW.Payment
	WHERE C.DensoCustomerId IS NULL	
	AND CNEW.NoCliente not in (SELECT NoCliente 
							  FROM @Customers
							   GROUP BY NoCliente 
							   HAVING COUNT(*)>1 )

	--las tres lineas de arriba en el where las deshabilite apenas.

	--AND CNEW.NoCliente NOT IN ( 100,10000,10800,1200,12000005,1210,1220,13001011,13001021,13001515,13002501,13002502,13002503,13004025,14002040,1500,15001251,15001275,15001281,15004015,1510,1540,1541,
	--1550,1581,1590,1591,178722,19000000,19100000,19100001,19100003,19200000,19400000,19600000,200,200177,20060,201,20600,24000000,240856,25000000,26000000,26000501,26002412,26014111,27000000,28000000,
	--2802,2803,2807,2900,29000000,300,30850,32000,33500,350,3800,3901,418,420,4700,4800,4801,4900,50015151,5010,5020,5030,5040,5050,6000,6100,6300,6500,67001035,80000586,80000587,80000588,80000589,80000590,
	--80000591,80000592,80000593,80000594,80000595,80000596,8110,8130,8140,8150,8200,8499,8530,8550,8599,9099, 900,099,99999906,99999908,99999910,
	--99999913,99999915,99999918,99999921,99999924,99999927,99999928,99999929)
	
	--NOTA Ing.Luis Hdez Hdez: revisar los duplicados porque tienen el mismo id.

	--SET IDENTITY_INSERT dbo.DensoCustomers OFF;

	SET @CounterIns = ISNULL(@@ROWCOUNT ,0 )

	SET @CounterDel = (SELECT count(Id) FROM  DensoCustomers WHERE IsActive = 0)

	--SET @CounterDel = @@ROWCOUNT 

	--Report Counters
    SELECT GETDATE(), @Counter, @CounterDel, @CounterUpd, @CounterIns
 	
 SET NOCOUNT OFF
END