using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Denso.HotSheet.Catalogs;
using Denso.HotSheet.Organization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.HotSheet
{
    [Table("DensoHotSheetShip")]
    public class HotSheetsShip : AuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        public int DocumentTypeId { get; set; }
        public DocumentType DocumentType { get; set; }

        [StringLength(30)]
        public string ProformaInvoice { get; set; }

        [StringLength(50)]
        public string TrackingNumber { get; set; }

        public long CarrierId { get; set; }
        public Carrier Carrier { get; set; }

        public long ServiceId { get; set; }
        public Service Service { get; set; }

        public int HotSheetReasonId { get; set; }
        public HotSheetReason Reason { get; set; }

        public string AdditionalExplanation { get; set; }

        public int? SpecialExpeditedReasonId { get; set; }
        public SpecialExpeditedReason SpecialExpeditedReason { get; set; }

        public int PaymentTermId { get; set; }
        public PaymentTerm PaymentTerm { get; set; }

        public int? PaymentStatusId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        // ShippedById
        [ForeignKey("Plant")]
        public long PlantId { get; set; }
        public Plant Plant { get; set; }

        // SoldToId
        [ForeignKey("Customer")]
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }

        // ShipToId
        [ForeignKey("CustomerPlant")]
        public long CustomerPlantId { get; set; }
        public CustomerPlant CustomerPlant { get; set; }

        public long? CustomerPlantContactId { get; set; }
        public CustomerPlantContact CustomerPlantContact { get; set; }

        public int HotSheetTermId { get; set; }
        public HotSheetTerm HotSheetTerm { get; set; }

        //Shippment Details
        [ForeignKey("RMAAssignment")]
        public int? RmaAssignmentId { get; set; }
        public RMAAssignment RMAAssignment { get; set; }

        [StringLength(100)]
        public string OtherBy { get; set; }
        [StringLength(100)]
        public string RMANumber { get; set; }
        [StringLength(100)]
        public string BNotice { get; set; }
        [StringLength(100)]
        public string AccountNumber { get; set; }

        public long CostPaidById { get; set; }
        public PaidBy CostPaidBy { get; set; }

        public long FreightPaidById { get; set; }
        public PaidBy FreightPaidBy { get; set; }

        public long? FreightPaidByDepartmentId { get; set; }
        [StringLength(300)]
        public string FreightPaidByOther { get; set; }
        public string FreightPrePaidExplanation { get; set; }

        [StringLength(3)]
        public string Currency { get; set; }

        public long? DepartmentId { get; set; }
        public Department Department { get; set; }

        [StringLength(30)]
        public string TelephoneExt { get; set; }
        public bool ShowBehalfFields { get; set; }

        [ForeignKey("DocumentStatus")]
        public int StatusId { get; set; }
        public DocumentStatus Status { get; set; }

        [ForeignKey("HotSheetShiptId")]
        public IList<HotSheetShipProduct> Products { get; set; } = new List<HotSheetShipProduct>();

        [ForeignKey("HotSheetShiptId")]
        public IList<HotSheetShipPackaging> Packaging { get; set; } = new List<HotSheetShipPackaging>();

        public List<CigmaExport> CigmaExports { get; set; } = new List<CigmaExport>();

        public bool IsTemplate { get; set; }
        [StringLength(100)]
        public string TemplateName { get; set; }
        [StringLength(500)]
        public string TemplateDescription { get; set; }

        public int? ExportedCigmaStatus { get; set; }
        public DateTime? ExportedCigmaDate { get; set; }

        [ForeignKey("HotSheetShiptId")]
        public IList<HotSheetShipHistory> History { get; set; } = new List<HotSheetShipHistory>();

        public long? OnBehalfOfUserId { get; set; }
        public long? OnBehalfOfDeptoId { get; set; }
        [StringLength(50)]
        public string OnBehalfOfExt { get; set; }

        //Info Guides 
        [StringLength(50)]
        public string GuideReference { get; set; }

        [StringLength(250)]
        public string GuideStatusDetail { get; set; }

        [StringLength(25)]
        public string GuideStatus { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? GuideCost { get; set; }

        [StringLength(10)] 
        public string GuideCurrency { get; set; }
                
        public DateTime? PaymentDate { get; set; }


    }
}
