using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Denso.HotSheet.HotSheet;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoPartNumbers")]
    public class PartNumber : AuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [StringLength(30)]
        public string Number { get; set; }

        [StringLength(200)]
        public string Description { get; set; }        

        [StringLength(300)]
        public string DescriptionSpanish { get; set; }

        public int? UnitMeasureId { get; set; }
        public UnitMeasure UnitMeasure { get; set; }

        public long? ProductCodeSATId { get; set; }
        public ProductCodeSAT ProductCodeSAT { get; set; }
        
        public int? OriginCountryId { get; set; }

        [StringLength(100)]
        public string OriginCountry { get; set; }

        [StringLength(30)]
        public string Fraction { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? Weight { get; set; }

        [ForeignKey("PartNumberId")]
        public virtual IList<HotSheetShipProduct> HotSheetShipProducts { get; set; } = new List<HotSheetShipProduct>();
    }
}
