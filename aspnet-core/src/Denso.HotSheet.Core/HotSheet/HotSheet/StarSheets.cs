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
    [Table("DensoStarSheets")]
    public class StarSheets : AuditedEntity<long>,IMayHaveTenant
    {
        //public long Id { get; set; }
        public int? TenantId { get; set; }

        [StringLength(30)]
        public string PlannerCode { get; set; }
        [StringLength(255)]
        public string PlannerName { get; set; }
        [StringLength(30)]
        public string SupplierCode { get; set; }
        [StringLength(255)]
        public string SupplierName { get; set; }
        [StringLength(255)]
        public string PartNumber { get; set; }
        [StringLength(255)]
        public string PartDescription { get; set; }
        public int? InTransitQty { get; set; }
        public int? DiaStockIn { get; set; }
        [StringLength(255)]
        public string DiaLocation { get; set; }
        public int? CIGMAReceived { get; set; }

        // TransportModeId        
        [ForeignKey("TransportMode")]
        public long? TransportModeId { get; set; }
        public TransportMode TransportMode { get; set; }
        public int? DeliveryOrder { get; set; }
        [StringLength(1000)]
        public string TrafficContainerFX { get; set; }
        [StringLength(1000)]
        public string UnitNumber { get; set; }
        public DateTime? EtaDNMX { get; set; }

        // StatusId                
        [ForeignKey("StatusHotSheet")]
        public long? StatusId { get; set; }
        public StatusHotSheet StatusHotSheet { get; set; }
        public DateTime? RealShortageDate { get; set; }

        // ShortageShiftId                
        [ForeignKey("ShortageShift")]        
        public long? ShortageShiftId { get; set; }

        public ShortageShift ShortageShift { get; set; }
        public string Shortage { get; set; }

        public int? ASN { get; set; }

        [StringLength(1000)]
        public string PCComments { get; set; }


        //Campos nuevos inicio
        public decimal StockShortageDate { get; set; }
        public string ShortagePriority { get; set; }
        public decimal OnHandTotalQty { get; set; }
        public decimal RequirementTotalQty { get; set; }
        public decimal OverDefProdQtyFromParentPartNo { get; set; }
        public decimal OverDefRecQty { get; set; }
        public decimal ShortageNoticeDateFrom { get; set; }
        public decimal Stock2 { get; set; }
        //Campos nuevos fin

    }
}
