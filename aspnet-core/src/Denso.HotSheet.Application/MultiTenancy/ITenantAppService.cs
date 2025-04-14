using Abp.Application.Services;
using Denso.HotSheet.MultiTenancy.Dto;

namespace Denso.HotSheet.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

