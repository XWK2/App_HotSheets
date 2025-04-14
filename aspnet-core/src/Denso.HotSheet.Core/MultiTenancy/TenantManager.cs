using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using Denso.HotSheet.Authorization.Users;
using Denso.HotSheet.Editions;

namespace Denso.HotSheet.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore)
        {
        }
    }
}
