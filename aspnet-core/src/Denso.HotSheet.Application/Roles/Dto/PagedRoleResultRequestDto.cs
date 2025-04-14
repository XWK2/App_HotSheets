using Abp.Application.Services.Dto;

namespace Denso.HotSheet.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

