
CREATE OR ALTER  PROCEDURE [dbo].spInterfaceUpdInfoShipping
	@iIdUser INT,
	@ShippingXML XML = NULL
AS
BEGIN
 SET NOCOUNT ON
 -------------------------------------------------------------------------------
 -- Creation by:    Luis Hdez Hdez
 -- Creation Date:  20 de Julio del 2023
 -- Purpose: Update Information of HotSheets with data from as400
 -------------------------------------------------------------------------------
	DECLARE @doc INT, @Counter INT= 0, @CounterDel INT = 0, @CounterUpd INT = 0, @CounterIns INT=0
	EXEC sp_xml_preparedocument @doc OUTPUT, @ShippingXML

	DECLARE @HotSheets TABLE 
	(
		Id bigint,
		TenantId int,
		DocumentTypeId int, 
		ProformaInvoice nvarchar(30), 
		TrackingNumber nvarchar(50), 
		CarrierId bigint,
		ServiceId bigint,
		HotSheetReasonId int,
		PaymentTermId int,
		PaymentStatusId int,
		PlantId bigint,
		CustomerId bigint,
		CustomerPlantId bigint,
		HotSheetTermId int,
		OtherBy nvarchar(100),
		RMANumber nvarchar(100),
		BNotice nvarchar(100),
		AccountNumber nvarchar(100),
		CostPaidById bigint,
		FreightPaidById bigint,						
		ShowBehalfFields bit,
		StatusId int,		
		RmaAssignmentId int,
		ExportedCigmaStatus int,
		ExportedCigmaDate datetime2(7),
		SpecialExpeditedReasonId int,
		TemplateDescription nvarchar(500),
		CustomerPlantContactId bigint,
		PaymentDateJuliana bigint
	)	 
	INSERT INTO @HotSheets
	(
		Id 
		,TenantId
		,DocumentTypeId
		,ProformaInvoice
		,TrackingNumber 
		,CarrierId 
		,ServiceId 
		,HotSheetReasonId
		,PaymentTermId
		,PaymentStatusId
		,PlantId
		,CustomerId 
		,CustomerPlantId 
		,HotSheetTermId 
		,OtherBy 
		,RMANumber 
		,BNotice 
		,AccountNumber
		,CostPaidById
		,FreightPaidById 
		,ShowBehalfFields 
		,StatusId 	
		,RmaAssignmentId 
		,ExportedCigmaStatus 
		,ExportedCigmaDate 
		,SpecialExpeditedReasonId 
		,TemplateDescription 
		,CustomerPlantContactId 
		,PaymentDateJuliana 
	)
	SELECT
		  Id 
		,TenantId
		,DocumentTypeId
		,ProformaInvoice
		,TrackingNumber 
		,CarrierId 
		,ServiceId 
		,HotSheetReasonId
		,PaymentTermId
		,PaymentStatusId
		,PlantId
		,CustomerId 
		,CustomerPlantId 
		,HotSheetTermId 
		,OtherBy 
		,RMANumber 
		,BNotice 
		,AccountNumber
		,CostPaidById
		,FreightPaidById 
		,ShowBehalfFields 
		,StatusId 	
		,RmaAssignmentId 
		,ExportedCigmaStatus 
		,ExportedCigmaDate 
		,SpecialExpeditedReasonId 
		,TemplateDescription 
		,CustomerPlantContactId 
		,PaymentDateJuliana
	FROM OPENXML(@doc,N'ArrayOfAdditionalShippingInformation/AdditionalShippingInformation',2)
	WITH
		( 
			Id bigint,
			TenantId int,
			DocumentTypeId int, 
			ProformaInvoice nvarchar(30), 
			TrackingNumber nvarchar(50), 
			CarrierId bigint,
			ServiceId bigint,
			HotSheetReasonId int,
			PaymentTermId int,
			PaymentStatusId int,
			PlantId bigint,
			CustomerId bigint,
			CustomerPlantId bigint,
			HotSheetTermId int,
			OtherBy nvarchar(100),
			RMANumber nvarchar(100),
			BNotice nvarchar(100),
			AccountNumber nvarchar(100),
			CostPaidById bigint,
			FreightPaidById bigint,						
			ShowBehalfFields bit,
			StatusId int,		
			RmaAssignmentId int,
			ExportedCigmaStatus int,
			ExportedCigmaDate datetime2(7),
			SpecialExpeditedReasonId int,
			TemplateDescription nvarchar(500),
			CustomerPlantContactId bigint,
			PaymentDateJuliana bigint
		)
	SET @Counter = @@ROWCOUNT 
	EXEC sp_xml_removedocument @doc

	
	UPDATE DSI
	SET 
		DSI.LastModifierUserId = @iIdUser,
		DSI.LastModificationTime = GETDATE(),
		DSI.ProformaInvoice = ST.ProformaInvoice,		
		DSI.PaymentStatusId = ST.PaymentStatusId,
		DSI.TrackingNumber = ST.TrackingNumber,
		DSI.ExportedCigmaStatus = ST.ExportedCigmaStatus,
		DSI.PaymentDate = (CASE WHEN ST.PaymentDateJuliana > 0 
							   THEN dateadd(dd,(ST.PaymentDateJuliana  % 1000)-1,dateadd(yy,ST.PaymentDateJuliana /1000,0)) 
							   ELSE DSI.PaymentDate
						  END)
	FROM DensoHotSheet DSI	
	INNER JOIN @HotSheets as ST ON ST.Id = DSI.Id
	WHERE DSI.Id = ST.Id

	SET @CounterUpd = ISNULL(@@ROWCOUNT ,0 )	

	--Report Counters
    SELECT GETDATE(), @Counter, @CounterDel, @CounterUpd, @CounterIns
 	
 SET NOCOUNT OFF
END

