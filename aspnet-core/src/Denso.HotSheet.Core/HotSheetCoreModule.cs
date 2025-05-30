﻿using Abp.Dapper;
using Abp.Localization;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Runtime.Security;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using Denso.HotSheet.Authorization.Roles;
using Denso.HotSheet.Authorization.Users;
using Denso.HotSheet.Configuration;
using Denso.HotSheet.Localization;
using Denso.HotSheet.MultiTenancy;
using Denso.HotSheet.Timing;

namespace Denso.HotSheet
{
    [DependsOn(
        typeof(AbpZeroCoreModule)
    )]
    public class HotSheetCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            HotSheetLocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = HotSheetConsts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();
            
            // Configuration.Localization.Languages.Add(new LanguageInfo("fa", "فارسی", "famfamfam-flags ir"));
            
            Configuration.Settings.SettingEncryptionConfiguration.DefaultPassPhrase = HotSheetConsts.DefaultPassPhrase;
            SimpleStringCipher.DefaultPassPhrase = HotSheetConsts.DefaultPassPhrase;

            // HotSheet Custom configs
            //Configuration.BackgroundJobs.IsJobExecutionEnabled = true;
            Configuration.Auditing.IsEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(HotSheetCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}
