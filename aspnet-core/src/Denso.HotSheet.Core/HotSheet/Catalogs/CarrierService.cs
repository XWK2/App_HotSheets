using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoCarrierService")]
    public class CarrierService : CreationAuditedEntity<long>
    {
        public long CarrierId { get; set; }

        public long? ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
