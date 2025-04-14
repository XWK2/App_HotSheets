using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Denso.HotSheet.Catalogs.Dto;
using Denso.HotSheet.Configuration.Dto;

namespace Denso.HotSheet.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);

        #region SettingsParameters
        Task<List<SettingsParametersDto>> GetSettings();

        Task CreateOrUpdateSetting(SettingsParametersDto input);
        #endregion
    }
}
