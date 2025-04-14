
CREATE OR ALTER PROCEDURE [dbo].[spInterfacePosition]
	@iIdUser INT,
	@PositionXML XML = NULL
AS
BEGIN
 SET NOCOUNT ON
 -------------------------------------------------------------------------------
 -- Creation by:    Luis Hdez Hdez
 -- Creation Date:  20 de Julio del 2023
 -- Purpose: Save Data of Position, processed by the interface
 -------------------------------------------------------------------------------
	DECLARE @doc INT, @Counter INT, @CounterDel INT, @CounterUpd INT, @CounterIns INT
	EXEC sp_xml_preparedocument @doc OUTPUT, @PositionXML

	DECLARE @Position TABLE 
	(
	  Codigo varchar(10),
	  Descripcion varchar(100)	  
	)	 
	INSERT INTO @Position
		   (
			Codigo,
		    Descripcion
		   )
	SELECT
		  Codigo, 
		  Descripcion
	FROM OPENXML(@doc,N'ArrayOfDBGRALPositions/DBGRALPositions',2)
	WITH
		( 
		  Codigo  varchar(10),
		  Descripcion varchar(100)		  
		)
	SET @Counter = @@ROWCOUNT 
	EXEC sp_xml_removedocument @doc

	--Update all records to inactive
	--Update P 
	--SET P.IsActive = 0 FROM DensoEmployeePositions AS P

	--Update the records that id exists with active
	UPDATE DP
	SET 
		DP.LastModifierUserId = @iIdUser,
		DP.LastModificationTime = GETDATE(),
		DP.[Name] = P.Descripcion,
		DP.IsActive = 1
	FROM DensoEmployeePositions DP
	INNER JOIN @Position P ON P.Codigo = DP.Code
	WHERE P.Codigo = DP.Code

	SET @CounterUpd = ISNULL(@@ROWCOUNT ,0 )

	--SET IDENTITY_INSERT dbo.DensoEmployeePositions ON;
	--Insert Plant Record that are new
	INSERT INTO DensoEmployeePositions
		(	
			Code,
			[Name],				
			IsActive,
			CreatorUserId,
			CreationTime
			--,TenantId
		)
	SELECT 
		   PNEW.Codigo,           
		   PNEW.Descripcion,  
           1,
           @iIdUser,
		   GETDATE()
		   --,1
	FROM @Position PNEW 
	LEFT JOIN DensoEmployeePositions AS DP ON DP.Code = PNEW.Codigo
	WHERE DP.Code IS NULL

	--SET IDENTITY_INSERT dbo.DensoEmployeePositions OFF;

	SET @CounterIns = ISNULL(@@ROWCOUNT ,0 )

	SET @CounterDel = (SELECT count(Id) FROM  DensoEmployeePositions WHERE IsActive = 0)

	--SET @CounterDel = @@ROWCOUNT 

	--Report Counters
    SELECT GETDATE(), @Counter, @CounterDel, @CounterUpd, @CounterIns
 	
 SET NOCOUNT OFF
END

