using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Denso.HotSheet.Controllers
{
    public abstract class HotSheetControllerBase: AbpController
    {
        protected HotSheetControllerBase()
        {
            LocalizationSourceName = HotSheetConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
