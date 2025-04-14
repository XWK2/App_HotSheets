
-- exec GetTrackingScrapSales
CREATE OR ALTER PROCEDURE [dbo].[GetTrackingScrapSales]
	@PlantId			INT = NULL,
	@CustomerId			BIGINT = NULL,
	@PaymentStatusId	INT = NULL,	
	@Manifest		VARCHAR(100) = NULL, -- Pending ??
	@Folio			VARCHAR(100) = NULL,
	@Invoice		VARCHAR(100) = NULL
AS
BEGIN
	
	--LHH: 28_Enero_2024
	---Update Change HotSheetReasonId = 8 then international else national.
	
	DECLARE @HotSheetTotals TABLE (HotSheetShiptId BIGINT, Total DECIMAL(12, 2))

	INSERT INTO @HotSheetTotals (HotSheetShiptId, Total)
	SELECT si.Id, SUM(sip.Total)
	FROM DensoHotSheet si
		INNER JOIN DensoHotSheetShipProducts sip ON sip.HotSheetShiptId = si.Id
	GROUP BY si.Id

	SELECT vsi.HotSheetShiptId,			
		REPLACE(vsi.PlantName, 'Planta ', '') As PlantName, --Planta		
		vsi.CustomerName, --Reciclador		
		CASE
			WHEN vsi.HotSheetReasonId = 8 THEN 'Internacional'			
			ELSE 'Nacional'
		END AS PedimentoImpo, --Pedimento Importación
		vsi.CreationDate, --Fecha Embarque,
		mani.Manifest, -- Manifest ??
		mani.HotSheetDate,
		mani.ReportDateStart,
		mani.ReportDateEnd,
		mani.Comments, --As ManifestReport, -- dbo.GetLastHistoryDateBy(vsi.HotSheetShiptId, 'ApprovalRequested') As ReportDate,		
		vsi.Folio, -- Shipping Instruction
		--'' As Comments, -- Comments ??
		vsi.ManagerApprovalDate, -- SHE - Aprobación Shipping Instruction
		vsi.IEStaffApprovalDate, -- IE - Aprobación Shipping Instruction

		null AS InvoiceEmissionDate, -- ???
		vsi.ProformaInvoice,
		totals.Total,
		
		vsi.Currency,

		vsi.PaymentDate As PaymentDate, ---LHH: 14_01_2025, se obtiene mediante el servicion windows de informacion complementaria, aplica solo si tiene manifiesto, productos y con paymentTermID = 1 que es facturas a pagar.
		vsi.PaymentStatus

	FROM DensoHotSheetShipManifests mani
		INNER JOIN vwHotSheet vsi ON vsi.HotSheetShiptId = mani.HotSheetShiptId
		INNER JOIN dbo.DensoServices AS serv ON serv.Id = vsi.ServiceId
		INNER JOIN @HotSheetTotals totals ON totals.HotSheetShiptId = vsi.HotSheetShiptId
	WHERE vsi.IsTemplate = 0
		AND vsi.PaymentTermId = 1 -- Only REMITTANCE
		AND (@PlantId IS NULL OR @PlantId = -1 OR vsi.PlantId = @PlantId)
		AND (@CustomerId IS NULL OR @CustomerId = -1 OR vsi.CustomerId = @CustomerId)
		AND (@PaymentStatusId IS NULL OR @PaymentStatusId = -1 OR vsi.PaymentStatusId = @PaymentStatusId)		
		AND (ISNULL(@Folio, '') = '' OR vsi.Folio = @Folio)
		AND (ISNULL(@Invoice, '') = '' OR vsi.ProformaInvoice = @Invoice)
		AND (ISNULL(@Manifest, '') = '' OR mani.Manifest = @Manifest)
END
GO

