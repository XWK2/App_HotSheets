
CREATE OR ALTER PROCEDURE [dbo].[UpdateOrInsertPartNumberXlsx]
	@iIdUser INT = 0,
	@PartsXML XML = NULL
AS
BEGIN
 SET NOCOUNT ON
 -------------------------------------------------------------------------------
 -- Creation by:    Luis Hdez Hdez
 -- Creation Date:  20 de Julio del 2023
 -- Purpose: Save part number information by loading an xlsx
 -------------------------------------------------------------------------------

	DECLARE @doc INT, @Counter INT, @CounterDel INT, @CounterUpd INT, @CounterIns INT
	EXEC sp_xml_preparedocument @doc OUTPUT, @PartsXML

	DECLARE @Parts TABLE 
	(
		 PartNumber varchar(100) 
        ,DescriptionInglish varchar(400)         
        ,DescriptionSpanish varchar(400)            
        ,UnitMeasureId varchar(20)      
        ,OriginCountryId varchar(2)        
        ,Fraction varchar(100)                
        ,Weight varchar(100)   
        ,ProductCodeSATId varchar(100)        
	)	 
	INSERT INTO @Parts
		   (
			 PartNumber 
			,DescriptionInglish 
			,DescriptionSpanish 
			,UnitMeasureId 
			,OriginCountryId 
			,Fraction 
			,Weight 
			,ProductCodeSATId
		   )
	SELECT
			 PartNumber 
			,DescriptionInglish 
			,DescriptionSpanish 
			,UnitMeasureId 
			,OriginCountryId 
			,Fraction 
			,Weight 
			,ProductCodeSATId

	FROM OPENXML(@doc,N'ArrayOfPartNumberXlsxDto/PartNumberXlsxDto',2)
	WITH
		( 
			 PartNumber varchar(100) 
			,DescriptionInglish varchar(400)         
			,DescriptionSpanish varchar(400)            
			,UnitMeasureId varchar(20)      
			,OriginCountryId varchar(2)        
			,Fraction varchar(100)                
			,Weight varchar(100)   
			,ProductCodeSATId varchar(100)        
		)
	SET @Counter = @@ROWCOUNT 
	EXEC sp_xml_removedocument @doc

	
	----Update all records to inactive
	Update PN SET PN.IsActive = 0 FROM DensoPartNumbers AS PN

	--Update the records that id exists with active
	UPDATE DC
	SET 
		DC.LastModifierUserId = @iIdUser,
		DC.LastModificationTime = GETDATE(),
		DC.Description = PN.DescriptionInglish,
		DC.[DescriptionSpanish] =  PN.DescriptionSpanish,
		DC.UnitMeasureId = ISNULL((SELECT TOP 1 UM.Id FROM DensoUnitMeasures UM WHERE  UM.SegroveCode = PN.UnitMeasureId),0),	
	    DC.Fraction = PN.Fraction,				
		DC.OriginCountryId = ISNULL((SELECT TOP 1 DC.Id FROM DensoCountries DC WHERE  DC.SegroveCode = PN.OriginCountryId),0),	
		--DC.OriginCountry = PN.OriginCountryId,
		DC.ProductCodeSATId = ISNULL((SELECT TOP 1 CS.Id FROM DensoProductCodesSAT CS WHERE RTRIM(LTRIM(CS.Code)) = RTRIM(LTRIM(PN.ProductCodeSATId))),0),  
		DC.IsActive = 1,
		DC.[Weight] = PN.[Weight]
	FROM DensoPartNumbers DC
	INNER JOIN @Parts PN ON PN.PartNumber = DC.Number	
	WHERE DC.Number = PN.PartNumber

	SET @CounterUpd = ISNULL(@@ROWCOUNT ,0 )

	---Insert Parts Record that are new
	INSERT INTO DensoPartNumbers
		(	
			Number,
			Description,
			DescriptionSpanish,				
			UnitMeasureId,
			Fraction,
			OriginCountryId,
			OriginCountry,			
			ProductCodeSATId,						
			IsActive,
			CreatorUserId,
			CreationTime,
			TenantId,
			[Weight]
		)
	SELECT 
		    PNEW.PartNumber	   
		   ,PNEW.DescriptionInglish
		   ,PNEW.DescriptionSpanish
		   ,ISNULL((SELECT TOP 1 UM.Id FROM DensoUnitMeasures UM WHERE UM.SegroveCode = PNEW.UnitMeasureId),0)
		   ,PNEW.Fraction
		   ,ISNULL((SELECT TOP 1 DC.Id FROM DensoCountries DC WHERE  DC.SegroveCode = PNEW.OriginCountryId),0)
		   ,PNEW.OriginCountryId
		   ,ISNULL((SELECT TOP 1 CS.Id FROM DensoProductCodesSAT CS WHERE CS.Code = PNEW.ProductCodeSATId),0)  
		   ,1
           ,@iIdUser
		   ,GETDATE()
		   ,1
		   ,PNEW.[Weight]
	FROM @Parts aS PNEW 
	LEFT JOIN DensoPartNumbers AS P ON P.Number = PNEW.PartNumber
	WHERE P.Number  IS NULL

	SET @CounterIns = ISNULL(@@ROWCOUNT ,0 )

	SET @CounterDel = (SELECT count(Id) FROM  DensoPartNumbersInternal WHERE IsActive = 0)	
	
    
	--Report Counters
    SELECT GETDATE() 'ProcessDate', @Counter 'Counter', @CounterDel 'CounterDel', @CounterUpd 'CounterUpd', @CounterIns 'CounterIns'
	
 	
 SET NOCOUNT OFF
END
