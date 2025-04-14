using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Configuration;

namespace Denso.HotSheet.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return GetDefaultSettings()
                .Union(GetTenantSettings());
                //.Union(GetUserSettings());
        }

        private IEnumerable<SettingDefinition> GetDefaultSettings()
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.UiTheme, "red", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider())
            };
        }

        private IEnumerable<SettingDefinition> GetTenantSettings()
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.DensoHotSheet.AS400.DataSource, string.Empty, scopes: SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.DensoHotSheet.AS400.UserID, string.Empty, scopes: SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.DensoHotSheet.AS400.Password, string.Empty, scopes: SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.DensoHotSheet.Interfaces.DaysForReminders, "3", scopes: SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.DensoHotSheet.Interfaces.EmailsAddressToNotify, string.Empty, scopes: SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.DensoHotSheet.General.DaysInAdvanceForNonWorkDaysNotification, "3", scopes: SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.DensoHotSheet.General.DefaulHelpUrl, "7", scopes: SettingScopes.Tenant),
            };
        }
    }
}
