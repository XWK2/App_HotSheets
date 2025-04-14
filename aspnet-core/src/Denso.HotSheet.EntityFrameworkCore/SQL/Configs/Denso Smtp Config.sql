
/* 
- Importante: La actualización de los valores de SMTP requiere reiniciar SOLO el servicio!!!
- Pruebas: Menu Administración -> Configuracion: Tab Configuración Email
*/

DECLARE @NumberRecords INT, @RowCount INT, @ConfigName VARCHAR(300), @ConfigValue VARCHAR(300)
DECLARE @tmpMailSmtp TABLE (RowId INT IDENTITY(1, 1), ConfigName VARCHAR(300), ConfigValue VARCHAR(300))

INSERT INTO @tmpMailSmtp (ConfigName,		ConfigValue)
SELECT 'Abp.Net.Mail.Smtp.Host',			'mail.smart-itcs.com'				UNION
SELECT 'Abp.Net.Mail.Smtp.Port',			'587'								UNION
SELECT 'Abp.Net.Mail.Smtp.EnableSsl',		'false'								UNION
SELECT 'Abp.Net.Mail.Smtp.UserName',		'porfirio.hernandez@smart-itcs.com'	UNION
SELECT 'Abp.Net.Mail.Smtp.Password',		'1PH@123'							UNION
SELECT 'Abp.Net.Mail.DefaultFromAddress',	'porfirio.hernandez@smart-itcs.com' UNION
SELECT 'Abp.Net.Mail.DefaultFromDisplayName', 'Notificaciones Denso - Hot Sheet'		UNION
SELECT 'Abp.Net.Mail.Smtp.Domain',					'' UNION
SELECT 'Abp.Net.Mail.Smtp.UseDefaultCredentials',	'false'

SET @NumberRecords = @@ROWCOUNT
SET @RowCount = 1

WHILE @RowCount <= @NumberRecords
BEGIN
	SELECT @ConfigName = ConfigName,
		@ConfigValue = ConfigValue
	FROM @tmpMailSmtp
	WHERE RowId = @RowCount

	IF NOT EXISTS(SELECT 1 FROM AbpSettings WHERE Name = @ConfigName)
	BEGIN
		INSERT INTO AbpSettings (CreationTime, TenantId, Name, Value) VALUES (GetDate(), 1, @ConfigName, @ConfigValue)
	END
		ELSE
		BEGIN
			UPDATE AbpSettings SET Value = @ConfigValue, LastModificationTime = GetDate() WHERE Name = @ConfigName
		END

	SET @RowCount = @RowCount + 1
END

SELECT Id, Name, Value, CreationTime, LastModificationTime FROM AbpSettings WHERE Name LIKE 'Abp.Net.Mail%'
