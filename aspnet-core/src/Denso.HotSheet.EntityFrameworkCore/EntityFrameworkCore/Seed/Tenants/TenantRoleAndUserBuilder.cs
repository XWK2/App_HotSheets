using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Denso.HotSheet.Authorization;
using Denso.HotSheet.Authorization.Roles;
using Denso.HotSheet.Authorization.Users;
using System.Collections.Generic;

namespace Denso.HotSheet.EntityFrameworkCore.Seed.Tenants
{
    public class TenantRoleAndUserBuilder
    {
        private readonly HotSheetDbContext _context;
        private readonly int _tenantId;

        public TenantRoleAndUserBuilder(HotSheetDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateRolesAndUsers();
            CreateDensoRoles();
        }

        private void CreateRolesAndUsers()
        {
            // Admin role

            var adminRole = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Admin);
            if (adminRole == null)
            {
                adminRole = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Admin, StaticRoleNames.Tenants.Admin) { IsStatic = true }).Entity;
                _context.SaveChanges();
            }

            // Grant all permissions to admin role

            var grantedPermissions = _context.Permissions.IgnoreQueryFilters()
                .OfType<RolePermissionSetting>()
                .Where(p => p.TenantId == _tenantId && p.RoleId == adminRole.Id)
                .Select(p => p.Name)
                .ToList();

            var permissions = PermissionFinder
                .GetAllPermissions(new HotSheetAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant) &&
                            !grantedPermissions.Contains(p.Name))
                .ToList();

            if (permissions.Any())
            {
                _context.Permissions.AddRange(
                    permissions.Select(permission => new RolePermissionSetting
                    {
                        TenantId = _tenantId,
                        Name = permission.Name,
                        IsGranted = true,
                        RoleId = adminRole.Id
                    })
                );
                _context.SaveChanges();
            }

            // Admin user

            var adminUser = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.TenantId == _tenantId && u.UserName == AbpUserBase.AdminUserName);
            if (adminUser == null)
            {
                adminUser = User.CreateTenantAdminUser(_tenantId, "admin@defaulttenant.com");
                adminUser.Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(adminUser, "123qwe");
                adminUser.IsEmailConfirmed = true;
                adminUser.IsActive = true;

                _context.Users.Add(adminUser);
                _context.SaveChanges();

                // Assign Admin role to admin user
                _context.UserRoles.Add(new UserRole(_tenantId, adminUser.Id, adminRole.Id));
                _context.SaveChanges();
            }
        }

        private void CreateDensoRoles()
        {
            // Authors: Create role and grant permissions
            int roleIdAuthors = CreateRole(StaticRoleNames.Tenants.Authors, true);            
            var grantedPermissionsAuthors = GetGrantedPermissions(roleIdAuthors);
            var permissionsAuthors = GetPermissionsAuthors(grantedPermissionsAuthors);
            GrantPermissions(roleIdAuthors, permissionsAuthors);            

            // Approvers: Create role and grant permissions            
            int roleIdApprovers = CreateRole(StaticRoleNames.Tenants.Approvers, false);
            var grantedPermissionsApprovers = GetGrantedPermissions(roleIdApprovers);
            var permissionsApprovers = GetPermissionsApprovers(grantedPermissionsApprovers);
            GrantPermissions(roleIdApprovers, permissionsApprovers);

            // StaffImpoExpo: Create role and grant permissions
            int roleIdStaffImpoExpo = CreateRole(StaticRoleNames.Tenants.StaffImpoExpo, false);
            var grantedPermissionsStaffImpoExpo = GetGrantedPermissions(roleIdStaffImpoExpo);
            var permissionsStaffImpoExpo = GetPermissionsStaffImpoExpo(grantedPermissionsStaffImpoExpo);
            GrantPermissions(roleIdStaffImpoExpo, permissionsStaffImpoExpo);

            // StaffAccounting: Create role and grant permissions
            int roleIdStaffAccounting = CreateRole(StaticRoleNames.Tenants.StaffAccounting, false);
            var grantedPermissionsStaffAccounting = GetGrantedPermissions(roleIdStaffAccounting);
            var permissionsStaffAccounting = GetPermissionsStaffAccounting(grantedPermissionsStaffAccounting);
            GrantPermissions(roleIdStaffAccounting, permissionsStaffAccounting);
        }

        private void GrantPermissions(int roleId, List<Permission> permissionsByRole)
        {
            if (permissionsByRole.Any())
            {
                _context.Permissions.AddRange(
                    permissionsByRole.Select(permission => new RolePermissionSetting
                    {
                        TenantId = _tenantId,
                        Name = permission.Name,
                        IsGranted = true,
                        RoleId = roleId
                    })
                );
                _context.SaveChanges();
            }
        }

        private int CreateRole(string roleName, bool isDefault)
        {
            var roleToAdd = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Name == roleName);
            if (roleToAdd == null)
            {
                roleToAdd = _context.Roles.Add(new Role(_tenantId, roleName, roleName) { IsStatic = true, IsDefault = isDefault }).Entity;
                _context.SaveChanges();
            }
            return roleToAdd.Id;
        }


        private List<string> GetGrantedPermissions(int roleId)
        {
            var grantedPermissions = _context.Permissions.IgnoreQueryFilters()
                .OfType<RolePermissionSetting>()
                .Where(p => p.TenantId == _tenantId && p.RoleId == roleId)
                .Select(p => p.Name)
                .ToList();

            return grantedPermissions;
        }

        private List<Permission> GetPermissionsAuthors(List<string> grantedPermissions)
        {
            var allowedPermissions = GetAllowedPermissionsAuthors();

            var permissions = PermissionFinder
                .GetAllPermissions(new HotSheetAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant) &&
                            !grantedPermissions.Contains(p.Name))
                .Where(p => allowedPermissions.Contains(p.Name))
                .ToList();

            return permissions;
        }

        private List<Permission> GetPermissionsApprovers(List<string> grantedPermissions)
        {
            var allowedPermissions = GetAllowedPermissionsApprovers();

            var permissions = PermissionFinder
                .GetAllPermissions(new HotSheetAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant) &&
                            !grantedPermissions.Contains(p.Name))
                .Where(p => allowedPermissions.Contains(p.Name))
                .ToList();

            return permissions;
        }

        private List<Permission> GetPermissionsStaffImpoExpo(List<string> grantedPermissions)
        {
            var allowedPermissions = GetAllowedPermissionsStaffImpoExpo();

            var permissions = PermissionFinder
                .GetAllPermissions(new HotSheetAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant) &&
                            !grantedPermissions.Contains(p.Name))
                .Where(p => allowedPermissions.Contains(p.Name))
                .ToList();

            return permissions;
        }

        private List<Permission> GetPermissionsStaffAccounting(List<string> grantedPermissions)
        {
            var allowedPermissions = GetAllowedPermissionsStaffAccounting();

            var permissions = PermissionFinder
                .GetAllPermissions(new HotSheetAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant) &&
                            !grantedPermissions.Contains(p.Name))
                .Where(p => allowedPermissions.Contains(p.Name))
                .ToList();

            return permissions;
        }

        private List<string> GetAllowedPermissionsAuthors()
        {
            string[] permissions = {
                "Pages.HotSheet",
                "Pages.HotSheet.Create",
                "Pages.HotSheet.Edit",
                "Pages.HotSheet.Cancel",
                // "Pages.HotSheet.Approvals",
                "Pages.HotSheet.ExportToAS400",
                "Pages.HotSheet.PendingForApproval",
                "Pages.HotSheet.Templates",

                "Pages.HotSheets",
            };

            return permissions.ToList();
        }

        private List<string> GetAllowedPermissionsApprovers()
        {
            string[] permissions = {
                "Pages.HotSheet",
                "Pages.HotSheet.Approvals",                
                "Pages.HotSheet.PendingForApproval",

                "Pages.HotSheets",
            };

            return permissions.ToList();
        }

        private List<string> GetAllowedPermissionsStaffImpoExpo()
        {
            string[] permissions = {
                "Pages.HotSheet",
                "Pages.HotSheet.Create",
                "Pages.HotSheet.Edit",
                "Pages.HotSheet.Cancel",
                "Pages.HotSheet.Approvals",
                "Pages.HotSheet.ExportToAS400",
                "Pages.HotSheet.PendingForApproval",
                "Pages.HotSheet.Templates",

                "Pages.Surveys",
                "Pages.HotSheets",
            };

            return permissions.ToList();
        }

        private List<string> GetAllowedPermissionsStaffAccounting()
        {
            string[] permissions = {
                "Pages.HotSheet",
                "Pages.HotSheet.Create",
                "Pages.HotSheet.Edit",
                "Pages.HotSheet.Cancel",
                "Pages.HotSheet.Approvals",
                "Pages.HotSheet.ExportToAS400",
                "Pages.HotSheet.PendingForApproval",
                "Pages.HotSheet.Templates",

                "Pages.Surveys",
                "Pages.HotSheets",
            };

            return permissions.ToList();
        }
    }
}
