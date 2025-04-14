using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoCarrierNonWorkingDays")]
    public class CarrierNonWorkingDay : AuditedEntity<long>
    {
        public long CarrierId { get; set; }

        [Column(TypeName = "date")]
        public DateTime NonWorkingDay { get; set; }
        public bool IsActive { get; set; }
    }
}
