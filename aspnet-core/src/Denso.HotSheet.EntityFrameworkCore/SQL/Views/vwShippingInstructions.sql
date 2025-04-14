
-- SELECT * FROM vwHotSheet

-------------------------------------------------------------------------------
 -- Update by:    Ing.Luis Hdez Hdez
 -- Update Date:  14 Noviembre del 2024
 -- Purpose: Update information of Guide by loading an xlsx 
 -------------------------------------------------------------------------------

CREATE OR ALTER VIEW [dbo].[vwHotSheet]
AS
	SELECT si.Id As HotSheetShiptId,
		CAST(si.Id AS VARCHAR(30)) + '-' + ISNULL(p.Sufix, '') As Folio,
		si.DocumentTypeId,
		dt.Name As DocumentTypeName,
		si.CarrierId,
		c.Name As CarrierName,
		si.ServiceId,
		s.Name As ServiceName,
		si.PlantId,
		p.Name As PlantName,
		si.CustomerId,
		cus.Name As CustomerName,
		si.CustomerPlantId,
		cp.Name As CustomerPlantName,
		si.CustomerPlantContactId,
		cpc.ContactName AS CustomerPlantContactName,
		si.DepartmentId,
		d.Name As DepartmentName,
		si.CreatorUserId,
		u.Name + ' ' + u.Surname As CreatorFullName,
		si.CreationTime As CreationDate,
		si.StatusId,
		ds.Name As Status,
		si.PaymentStatusId,
		ps.Name As PaymentStatus,
		si.Currency,
		si.HotSheetReasonId,
		sr.Description As HotSheetReason,
		si.AdditionalExplanation,
		si.ExportedCigmaStatus,
		si.PaymentTermId,
		pt.Name AS PaymentTerm,
		si.RmaAssignmentId,
		si.CostPaidById,
		si.FreightPaidById,
		si.HotSheetTermId,
		st.Name as HotSheetTerm,
		si.OtherBy,
		si.RMANumber,
		si.BNotice,
		si.AccountNumber,
		si.SpecialExpeditedReasonId,
		si.ProformaInvoice,
		si.TrackingNumber,
		si.ShowBehalfFields,
		si.TelephoneExt,
		si.IsTemplate,
		si.TemplateName,
		si.TemplateDescription,
		si.FreightPaidByDepartmentId,
		si.FreightPaidByOther,
		si.FreightPrePaidExplanation,

		-- Approvers Info
		approvers.ManagerApprovalId,
		approvers.ManagerApprovalDate,
		ISNULL(approvers.ManagerIsApproved, 0) AS ManagerIsApproved,
		managerApproval.Name + ' ' + managerApproval.Surname As ManagerName,
		managerApproval.EmailAddress As ManagerEmailAddress,
		approvers.IEStaffId,
		approvers.IEStaffApprovalDate,
		ISNULL(approvers.IEStaffIsApproved, 0) AS IEStaffIsApproved,
		ieStaffApproval.Name + ' ' + ieStaffApproval.Surname As IEStaffName,
		ieStaffApproval.EmailAddress As IEStaffEmailAddress,
		ieStaff.UserId AS IEStaffApproverUserId,
		approvers.AccountingApprovalId,
		approvers.AccountingApprovalDate,
		ISNULL(approvers.AccountingIsApproved, 0) AS AccountingIsApproved,
		accStaff.UserId AS AccountingApproverUserId,
		accStaffApproval.Name + ' ' + accStaffApproval.Surname As AccountingName,
		accStaffApproval.EmailAddress As AccountingEmailAddress,
		
		si.OnBehalfOfUserId,
		onBehalfUser.Name + ' ' + onBehalfUser.Surname As OnBehalfUserFullName,
		si.OnBehalfOfDeptoId,
		CAST(onBehalfDepto.Id AS VARCHAR(50)) + ' - ' + onBehalfDepto.Name As OnBehalfDeptoName,
		si.OnBehalfOfExt,
		si.GuideReference,		
		si.GuideStatusDetail,
		si.GuideStatus,
		si.GuideCost,
		si.GuideCurrency,
		si.PaymentDate
	FROM DensoHotSheet si
		INNER JOIN AbpUsers u ON u.Id = si.CreatorUserId
		INNER JOIN DensoDocumentTypes dt ON dt.Id = si.DocumentTypeId
		INNER JOIN DensoCarriers c ON c.Id = si.CarrierId
		INNER JOIN DensoServices s ON s.Id = si.ServiceId
		INNER JOIN DensoPlants p ON p.Id = si.PlantId
		INNER JOIN DensoCustomers cus ON cus.Id = si.CustomerId
		INNER JOIN DensoCustomerPlants cp ON cp.Id = si.CustomerPlantId
		INNER JOIN DensoDocumentStatuses ds ON ds.Id = si.StatusId
		INNER JOIN DensoDepartments d ON d.Id = si.DepartmentId
		INNER JOIN DensoHotSheetReasons sr ON sr.Id = si.HotSheetReasonId
		INNER JOIN DensoPaymentStatus ps ON ps.Id = si.PaymentStatusId
		INNER JOIN DensoPaymentTerms pt ON pt.Id = si.PaymentTermId
		INNER JOIN DensoHotSheetTerms st ON st.Id = si.HotSheetTermId
		LEFT OUTER JOIN DensoCustomerPlantContacts cpc ON cpc.Id = si.CustomerPlantContactId

		LEFT OUTER JOIN AbpUsers onBehalfUser ON onBehalfUser.Id = si.OnBehalfOfUserId
		LEFT OUTER JOIN DensoDepartments onBehalfDepto ON onBehalfDepto.Id = si.OnBehalfOfDeptoId

		-- Approvers
		LEFT OUTER JOIN DensoHotSheetApprovals approvers ON approvers.HotSheetShiptId = si.Id		
		LEFT OUTER JOIN DensoStaff accStaff ON accStaff.Id = approvers.AccountingApprovalId
		LEFT OUTER JOIN DensoStaff ieStaff ON ieStaff.Id = approvers.IEStaffId
		LEFT OUTER JOIN AbpUsers managerApproval ON managerApproval.Id = approvers.ManagerApprovalId
		LEFT OUTER JOIN AbpUsers accStaffApproval ON accStaffApproval.Id = accStaff.UserId
		LEFT OUTER JOIN AbpUsers ieStaffApproval ON ieStaffApproval.Id = ieStaff.UserId
GO
