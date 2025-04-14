
CREATE OR ALTER PROCEDURE [dbo].[HotSheetCreateOrUpdate]
		@UserId					BIGINT,
	@TenantId				INT,
	@HotSheetShiptId	BIGINT = NULL,	
	@DocumentTypeId			INT,
	@CarrierId				BIGINT,	
	@ServiceId				BIGINT,
	@HotSheetReasonId		BIGINT,
	@PaymentTermId			INT,	
	@AdditionalExplanation		VARCHAR(500),
	@SpecialExpeditedReasonId	INT = NULL,

	@PlantId				BIGINT,
	@CustomerId				BIGINT,
	@CustomerPlantId		BIGINT,
	@CustomerPlantContactId	BIGINT,
	@HotSheetTermId			INT,

	@RmaAssignmentId		INT,
	@OtherBy				VARCHAR(100),
	@RmaNumber				VARCHAR(100),
	@BNotice				VARCHAR(100),
	@AccountNumber			VARCHAR(100),
	@CostPaidById			INT,
	@FreightPaidById		INT,
	@Currency				VARCHAR(10),
	@DepartmentId			BIGINT,

	@IEStaffId				BIGINT = NULL,
	@ManagerApprovalId		BIGINT = NULL,
	@AccountingApprovalId	BIGINT = NULL,

	@ShowBehalfFields		BIT,
	@TelephoneExt			VARCHAR(100),

	@IsTemplate				BIT,
	@TemplateName			VARCHAR(300),
	@TemplateDescription	VARCHAR(300),
	
	@FreightPaidByDepartmentId	BIGINT = NULL,
	@FreightPaidByOther			VARCHAR(300),
	@FreightPrePaidExplanation	VARCHAR(500),

	@ContactName			VARCHAR(300),
	@PhoneNumber			VARCHAR(300),
	@DepartmentOrSection	VARCHAR(300),
	@NetNumber				VARCHAR(300),

	@OnBehalfOfUserId		BIGINT = NULL,
	@OnBehalfOfDeptoId		BIGINT = NULL,
	@OnBehalfOfExt			VARCHAR(50),

	@GuideReference			NVARCHAR(50) = NULL,
    @GuideStatusDetail		NVARCHAR(250) = NULL,
    @GuideStatus			NVARCHAR(25) = NULL,
    @GuideCost				DECIMAL(18,2) = NULL,
    @GuideCurrency			NVARCHAR(10) = NULL,

	@HotSheetShiptIdUpdated BIGINT OUT
AS
BEGIN
	DECLARE @StatusId INT = 1 -- Default

	IF @CustomerPlantContactId = -1
	BEGIN
		INSERT INTO dbo.DensoCustomerPlantContacts(CustomerPlantId, ContactName, PhoneNumber, DepartmentOrSection, NetNumber, IsActive, CreatorUserId, CreationTime)
		VALUES (@CustomerPlantId, @ContactName, @PhoneNumber, @DepartmentOrSection, @NetNumber, 1, @UserId, GETDATE())

		SET @CustomerPlantContactId = @@IDENTITY
	END

	IF @HotSheetShiptId IS NULL
	BEGIN
		
		-- Columns pending: ProformaInvoice, TrackingNumber, Paid, TelephoneExt, ShowBehalfFields, IEStaffApprovalDate, IEStaffComments,  ManagerApprovalDate, ManagerComments
		--					AccountingApprovalDate, AccountingComments
		-- EXEC sp_help 'dbo.DensoHotSheet';

		INSERT INTO DensoHotSheet (TenantId, DocumentTypeId, CarrierId, ServiceId, HotSheetReasonId, AdditionalExplanation, PaymentTermId, PlantId,
			CustomerId, CustomerPlantId, CustomerPlantContactId, HotSheetTermId, RmaAssignmentId, OtherBy, RMANumber, BNotice, AccountNumber, CostPaidById, 
			FreightPaidById, Currency, DepartmentId, StatusId, IsTemplate, TemplateName, TemplateDescription, CreationTime, CreatorUserId,
			PaymentStatusId, ShowBehalfFields, SpecialExpeditedReasonId, TelephoneExt, FreightPaidByDepartmentId, FreightPaidByOther, FreightPrePaidExplanation,
			OnBehalfOfUserId, OnBehalfOfDeptoId, OnBehalfOfExt,
			GuideReference,GuideStatusDetail, GuideStatus, GuideCost, GuideCurrency)
		VALUES (@TenantId, @DocumentTypeId, @CarrierId, @ServiceId, @HotSheetReasonId, @AdditionalExplanation, @PaymentTermId, @PlantId,
			@CustomerId, @CustomerPlantId, @CustomerPlantContactId, @HotSheetTermId, @RmaAssignmentId, @OtherBy, @RMANumber, @BNotice, @AccountNumber, @CostPaidById,
			@FreightPaidById, @Currency, @DepartmentId, @StatusId, @IsTemplate, @TemplateName, @TemplateDescription, GETDATE(), @UserId,
			1, @ShowBehalfFields, @SpecialExpeditedReasonId, @TelephoneExt, @FreightPaidByDepartmentId, @FreightPaidByOther, @FreightPrePaidExplanation,
			@OnBehalfOfUserId, @OnBehalfOfDeptoId, @OnBehalfOfExt,
			@GuideReference,@GuideStatusDetail, @GuideStatus, @GuideCost, @GuideCurrency)
		
		SET @HotSheetShiptId = @@IDENTITY
	END
		ELSE
		BEGIN
			UPDATE DensoHotSheet
			SET 
				DocumentTypeId = @DocumentTypeId,
				CarrierId = @CarrierId,
				ServiceId = @ServiceId,
				HotSheetReasonId = @HotSheetReasonId,
				AdditionalExplanation = @AdditionalExplanation,
				SpecialExpeditedReasonId = @SpecialExpeditedReasonId,
				PaymentTermId = @PaymentTermId,
				PlantId = @PlantId,
				CustomerId = @CustomerId,
				CustomerPlantId = @CustomerPlantId,
				CustomerPlantContactId = @CustomerPlantContactId,
				HotSheetTermId = @HotSheetTermId,
				RmaAssignmentId = @RmaAssignmentId,
				OtherBy = @OtherBy,
				RMANumber = @RMANumber,
				BNotice = @BNotice,
				AccountNumber = @AccountNumber,
				CostPaidById = @CostPaidById,
				FreightPaidById = @FreightPaidById,
				Currency = @Currency,
				DepartmentId = @DepartmentId,
				IsTemplate = @IsTemplate,
				TemplateName = @TemplateName,
				TemplateDescription = @TemplateDescription,
				LastModificationTime = GETDATE(),
				LastModifierUserId = @UserId,
				ShowBehalfFields = @ShowBehalfFields,
				TelephoneExt = @TelephoneExt,
				FreightPaidByDepartmentId = @FreightPaidByDepartmentId,
				FreightPaidByOther = @FreightPaidByOther,
				FreightPrePaidExplanation = @FreightPrePaidExplanation,
				OnBehalfOfUserId = @OnBehalfOfUserId,
				OnBehalfOfDeptoId = @OnBehalfOfDeptoId,
				OnBehalfOfExt = @OnBehalfOfExt,
				GuideReference = @GuideReference,
				GuideStatusDetail = @GuideStatusDetail, 
				GuideStatus = @GuideStatus, 
				GuideCost = @GuideCost, 
				GuideCurrency = @GuideCurrency
			WHERE Id = @HotSheetShiptId
		END

		SET @HotSheetShiptIdUpdated = @HotSheetShiptId;

		IF @IEStaffId IS NOT NULL OR @ManagerApprovalId IS NOT NULL OR @AccountingApprovalId IS NOT NULL
		BEGIN
			IF NOT EXISTS(SELECT 1 FROM DensoHotSheetApprovals WHERE HotSheetShiptId = @HotSheetShiptIdUpdated)
			BEGIN
				INSERT INTO DensoHotSheetApprovals (HotSheetShiptId, IEStaffId, ManagerApprovalId, AccountingApprovalId, CreationTime, CreatorUserId)
				VALUES (@HotSheetShiptIdUpdated, @IEStaffId, @ManagerApprovalId, @AccountingApprovalId, GETDATE(), @userId)
			END
				ELSE
				BEGIN
					UPDATE DensoHotSheetApprovals
					SET IEStaffId = @IEStaffId,
						ManagerApprovalId = @ManagerApprovalId,
						AccountingApprovalId = @AccountingApprovalId,
						LastModificationTime = GETDATE(),
						LastModifierUserId = @UserId
					WHERE HotSheetShiptId = @HotSheetShiptIdUpdated
				END
		END
END
GO
