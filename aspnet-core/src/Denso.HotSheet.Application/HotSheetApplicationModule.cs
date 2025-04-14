using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Denso.HotSheet.Authorization;

namespace Denso.HotSheet
{
    [DependsOn(
        typeof(HotSheetCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class HotSheetApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<HotSheetAuthorizationProvider>();

            // Adding HotSheet custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(HotSheetCustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(HotSheetApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
