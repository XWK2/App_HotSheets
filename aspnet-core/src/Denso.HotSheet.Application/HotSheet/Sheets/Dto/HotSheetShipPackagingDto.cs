using Abp.Domain.Entities;
using Denso.HotSheet.Catalogs.Dto;

namespace Denso.HotSheet.Sheets.Dto
{
    public class HotSheetShipPackagingDto : Entity<long?>
    {
        public long HotSheetShiptId { get; set; }
        
        public long PackagingId { get; set; }
        public PackagingDto Packaging { get; set; }

        public decimal DimensionLL { get; set; }
        public decimal DimensionWA { get; set; }        
        public decimal DimensionHA { get; set; }

        public decimal WeightPerBox { get; set; }
        public decimal BoxQuantity { get; set; }
        public decimal NetWeight { get; set; }        
        public decimal GrossWeight { get; set; }
    }
}
