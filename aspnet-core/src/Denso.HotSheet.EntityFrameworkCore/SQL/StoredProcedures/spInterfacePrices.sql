
CREATE OR ALTER  PROCEDURE [dbo].[spInterfacePrices]
	@iIdUser INT,
	@PricesXML XML = NULL
AS
BEGIN
 SET NOCOUNT ON

 -------------------------------------------------------------------------------
 -- Creation by:    Luis Hdez Hdez
 -- Creation Date:  20 de Julio del 2023
 -- Purpose: Save Data of Prices, processed by the interface
 -------------------------------------------------------------------------------
	DECLARE @doc INT, @Counter INT, @CounterDel INT, @CounterUpd INT, @CounterIns INT
	EXEC sp_xml_preparedocument @doc OUTPUT, @PricesXML

	DECLARE @PricesFinal TABLE 
	(
		 NoCliente varchar(100)      
        ,NoParte varchar(100)      
        ,PrecioUnitario varchar(100)      
        ,Moneda varchar(100)      
        ,FechaPosteo varchar(100)      

	)	 

	DECLARE @Prices TABLE 
	(
		 NoCliente varchar(100)      
        ,NoParte varchar(100)      
        ,PrecioUnitario varchar(100)      
        ,Moneda varchar(100)      
        ,FechaPosteo varchar(100)      

	)	 

	INSERT INTO @Prices
		   (
				 NoCliente 
				,NoParte 
				,PrecioUnitario 
				,Moneda 
				,FechaPosteo 
		   )
	SELECT			
			 RTRIM(LTRIM(NoCliente)) as NoCliente
			,RTRIM(LTRIM(NoParte)) as NoParte
			,RTRIM(LTRIM(PrecioUnitario)) as PrecioUnitario
			,RTRIM(LTRIM(Moneda)) as Moneda
			,RTRIM(LTRIM(FechaPosteo)) as FechaPosteo

	FROM OPENXML(@doc,N'ArrayOfAS400Prices/AS400Prices',2)
	WITH
		( 
			 NoCliente varchar(100)      
			,NoParte varchar(100)      
			,PrecioUnitario varchar(100)      
			,Moneda varchar(100)      
			,FechaPosteo varchar(100)      
		)
	SET @Counter = @@ROWCOUNT 
	EXEC sp_xml_removedocument @doc

	INSERT INTO @PricesFinal (NoCliente,NoParte,PrecioUnitario,Moneda,FechaPosteo)
	SELECT NoCliente,NoParte,PrecioUnitario,Moneda,FechaPosteo 
	FROM  @Prices AS P 
	INNER JOIN DensoCustomers AS C ON  RTRIM(LTRIM(CAST(C.DensoCustomerId AS VARCHAR(100)))) = RTRIM(LTRIM(P.NoCliente))
	INNER JOIN DensoPartNumbers AS NI ON RTRIM(LTRIM((NI.Number))) = RTRIM(LTRIM(P.NoParte))

	--select * from @PricesFinal
	----Update all records to inactive
	--Update PN SET PN.IsActive = 0 FROM DensoPartNumberPricesInternal AS PN

	--Update the records that id exists with active
	UPDATE DP
	SET 
		DP.LastModifierUserId = @iIdUser,
		DP.LastModificationTime = GETDATE(),		
		DP.UnitPrice = PrecioUnitario,
		DP.Currency = Moneda,
		--DP.CustomerId = NoCliente,
		DP.PublishDate = CAST(FechaPosteo as date),
		DP.IsActive = 1
	FROM DensoPartNumberPricesInternal DP	
	INNER JOIN DensoPartNumbers DN ON DN.Id = DP.PartNumberInternalId
	INNER JOIN @PricesFinal PN ON RTRIM(LTRIM(DN.Number)) = RTRIM(LTRIM(PN.NoParte))	
	INNER JOIN DensoCustomers DC on RTRIM(LTRIM(CAST(DC.DensoCustomerId AS VARCHAR(100)))) = RTRIM(LTRIM(PN.NoCliente))
	WHERE DN.Number = PN.NoParte

	SET @CounterUpd = ISNULL(@@ROWCOUNT ,0 )

	----SET IDENTITY_INSERT dbo.DensoPartNumberPricesInternal ON;

	----Insert Parts Record that are new
	INSERT INTO DensoPartNumberPricesInternal
		(	
			PartNumberInternalId,
			UnitPrice,
			Currency,
			CustomerId,
			PublishDate,
			IsActive,
			CreatorUserId,
			CreationTime					
		)
	SELECT 			 
			DN.Id,						
			PNEW.PrecioUnitario,
			PNEW.Moneda,			
			DC.Id,
			CAST(PNEW.FechaPosteo as date),
		    1,
            @iIdUser,
		    GETDATE()		   
	FROM @PricesFinal aS PNEW 
	INNER JOIN DensoCustomers DC on DC.DensoCustomerId = PNEW.NoCliente
	INNER JOIN DensoPartNumbers AS DN ON RTRIM(LTRIM(DN.Number)) = RTRIM(LTRIM(PNEW.NoParte))	
	LEFT JOIN DensoPartNumberPricesInternal AS P ON P.PartNumberInternalId = DN.Id	
	--WHERE P.Id IS NULL AND DN.Id IS NULL

	----SET IDENTITY_INSERT dbo.DensoPartNumberPricesInternal OFF;

	SET @CounterIns = ISNULL(@@ROWCOUNT ,0 )

	SET @CounterDel = (SELECT count(Id) FROM  DensoPartNumberPricesInternal WHERE IsActive = 0)	

	--Report Counters
    SELECT GETDATE(), @Counter 'NoRows', @CounterDel 'Del', @CounterUpd 'Upd', @CounterIns 'Ins'
 	 	
 SET NOCOUNT OFF
END