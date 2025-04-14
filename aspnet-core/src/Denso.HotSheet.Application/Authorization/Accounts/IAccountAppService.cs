using System.Threading.Tasks;
using Abp.Application.Services;
using Denso.HotSheet.Authorization.Accounts.Dto;

namespace Denso.HotSheet.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
