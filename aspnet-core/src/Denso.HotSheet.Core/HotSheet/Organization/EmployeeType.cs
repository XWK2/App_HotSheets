using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Organization
{
    [Table("DensoEmployeeTypes")]
    public class EmployeeType : AuditedEntity<int>
    {
        [StringLength(300)]
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
