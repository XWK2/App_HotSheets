
/*** Currencies ***/
DECLARE @Currencies TABLE(Code VARCHAR(20), DensoCode VARCHAR(20), Name VARCHAR(100))
INSERT INTO @Currencies
VALUES 
	('EUR', 'EU', 'Euro'),
	('JPY', 'JY', 'Yen'),
	('MXN', 'MP', 'Peso'),
	('USD', 'UD', 'Dollar')

INSERT INTO DensoCurrencies(Code, DensoCode, Name, CreationTime, CreatorUserId, IsActive)
SELECT t1.Code, t1.DensoCode, t1.Name, GetDate(), 1, 1
FROM @Currencies t1
	LEFT OUTER JOIN DensoCurrencies t2 ON t1.Code = t2.Code
WHERE t2.Id IS NULL
GO

/*** Unit Measures ***/
SET IDENTITY_INSERT dbo.DensoUnitMeasures ON;
GO

DECLARE @UnitMeasures TABLE(Id INT, Name VARCHAR(100), DensoCode VARCHAR(10), SatCode VARCHAR(10))

INSERT INTO @UnitMeasures
VALUES 
	(1, 'KILOGRAMO',	'KG', 'KGM'), --
	(2, 'GRAMO',		'GR', 'GRM'), ---
	(3, 'MILILITRO',	'ML', 'MLT'), --
	(4, 'M2',			'M2', null),
	(5, 'M3',			'M3', null),
	(6, 'PIEZA',		'EA', 'H87'), ---
	(7, 'MILIMETRO',	'MM', '35'),  -- ??? No existe en Denso
	(8, 'LITRO',		'LT', 'LTR'), --
	(9, 'PAR',			'PR', 'PR'),  -- *** No tenía nombre
	(10, 'DECIMETRO',	'DM', 'DMT'), -- ??? No existe en Denso
	(11, 'MR',			'MR', null),
	(12, 'Conjunto',	'ST', 'SET'), --*** No tenía nombre
	(14, 'Tonelada',	'TN', 'A75'), --*** No tenía nombre
	(15, 'BR',			'BR', null),
	(16, 'GN',			'GN', null),
	(17, 'DE',			'DE', null),
	(18, 'Actividad',	'CT', 'ACT'), --- *** No tenía nombre
	(19, 'DO',			'DO', null),
	(20, 'Caja',		'BX', 'XBX'), --*** No tenía nombre
	(21, 'BO',			'BO', null),
	(22, 'GALON',		'GL', 'GLL'), -- ??? No existe en Denso
	(23, 'LOTE',		'LO', 'LO'), -- ??? No existe en Denso
	(24, 'LIBRA',		'LB', 'M86'), -- ??? No existe en Denso
	(25, 'METRO',		'MT', 'MTR') -- ??? No existe en Denso

INSERT INTO DensoUnitMeasures(TenantId, Id, Name, DensoCode, SatCode, CreationTime, CreatorUserId, IsActive)
SELECT 1, um.Id, um.Name, um.DensoCode, um.SatCode, GetDate(), 1, 1
FROM @UnitMeasures um
	LEFT OUTER JOIN DensoUnitMeasures dum ON dum.Id = um.id
WHERE dum.Id IS NULL

SET IDENTITY_INSERT dbo.DensoUnitMeasures OFF;
GO

/*** Product Codes SAT ***/
DECLARE @ProductCodesSAT TABLE(Code VARCHAR(30), Description VARCHAR(500))

INSERT INTO @ProductCodesSAT
VALUES
	('11191609', 'Chatarra de zinc'),
	('11191610', 'Chatarra de aluminio'),
	('11191611', 'Chatarra de estaño'),
	('12171703', 'Tintas'),
	('13111000', 'Resinas'),
	('14121500', 'Cartón y papel para embalaje'),
	('23153200', 'Robotica'),
	('23153400', 'Emsambladoras'),
	('23153600', 'Maquinas para el marcado de partes'),
	('24112903', 'Embalaje de plastico')

INSERT INTO DensoProductCodesSAT(TenantId, Code, Description, CreationTime, CreatorUserId, IsActive)
SELECT 1, pcs.Code, pcs.Description, GetDate(), 1, 1
FROM @ProductCodesSAT pcs
	LEFT OUTER JOIN DensoProductCodesSAT pcs2 ON pcs2.Code = pcs.Code
WHERE pcs2.Id IS NULL
GO


/*** Payment Status ***/
SET IDENTITY_INSERT dbo.DensoPaymentStatus ON;
GO

DECLARE @PaymentStatus TABLE(Id INT, Code VARCHAR(10), Name VARCHAR(100))
INSERT INTO @PaymentStatus
VALUES 
	(1, 'NP', 'No Paid'),
	(2, 'PP', 'Partially Paid'),
	(3, 'PA', 'Paid');

INSERT INTO DensoPaymentStatus (Id, Code, Name, CreationTime, CreatorUserId)
SELECT t1.Id, t1.Code, t1.Name, GetDate(), 1
FROM @PaymentStatus t1
	LEFT OUTER JOIN DensoPaymentStatus t2 ON t1.Id = t2.Id
WHERE t2.Id IS NULL
GO

SET IDENTITY_INSERT dbo.DensoPaymentStatus OFF;
GO