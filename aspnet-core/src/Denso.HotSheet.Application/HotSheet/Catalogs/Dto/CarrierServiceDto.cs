using Abp.Application.Services.Dto;

namespace Denso.HotSheet.Catalogs.Dto
{
    public class CarrierServiceDto : EntityDto<long?>
    {
        public long CarrierId { get; set; }

        public long ServiceId { get; set; }
        public ServiceDto Service { get; set; }
    }
}
