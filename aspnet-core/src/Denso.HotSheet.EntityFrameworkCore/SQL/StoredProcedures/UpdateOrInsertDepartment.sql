CREATE OR ALTER PROCEDURE UpdateOrInsertDepartment
	@iId int,
	@sNombre nvarchar(200),
	@bActive bit,
	@iPreviousid int = null
AS
BEGIN
	-------------------------------------------------------------------------------
 -- Creation by:    Luis Hdez Hdez
 -- Creation Date:  20 de Octubre del 2023
 -- Purpose: Update Information of Department
 -------------------------------------------------------------------------------
	  --REMPLACE ID
	  IF(@iPreviousid <> @iId)	  
	  BEGIN
		
		SET IDENTITY_INSERT [DensoDepartments] ON;  

		INSERT INTO [DensoDepartments] 
		( Id,TenantId,Name,CreationTime,CreatorUserId,LastModificationTime, LastModifierUserId, IsActive)
		VALUES 
		(@iId, 1, @sNombre, GETDATE(), 1, GETDATE(), 0, 1)
		
		SET IDENTITY_INSERT [DensoDepartments] ON;

		UPDATE DensoDepartmentUsers SET DepartmentId = @iId WHERE DepartmentId = @iPreviousid		

		UPDATE dbo.DensoEmployees SET DepartmentId = @iId WHERE DepartmentId = @iPreviousid		

		DELETE [DensoDepartments] WHERE Id = @iPreviousid		

	  END
	 ELSE IF(@iPreviousid = 0)
	 BEGIN 
		--NEW DEPTO

		SET IDENTITY_INSERT [DensoDepartments] ON;  

		INSERT INTO [DensoDepartments] 
		( Id,TenantId,Name,CreationTime,CreatorUserId,LastModificationTime, LastModifierUserId, IsActive)
		VALUES 
		(@iId, 1, @sNombre, GETDATE(), 1, GETDATE(), 0, 1)

		SET IDENTITY_INSERT [DensoDepartments] ON;
	 END
	 ELSE
	 BEGIN
		--UPDATE NAME ONLY OF DEPTO
		UPDATE [DensoDepartments] SET Name = @sNombre, IsActive= @bActive WHERE Id = @iId
	 END
	  
	 SELECT * FROM [DensoDepartments] wHERE  Id = @iId
   
END
