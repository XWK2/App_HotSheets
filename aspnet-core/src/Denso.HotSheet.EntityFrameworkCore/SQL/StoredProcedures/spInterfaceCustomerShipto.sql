
CREATE OR ALTER      PROCEDURE [dbo].[spInterfaceCustomerShipto]
	@iIdUser INT,
	@CustomerXML XML = NULL
AS
BEGIN
 SET NOCOUNT ON
-------------------------------------------------------------------------------
 -- Creation by:    Luis Hdez Hdez
 -- Creation Date:  20 de Julio del 2023
 -- Purpose: Save Data of CustomerShipto, processed by the interface
 -- Update: 03_04_2024, The insertion of records was optimized, since it did not insert more than one detail per client
 -------------------------------------------------------------------------------
	DECLARE @doc INT, @Counter INT, @CounterDel INT, @CounterUpd INT, @CounterIns INT
	SET @Counter = 0
	SET @CounterDel = 0
	SET @CounterUpd = 0
	SET @CounterIns = 0

	EXEC sp_xml_preparedocument @doc OUTPUT, @CustomerXML

	DECLARE @CustomersShipto TABLE 
	(
		Id						int identity(1,1)
		,NoCliente				varchar(100) 
		,NoShipto				varchar(100) 		
		,NombreClienteShipto	varchar(600) 
		,Direccion1				varchar(200) 
		,Direccion2				varchar(200) 
		,Direccion3				varchar(200) 
		,Direccion4				varchar(200) 
		,RFC					nvarchar(40) 
		,Estado					varchar(100) 
		,Pais					varchar(100) 
		,CP						varchar(100) 		
		,TaxID					varchar(100)
	)	 
	INSERT INTO @CustomersShipto
		   (
			 NoCliente				 
			,NoShipto				 		
			,NombreClienteShipto	 
			,Direccion1				 
			,Direccion2				 
			,Direccion3				 
			,Direccion4				 
			,RFC					 
			,Estado					 
			,Pais					 
			,CP						 		
			,TaxID					
		   )
	SELECT
		     NoCliente
			,NoShipto					 
			,RTRIM(LTRIM(NombreClienteShipto)) as NombreClienteShipto
			,RTRIM(LTRIM(Direccion1)) as Direccion1
			,RTRIM(LTRIM(Direccion2)) as Direccion2
			,RTRIM(LTRIM(Direccion3)) as  Direccion3
			,RTRIM(LTRIM(Direccion4)) as Direccion4
			,RTRIM(LTRIM(RFC)) as RFC				 
			,RTRIM(LTRIM(Estado)) as Estado			 
			,RTRIM(LTRIM(Pais)) as Pais			 
			,RTRIM(LTRIM(CP)) as CP				 			
			,TaxID
	FROM OPENXML(@doc,N'ArrayOfAS400CustomerShipto/AS400CustomerShipto',2)
	WITH
		( 
			 NoCliente				varchar(100) 
			,NoShipto				varchar(100) 		
			,NombreClienteShipto	varchar(600) 
			,Direccion1				varchar(200) 
			,Direccion2				varchar(200) 
			,Direccion3				varchar(200) 
			,Direccion4				varchar(200) 
			,RFC					nvarchar(40) 
			,Estado					varchar(100) 
			,Pais					varchar(100) 
			,CP						varchar(100) 		
			,TaxID					varchar(100)
		)
	SET @Counter = @@ROWCOUNT 
	EXEC sp_xml_removedocument @doc
		
	--Update the records that id exists with active
	UPDATE DC
	SET 
		DC.LastModifierUserId = @iIdUser,
		DC.LastModificationTime = GETDATE(),
		DC.[Name] =  RTRIM(LTRIM(CN.NombreClienteShipto)),		
		DC.RFC = (CASE WHEN RTRIM(LTRIM(CN.RFC)) != '' 
					  THEN SUBSTRING(RTRIM(LTRIM(CN.RFC)), 1,  (CASE WHEN LEN(CN.RFC) >= 20 THEN 20 ELSE LEN(CN.RFC)-1 END))
				      ELSE '' END),
		DC.AddressLine1 = (REPLACE(RTRIM(LTRIM(CN.Direccion1)) ,'''','')),
		DC.AddressLine2 = (REPLACE(RTRIM(LTRIM(CN.Direccion2)) ,'''','')),
		DC.AddressLine3 = (REPLACE(RTRIM(LTRIM(CN.Direccion3)) ,'''','')),
		DC.AddressLine4 = (REPLACE(RTRIM(LTRIM(CN.Direccion4)) ,'''','')),
		DC.[State] = CN.Estado,
		DC.Country = CN.Pais,
		DC.TaxID = CN.TaxID,
		DC.IsActive = 1
	FROM DensoCustomerPlants AS DC with (nolock)
	INNER JOIN DensoCustomers AS D with (nolock) ON D.Id = DC.CustomerId
	INNER JOIN @CustomersShipto AS CN ON CN.NoCliente = D.DensoCustomerId AND CN.NoShipto = DC.ShipToNumber
	WHERE D.DensoCustomerId = CN.NoCliente

	SET @CounterUpd = ISNULL(@@ROWCOUNT ,0 )
	
	DECLARE @Total int, @Count int 
	SET @Count = 1
	SET @Total = (SELECT count(*) FROM @CustomersShipto)
		
	WHILE (@Count <= @Total)
	BEGIN
		
		DECLARE @NoClieteSelected bigint, @NoShiptoSelected int, @IdClienteSelected bigint

		SELECT @NoClieteSelected = CAST(NoCliente AS bigint), 
			   @NoShiptoSelected = CAST(NoShipto AS int)
		FROM @CustomersShipto
		WHERE Id = @Count
						

		Select @IdClienteSelected = Id FROM DensoCustomers  with (nolock) WHERE DensoCustomerId = @NoClieteSelected		
		IF (@IdClienteSelected != NULL)
		BEGIN
			
			IF (NOT EXISTS( SELECT * FROM DensoCustomerPlants DCP with (nolock) 
							WHERE DCP.CustomerId = @IdClienteSelected AND  DCP.ShipToNumber = @NoShiptoSelected	))
			BEGIN						
			
					----Insert Plant Record that are new
					INSERT INTO DensoCustomerPlants
						(	
							CustomerId,
							ShipToNumber,
							[Name],				
							RFC,
							AddressLine1,
							AddressLine2,
							AddressLine3,
							AddressLine4,			
							IsActive,
							CreatorUserId,
							CreationTime,
							--TenantId,
							[State],
							Country,
							TaxID
						)
					SELECT 
						   @IdClienteSelected
						   ,CNEW.NoShipto
						   ,RTRIM(LTRIM(CNEW.NombreClienteShipto))		   
						   ,(CASE WHEN RTRIM(LTRIM(CNEW.RFC)) != '' 
									  THEN SUBSTRING(RTRIM(LTRIM(CNEW.RFC)), 1,  (CASE WHEN LEN(CNEW.RFC) >= 20 THEN 20 ELSE LEN(CNEW.RFC)-1 END))
									  ELSE '' END)
						   ,(REPLACE(RTRIM(LTRIM(CNEW.Direccion1)) ,'''',''))
						   ,(REPLACE(RTRIM(LTRIM(CNEW.Direccion2)) ,'''',''))
						   ,(REPLACE(RTRIM(LTRIM(CNEW.Direccion3)) ,'''',''))
						   ,(REPLACE(RTRIM(LTRIM(CNEW.Direccion4)) ,'''',''))		   
						   ,1
						   ,@iIdUser
						   ,GETDATE()
						   --,1
						   ,CNEW.Estado
						   ,CNEW.Pais
						   ,CNEW.TaxID
					FROM @CustomersShipto aS CNEW 	
					WHERE CNEW.Id = @Count

					SET @CounterIns = @CounterIns + 1
				
			   END	
		END
		
		SET @Count = @Count + 1

	END

	SET @CounterDel = (SELECT count(Id) FROM  DensoCustomerPlants WHERE IsActive = 0)	

	----Report Counters
	SELECT GETDATE(), @Counter, @CounterDel, @CounterUpd , @CounterIns
 	
	SET NOCOUNT OFF
END
