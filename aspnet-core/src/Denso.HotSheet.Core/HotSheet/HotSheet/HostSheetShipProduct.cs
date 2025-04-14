using Abp.Domain.Entities.Auditing;
using Denso.HotSheet.Catalogs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.HotSheet
{
    [Table("DensoHotSheetShipProducts")]
    public class HotSheetShipProduct : AuditedEntity<long>
    {
        public long HotSheetShiptId { get; set; }

        [ForeignKey("PartNumber")]
        public long? PartNumberId { get; set; }

        [ForeignKey("PartNumberInternal")]
        public long? PartNumberInternalId { get; set; }

        public int? UnitMeasureId { get; set; }
        public UnitMeasure UnitMeasure { get; set; }

        public string Description { get; set; }
        public string DescriptionSpanish { get; set; }
        
        public long? ProductCodeSATId { get; set; }
        public ProductCodeSAT ProductCodeSAT { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Total { get; set; }

        [StringLength(100)]
        public string Model { get; set; }
        [StringLength(100)]
        public string Serial { get; set; }
        [StringLength(100)]
        public string Maker { get; set; }
        [StringLength(100)]
        public string TechInfo { get; set; }
        [StringLength(100)]
        public string PoNumber { get; set; }

        [ForeignKey("Country")]
        public int? OriginCountryId { get; set; }
        public Country OriginCountry { get; set; }
    }
}
