
CREATE OR ALTER   PROCEDURE [dbo].[spInterfaceEmployee]
	@iIdUser INT,
	@EmployeeXML XML = NULL
AS
BEGIN
 SET NOCOUNT ON

  -------------------------------------------------------------------------------
 -- Creation by:    Luis Hdez Hdez
 -- Creation Date:  20 de Julio del 2023
 -- Purpose: Save Data of Employees, processed by the interface
 -------------------------------------------------------------------------------
	DECLARE @doc INT, @Counter INT, @CounterDel INT, @CounterUpd INT, @CounterIns INT
	EXEC sp_xml_preparedocument @doc OUTPUT, @EmployeeXML

	IF (@iIdUser = 0)
		set @iIdUser = 1

	DECLARE @Employees TABLE 
	(
		 ID_Empleado int	  
        ,Credencial varchar(100)	  
        ,Nombre varchar(100)	  
        ,Apellidos varchar(100)	  
        ,NombreC varchar(100)	  
        ,RFC varchar(100)	  
        ,Fecha_Nacimiento varchar(100)	  
        ,IMSS varchar(100)	  
        ,CURP varchar(100)	  
        ,CodigoDepartamento varchar(100)	  
        ,CodigoTipoEmpleado varchar(100)	  
        ,CodigoNivel varchar(100)	  
        ,CodigoPlanta varchar(100)	  
        ,CodigoPuesto varchar(100)	  
        ,Fecha_Alta varchar(100)	  
        ,Fecha_Baja varchar(100)	  
        ,Extras varchar(100)	  
        ,NoRequiereAHE varchar(100)	  
        ,Supervisor varchar(100)	  
        ,Subsidio varchar(100)	  
        ,CargoNivel varchar(100)	  
        ,Dato0 varchar(100)	  
        ,Dato1 varchar(100)	  
        ,Dato2 varchar(100)	  
        ,Dato3 varchar(100)	  
        ,Dato4 varchar(100)	  
        ,Dato5 varchar(100)	  
        ,Dato6 varchar(100)	  
        ,Dato7 varchar(100)	  
        ,Dato8 varchar(100)	  
        ,Dato9 varchar(100)	  
        ,ArchivoCredencial varchar(100)	  
        ,ImprimirCredencial varchar(100)	  
        ,Puesto varchar(100)	  
        ,NoDepto int
        ,NomDepto varchar(100)	  
        ,notesID varchar(100)	  
        ,Planta varchar(100)	  
        ,Ext varchar(100)	  
        ,Ext2 varchar(100)	  			
	)	 
	INSERT INTO @Employees
		   (
			 ID_Empleado 
            ,Credencial 
            ,Nombre 
            ,Apellidos 
            ,NombreC 
            ,RFC 
            ,Fecha_Nacimiento 
            ,IMSS 
            ,CURP 
            ,CodigoDepartamento 
            ,CodigoTipoEmpleado 
            ,CodigoNivel 
            ,CodigoPlanta 
            ,CodigoPuesto 
            ,Fecha_Alta 
            ,Fecha_Baja 
            ,Extras 
            ,NoRequiereAHE 
            ,Supervisor 
            ,Subsidio 
            ,CargoNivel 
            ,Dato0 
            ,Dato1 
            ,Dato2 
            ,Dato3 
            ,Dato4 
            ,Dato5 
            ,Dato6 
            ,Dato7 
            ,Dato8 
            ,Dato9 
            ,ArchivoCredencial 
            ,ImprimirCredencial 
            ,Puesto 
            ,NoDepto 
            ,NomDepto 
            ,notesID 
            ,Planta 
            ,Ext 
            ,Ext2 
		   )
	SELECT
		    ID_Empleado 
            ,Credencial 
            ,Nombre 
            ,Apellidos 
            ,NombreC 
            ,RFC 
            ,Fecha_Nacimiento 
            ,IMSS 
            ,CURP 
            ,CodigoDepartamento 
            ,CodigoTipoEmpleado 
            ,CodigoNivel 
            ,CodigoPlanta 
            ,CodigoPuesto 
            ,Fecha_Alta 
            ,Fecha_Baja 
            ,Extras 
            ,NoRequiereAHE 
            ,Supervisor 
            ,Subsidio 
            ,CargoNivel 
            ,Dato0 
            ,Dato1 
            ,Dato2 
            ,Dato3 
            ,Dato4 
            ,Dato5 
            ,Dato6 
            ,Dato7 
            ,Dato8 
            ,Dato9 
            ,ArchivoCredencial 
            ,ImprimirCredencial 
            ,Puesto 
            ,NoDepto 
            ,NomDepto 
            ,notesID 
            ,Planta 
            ,Ext 
            ,Ext2 
	FROM OPENXML(@doc,N'ArrayOfDBGRALEmployees/DBGRALEmployees',2)
	WITH
		( 
		 ID_Empleado int
        ,Credencial varchar(100)	  
        ,Nombre varchar(100)	  
        ,Apellidos varchar(100)	  
        ,NombreC varchar(100)	  
        ,RFC varchar(100)	  
        ,Fecha_Nacimiento varchar(100)	  
        ,IMSS varchar(100)	  
        ,CURP varchar(100)	  
        ,CodigoDepartamento varchar(100)	  
        ,CodigoTipoEmpleado varchar(100)	  
        ,CodigoNivel varchar(100)	  
        ,CodigoPlanta varchar(100)	  
        ,CodigoPuesto varchar(100)	  
        ,Fecha_Alta varchar(100)	  
        ,Fecha_Baja varchar(100)	  
        ,Extras varchar(100)	  
        ,NoRequiereAHE varchar(100)	  
        ,Supervisor varchar(100)	  
        ,Subsidio varchar(100)	  
        ,CargoNivel varchar(100)	  
        ,Dato0 varchar(100)	  
        ,Dato1 varchar(100)	  
        ,Dato2 varchar(100)	  
        ,Dato3 varchar(100)	  
        ,Dato4 varchar(100)	  
        ,Dato5 varchar(100)	  
        ,Dato6 varchar(100)	  
        ,Dato7 varchar(100)	  
        ,Dato8 varchar(100)	  
        ,Dato9 varchar(100)	  
        ,ArchivoCredencial varchar(100)	  
        ,ImprimirCredencial varchar(100)	  
        ,Puesto varchar(100)	  
        ,NoDepto int
        ,NomDepto varchar(100)	  
        ,notesID varchar(100)	  
        ,Planta varchar(100)	  
        ,Ext varchar(100)	  
        ,Ext2 varchar(100)	  				  
		)
	SET @Counter = @@ROWCOUNT 
	EXEC sp_xml_removedocument @doc

	UPDATE DensoEmployees SET IsActive = 0 

	--Update all records to inactive
	UPDATE DE
	SET	
		
		   DE.Credential = CAST(ENEW.Credencial as NVARCHAR(100)),
		   DE.Name =  ENEW.Nombre,  
		   DE.Surnames =  ENEW.Apellidos,
		   DE.Rfc = ENEW.RFC,
		   DE.BirthDate = ENEW.Fecha_Nacimiento,
		   DE.Nss = ENEW.[IMSS],		   
		   DE.Curp = ENEW.CURP,
		   DE.DepartmentId =  (SELECT Top 1 Id FROM DensoDepartments DL WHERE DL.Id = ENEW.CodigoDepartamento),
		   DE.PositionId = (SELECT Top 1 Id FROM DensoEmployeePositions DL WHERE DL.Code =  ENEW.CodigoPuesto),	  
		   DE.LevelId = (SELECT Top 1 Id FROM DensoEmployeeLevels DL WHERE DL.Level =  ENEW.CodigoNivel),
		   DE.TypeId =(SELECT Top 1 Id FROM DensoEmployeeTypes DL WHERE DL.Id =  ENEW.CodigoTipoEmpleado),
		   DE.PlantId = (SELECT Top 1 Id FROM DensoPlants DL WHERE DL.Id =  ENEW.CodigoPlanta), 		   
		   
		   DE.CreationTime = ENEW.Fecha_Alta,		
			--Fecha_Baja,	  
		   DE.Extras =	ENEW.Extras,	  
		   DE.NotRequiredAHE = ENEW.NoRequiereAHE,	  
		   DE.Supervisor =	ENEW.Supervisor,	  
		   DE.Subsidy =	ENEW.Subsidio,	  
		   De.PositionLevel =	ENEW.CargoNivel,	  	
					  
		   DE.AddressLine1 = ENEW.Dato1,
		   DE.AddressLine2 = ENEW.Dato2,
		   DE.AddressLine3 = ENEW.Dato3,
		   DE.AddressLine4 = ENEW.Dato4

	FROM DensoEmployees DE
	INNER JOIN @Employees ENEW ON ENEW.ID_Empleado = DE.DensoEmployeeId
	WHERE ENEW.ID_Empleado = DE.DensoEmployeeId
		

	SET @CounterUpd = ISNULL(@@ROWCOUNT ,0 )

	INSERT INTO DensoEmployees
		(	
			DensoEmployeeId,
			[Credential],
			[Name],	
			[Surnames],
			[Rfc],
			[BirthDate],
			[Nss],
			[Curp],		

		    [DepartmentId],
			[PositionId],
			[LevelId],				
		    [TypeId],	 
			[PlantId],		

			[EntryDate],			
			[Extras],
			[NotRequiredAHE],
			[Supervisor],
			[Subsidy],
			[PositionLevel],

			[AddressLine1],
			[AddressLine2],
			[AddressLine3],
			[AddressLine4],
		  
			IsActive,
			CreatorUserId,
			CreationTime
			,TenantId
		)
	SELECT 
		   ENEW.ID_Empleado,   
		   CAST(ENEW.Credencial as nvarchar(100)),
		   ENEW.Nombre,  
		   ENEW.Apellidos,
		   ENEW.RFC,
		   ENEW.Fecha_Nacimiento,
		   ENEW.[IMSS],		   
		   ENEW.CURP,
		   DeptoId =(SELECT Top 1 Id FROM DensoDepartments DL WHERE DL.Id =   ENEW.CodigoDepartamento),
		   PositionId = (SELECT Top 1 Id FROM DensoEmployeePositions DL WHERE DL.Code =  ENEW.CodigoPuesto),	  
		   LevelId = (SELECT Top 1 Id FROM DensoEmployeeLevels DL WHERE DL.Level =  ENEW.CodigoNivel),
		   TypeId =(SELECT Top 1 Id FROM DensoEmployeeTypes DL WHERE DL.Id =  ENEW.CodigoTipoEmpleado),
		   PlantId = (SELECT Top 1 Id FROM DensoPlants DL WHERE DL.Id =  ENEW.CodigoPlanta), 		   
		   
		   ENEW.Fecha_Alta,		
			--Fecha_Baja,	  
			ENEW.Extras,	  
			ENEW.NoRequiereAHE,	  
			ENEW.Supervisor,	  
			ENEW.Subsidio,	  
			ENEW.CargoNivel,	  	
					  
			ENEW.Dato1,
			ENEW.Dato2,
			ENEW.Dato3,
			ENEW.Dato4,
			
           1,
           @iIdUser,
		   GETDATE()
		   ,1
	FROM @Employees AS ENEW 
	LEFT JOIN DensoEmployees AS DE ON DE.DensoEmployeeId = ENEW.ID_Empleado
	WHERE DE.DensoEmployeeId IS NULL 
		  AND ENEW.ID_Empleado not in
		  (
		   SELECT ID_Empleado FROM @Employees  
		   GROUP BY ID_Empleado 
		   HAVING COUNT(*)>1
		  )

	SET @CounterIns = ISNULL(@@ROWCOUNT ,0 )

	SET @CounterDel = (SELECT count(Id) FROM  DensoEmployees WHERE IsActive = 0)

	UPDATE U
	SET U.IsActive = 0
	FROM AbpUsers U 
	INNER JOIN DensoEmployees DE ON DE.Id = U.EmployeeId	
	WHERE DE.IsActive = 0

	SET @CounterDel = @@ROWCOUNT 


	--Report Counters
    SELECT GETDATE(), @Counter, @CounterDel, @CounterUpd, @CounterIns
 	
 SET NOCOUNT OFF
END