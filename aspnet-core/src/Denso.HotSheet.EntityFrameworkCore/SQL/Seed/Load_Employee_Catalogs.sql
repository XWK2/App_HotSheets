
/*** Employee Types ***/
SET IDENTITY_INSERT dbo.DensoEmployeeTypes ON;
GO

DECLARE @EmployeeTypes TABLE(Id INT, Name VARCHAR(100))
INSERT INTO @EmployeeTypes
VALUES 
	(1, 'Semanal'),
	(2, 'Catorcenal'),
	(3, 'Practicante'),
	(4, 'Temporal'),
	(5, 'Contratista'),
	(6, 'Gerente & Coordinador'),
	(7, 'Gerente general & Coordinador Avanzado'),
	(8, 'Vicepresidente'),
	(9, 'Presidente'),
	(10, 'Externo')

INSERT INTO DensoEmployeeTypes(Id, Name, CreationTime, CreatorUserId, IsActive)
SELECT t1.Id, t1.Name, GetDate(), 1, 1
FROM @EmployeeTypes t1
	LEFT OUTER JOIN DensoEmployeeTypes t2 ON t2.Id = t1.Id
WHERE t2.Id IS NULL

SET IDENTITY_INSERT dbo.DensoEmployeeTypes OFF;
GO


/*** Employee Levels ***/
DECLARE @EmployeeLevels TABLE(Level VARCHAR(10), Name VARCHAR(300))
INSERT INTO @EmployeeLevels
VALUES 	('1', 'Categoria 1'),
	('2', 'Categoria 2'),
	('3', 'Categoria 3'),
	('4', 'Categoria 4'),
	('A', 'ENTRENAMIENTO A'),
	('5', 'Categoria 5'),
	('6', 'Categoria 6'),
	('7', 'Categoria 7'),
	('8', 'Categoria 8'),
	('9', 'Categoria 9'),
	('10', 'Categoria 10'),
	('11', 'Categoria 11'),
	('12', 'Categoria 12'),
	('13', 'Categoria 13'),
	('14', 'Categoria 14'),
	('15', 'Categoria 15'),
	('B', 'ENTRENAMIENTO B'),
	('C', 'C MEJORA'),
	('D', 'Categoria D'),
	('C1', 'OPERADOR JUNIOR'),
	('C2', 'C2 MEJORA'),
	('16', 'Categoria 16'),
	('17', 'Categoria 17'),
	('E1', 'Categoria E1'),
	('D4', 'Categoria D4'),
	('0', 'Practicante'),
	('B1', 'ENTRENAMIENTO B'),
	('C3', 'OPERADOR'),
	('C4', 'Categoria C4'),
	('D1', 'OPERADOR AVANZADO'),
	('D2', 'Categoria D2'),
	('D3', 'OPERADOR EXPERTO')

INSERT INTO DensoEmployeeLevels(Level, Name, CreationTime, CreatorUserId, IsActive)
SELECT t1.Level, t1.Name, GetDate(), 1, 1
FROM @EmployeeLevels t1
	LEFT OUTER JOIN DensoEmployeeLevels t2 ON t2.Level = t1.Level
WHERE t2.Id IS NULL
GO

/*** Employee Positions ***/
/*
DECLARE @EmployeePositions TABLE(Code VARCHAR(10), Name VARCHAR(300))
INSERT INTO @EmployeePositions
VALUES ('AM', 'ASISTENTE DE GERENTE'),
	('D', 'DIRECTOR'),
	('E1', 'OK'), -- Hay nombres repetidos pero diferente code ???????	
*/