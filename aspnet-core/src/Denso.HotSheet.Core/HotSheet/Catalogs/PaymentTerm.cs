using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoPaymentTerms")]
    public class PaymentTerm : AuditedEntity<int>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public bool AlwaysDnmx { get; set; }
        public bool AccountingApprovalRequired { get; set; }
        public bool ExcludeOnSamples { get; set; }

        // Warning 1 => parameters for Carries-Company Names
        public string Warning1CompanyIds { get; set; }
        public string Warning1Message { get; set; }
                

        // Warning 2 => parameters Big Amounts
        public string Warning2CompanyIds { get; set; }
        public string Warning2Message { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Warning2Amount { get; set; }

        public string POWarning { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("PaymentTermId")]
        public List<PaymentTermCarrier> Carriers { get; set; } = new List<PaymentTermCarrier>();
    }    
}
