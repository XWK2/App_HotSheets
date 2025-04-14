    using Abp.Domain.Entities;
using Denso.HotSheet.Catalogs.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Sheets.Dto
{
    public class HotSheetShipDto : Entity<long?>
    {
        public int DocumentTypeId { get; set; }

        public string Folio { get; set; }
        public string ProformaInvoice { get; set; }
        public string TrackingNumber { get; set; }

        public long CarrierId { get; set; }
        public long ServiceId { get; set; }
        public int HotSheetReasonId { get; set; }

        public string AdditionalExplanation { get; set; }
        public int? SpecialExpeditedReasonId { get; set; }

        public int PaymentTermId { get; set; }
        public bool Paid { get; set; }
        public long PlantId { get; set; }
        public long CustomerId { get; set; }
        public long CustomerPlantId { get; set; }
        public long? CustomerPlantContactId { get; set; }
        public CustomerPlantContactDto CustomerPlantContact { get; set; }

        public int HotSheetTermId { get; set; }

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
        
        public string TelephoneExt { get; set; }
        public bool ShowBehalfFields { get; set; }

        public long? IEStaffId { get; set; }        
        public DateTime? IEStaffApprovalDate { get; set; }
        public string IEStaffComments { get; set; }

        public long? ManagerApprovalId { get; set; }
        public DateTime? ManagerApprovalDate { get; set; }
        public string ManagerComments { get; set; }

        public long? AccountingApprovalId { get; set; }
        public DateTime? AccountingApprovalDate { get; set; }
        public string AccountingComments { get; set; }

        
        public int StatusId { get; set; }

        public List<HotSheetShipProductDto> Products { get; set; } = new List<HotSheetShipProductDto>();
        public List<HotSheetShipPackagingDto> Packaging { get; set; } = new List<HotSheetShipPackagingDto>();
        public List<HotSheetShipManifestDto> Manifests { get; set; } = new List<HotSheetShipManifestDto>();

        public bool IsTemplate { get; set; }
        public string TemplateName { get; set; }
        public string TemplateDescription { get; set; }

        public int ExportedCigmaStatus { get; set; }

        public string CreatorFullName { get; set; }
        public bool IsOwner { get; set; }

        public long? SentForApprovalBy { get; set; }
        public DateTime? SentForApprovalDate { get; set; }

        public long? OnBehalfOfUserId { get; set; }
        public long? OnBehalfOfDeptoId { get; set; }
        public string OnBehalfOfExt { get; set; }

        //Info Guides         
        public string GuideReference { get; set; }
        public string GuideStatusDetail { get; set; }        
        public string GuideStatus { get; set; }        
        public decimal? GuideCost { get; set; }
        public string GuideCurrency { get; set; }

        public DateTime? PaymentDate { get; set; }
    }
}
