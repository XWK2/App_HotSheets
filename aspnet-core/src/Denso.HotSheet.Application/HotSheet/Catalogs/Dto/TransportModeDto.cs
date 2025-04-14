using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Denso.HotSheet.Catalogs.Dto
{
    [AutoMapFrom(typeof(TransportMode))]
    public class TransportModeDto : EntityDto<long?>
    {
        public string Code { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}
