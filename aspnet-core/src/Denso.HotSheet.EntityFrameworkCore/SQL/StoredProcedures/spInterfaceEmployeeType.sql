
CREATE OR ALTER PROCEDURE [dbo].[spInterfaceEmployeeType]
	@iIdUser INT,
	@EmployeeTypeXML XML = NULL
AS
BEGIN
 SET NOCOUNT ON
-------------------------------------------------------------------------------
 -- Creation by:    Luis Hdez Hdez
 -- Creation Date:  20 de Julio del 2023
 -- Purpose: Save Data of Employee Types, processed by the interface
 -------------------------------------------------------------------------------
	DECLARE @doc INT, @Counter INT, @CounterDel INT, @CounterUpd INT, @CounterIns INT
	EXEC sp_xml_preparedocument @doc OUTPUT, @EmployeeTypeXML

	DECLARE @EmployeeType TABLE 
	(
	  NoEmployeeType int,
	  Descripcion varchar(100)	  
	)	 
	INSERT INTO @EmployeeType
		   (
			NoEmployeeType,
		    Descripcion
		   )
	SELECT
		  NoEmployeeType, 
		  Descripcion
	FROM OPENXML(@doc,N'ArrayOfDBGRALEmployeeType/DBGRALEmployeeType',2)
	WITH
		( 
		  NoEmployeeType int,
		  Descripcion varchar(100)		  
		)
	SET @Counter = @@ROWCOUNT 
	EXEC sp_xml_removedocument @doc

	--Update all records to inactive
	--Update P 
	--SET P.IsActive = 0 FROM DensoPlants AS P

	--Update the records that id exists with active
	UPDATE DET
	SET 
		DET.LastModifierUserId = @iIdUser,
		DET.LastModificationTime = GETDATE(),
		DET.[Name] = ET.Descripcion,
		DET.IsActive = 1
	FROM DensoEmployeeTypes DET
	INNER JOIN @EmployeeType ET ON ET.NoEmployeeType = DET.Id
	WHERE ET.NoEmployeeType = DET.Id

	SET @CounterUpd = ISNULL(@@ROWCOUNT ,0 )

	SET IDENTITY_INSERT dbo.DensoEmployeeTypes ON;
	--Insert Plant Record that are new
	INSERT INTO DensoEmployeeTypes
		(	
			Id,
			[Name],				
			IsActive,
			CreatorUserId,
			CreationTime
			--,TenantId
		)
	SELECT 
		   DETN.NoEmployeeType,           
		   DETN.Descripcion,  
           1,
           @iIdUser,
		   GETDATE()
		   --,1
	FROM @EmployeeType aS DETN 
	LEFT JOIN DensoEmployeeTypes AS DET ON DET.Id = DETN.NoEmployeeType
	WHERE DET.Id IS NULL

	SET IDENTITY_INSERT dbo.DensoEmployeeTypes OFF;

	SET @CounterIns = ISNULL(@@ROWCOUNT ,0 )

	SET @CounterDel = (SELECT count(Id) FROM  DensoEmployeeTypes WHERE IsActive = 0)

	--SET @CounterDel = @@ROWCOUNT 

	--Report Counters
    SELECT GETDATE(), @Counter, @CounterDel, @CounterUpd, @CounterIns
 	
 SET NOCOUNT OFF
END