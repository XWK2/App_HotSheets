using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoPaymentStatus")]
    public class PaymentStatus : CreationAuditedEntity<int>
    {
        [StringLength(10)]
        public string Code { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}
