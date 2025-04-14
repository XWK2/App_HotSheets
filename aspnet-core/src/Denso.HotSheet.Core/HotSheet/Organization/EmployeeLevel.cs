using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Organization
{
    [Table("DensoEmployeeLevels")]
    public class EmployeeLevel : AuditedEntity<int>
    {
        [StringLength(10)]
        public string Level { get; set; }

        [StringLength(300)]
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
