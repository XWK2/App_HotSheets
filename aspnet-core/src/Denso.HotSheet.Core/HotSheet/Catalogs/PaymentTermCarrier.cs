using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoPaymentTermCarriers")]
    public class PaymentTermCarrier : CreationAuditedEntity<long>
    {
        public int PaymentTermId { get; set; }

        public long CarrierId { get; set; }
        public int WarningType { get; set; }
    }
}
