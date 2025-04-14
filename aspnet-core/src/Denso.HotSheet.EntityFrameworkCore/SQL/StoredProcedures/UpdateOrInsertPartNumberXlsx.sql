
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
 -- Update: 21_Feb_2024, Update IsActive = 0 commented
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
		      RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(PartNumber,CHAR(9),''),CHAR(10),''),CHAR(13),''),CHAR(160),'')))
			 ,RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(DescriptionInglish,CHAR(9),''),CHAR(10),''),CHAR(13),''),CHAR(160),'')))
			 ,RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(DescriptionSpanish,CHAR(9),''),CHAR(10),''),CHAR(13),''),CHAR(160),'')))
			 ,RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(UnitMeasureId,CHAR(9),''),CHAR(10),''),CHAR(13),''),CHAR(160),'')))
			 ,RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(OriginCountryId,CHAR(9),''),CHAR(10),''),CHAR(13),''),CHAR(160),'')))
			 ,RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(Fraction,CHAR(9),''),CHAR(10),''),CHAR(13),''),CHAR(160),'')))
			 ,RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(Weight,CHAR(9),''),CHAR(10),''),CHAR(13),''),CHAR(160),'')))
			 ,RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(ProductCodeSATId,CHAR(9),''),CHAR(10),''),CHAR(13),''),CHAR(160),'')))
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
	--Update PN SET PN.IsActive = 0 FROM DensoPartNumbers AS PN

	--Update the records that id exists with active
	UPDATE DC
	SET 
		DC.LastModifierUserId = @iIdUser,
		DC.LastModificationTime = GETDATE(),
		DC.Description = RTRIM(LTRIM(PN.DescriptionInglish)),
		DC.[DescriptionSpanish] =  RTRIM(LTRIM(PN.DescriptionSpanish)),
		DC.UnitMeasureId = ISNULL((SELECT TOP 1 UM.Id FROM DensoUnitMeasures UM WHERE  UM.SegroveCode = PN.UnitMeasureId),0),	
	    DC.Fraction = RTRIM(LTRIM(PN.Fraction)),				
		DC.OriginCountryId = ISNULL((SELECT TOP 1 DC.Id FROM DensoCountries DC WHERE  DC.SegroveCode = PN.OriginCountryId),0),	
		--DC.OriginCountry = PN.OriginCountryId,
		DC.ProductCodeSATId = ISNULL((SELECT TOP 1 CS.Id FROM DensoProductCodesSAT CS WHERE RTRIM(LTRIM(CS.Code)) = RTRIM(LTRIM(PN.ProductCodeSATId))),0),  
		DC.IsActive = 1,
		DC.[Weight] = RTRIM(LTRIM(PN.[Weight]))
	FROM DensoPartNumbers DC
	INNER JOIN @Parts PN ON RTRIM(LTRIM(PN.PartNumber)) = RTRIM(LTRIM(DC.Number))
	WHERE RTRIM(LTRIM(DC.Number)) = RTRIM(LTRIM(PN.PartNumber))

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
		    RTRIM(LTRIM(PNEW.PartNumber))
		   ,RTRIM(LTRIM(PNEW.DescriptionInglish))
		   ,RTRIM(LTRIM(PNEW.DescriptionSpanish))
		   ,ISNULL((SELECT TOP 1 UM.Id FROM DensoUnitMeasures UM WHERE UM.SegroveCode = PNEW.UnitMeasureId),0)
		   ,RTRIM(LTRIM(PNEW.Fraction))
		   ,ISNULL((SELECT TOP 1 DC.Id FROM DensoCountries DC WHERE  DC.SegroveCode = PNEW.OriginCountryId),0)
		   ,RTRIM(LTRIM(PNEW.OriginCountryId))
		   ,ISNULL((SELECT TOP 1 CS.Id FROM DensoProductCodesSAT CS WHERE CS.Code = PNEW.ProductCodeSATId),0)  
		   ,1
           ,@iIdUser
		   ,GETDATE()
		   ,1
		   ,RTRIM(LTRIM(PNEW.[Weight]))
	FROM @Parts aS PNEW 
	LEFT JOIN DensoPartNumbers AS P ON RTRIM(LTRIM(P.Number)) = RTRIM(LTRIM(PNEW.PartNumber))
	WHERE P.Number IS NULL

	SET @CounterIns = ISNULL(@@ROWCOUNT ,0 )

	SET @CounterDel = (SELECT count(Id) FROM  DensoPartNumbersInternal WHERE IsActive = 0)	
	
    
	--Report Counters
    SELECT GETDATE() 'ProcessDate', @Counter 'Counter', @CounterDel 'CounterDel', @CounterUpd 'CounterUpd', @CounterIns 'CounterIns'
	
 	
 SET NOCOUNT OFF
END
