
CREATE OR ALTER PROCEDURE [dbo].[UpdateHotSheetGuidesXlsx]
	@iIdUser INT = 0,
	@HotSheetGuidesXML XML = NULL
AS
BEGIN
 SET NOCOUNT ON
 -------------------------------------------------------------------------------
 -- Creation by:    Ing.Luis Hdez Hdez
 -- Creation Date:  13 Noviembre del 2024
 -- Purpose: Update information of Guide by loading an xlsx 
 -------------------------------------------------------------------------------

	DECLARE @doc INT, @Counter INT, @CounterUpd INT
	EXEC sp_xml_preparedocument @doc OUTPUT, @HotSheetGuidesXML

	DECLARE @Guides TABLE 
	(
		TrackingNumber nvarchar(50)
		,GuideReference nvarchar(50)		
		,GuideStatusDetail nvarchar(250)
		,GuideStatus nvarchar(25)
		,GuideCost decimal(18,2)
		,GuideCurrency nvarchar(10)
	)	 
	INSERT INTO @Guides
	(
		 TrackingNumber
		,GuideReference 		
		,GuideStatusDetail 
		,GuideStatus 
		,GuideCost 
		,GuideCurrency 
	)
	SELECT
		      RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(TrackingNumber,CHAR(9),''),CHAR(10),''),CHAR(13),''),CHAR(160),'')))
			 ,RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(GuideReference,CHAR(9),''),CHAR(10),''),CHAR(13),''),CHAR(160),'')))			 
			 ,RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(GuideStatusDetail,CHAR(9),''),CHAR(10),''),CHAR(13),''),CHAR(160),'')))
			 ,RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(GuideStatus,CHAR(9),''),CHAR(10),''),CHAR(13),''),CHAR(160),'')))			 
			 ,GuideCost
			 ,RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(GuideCurrency,CHAR(9),''),CHAR(10),''),CHAR(13),''),CHAR(160),'')))			 
	FROM OPENXML(@doc,N'ArrayOfHotSheetGuidesXlsxDto/HotSheetGuidesXlsxDto',2)
	WITH
		( 
			 TrackingNumber nvarchar(50)
			,GuideReference nvarchar(50)			
			,GuideStatusDetail nvarchar(250)
			,GuideStatus nvarchar(25)
			,GuideCost decimal(18,2)
			,GuideCurrency nvarchar(10)      
		)
	SET @Counter = @@ROWCOUNT 
	EXEC sp_xml_removedocument @doc
	
	--Update the records that id exists with active
	UPDATE DSI
	SET 
		 DSI.LastModifierUserId = @iIdUser
		,DSI.LastModificationTime = GETDATE()			
		,DSI.GuideReference = RTRIM(LTRIM(G.GuideReference))		
		,DSI.GuideStatusDetail = RTRIM(LTRIM(G.GuideStatusDetail))
		,DSI.GuideStatus = RTRIM(LTRIM(G.GuideStatus))
		,DSI.GuideCost = G.GuideCost
		,DSI.GuideCurrency = RTRIM(LTRIM(G.GuideCurrency))

	FROM DensoHotSheet DSI
	INNER JOIN @Guides G ON RTRIM(LTRIM(G.TrackingNumber)) = RTRIM(LTRIM(DSI.TrackingNumber))
	WHERE RTRIM(LTRIM(DSI.TrackingNumber)) = RTRIM(LTRIM(G.TrackingNumber))

	SET @CounterUpd = ISNULL(@@ROWCOUNT ,0 )
	    
	--Report Counters
    SELECT GETDATE() 'ProcessDate', @Counter 'Counter', @CounterUpd 'CounterUpd'
	
 	
 SET NOCOUNT OFF
END
