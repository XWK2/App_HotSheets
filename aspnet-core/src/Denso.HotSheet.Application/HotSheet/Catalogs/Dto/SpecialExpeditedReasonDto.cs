using Abp.Application.Services.Dto;

namespace Denso.HotSheet.Catalogs.Dto
{
    public class SpecialExpeditedReasonDto : EntityDto<int?>
    {
        public string Reason { get; set; }
        public bool IsActive { get; set; }
    }
}
