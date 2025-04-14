
-- exec GetDashboardData 36348, 2023
CREATE OR ALTER PROCEDURE [dbo].[GetDashboardData]
	@UserId	BIGINT,
	@Year INT
AS
BEGIN
	DECLARE @TenantId INT = 1
	DECLARE @FilterBy INT = 0

	DECLARE @DepartmentsByUser TABLE(DepartmentId INT)

	IF EXISTS(SELECT 1 FROM AbpUserRoles ur
			INNER JOIN AbpRoles r ON r.Id = ur.RoleId
		WHERE UserId = @UserId AND ur.TenantId = @TenantId
			AND r.Name IN ('Admin', 'StaffImpoExpo', 'StaffAccounting'))
	BEGIN
		SET @FilterBy = 1 -- Todo
	END

	IF @FilterBy = 0
		AND EXISTS(SELECT 1 FROM AbpUserRoles ur
				INNER JOIN AbpRoles r ON r.Id = ur.RoleId
			WHERE UserId = @UserId AND ur.TenantId = @TenantId
				AND r.Name IN ('Approvers'))
	BEGIN
		SET @FilterBy = 2 -- By Deptos

		INSERT INTO @DepartmentsByUser
		SELECT d.Id As DepartmentId		
		FROM DensoDepartments d WITH (NOLOCK)
			INNER JOIN DensoDepartmentUsers du WITH (NOLOCK) on du.DepartmentId = d.Id
			INNER JOIN AbpUsers u WITH (NOLOCK) on u.Id = du.UserId AND u.IsActive = 1
		WHERE d.IsActive = 1
			AND u.Id = @UserId
	END

	IF @FilterBy = 0
		AND EXISTS(SELECT 1 FROM AbpUserRoles ur
				INNER JOIN AbpRoles r ON r.Id = ur.RoleId
			WHERE UserId = @UserId AND ur.TenantId = @TenantId
				AND r.Name IN ('Authors'))
	BEGIN
		SET @FilterBy = 3 -- By Authors
	END

	SELECT si.Id As HotSheetShiptId,
		si.StatusId,
		es.Name As StatusName,
		si.PlantId,
		p.Name As PlantName,
		si.DepartmentId,
		d.Name As DepartmentName,

		si.CarrierId,
		carrier.Name As CarrierName,
		si.CustomerId,
		customer.Name As CustomerName,
		si.ServiceId,
		serv.Name As ServiceName,

		si.CreatorUserId,
		creatorUser.Name + ' ' + creatorUser.Surname AS CreatorUser,
		si.CreationTime As CreationDate,

		-- Flags
		CASE
			WHEN 
				si.StatusId = 5
				AND ISNULL(sia.ManagerIsApproved, 0) = 1
				AND ISNULL(sia.IEStaffIsApproved, 0) = 0 
			THEN 1
		ELSE 0 END AS PendingApprovalByIE,
		CASE
			WHEN si.StatusId = 5 THEN 1
		ELSE 0 END AS PendingForApproval,
		CASE
			WHEN si.StatusId = 3 -- Approved
				AND si.exportedCigmaStatus = 2 -- Processed by Cigma
				AND si.ProformaInvoice IS NULL THEN 1
		ELSE 0 END AS PendingForProformaInvoice,
		CASE
			WHEN si.StatusId NOT IN (4, 6) 
				AND si.PaymentStatusId != 3 
				AND si.PaymentTermId = 1 --REMITTANCE
			THEN 1
		ELSE 0 END AS PendingForPayment,
		DATEDIFF(day, ISNULL(dbo.GetLastHistoryDateBy(si.Id, 'ApprovalRequested'), GETDATE()), GETDATE()) AS DaysLateApproval,
		dbo.GetLastHistoryDateBy(si.Id, 'ApprovalRequested')AS SentForApprovalDate,
		ieStaffApproval.Name + ' ' + ieStaffApproval.Surname AS IEStaffName,
		
		si.HotSheetReasonId,
		reason.Description As HotSheetReason,
		si.DocumentTypeId,
		REPLACE(
			REPLACE(
				REPLACE(dt.Name, '/Instrucción de Embarque Aereo', '')
			, '/Instrucción de Embarque Terrestre', '')
		, '/Instrucción de Embarque Maritimo', '') As DocumentType,
		si.PaymentTermId,
		payment.Name As PaymentTerm,
		si.exportedCigmaStatus

	FROM DensoHotSheet si
		INNER JOIN dbo.AbpUsers AS creatorUser ON creatorUser.Id = si.CreatorUserId
		INNER JOIN DensoPlants p ON p.Id = si.PlantId
		INNER JOIN DensoDepartments d ON d.Id = si.DepartmentId
		INNER JOIN DensoDocumentStatuses es ON es.Id = si.StatusId
		INNER JOIN DensoDocumentTypes dt ON dt.Id = si.DocumentTypeId
		INNER JOIN DensoPaymentTerms payment ON payment.Id = si.PaymentTermId
		
		INNER JOIN dbo.DensoCarriers AS carrier ON carrier.Id = si.CarrierId
		INNER JOIN dbo.DensoCustomers AS customer ON customer.Id = si.CustomerId
		INNER JOIN dbo.DensoServices AS serv ON serv.Id = si.ServiceId
		INNER JOIN dbo.DensoHotSheetReasons AS reason ON reason.Id = si.HotSheetReasonId

		LEFT OUTER JOIN DensoHotSheetApprovals sia ON sia.HotSheetShiptId = si.Id
		LEFT OUTER JOIN DensoStaff accStaff ON accStaff.Id = sia.AccountingApprovalId
		LEFT OUTER JOIN DensoStaff ieStaff ON ieStaff.Id = sia.IEStaffId
		LEFT OUTER JOIN dbo.AbpUsers AS ieStaffApproval ON ieStaffApproval.Id = ieStaff.UserId
	WHERE si.IsTemplate = 0
		AND (@FilterBy = 1 -- All HotSheets
			OR (@FilterBy = 2 AND si.DepartmentId IN (SELECT deptoByUser.DepartmentId FROM @DepartmentsByUser AS deptoByUser)) -- HotSheets by Deptos
			OR (@FilterBy = 3 AND si.CreatorUserId = @UserId) -- HotSheets by User creator
			)
END
GO
