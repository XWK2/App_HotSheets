using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoUnitMeasuresSAT")]
    public class UnitMeasureSAT : AuditedEntity<int>
    {
        [StringLength(100)]
        public string Code { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public int? UnitMeasureId { get; set; }
        public UnitMeasure UnitMeasure { get; set; }

        public bool IsActive { get; set; }
    }
}
