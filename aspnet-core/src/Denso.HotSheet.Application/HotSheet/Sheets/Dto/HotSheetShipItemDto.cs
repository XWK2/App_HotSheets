using Denso.HotSheet.Catalogs.Dto;
using Denso.HotSheet.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Denso.HotSheet.Sheets.Dto
{
    public class HotSheetShipItemDto
    {
        public long? HotSheetShiptId { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }

        public string Folio { get; set; }
        public string ProformaInvoice { get; set; }
        public string TrackingNumber { get; set; }

        public long CarrierId { get; set; }
        public string CarrierName { get; set; }

        public long ServiceId { get; set; }
        public string ServiceName { get; set; }

        public int HotSheetReasonId { get; set; }
        public string HotSheetReason { get; set; }

        public string AdditionalExplanation { get; set; }
        public int? SpecialExpeditedReasonId { get; set; }

        public int PaymentTermId { get; set; }
        public string PaymentTerm { get; set; }
        public bool Paid { get; set; }

        public int PaymentStatusId { get; set; }
        public string PaymentStatus { get; set; }

        public long PlantId { get; set; }
        public string PlantName { get; set; }

        public long CustomerId { get; set; }
        public string CustomerName { get; set; }

        public long CustomerPlantId { get; set; }
        public string CustomerPlantName { get; set; }

        public long? CustomerPlantContactId { get; set; }
        public CustomerPlantContactDto CustomerPlantContact { get; set; }

        //public CustomerPlantContactDto NewCustomerPlantContact { get; set; }

        public int HotSheetTermId { get; set; }
        public string HotSheetTerm { get; set; }

        public int? RmaAssignmentId { get; set; }
        public string OtherBy { get; set; }        
        public string RMANumber { get; set; }        
        public string BNotice { get; set; }        
        public string AccountNumber { get; set; }

        public long CostPaidById { get; set; }        
        public long FreightPaidById { get; set; }

        public long? FreightPaidByDepartmentId { get; set; }
        public string FreightPaidByOther { get; set; }
        public string FreightPrePaidExplanation { get; set; }

        public string Currency { get; set; }
        
        public long? DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public string TelephoneExt { get; set; }
        public bool ShowBehalfFields { get; set; }

        public long? IEStaffId { get; set; }
        public DateTime? IEStaffApprovalDate { get; set; }        
        public long? IEStaffApproverUserId { get; set; }
        public string IEStaffEmailAddress { get; set; }
        public bool IEStaffIsApproved { get; set; }

        public long? ManagerApprovalId { get; set; }
        public DateTime? ManagerApprovalDate { get; set; }        
        public string ManagerEmailAddress { get; set; }
        public bool ManagerIsApproved { get; set; }

        public long? AccountingApprovalId { get; set; }
        public DateTime? AccountingApprovalDate { get; set; }        
        public long? AccountingApproverUserId { get; set; }
        public string AccountingEmailAddress { get; set; }
        public bool AccountingIsApproved { get; set; }

        public int StatusId { get; set; }
        public string Status { get; set; }

        public bool IsTemplate { get; set; }
        public string TemplateName { get; set; }
        public string TemplateDescription { get; set; }

        public long CreatorUserId { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreatorFullName { get; set; }

        public UserDto Creator { get; set; }

        public int ExportedCigmaStatus { get; set; }
        public bool IsOwner { get; set; }

        public long? SentForApprovalBy { get; set; }
        public DateTime? SentForApprovalDate { get; set; }

        public string ManagerName { get; set; }
        public string IEStaffName { get; set; }
        public string AccountingName { get; set; }
        public string SentForApprovalName { get; set; }

        public DateTime? LastNotificationDate { get; set; }
        public int DaysLateApproval { get; set; }

        public List<HotSheetShipProductDto> Products { get; set; } = new List<HotSheetShipProductDto>();
        public List<HotSheetShipPackagingDto> Packaging { get; set; } = new List<HotSheetShipPackagingDto>();

        public List<HotSheetHistoryDto> History { get; set; } = new List<HotSheetHistoryDto>();
        
        public List<FileDto> Files { get; set; }
        public List<HotSheetShipManifestDto> Manifests { get; set; } = new List<HotSheetShipManifestDto>();

        public int TotalDaysPassed { get; set; }

        public long? OnBehalfOfUserId { get; set; }
        public string OnBehalfUserFullName { get; set; }
        public long? OnBehalfOfDeptoId { get; set; }
        public string OnBehalfDeptoName { get; set; }
        public string OnBehalfOfExt { get; set; }

        public string PendingApprover { get; set; }

        public bool CanUploadFiles { get; set; } = false;

        //Info Guides         
        public string GuideReference { get; set; }
        public string GuideStatusDetail { get; set; }
        public string GuideStatus { get; set; }
        public decimal? GuideCost { get; set; }
        public string GuideCurrency { get; set; }

        public DateTime? PaymentDate { get; set; }
    }
}
