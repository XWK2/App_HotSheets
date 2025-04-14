using Abp.Application.Services.Dto;

namespace Denso.HotSheet.Catalogs.Dto
{
    public class PackagingDto : EntityDto<long?>
    {
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
