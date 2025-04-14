using Abp.Authorization;
using Abp.Configuration;
using Abp.Net.Mail;
using Abp.Runtime.Security;
using Abp.Runtime.Session;
using Denso.HotSheet.Configuration;
using Denso.HotSheet.Email;
using Denso.HotSheet.Tenants.Dto;
using System.Threading.Tasks;
using static Denso.HotSheet.Configuration.AppSettingNames;
using Abp.Extensions;
using Denso.HotSheet.Authorization;

namespace Denso.HotSheet.Tenants
{
    [AbpAuthorize]
    public class TenantSettingsAppService : HotSheetAppServiceBase, ITenantSettingsAppService
    {
        private readonly IEmailManager _emailManager;

        public TenantSettingsAppService(IEmailManager emailManager)
        {
            _emailManager = emailManager;
        }

        #region Get Settings

        public async Task<TenantSettingsEditDto> GetAllSettings()
        {
            var settings = new TenantSettingsEditDto
            {
                Email = await GetEmailSettingsAsync(), 
                Denso = await GetDensoSettingsAsync()
            };

            return settings;
        }

        private async Task<DensoSettingsEditDto> GetDensoSettingsAsync()
        {
            int tenantId = AbpSession.GetTenantId();
            var as400Password = await SettingManager.GetSettingValueForTenantCustomAsync(DensoHotSheet.AS400.Password, tenantId);

            return new DensoSettingsEditDto
            {
                 AS400 = new DensoAS400SettingsEditDto
                 {
                    DataSource = await SettingManager.GetSettingValueForTenantCustomAsync(DensoHotSheet.AS400.DataSource, tenantId),
                    UserID = await SettingManager.GetSettingValueForTenantCustomAsync(DensoHotSheet.AS400.UserID, tenantId),
                    Password = string.IsNullOrEmpty(as400Password) ? string.Empty : SimpleStringCipher.Instance.Decrypt(as400Password),
                 },
                 Interfaces = new DensoInterfacesSettingsEditDto
                 {
                     DaysForReminders = await SettingManager.GetSettingValueForTenantCustomAsync(DensoHotSheet.Interfaces.DaysForReminders, tenantId),
                     EmailsAddressToNotify = await SettingManager.GetSettingValueForTenantCustomAsync(DensoHotSheet.Interfaces.EmailsAddressToNotify, tenantId),
                 },
                 General = new DensoGeneralSettingsEditDto
                 {
                     DaysInAdvanceForNonWorkDaysNotification = await SettingManager.GetSettingValueForTenantCustomAsync(DensoHotSheet.General.DaysInAdvanceForNonWorkDaysNotification, tenantId),

                     DefaulHelpUrl = await SettingManager.GetSettingValueForTenantCustomAsync(DensoHotSheet.General.DefaulHelpUrl, tenantId),
                 }                
            };
        }

        private async Task<TenantEmailSettingsEditDto> GetEmailSettingsAsync()
        {
            int tenantId = AbpSession.GetTenantId();

            var useHostDefaultEmailSettings = await SettingManager.GetSettingValueForTenantCustomAsync(AppSettingNames.Email.UseHostDefaultEmailSettings, tenantId);

            if (!string.IsNullOrEmpty(useHostDefaultEmailSettings))
            {
                return new TenantEmailSettingsEditDto
                {
                    UseHostDefaultEmailSettings = bool.Parse(useHostDefaultEmailSettings)
                };
            }

            var smtpPassword = await SettingManager.GetSettingValueForTenantCustomAsync(EmailSettingNames.Smtp.Password, tenantId);

            return new TenantEmailSettingsEditDto
            {
                UseHostDefaultEmailSettings = false,
                DefaultFromAddress = await SettingManager.GetSettingValueForTenantAsync(EmailSettingNames.DefaultFromAddress, tenantId),
                DefaultFromDisplayName = await SettingManager.GetSettingValueForTenantAsync(EmailSettingNames.DefaultFromDisplayName, tenantId),
                SmtpHost = await SettingManager.GetSettingValueForTenantAsync(EmailSettingNames.Smtp.Host, tenantId),
                SmtpPort = await SettingManager.GetSettingValueForTenantAsync<int>(EmailSettingNames.Smtp.Port, tenantId),
                SmtpUserName = await SettingManager.GetSettingValueForTenantAsync(EmailSettingNames.Smtp.UserName, tenantId),
                // SmtpPassword = string.IsNullOrEmpty(smtpPassword) ? string.Empty : SimpleStringCipher.Instance.Decrypt(smtpPassword),
                SmtpPassword = smtpPassword,
                SmtpDomain = await SettingManager.GetSettingValueForTenantAsync(EmailSettingNames.Smtp.Domain, tenantId),
                SmtpEnableSsl = await SettingManager.GetSettingValueForTenantAsync<bool>(EmailSettingNames.Smtp.EnableSsl, tenantId),
                SmtpUseDefaultCredentials = await SettingManager.GetSettingValueForTenantAsync<bool>(EmailSettingNames.Smtp.UseDefaultCredentials, tenantId)
            };
        }

        #endregion

        #region Update Settings

        [AbpAuthorize(PermissionNames.Pages_Administration_Settings)]
        public async Task UpdateDensoAs400Settings(DensoAS400SettingsEditDto settingsAS400)
        {
            await UpdateSettingForTenantAsync(DensoHotSheet.AS400.DataSource, settingsAS400.DataSource);
            await UpdateSettingForTenantAsync(DensoHotSheet.AS400.UserID, settingsAS400.UserID);

            string passwordEncrypted = string.Empty;
            if (!string.IsNullOrEmpty(settingsAS400.Password))
            {
                passwordEncrypted = SimpleStringCipher.Instance.Encrypt(settingsAS400.Password);
            }

            await UpdateSettingForTenantAsync(DensoHotSheet.AS400.Password, passwordEncrypted);
        }

        [AbpAuthorize(PermissionNames.Pages_Administration_Settings)]
        public async Task UpdateDensoInterfacesSettings(DensoInterfacesSettingsEditDto interfacesSettings)
        {
            await UpdateSettingForTenantAsync(DensoHotSheet.Interfaces.DaysForReminders, interfacesSettings.DaysForReminders);
            await UpdateSettingForTenantAsync(DensoHotSheet.Interfaces.EmailsAddressToNotify, interfacesSettings.EmailsAddressToNotify);
        }

        [AbpAuthorize(PermissionNames.Pages_Administration_Settings)]
        public async Task UpdateDensoGeneralSettings(DensoGeneralSettingsEditDto generalSettings)
        {
            await UpdateSettingForTenantAsync(DensoHotSheet.General.DaysInAdvanceForNonWorkDaysNotification, generalSettings.DaysInAdvanceForNonWorkDaysNotification);
        }

        private async Task UpdateSettingForTenantAsync(string settingName, string settingValue)
        {
            int tenantId = AbpSession.GetTenantId();

            await SettingManager.ChangeSettingForTenantAsync(
                tenantId,
                settingName,
                settingValue
            );
        }

        #endregion

        #region Tests

        public async Task TestEmail(string emailAddress)
        {
            string message = "Estimado Usuario,<br/> <p>Se le informa que este correo es una prueba enviada desde la Web App Hot Sheet.</p>";           

            _emailManager.Send(emailAddress, "Denso - Hot Sheet", message);
        }

        #endregion
    }

    public static class SettingManagerHelper
    {
        public static async Task<T> GetSettingValueForTenantCustomAsync<T>(this ISettingManager settingManager, string name, int tenantId)
           where T : struct
        {
            try
            {
                return (await settingManager.GetSettingValueForTenantAsync(name, tenantId)).To<T>();
            }
            catch
            {
                return default(T);
            }
        }

        public static async Task<string> GetSettingValueForTenantCustomAsync(this ISettingManager settingManager, string name, int tenantId)
        {
            try
            {
                return await settingManager.GetSettingValueForTenantAsync(name, tenantId);
            }
            catch
            {
                return null;
            }
        }       
    }
}
