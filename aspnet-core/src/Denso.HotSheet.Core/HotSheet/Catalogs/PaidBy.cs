using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoPaidBy")]
    public class PaidBy : AuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("PaidById")]
        public List<PaidByPaymentTerm> PaymentTerms { get; set; } = new List<PaidByPaymentTerm>();

        [ForeignKey("PaidById")]
        public List<PaidByHotSheetTerm> HotSheetTerms { get; set; } = new List<PaidByHotSheetTerm>();
    }
}
