

 -------------------------------------------------------------------------------
 -- Creation by:    Luis Hdez Hdez
 -- Creation Date:  23 de Julio del 2023
 -- Purpose: Load Initial value default Help Url 
 -------------------------------------------------------------------------------

DECLARE @FullUrl NVARCHAR(MAX), @ConfigName NVARCHAR(512)

SET @FullUrl = 'http://localhost/shipping-instructions/assets/img/ManualDeUsuario.pdf'
SET @ConfigName = 'Denso.HotSheet.DefaulHelpUrl'

IF NOT EXISTS(SELECT 1 FROM AbpSettings WHERE Name = @ConfigName)
BEGIN
		INSERT INTO [App_DensoHotSheet].[dbo].[AbpSettings]
		([CreationTime],[CreatorUserId],[LastModificationTime],[LastModifierUserId],[Name],[TenantId],[UserId],[Value])
		VALUES
		(GETDATE(), 2, GETDATE(), 1, @ConfigName, 1, 2, @FullUrl)
		  
END
ELSE
BEGIN
		UPDATE AbpSettings SET Value = @FullUrl, LastModificationTime = GetDate() WHERE Name = @ConfigName
END	


SELECT Id, Name, Value, CreationTime, LastModificationTime FROM AbpSettings WHERE Name = @ConfigName