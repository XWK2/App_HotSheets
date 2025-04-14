
CREATE OR ALTER PROCEDURE [dbo].[spShippingIdsPendingForApproval_s]
	@Active bit
AS
BEGIN
	-------------------------------------------------------------------------------
	 -- Creation by:    Luis Hdez Hdez
	 -- Creation Date:  20 de Julio del 2023
	 -- Purpose: Get Ids Pending For Approval, necessary for the interface
	 -------------------------------------------------------------------------------

		DECLARE @DaysForReminders INT
		DECLARE @HotSheetsIdsPendingForApproval TABLE(ShippingInstuctionId BIGINT)
		DECLARE @HotSheetsHistory TABLE(ShippingInstuctionId BIGINT, LastNotificationDate DATETIME, RowNumber INT)

		SELECT @DaysForReminders = CAST(Value AS INT) FROM AbpSettings WHERE NAME='Denso.HotSheet.DaysForReminders'
		
		INSERT INTO @HotSheetsIdsPendingForApproval
		SELECT Id
		FROM DensoHotSheet
		WHERE StatusId = 5 --PendingForApproval

		-- SELECT * FROM @HotSheetsIdsPendingForApproval

		INSERT INTO @HotSheetsHistory
		SELECT h.HotSheetShiptId, 
			   CreationTime,
			   ROW_NUMBER() OVER(PARTITION BY h.HotSheetShiptId ORDER BY CreationTime DESC) AS RowNumber
		FROM DensoHotSheetHistory h
		WHERE HotSheetShiptId IN (SELECT p.ShippingInstuctionId FROM @HotSheetsIdsPendingForApproval p)
			AND HistoryType = 'EmailNotificationSent'

		--SELECT * FROM @HotSheetsHistory WHERE RowNumber = 1
	
		DECLARE @HotSheetsResult TABLE
		(
			ShippingInstuctionId BIGINT, 
			Folio VARCHAR(30),
			CreationDate DATETIME,
			CreatorUserId BIGINT,
			CreatorFullName VARCHAR(100),
			DocumentTypeId INT,
			CustomerName VARCHAR(100),
			LastNotificationDate DATETIME, 
			DaysDiff INT,
			UserIdManager BIGINT,
			ManagerFullName VARCHAR(100),
			ManagerEmailAddress VARCHAR(50),
			ManagerIsApproved BIT,
			UserIdStaff BIGINT,
			StaffFullName VARCHAR(100),
			StaffEmailAddress VARCHAR(50),
			IEStaffIsApproved BIT,
			UserIdAccounting BIGINT,
			AccountingFullName VARCHAR(100),
			AccountingEmailAddress VARCHAR(50),
			AccountingApproved BIT
		)

		INSERT INTO @HotSheetsResult		
		SELECT si.Id As ShippingInstuctionId,
			CAST(si.Id as varchar(20)) + ' - ' + ISNULL(dp.Sufix,'') as Folio,
			CAST(si.CreationTime as datetime) as CreationDate,
			si.CreatorUserId,
			desi.[Name] + ' ' + desi.Surnames  as CreatorFullName,
			si.DocumentTypeId,
			dc.[Name] CustomerName,
			h.LastNotificationDate,
			DATEDIFF(day, ISNULL(h.LastNotificationDate, GETDATE()), GETDATE()) AS DaysDiff,			
			CASE WHEN ISNULL(a.ManagerIsApproved,0) = 1 THEN 0 ELSE ISNULL(au.Id,0) END as UserIdManager,											--Manager
			CASE WHEN ISNULL(a.ManagerIsApproved,0) = 1 THEN '' ELSE ISNULL(de.[Name],'') + ' '+ ISNULL(de.Surnames,'') END as ManagerFullName,		--Manager
			CASE WHEN ISNULL(a.ManagerIsApproved,0) = 1 THEN '' ELSE ISNULL(au.EmailAddress,'') END as ManagerEmailAddress,							--Manager			
			ISNULL(a.ManagerIsApproved,0) as ManagerIsApproved,																						--Manager	
			CASE WHEN ISNULL(a.IEStaffIsApproved,0) = 1 THEN 0 ELSE ISNULL(aus.Id,0) END  as UserIdStaff,											--Staff
			CASE WHEN ISNULL(a.IEStaffIsApproved,0) = 1 THEN '' ELSE ISNULL(dess.[Name],'') + ' '+ ISNULL(dess.Surnames,'') END as StaffFullName,	--Staff
			CASE WHEN ISNULL(a.IEStaffIsApproved,0) = 1 THEN '' ELSE ISNULL(aus.EmailAddress,'') END as StaffEmailAddress,							--Staff
			ISNULL(a.IEStaffIsApproved,0) as IEStaffIsApproved,																						--Staff
			CASE WHEN ISNULL(a.IEStaffIsApproved,0) = 1 THEN 0 ELSE ISNULL(ausa.Id,0) END as UserIdAccounting,												--Accounting
			CASE WHEN ISNULL(a.IEStaffIsApproved,0) = 1 THEN '' ELSE ISNULL(dessa.Name,'') + ' '+ ISNULL(dessa.Surnames,'') END as AccountingFullName,		--Accounting
			CASE WHEN ISNULL(a.IEStaffIsApproved,0) = 1 THEN '' ELSE ISNULL(ausa.EmailAddress,'') END as AccountingEmailAddress,							--Accounting
			ISNULL(a.AccountingIsApproved,0) as AccountingApproved																						    --Accounting
		FROM DensoHotSheet si
			LEFT OUTER JOIN @HotSheetsHistory h ON h.ShippingInstuctionId = si.Id AND h.RowNumber = 1
			INNER JOIN DensoHotSheetApprovals as a ON a.HotSheetShiptId = h.ShippingInstuctionId
			INNER JOIN DensoPlants dp on dp.Id = si.PlantId
			INNER JOIN DensoCustomers dc on dc.Id = si.CustomerId
			INNER JOIN AbpUsers ausi on ausi.id = si.CreatorUserId
			INNER JOIN DensoEmployees desi on desi.Id = ausi.EmployeeId
			LEFT JOIN AbpUsers au on au.Id = a.ManagerApprovalId			--Manager
			LEFT JOIN DensoEmployees de on de.Id = au.EmployeeId			--Manager
			LEFT JOIN DensoStaff ds on ds.Id = a.IEStaffId					--Staff
			LEFT JOIN AbpUsers aus on aus.id = ds.UserId					--Staff
			LEFT JOIN DensoEmployees dess on dess.Id = aus.EmployeeId		--Staff
			LEFT JOIN DensoStaff dsa on dsa.Id = a.AccountingApprovalId		--Accounting
			LEFT JOIN AbpUsers ausa on ausa.id = dsa.UserId					--Accounting
			LEFT JOIN DensoEmployees dessa on dessa.Id = ausa.EmployeeId	--Accounting
		WHERE si.Id IN (SELECT p.ShippingInstuctionId FROM @HotSheetsIdsPendingForApproval p)
			AND DATEDIFF(day, h.LastNotificationDate, GETDATE()) >= @DaysForReminders 		
						
		---All
		--SELECT * FROM @HotSheetsResult

		--Second
		SELECT UserIdManager as UserId, ManagerFullName as FullName, ManagerEmailAddress as EmailAddress, 
		ShippingInstuctionId, Folio,CreationDate, CreatorUserId, CreatorFullName, DocumentTypeId, CustomerName
		FROM (
			SELECT distinct(UserIdManager) , ManagerFullName, ManagerEmailAddress, ShippingInstuctionId, Folio,CreationDate, CreatorUserId, CreatorFullName, DocumentTypeId, CustomerName 
			FROM @HotSheetsResult WHERE UserIdManager != 0
			UNION
			SELECT distinct(UserIdStaff), StaffFullName, StaffEmailAddress, ShippingInstuctionId, Folio,CreationDate, CreatorUserId, CreatorFullName, DocumentTypeId, CustomerName 
			FROM @HotSheetsResult WHERE UserIdStaff != 0
			UNION
			SELECT distinct(UserIdAccounting), AccountingFullName, AccountingEmailAddress,ShippingInstuctionId, Folio, CreationDate, CreatorUserId, CreatorFullName, DocumentTypeId, CustomerName 
			FROM @HotSheetsResult WHERE UserIdAccounting != 0	
		)QUERY_RESULT
	
END