using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Denso.HotSheet.Catalogs.Dto
{
    [AutoMapFrom(typeof(PaidByHotSheetTerm))]
    public class PaidByHotSheetTermDto : EntityDto<int?>
    {
        public long PaidById { get; set; }
        public int HotSheetTermId { get; set; }
    }
}
