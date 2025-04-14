using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Organization
{
    [Table("DensoDepartments")]
    public class Department : AuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("DepartmentId")]
        public List<DepartmentUser> Users { get; set; } = new List<DepartmentUser>();
    }
}
