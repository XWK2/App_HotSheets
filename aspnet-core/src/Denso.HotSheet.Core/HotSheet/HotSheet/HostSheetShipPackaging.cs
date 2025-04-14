using Abp.Domain.Entities.Auditing;
using Denso.HotSheet.Catalogs;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.HotSheet
{
    [Table("DensoHotSheetShipPackaging")]
    public class HotSheetShipPackaging : AuditedEntity<long>
    {
        public long HotSheetShiptId { get; set; }

        [ForeignKey("Packaging")]
        public long? PackagingId { get; set; }
        public Packaging Packaging { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal DimensionLL { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DimensionWA { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DimensionHA { get; set; }


        [Column(TypeName = "decimal(18,3)")]
        public decimal WeightPerBox { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal BoxQuantity { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal NetWeight { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal GrossWeight { get; set; }
    }
}
