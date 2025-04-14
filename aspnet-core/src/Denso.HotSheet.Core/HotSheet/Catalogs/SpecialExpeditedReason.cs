using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoSpecialExpeditedReasons")]
    public class SpecialExpeditedReason : AuditedEntity<int>
    {
        [StringLength(300)]
        public string Reason { get; set; }

        public bool IsActive { get; set; }
    }
}
