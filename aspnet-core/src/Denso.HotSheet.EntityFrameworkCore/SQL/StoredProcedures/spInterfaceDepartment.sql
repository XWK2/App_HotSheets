
CREATE OR ALTER PROCEDURE [dbo].[spInterfaceDepartment]
	@iIdUser INT,
	@DepartmentXML XML = NULL
AS
BEGIN
 SET NOCOUNT ON
-------------------------------------------------------------------------------
 -- Creation by:    Luis Hdez Hdez
 -- Creation Date:  20 de Julio del 2023
 -- Purpose: Save Data of Departments, processed by the interface
 -------------------------------------------------------------------------------
	DECLARE @doc INT, @Counter INT, @CounterDel INT, @CounterUpd INT, @CounterIns INT
	EXEC sp_xml_preparedocument @doc OUTPUT, @DepartmentXML

	DECLARE @Department TABLE 
	(
	  NoDepartment int,
	  Descripcion varchar(100)	  
	)	 
	INSERT INTO @Department
		   (
			NoDepartment,
		    Descripcion
		   )
	SELECT
		  NoDepartment, 
		  Descripcion
	FROM OPENXML(@doc,N'ArrayOfDBGRALDepartments/DBGRALDepartments',2)
	WITH
		( 
		  NoDepartment int,
		  Descripcion varchar(100)		  
		)
	SET @Counter = @@ROWCOUNT 
	EXEC sp_xml_removedocument @doc

	--Update all records to inactive
	--Update P 
	--SET P.IsActive = 0 FROM DensoDepartments AS P

	--Update the records that id exists with active
	UPDATE DD
	SET 
		DD.LastModifierUserId = @iIdUser,
		DD.LastModificationTime = GETDATE(),
		DD.[Name] = D.Descripcion,
		DD.IsActive = 1		
	FROM DensoDepartments DD
	INNER JOIN @Department D ON D.NoDepartment = DD.Id
	WHERE D.NoDepartment = DD.Id

	SET @CounterUpd = ISNULL(@@ROWCOUNT ,0 )

	SET IDENTITY_INSERT dbo.DensoDepartments ON;

	--Insert Plant Record that are new
	INSERT INTO DensoDepartments
		(	
			Id,
			[Name],				
			IsActive,
			CreatorUserId,
			CreationTime,
			TenantId
		)
	SELECT 
		   DNEW.NoDepartment,           
		   DNEW.Descripcion,  
           1,
           @iIdUser,
		   GETDATE(),
		   1
	FROM @Department DNEW 
	LEFT JOIN DensoDepartments AS DD ON DD.Id = DNEW.NoDepartment
	WHERE DD.Id IS NULL

	SET IDENTITY_INSERT dbo.DensoDepartments OFF;

	SET @CounterIns = ISNULL(@@ROWCOUNT ,0 )

	SET @CounterDel = (SELECT count(Id) FROM  DensoDepartments WHERE IsActive = 0)

	--SET @CounterDel = @@ROWCOUNT 

	--Report Counters
    SELECT GETDATE(), @Counter, @CounterDel, @CounterUpd, @CounterIns
 	
 SET NOCOUNT OFF
END

