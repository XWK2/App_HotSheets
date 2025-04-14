CREATE OR ALTER  PROCEDURE [dbo].[spInterfaceCreateUsers]
@iIdUser INT = NULL,
@tenantId INT = NULL,
@EmployeeXML nvarchar(max) = ''
AS
BEGIN
	SET NOCOUNT ON;
 -------------------------------------------------------------------------------
 -- Creation by:    Luis Hdez Hdez
 -- Creation Date:  20 de Julio del 2023
 -- Purpose: Create Users into App Shipping, processed by the interface
 -------------------------------------------------------------------------------

	--DECLARE	@iIdUser INT = NULL,
	--	@tenantId INT = NULL,
	--	@EmployeeXML nvarchar(max) = NULL

	--SET @EmployeeXML = '1013728,1013729'
	--SET @EmployeeXML = '1013753'
		
	--select * from DensoEmployees 
	--WHERE cast(id as nvarchar(10)) in (select value from STRING_SPLIT(@EmployeeXML,','))

	DECLARE @Counter INT = 0, @CounterDel INT = 0, @CounterUpd INT = 0, @CounterIns INT = 0

	IF (@iIdUser =0 OR @iIdUser IS NULL)
	SET @iIdUser = 1

	IF (@tenantId = 0 OR @tenantId IS NULL)
	SET @tenantId = 1

	DECLARE @Employees TABLE 
	(
		[Id] [bigint],
		[Credential] [nvarchar](100) NULL,
		[Name] [nvarchar](100) NULL,
		[Surnames] [nvarchar](100) NULL,
		[Rfc] [nvarchar](30) NULL,
		[BirthDate] [date] NULL,
		[Nss] [nvarchar](20) NULL,
		[Curp] [nvarchar](20) NULL,
		[DepartmentId] [bigint] NULL,
		[PositionId] [int] NULL,
		[TypeId] [int] NULL,
		[PlantId] [bigint] NULL,
		[EntryDate] [date] NOT NULL,
		[Extras] [bit] NOT NULL,
		[NotRequiredAHE] [bit] NOT NULL,
		[Supervisor] [bit] NOT NULL,
		[Subsidy] [bit] NOT NULL,
		[PositionLevel] [bit] NOT NULL,
		[AddressLine1] [nvarchar](300) NULL,
		[AddressLine2] [nvarchar](200) NULL,
		[AddressLine3] [nvarchar](100) NULL,
		[AddressLine4] [nvarchar](100) NULL,
		[CreationTime] [datetime2](7) NOT NULL,
		[CreatorUserId] [bigint] NULL,
		[LastModificationTime] [datetime2](7) NULL,
		[LastModifierUserId] [bigint] NULL,
		[EmailAddress] [nvarchar](200) NULL,
		[IsActive] [bit] NOT NULL,
		[LevelId] [int] NULL,
		[DensoEmployeeId] [bigint] NOT NULL,
		[TenantId] [int] NULL
	)	 

	INSERT INTO @Employees
	(
		Id
		,Credential
		,Name
		,Surnames
		,Rfc
		,BirthDate
		,Nss
		,Curp
		,DepartmentId
		,PositionId
		,TypeId
		,PlantId
		,EntryDate
		,Extras
		,NotRequiredAHE
		,Supervisor
		,Subsidy
		,PositionLevel
		,AddressLine1
		,AddressLine2
		,AddressLine3
		,AddressLine4
		,CreationTime
		,CreatorUserId
		,LastModificationTime
		,LastModifierUserId
		,EmailAddress
		,IsActive
		,LevelId
		,DensoEmployeeId
		,TenantId
	)
	SELECT
		Id
		,Credential
		,Name
		,Surnames
		,Rfc
		,BirthDate
		,Nss
		,Curp
		,DepartmentId
		,PositionId
		,TypeId
		,PlantId
		,EntryDate
		,Extras
		,NotRequiredAHE
		,Supervisor
		,Subsidy
		,PositionLevel
		,AddressLine1
		,AddressLine2
		,AddressLine3
		,AddressLine4
		,CreationTime
		,CreatorUserId
		,LastModificationTime
		,LastModifierUserId
		,EmailAddress
		,IsActive
		,LevelId
		,DensoEmployeeId
		,TenantId
	FROM 
	DensoEmployees	
	WHERE cast(id as nvarchar(10)) in (select value from STRING_SPLIT(@EmployeeXML,','))

--select * from @Employees


--NOTA: Manera correcta de hacer split a un string.
----------------------------------------------------------------------------------------
--SELECT * FROM DensoEmployees
--WHERE cast(id as nvarchar(10)) in (select value from STRING_SPLIT(@EmployeeXML,','))
----------------------------------------------------------------------------------------

--Insert Plant Record that are new
INSERT INTO AbpUsers
		(				
			[Name],	
			AccessFailedCount,  --int
			EmailAddress,  --varchar
			IsDeleted,		--bit
			IsEmailConfirmed,  --bit
			IsLockoutEnabled,   --bit
			IsPhoneNumberConfirmed,  --bit
			IsTwoFactorEnabled,  --bit
			NormalizedEmailAddress,   --varchar
			NormalizedUserName,  --varchar
			
			[Password],   --varchar
			Surname, --varchar
			UserName,	--varchar

			ConcurrencyStamp,
			SecurityStamp, --varchar

			IsActive,
			CreatorUserId,
			CreationTime,
			TenantId,
			EmployeeId
		)
	SELECT 		   
		   ENEW.Name,   
		   0 as 'AccessFailedCount',
		   ENEW.EmailAddress,
		   0 as 'IsDeleted',	
		   1 as 'IsEmailConfirmed',
		   0 as 'IsLockoutEnabled',	
		   0 as 'IsPhoneNumberConfirmed',	
		   0 as 'IsTwoFactorEnabled',
		   UPPER(ENEW.EmailAddress) as 'NormalizePassword',
		   UPPER(ENEW.DensoEmployeeId) as 'NormalizedUserName',
		   
		   --SUBSTRING(CAST(NEWID() AS VARCHAR(50)),1,16) as 'Password',		   
		   'AQAAAAIAAYagAAAAEGweYWEzlfGH1aWfpQeBMI+t5jxlCwSZISWWydtZdA4cgBYf5eVxDRzunQhXHZrwCw==' as 'Password',  --contraseña '123qwe' es el default
		   ENEW.Surnames as 'SurName', 
		   ENEW.DensoEmployeeId as 'UserName',

		   NEWID() as 'ConcurrencyStamp',
		   NEWID() as 'SecurityStamp',

           1 as 'IsActive',
           @iIdUser as 'CreatorUserId',
		   GETDATE() as 'CreationTime',
		   @tenantId as TenantId,
		   ENEW.Id  ---Checar si ponemos este o el id de employee que es el consecutivo.
	FROM @Employees AS ENEW 	
	
	SET @Counter = ISNULL(@@ROWCOUNT,0)
	SET @CounterIns = @Counter

	--SELECT * FROM AbpUserRoles   --14 records exist actuallity
	DECLARE @IdRolDefault int = 0
	SET @IdRolDefault = (select top 1 id from AbpRoles where Name in ('Authors'))
	
	INSERT INTO AbpUserRoles
	(CreationTime,CreatorUserId, RoleId, TenantId, UserId)
	SELECT GETDATE(), @iIdUser, @IdRolDefault,  @tenantId, U.Id
	FROM @Employees as EN
	INNER JOIN DensoEmployees as DN ON DN.DensoEmployeeId = EN.DensoEmployeeId
	INNER JOIN AbpUsers as U ON U.EmployeeId = DN.Id

	--SELECT * FROM DensoPlantUsers  --32 records Actuallity

	INSERT INTO DensoPlantUsers (PlantId, UserId, IsSupervisor, CreationTime, CreatorUserId)	
	SELECT EN.PlantId, U.Id, EN.Supervisor,  GETDATE(),  @iIdUser 
	FROM @Employees as EN
	INNER JOIN DensoEmployees as DN ON DN.DensoEmployeeId = EN.DensoEmployeeId
	INNER JOIN AbpUsers as U ON U.EmployeeId = DN.Id
	
	--SELECT * FROM DensoDepartmentUsers  -- 37 records exist actuallity
	
	INSERT INTO DensoDepartmentUsers
	(DepartmentId, UserId, IsApprover, IsSupervisor, CreationTime, CreatorUserId)
	SELECT EN.DepartmentId, U.Id, 0, EN.Supervisor,  GETDATE(),  @iIdUser 
	FROM @Employees as EN
	INNER JOIN DensoEmployees as DN ON DN.DensoEmployeeId = EN.DensoEmployeeId
	INNER JOIN AbpUsers as U ON U.EmployeeId = DN.Id


	SELECT GETDATE(), @Counter, @CounterDel, @CounterUpd, @CounterIns
	

	SET NOCOUNT OFF;
END