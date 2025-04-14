
CREATE OR ALTER PROCEDURE [dbo].[spInterfaceLevel]
	@iIdUser INT,
	@LevelXML XML = NULL
AS
BEGIN
 SET NOCOUNT ON

 -------------------------------------------------------------------------------
 -- Creation by:    Luis Hdez Hdez
 -- Creation Date:  20 de Julio del 2023
 -- Purpose: Save Data of Levels, processed by the interface
 -------------------------------------------------------------------------------

	DECLARE @doc INT, @Counter INT, @CounterDel INT, @CounterUpd INT, @CounterIns INT
	EXEC sp_xml_preparedocument @doc OUTPUT, @LevelXML

	DECLARE @Level TABLE 
	(
	  NoLevel varchar(10),
	  Descripcion varchar(100)	  
	)	 
	INSERT INTO @Level
		   (
			NoLevel,
		    Descripcion
		   )
	SELECT
		  NoLevel, 
		  Descripcion
	FROM OPENXML(@doc,N'ArrayOfDBGRALLevel/DBGRALLevel',2)
	WITH
		( 
		  NoLevel  varchar(10),
		  Descripcion varchar(100)		  
		)
	SET @Counter = @@ROWCOUNT 
	EXEC sp_xml_removedocument @doc

	--Update all records to inactive
	--Update P 
	--SET P.IsActive = 0 FROM DensoPlants AS P

	--Update the records that id exists with active
	UPDATE DL
	SET 
		DL.LastModifierUserId = @iIdUser,
		DL.LastModificationTime = GETDATE(),
		DL.[Name] = L.Descripcion
		,DL.IsActive = 1
	FROM DensoEmployeeLevels DL
	INNER JOIN @Level L ON L.NoLevel = DL.[Level]
	WHERE L.NoLevel = DL.[Level]

	SET @CounterUpd = ISNULL(@@ROWCOUNT ,0 )

	--SET IDENTITY_INSERT dbo.DensoEmployeeLevels ON;
	--Insert Plant Record that are new
	INSERT INTO DensoEmployeeLevels
		(	
			[Level],
			[Name],				
			IsActive,
			CreatorUserId,
			CreationTime
			--,TenantId
		)
	SELECT 
		   LNEW.NoLevel,           
		   LNEW.Descripcion,  
           1,
           @iIdUser,
		   GETDATE()
		   --,1
	FROM @Level LNEW 
	LEFT JOIN DensoEmployeeLevels AS DL ON DL.[Level] = LNEW.NoLevel
	WHERE DL.[Level] IS NULL

	--SET IDENTITY_INSERT dbo.DensoEmployeeLevels OFF;

	SET @CounterIns = ISNULL(@@ROWCOUNT ,0 )

	SET @CounterDel = (SELECT count(Id) FROM  DensoEmployeeLevels WHERE IsActive = 0)

	--SET @CounterDel = @@ROWCOUNT 

	--Report Counters
    SELECT GETDATE(), @Counter, @CounterDel, @CounterUpd, @CounterIns
 	
 SET NOCOUNT OFF
END

