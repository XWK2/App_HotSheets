using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoUnitMeasures")]
    public class UnitMeasure : AuditedEntity<int>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(10)]
        public string DensoCode { get; set; }

        [StringLength(10)]
        public string SatCode { get; set; }

        [StringLength(10)]
        public string SegroveCode { get; set; }

        public bool IsActive { get; set; }
    }
}
