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
    [Table("DensoPurchaseOrders")]
    public class PurchaseOrders : AuditedEntity<long>,IMayHaveTenant
    {
        //public long Id { get; set; }
        public int? TenantId { get; set; }

        [StringLength(30)]
        public string PlannerCode { get; set; }

        [StringLength(255)]
        public string PlannerName { get; set; }

        [StringLength(30)]
        public string PurchaseOrder { get; set; }

        public int? Line { get; set; }

        [StringLength(30)]
        public string PartNumber { get; set; }
        [StringLength(255)]
        public string PartDescription { get; set; }

        [StringLength(30)]
        public string SupplierCode { get; set; }
        
        [StringLength(255)]
        public string SupplierName { get; set; }

        public int? Qty { get; set; }

        public DateTime? RequiredDate { get; set; }

        public long? StatusId { get; set; }

        [StringLength(30)]
        public string Ticket { get; set; }       

    }
}
