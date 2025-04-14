using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Denso.ShippingInstructions.EntityFrameworkCore;
using Denso.ShippingInstructions.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Denso.ShippingInstructions.Web.Tests
{
    [DependsOn(
        typeof(ShippingInstructionsWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class ShippingInstructionsWebTestModule : AbpModule
    {
        public ShippingInstructionsWebTestModule(ShippingInstructionsEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ShippingInstructionsWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(ShippingInstructionsWebMvcModule).Assembly);
        }
    }
}