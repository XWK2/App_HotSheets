using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoServices")]
    public class Service : AuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public bool IsNational { get; set; }
        public bool IsInternational { get; set; }
        public bool ShowHigestCostWarning { get; set; }

        // Shipping/Services Types
        public bool Ground { get; set; }
        public bool Air { get; set; }
        public bool Sea { get; set; }

        public bool IsActive { get; set; }
    }
}
