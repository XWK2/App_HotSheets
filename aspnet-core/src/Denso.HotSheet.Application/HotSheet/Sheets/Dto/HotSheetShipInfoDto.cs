using Abp.Domain.Entities;
using Denso.HotSheet.Catalogs.Dto;

namespace Denso.HotSheet.Sheets.Dto
{
    public class HotSheetShipInfoDto : Entity<long?>
    {
        public string Folio { get; set; }
        
        public long PlantId { get; set; }
        public PlantDto Plant { get; set; }
    }
}
