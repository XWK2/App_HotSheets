
CREATE OR ALTER   PROCEDURE [dbo].[spInterfaceParts]
	@iIdUser INT,
	@PartsXML XML = NULL
AS
BEGIN
 SET NOCOUNT ON
 -------------------------------------------------------------------------------
 -- Creation by:    Luis Hdez Hdez
 -- Creation Date:  20 de Julio del 2023
 -- Purpose: Save Data of Part Numbers, processed by the interface
 -- Update 11_Dic_2024: Insert Part Number into DensoPartNumbersInternal where Price and Weight != 0 and Insert into DensoPartNumbers where Price and Weigth = 0
 -------------------------------------------------------------------------------
	DECLARE @doc INT, @Counter INT, @CounterDel INT, @CounterUpd INT, @CounterIns INT
	EXEC sp_xml_preparedocument @doc OUTPUT, @PartsXML

	DECLARE @Parts TABLE 
	(
		 NoProducto varchar(100) 
        ,DescripcionIngles1 varchar(100) 
        ,DescripcionIngles2 varchar(100) 
        ,DescripcionEsp1 varchar(100) 
        ,DescripcionEsp2 varchar(100)       
        ,UMDENSO varchar(100)      
        ,Pais varchar(100)        
        ,Fraccion varchar(100)        
        ,Precio varchar(100)    
        ,Peso varchar(100)   
        ,CveProdSAT varchar(100)        
        ,UMSAT varchar(100)      

	)	 
	INSERT INTO @Parts
		   (
			 NoProducto 
			,DescripcionIngles1 
			,DescripcionIngles2 
			,DescripcionEsp1 
			,DescripcionEsp2       
			,UMDENSO      
			,Pais        
			,Fraccion        
			,Precio    
			,Peso   
			,CveProdSAT        
			,UMSAT  
		   )
	SELECT
			 NoProducto 
			,RTRIM(LTRIM(DescripcionIngles1)) as DescripcionIngles1
			,RTRIM(LTRIM(DescripcionIngles2)) as DescripcionIngles2
			,RTRIM(LTRIM(DescripcionEsp1)) as DescripcionEsp1
			,RTRIM(LTRIM(DescripcionEsp2)) as DescripcionEsp2    
			,RTRIM(LTRIM(UMDENSO)) as UMDENSO          
			,RTRIM(LTRIM(Pais)) as Pais            
			,RTRIM(LTRIM(Fraccion)) as Fraccion            
			,RTRIM(LTRIM(Precio)) as Precio        
			,RTRIM(LTRIM(Peso)) as Peso       
			,RTRIM(LTRIM(CveProdSAT)) as CveProdSAT            
			,RTRIM(LTRIM(UMSAT)) as UMSAT      

	FROM OPENXML(@doc,N'ArrayOfAS400Parts/AS400Parts',2)
	WITH
		( 
			 NoProducto varchar(100) 
			,DescripcionIngles1 varchar(100) 
			,DescripcionIngles2 varchar(100) 
			,DescripcionEsp1 varchar(100) 
			,DescripcionEsp2 varchar(100)       
			,UMDENSO varchar(100)      
			,Pais varchar(100)        
			,Fraccion varchar(100)        
			,Precio varchar(100)    
			,Peso varchar(100)   
			,CveProdSAT varchar(100)        
			,UMSAT varchar(100)      
		)
	SET @Counter = @@ROWCOUNT 
	EXEC sp_xml_removedocument @doc


	--select * from @Parts
	
	--UPDATE INTO TWO TABLES--

	--1. Update the records that id part number exists into DensoPartNumberInternal ('Miselaneos') Table as400: EM692PR
	--   Of diference is that Weight and Price have value 
	UPDATE DC
	SET 
		DC.LastModifierUserId = @iIdUser,
		DC.LastModificationTime = GETDATE(),
		DC.Description = PN.DescripcionIngles1 + '' + PN.DescripcionIngles2,
		DC.[DescriptionSpanish] =  PN.DescripcionEsp1 + PN.DescripcionEsp2,
		DC.UnitMeasureId = (SELECT TOP 1 UM.Id FROM DensoUnitMeasures UM WHERE  UM.DensoCode = PN.UMDENSO),	
	    DC.Fraction = PN.Fraccion,
		DC.OriginCountry = RTRIM(LTRIM(PN.Pais)),
		DC.OriginCountryId = (SELECT TOP 1 DCC.Id FROM DensoCountries DCC WHERE (DCC.SatCode = RTRIM(LTRIM(PN.Pais)) OR DCC.Name = RTRIM(LTRIM(PN.Pais)))),
		DC.ProductCodeSATId = (SELECT TOP 1 PCS.Id FROM DensoProductCodesSAT PCS WHERE  PCS.Code = PN.CveProdSAT),
		DC.IsActive = 1,
		DC.Weight = CAST(PN.Peso as Decimal(18,4)),
		DC.Price =  CAST(PN.Precio as Decimal(18,4))
	FROM DensoPartNumbersInternal DC
	INNER JOIN @Parts PN ON PN.NoProducto = DC.Number	
	WHERE DC.Number = PN.NoProducto AND PN.Peso != '' --AND PN.Precio != ''
	
	--2. Update the records that id part number exists into DensoPartNumber Table as400: EM680PR
	--   Of diference is that Weight and Price no value
	UPDATE DC
	SET 
		DC.LastModifierUserId = @iIdUser,
		DC.LastModificationTime = GETDATE(),
		DC.Description = PN.DescripcionIngles1 + '' + PN.DescripcionIngles2,
		DC.[DescriptionSpanish] =  PN.DescripcionEsp1 + PN.DescripcionEsp2,
		DC.UnitMeasureId = (SELECT TOP 1 UM.Id FROM DensoUnitMeasures UM WHERE  UM.DensoCode = PN.UMDENSO),	
	    DC.Fraction = PN.Fraccion,				
		DC.OriginCountry = RTRIM(LTRIM(PN.Pais)),
		DC.OriginCountryId = (SELECT TOP 1 DCC.Id FROM DensoCountries DCC WHERE (DCC.SatCode = RTRIM(LTRIM(PN.Pais)) OR DCC.Name = RTRIM(LTRIM(PN.Pais)))),
		DC.ProductCodeSATId = (SELECT TOP 1 PCS.Id FROM DensoProductCodesSAT PCS WHERE  PCS.Code = PN.CveProdSAT),
		DC.IsActive = 1		
	FROM DensoPartNumbers DC
	INNER JOIN @Parts PN ON PN.NoProducto = DC.Number	
	WHERE DC.Number = PN.NoProducto AND PN.Peso = '' --AND PN.Precio = ''
	
	SET @CounterUpd = ISNULL(@@ROWCOUNT ,0 )	

	--INSERT INTO TWO TABLES--

	---1. Insert Parts Record that are new into table DensoPartNumbersInternal ('Miselaneos') Table as400: EM692PR
	--  Of diference is that Weight and Price have value 

	INSERT INTO DensoPartNumbersInternal
		(	
			Number,
			Description,
			DescriptionSpanish,				
			UnitMeasureId,
			Fraction,
			OriginCountry,
			OriginCountryId,
			ProductCodeSATId,						
			IsActive,
			CreatorUserId,
			CreationTime,
			TenantId,			
			Weight,
			Price
		)
	SELECT 
		    PNEW.NoProducto	   
		   ,PNEW.DescripcionIngles1 + ' ' + PNEW.DescripcionIngles2
		   ,PNEW.DescripcionEsp1 + ' ' + PNEW.DescripcionEsp2	
		   ,ISNULL((SELECT TOP 1 UM.Id FROM DensoUnitMeasures UM WHERE UM.DensoCode = PNEW.UMDENSO),0)
		   ,PNEW.Fraccion
		   ,PNEW.Pais
		   ,ISNULL((SELECT TOP 1 DCC.Id FROM DensoCountries DCC WHERE (DCC.SatCode = RTRIM(LTRIM(PNEW.Pais)) OR DCC.Name = RTRIM(LTRIM(PNEW.Pais)))),0)
		   ,ISNULL((SELECT TOP 1 PCS.Id FROM DensoProductCodesSAT PCS WHERE  PCS.Code = PNEW.CveProdSAT),0)
		   ,1
           ,@iIdUser
		   ,GETDATE()
		   ,1
		   ,CAST(PNEW.Peso as Decimal(18,4))
		   ,CAST(PNEW.Precio as Decimal(18,4))
	FROM @Parts aS PNEW 
	LEFT JOIN DensoPartNumbersInternal AS P ON P.Number = PNEW.NoProducto
	WHERE P.Number  IS NULL	AND PNEW.Peso != '' AND PNEW.Precio != ''

	---2. Insert Parts Record that are new into table DensoPartNumbers Table as400: EM680PR
	--  Of diference is that Weight and Price no value 
	
	INSERT INTO DensoPartNumbers
		(	
			Number,
			Description,
			DescriptionSpanish,				
			UnitMeasureId,
			Fraction,
			OriginCountry,
			OriginCountryId,
			ProductCodeSATId,						
			IsActive,
			CreatorUserId,
			CreationTime,
			TenantId
		)
	SELECT 
		    PNEW.NoProducto	   
		   ,PNEW.DescripcionIngles1 + ' ' + PNEW.DescripcionIngles2
		   ,PNEW.DescripcionEsp1 + ' ' + PNEW.DescripcionEsp2	
		   ,ISNULL((SELECT TOP 1 UM.Id FROM DensoUnitMeasures UM WHERE UM.DensoCode = PNEW.UMDENSO),0)
		   ,PNEW.Fraccion
		   ,PNEW.Pais
		   ,ISNULL((SELECT TOP 1 DCC.Id FROM DensoCountries DCC WHERE (DCC.SatCode = RTRIM(LTRIM(PNEW.Pais)) OR DCC.Name = RTRIM(LTRIM(PNEW.Pais)))),0)
		   ,ISNULL((SELECT TOP 1 PCS.Id FROM DensoProductCodesSAT PCS WHERE  PCS.Code = PNEW.CveProdSAT),0)
		   ,1
           ,@iIdUser
		   ,GETDATE()
		   ,1		   
	FROM @Parts aS PNEW 
	LEFT JOIN DensoPartNumbers AS P ON P.Number = PNEW.NoProducto
	WHERE P.Number  IS NULL	AND PNEW.Peso = '' AND PNEW.Precio = ''
	
	SET @CounterIns = ISNULL(@@ROWCOUNT ,0 )

	SET @CounterDel = (SELECT count(Id) FROM  DensoPartNumbersInternal WHERE IsActive = 0)		

	--Report Counters
    SELECT GETDATE(), @Counter 'NoRows', @CounterDel 'Del', @CounterUpd 'Upd', @CounterIns 'Ins'
 	
 SET NOCOUNT OFF
END
GO

