using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Denso.HotSheet.Configuration;

namespace Denso.HotSheet.Web.Host.Startup
{
    [DependsOn(
       typeof(HotSheetWebCoreModule))]
    public class HotSheetWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public HotSheetWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(HotSheetWebHostModule).GetAssembly());
        }
    }
}
