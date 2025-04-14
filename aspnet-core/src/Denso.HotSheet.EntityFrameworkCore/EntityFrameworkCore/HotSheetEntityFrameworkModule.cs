using Abp.Dapper;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using Denso.HotSheet.EntityFrameworkCore.Seed;
using System.Collections.Generic;
using System.Reflection;

namespace Denso.HotSheet.EntityFrameworkCore
{
    [DependsOn(
        typeof(HotSheetCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule),
        typeof(AbpDapperModule))]
    public class HotSheetEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<HotSheetDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        HotSheetDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        HotSheetDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(HotSheetEntityFrameworkModule).GetAssembly());
            //DapperExtensions.SetMappingAssemblies(new List<Assembly> { typeof(HotSheetCoreModule).GetAssembly() });
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                // SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}
