using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoRMAAssignments")]
    public class RMAAssignment : AuditedEntity<int>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
