
CREATE OR ALTER   PROCEDURE [dbo].[spEmployeesShipping_s]
	@Active bit
AS
BEGIN
		-------------------------------------------------------------------------------
		-- Creation by:    Luis Hdez Hdez
		-- Creation Date:  20 de Julio del 2023
		-- Purpose: obtain user information to send email reminders
		-------------------------------------------------------------------------------			

		--EJEMPLO prueba 
		--UPDATE DensoEmployees SET EmailAddress = 'jrmc123lhh@gmail.com' WHERE [Credential] =   '0000004132'
		--UPDATE DensoEmployees SET EmailAddress = 'jrmc123lhh@gmail.com' WHERE [Credential] =   '0000003763'

		--SELECT E.Id, E.[Credential], E.[Name], E.Surnames, E.Rfc, E.EmailAddress, U.EmailAddress
		
		SELECT E.Id as Id, E.[Credential] as Credencial, E.[Name] as Nombre, E.Surnames as Apellidos, E.Rfc as RFC, E.EmailAddress as Email, DepartmentId, PlantId
		FROM DensoEmployees as E
		LEFT JOIN AbpUsers as U on U.EmployeeId = E.Id
		WHERE U.EmailAddress IS NULL 
		AND E.EmailAddress IS NOT NULL
		AND DepartmentId IS NOT NULL
		AND PlantId IS NOT NULL

	
END