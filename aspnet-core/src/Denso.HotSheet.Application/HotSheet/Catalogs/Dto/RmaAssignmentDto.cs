using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Denso.HotSheet.Catalogs.Dto
{
    [AutoMapFrom(typeof(RMAAssignment))]
    public class RmaAssignmentDto : EntityDto<int?>
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
