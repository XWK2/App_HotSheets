using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoStatusHotSheet")]
    public class StatusHotSheet : AuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}
