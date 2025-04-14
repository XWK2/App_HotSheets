using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoPaidByHotSheetTerms")]
    public class PaidByHotSheetTerm : CreationAuditedEntity<int>
    {
        public long PaidById { get; set; }
        public int HotSheetTermId { get; set; }
    }
}
