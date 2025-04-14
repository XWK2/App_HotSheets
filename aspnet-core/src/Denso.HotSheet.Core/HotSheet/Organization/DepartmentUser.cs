using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Organization
{
    [Table("DensoDepartmentUsers")]
    public class DepartmentUser : AuditedEntity<long>
    {
        public long DepartmentId { get; set; }
        public long UserId { get; set; }

        public bool IsApprover { get; set; }
        public bool IsSupervisor { get; set; }
    }
}
