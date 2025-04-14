using Abp.Application.Services;
using Denso.HotSheet.Tenants.Dto;
using System.Threading.Tasks;

namespace Denso.HotSheet.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();
        Task UpdateDensoAs400Settings(DensoAS400SettingsEditDto settingsAS400);
        Task UpdateDensoInterfacesSettings(DensoInterfacesSettingsEditDto interfacesSettings);

        Task TestEmail(string emailAddress);
    }
}
