using Abp.Authorization;
using Denso.HotSheet.Authorization.Roles;
using Denso.HotSheet.Authorization.Users;

namespace Denso.HotSheet.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
