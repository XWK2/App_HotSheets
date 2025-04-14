CREATE OR ALTER   PROCEDURE [dbo].[spShippingIdsForAddInfoAdditional_s]
@Active bit
AS
BEGIN
 -------------------------------------------------------------------------------
 -- Creation by:    Luis Hdez Hdez
 -- Creation Date:  20 de Julio del 2023
 -- Purpose: Obtains shipping ids that need additional information
 -------------------------------------------------------------------------------

 --LHH: 28_Enero_2025, se agrego un na cuando proforma tiene dato y no traking porque truena en el servicio de por alguna razon,

	SELECT 
	   DSI.[Id]
      ,DSI.[TenantId]			--int
      ,DSI.[DocumentTypeId]		--int
      ,ISNULL(DSI.[ProformaInvoice], '') as [ProformaInvoice]	--string
      ,(CASE WHEN ISNULL(DSI.[ProformaInvoice], '') != '' 
				  AND 
				  ISNULL(DSI.[TrackingNumber],'') = '' THEN 'NA' ELSE ISNULL(DSI.[TrackingNumber],'') END) AS [TrackingNumber]			--string
      ,DSI.[CarrierId]			--long
      ,DSI.[ServiceId]			--long
      ,DSI.[HotSheetReasonId]	--int    
      ,DSI.[PaymentTermId]		--int	
      ,ISNULL(DSI.PaymentStatusId,0) as PaymentStatusId--int
      ,DSI.[PlantId]			--long
      ,DSI.[CustomerId]			--long
      ,DSI.[CustomerPlantId]	--long
      ,DSI.[HotSheetTermId]		--int
      ,ISNULL(DSI.[OtherBy],'')	as [OtherBy]		--string
      ,ISNULL(DSI.[RMANumber],'') as [RMANumber]	--string
      ,ISNULL(DSI.[BNotice],'')	as [BNotice]		--string
      ,ISNULL(DSI.[AccountNumber],'') as [AccountNumber]		--string
      ,DSI.[CostPaidById]		--long
      ,DSI.[FreightPaidById]	--long
      ,DSI.[ShowBehalfFields]	--bool
      ,DSI.[StatusId]			--int	
      ,DSI.[RmaAssignmentId]	--int   
	  ,ISNULL(DSI.ExportedCigmaStatus,0) as ExportedCigmaStatus		--int	  
	  ,(CASE WHEN DSI.PaymentDate is null THEN 0 ELSE 1 END) as PaymentDate
  FROM [DensoHotSheet] as DSI
  INNER JOIN [DensoDocumentStatuses] as SS on SS.Id = DSI.StatusId
  WHERE SS.Name = 'Approved' and (
									(
										ProformaInvoice IS NULL OR ProformaInvoice = ''
										OR 
										TrackingNumber IS NULL OR TrackingNumber = ''
									)
									OR 
									(
										paymentTermId in (1) AND 
										(
											(PaymentStatusId IS NULL OR PaymentStatusId IN (1,2))
											or
											(PaymentStatusId=3 AND PaymentDate IS NULL)
										)
									)
								)
 AND ISNULL(ExportedCigmaStatus,0) in(1,2) --and DSI.[Id] not in(173888,198661)
 -- Busca todas las shipping que hayan sido envadas al AS400 y que no tengan toda su información adicional completa
 ORDER BY Id

END

