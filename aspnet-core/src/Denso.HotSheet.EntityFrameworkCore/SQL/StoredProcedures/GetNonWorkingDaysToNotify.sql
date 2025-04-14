
CREATE OR ALTER PROCEDURE [dbo].[GetNonWorkingDaysToNotify]
AS
BEGIN
	DECLARE @CurrentDate DATE = GETDATE(), @DaysInAdvanceForNonWorkDaysNotification INT

	SELECT 
		@DaysInAdvanceForNonWorkDaysNotification =
		CASE
			WHEN [Value] IS NOT NULL THEN CAST([Value] AS INT)
			ELSE 1
		END
	FROM AbpSettings
	WHERE Name = 'Denso.HotSheet.DaysInAdvanceForNonWorkDaysNotification'

	IF @DaysInAdvanceForNonWorkDaysNotification IS NULL
	BEGIN
		SET @DaysInAdvanceForNonWorkDaysNotification = 1
	END

	SELECT nonwd.Id, nonwd.CarrierId, c.Name AS CarrierName, nonwd.NonWorkingDay, nonwd.IsActive
	FROM DensoCarrierNonWorkingDays nonwd
		INNER JOIN DensoCarriers c ON c.Id = nonwd.CarrierId
	WHERE nonwd.NonWorkingDay >= @CurrentDate
		AND @CurrentDate >= CAST(DATEADD(day, -@DaysInAdvanceForNonWorkDaysNotification, nonwd.NonWorkingDay) AS DATE)
		AND nonwd.IsActive = 1
END
GO
