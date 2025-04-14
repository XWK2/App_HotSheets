using Abp.MultiTenancy;
using Denso.HotSheet.Authorization.Users;

namespace Denso.HotSheet.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
