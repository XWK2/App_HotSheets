
/*** Employee Types ***/
SET IDENTITY_INSERT dbo.DensoInterfaces ON;
GO

DECLARE @DensoInterfaces TABLE(Id INT, Name VARCHAR(100))
INSERT INTO @DensoInterfaces
VALUES 
	(1, 'Customers'),	
	(2, 'Employees'),
	(3, 'Departments'),
	(4, 'Plants'),
	(5, 'PartNumbers'),
	(6, 'Prices'),
	(7, 'ApprovalsReminder')

INSERT INTO DensoInterfaces(Id, TenantId, Name, Notify, IsActive, CreationTime, CreatorUserId)
SELECT t1.Id, 1, t1.Name, 1, 1, GetDate(), 1
FROM @DensoInterfaces t1
	LEFT OUTER JOIN DensoInterfaces t2 ON t2.Name = t1.Name
WHERE t2.Id IS NULL

SET IDENTITY_INSERT dbo.DensoInterfaces OFF;
GO


