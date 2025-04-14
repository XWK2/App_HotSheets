using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Organization
{
    [Table("DensoEmployeePositions")]
    public class EmployeePosition : AuditedEntity<int>
    {
        [StringLength(10)]
        public string Code { get; set; }

        [StringLength(300)]
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
