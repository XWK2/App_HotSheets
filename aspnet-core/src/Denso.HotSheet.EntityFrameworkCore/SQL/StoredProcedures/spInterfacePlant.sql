
CREATE OR ALTER PROCEDURE [dbo].[spInterfacePlant]
	@iIdUser INT,
	@PlantXML XML = NULL
AS
BEGIN
 SET NOCOUNT ON
 -------------------------------------------------------------------------------
 -- Creation by:    Luis Hdez Hdez
 -- Creation Date:  20 de Julio del 2023
 -- Purpose:Save Data of Plants, processed by the interface
 -------------------------------------------------------------------------------
	DECLARE @doc INT, @Counter INT, @CounterDel INT, @CounterUpd INT, @CounterIns INT
	EXEC sp_xml_preparedocument @doc OUTPUT, @PlantXML

	DECLARE @Plants TABLE 
	(
	  NoPlant int,
	  Descripcion varchar(100)	  
	)	 
	INSERT INTO @Plants
		   (
			NoPlant,
		    Descripcion
		   )
	SELECT
		  NoPlant, 
		  Descripcion
	FROM OPENXML(@doc,N'ArrayOfDBGRALPlants/DBGRALPlants',2)
	WITH
		( 
		  NoPlant int,
		  Descripcion varchar(100)		  
		)
	SET @Counter = @@ROWCOUNT 
	EXEC sp_xml_removedocument @doc

	--Update all records to inactive
	--Update P 
	--SET P.IsActive = 0 FROM DensoPlants AS P

	--Update the records that id exists with active
	UPDATE DP
	SET 
		DP.LastModifierUserId = @iIdUser,
		DP.LastModificationTime = GETDATE(),
		DP.[Name] = PN.Descripcion,
		DP.IsActive = 1
	FROM DensoPlants DP
	INNER JOIN @Plants PN ON PN.NoPlant = DP.Id	
	WHERE DP.Id = PN.NoPlant

	SET @CounterUpd = ISNULL(@@ROWCOUNT ,0 )

	SET IDENTITY_INSERT dbo.DensoPlants ON;
	--Insert Plant Record that are new
	INSERT INTO DensoPlants
		(	
			Id,
			[Name],	
			AddressLine1,
			AddressLine2,
			AddressLine3,
			AddressLine4,
			RFC,
			Sufix,
			IsActive,
			CreatorUserId,
			CreationTime,
			TenantId
		)
	SELECT 
		   PNEW.NoPlant,           
		   PNEW.Descripcion,   
		   '',
		   '',
		   '',
		   '',
		   '',
		   '',
           1,
           @iIdUser,
		   GETDATE(),
		   1
	FROM @Plants aS PNEW 
	LEFT JOIN DensoPlants AS P ON P.Id = PNEW.NoPlant
	WHERE P.Id IS NULL

	SET IDENTITY_INSERT dbo.DensoPlants OFF;

	SET @CounterIns = ISNULL(@@ROWCOUNT ,0 )

	SET @CounterDel = (SELECT count(Id) FROM  DensoPlants WHERE IsActive = 0)

	--SET @CounterDel = @@ROWCOUNT 

	--Report Counters
    SELECT GETDATE(), @Counter, @CounterDel, @CounterUpd, @CounterIns
 	
 SET NOCOUNT OFF
END

