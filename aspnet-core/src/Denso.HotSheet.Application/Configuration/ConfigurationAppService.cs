using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Dapper.Repositories;
using Abp.Domain.Repositories;
using Abp.Configuration;
using Abp.Runtime.Session;
using Denso.HotSheet.Configuration.Dto;
using Denso.HotSheet.Organization;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Denso.HotSheet.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : HotSheetAppServiceBase, IConfigurationAppService
    {
        private readonly IRepository<SettingsParameters> _settingParametersRepository;

        public ConfigurationAppService(
            IRepository<SettingsParameters> settingParametersRepository
        )
        {
            _settingParametersRepository = settingParametersRepository;
        }

        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }        

        #region Setting

        public async Task<List<SettingsParametersDto>> GetSettings()
        {
            var items = await _settingParametersRepository.GetAllListAsync();
            var itemsDto = ObjectMapper.Map<List<SettingsParametersDto>>(items);

            return new List<SettingsParametersDto>(itemsDto);
        }

        public async Task CreateOrUpdateSetting(SettingsParametersDto input)
        {
            if (input.Id.HasValue)
            {
                var settingParameter = await _settingParametersRepository.GetAsync((int)input.Id.Value);
                if (settingParameter != null)
                {
                    settingParameter.Name = input.Name;
                    settingParameter.Value = input.Value;

                    await _settingParametersRepository.UpdateAsync(settingParameter);
                }
            }
            else
            {
                var settingParameter = ObjectMapper.Map<SettingsParameters>(input);
                await _settingParametersRepository.InsertAsync(settingParameter);
            }
        }

        #endregion

    }
}
