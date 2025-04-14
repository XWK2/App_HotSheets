using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoCarriers")]
    public class Carrier : AuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [StringLength(300)]
        public string Name { get; set; }

        public int DocumentTypeId { get; set; }
        public DocumentType DocumentType { get; set; }

        public int? DivisorNumber { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("CarrierId")]
        public IList<CarrierService> Services { get; set; } = new List<CarrierService>();

        [ForeignKey("CarrierId")]
        public IList<PaymentTermCarrier> PaymentTerms { get; set; } = new List<PaymentTermCarrier>();

        [ForeignKey("CarrierId")]
        public IList<CarrierNonWorkingDay> NonWorkingDays { get; set; } = new List<CarrierNonWorkingDay>();
    }
}
