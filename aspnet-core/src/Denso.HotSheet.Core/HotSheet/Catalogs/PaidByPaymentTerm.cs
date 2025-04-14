using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoPaidByPaymentTerms")]
    public class PaidByPaymentTerm : CreationAuditedEntity<int>
    {
        public long PaidById { get; set; }
        public int PaymentTermId { get; set; }
    }
}
