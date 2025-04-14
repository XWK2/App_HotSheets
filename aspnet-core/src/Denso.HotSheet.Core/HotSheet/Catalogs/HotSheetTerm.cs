using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoHotSheetTerms")]
    public class HotSheetTerm : AuditedEntity<int>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}
