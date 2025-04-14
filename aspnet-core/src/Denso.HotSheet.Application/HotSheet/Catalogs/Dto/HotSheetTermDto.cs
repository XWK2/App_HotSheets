using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Denso.HotSheet.Catalogs;

namespace Denso.HotSheet.Catalogs.Dto
{
    [AutoMapFrom(typeof(HotSheetTerm))]
    public class HotSheetTermDto : EntityDto<int?>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
