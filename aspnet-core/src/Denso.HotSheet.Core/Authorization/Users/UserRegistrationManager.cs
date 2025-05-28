using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Abp.Authorization.Users;
using Abp.Domain.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Abp.UI;
using Denso.HotSheet.Authorization.Roles;
using Denso.HotSheet.MultiTenancy;
using Denso.HotSheet.Organization;
using Abp.Domain.Repositories;
using Microsoft.Extensions.Options;
using static Denso.HotSheet.Authorization.Roles.StaticRoleNames;

namespace Denso.HotSheet.Authorization.Users
{
    public class UserRegistrationManager : DomainService
    {
        public IAbpSession AbpSession { get; set; }

        private readonly TenantManager _tenantManager;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IRepository<Employee, long> _employeeRepository;
        private readonly IRepository<DepartmentUser, long> _departmentUserRepository;
        private readonly IRepository<PlantUser, long> _plantUserRepository;

        //const string passwordBaseUser = "123qwe";
        const string passwordBaseUser = "Denso123";

        public UserRegistrationManager(
            TenantManager tenantManager,
            UserManager userManager,
            RoleManager roleManager,
            IPasswordHasher<User> passwordHasher,
            IRepository<Employee, long> employeeRepository,
            IRepository<DepartmentUser, long> departmentUserRepository,
            IRepository<PlantUser, long> plantUserRepository)
        {
            _tenantManager = tenantManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
            _employeeRepository = employeeRepository;
            _departmentUserRepository = departmentUserRepository;
            _plantUserRepository = plantUserRepository;

            AbpSession = NullAbpSession.Instance;
        }

        public async Task<User> RegisterAsync(string name, string surname, string emailAddress, string userName, string plainPassword, bool isEmailConfirmed, long? employeeId = null)
        {
            CheckForTenant();

            userName = "";

            var tenant = await GetActiveTenantAsync();

            var user = new User
            {
                TenantId = tenant.Id,
                Name = name,
                Surname = surname,
                EmailAddress = emailAddress,
                IsActive = true,
                UserName = userName,
                IsEmailConfirmed = isEmailConfirmed,
                Roles = new List<UserRole>(),
                EmployeeId = employeeId
            };

            //user.SetNormalizedNames();
           
            foreach (var defaultRole in await _roleManager.Roles.Where(r => r.IsDefault).ToListAsync())
            {
                user.Roles.Add(new UserRole(tenant.Id, user.Id, defaultRole.Id));
            }

            user.UserName = employeeId.ToString();

            user.SetNormalizedNames();

            await _userManager.InitializeOptionsAsync(tenant.Id);

            CheckErrors(await _userManager.CreateAsync(user, plainPassword));
            await CurrentUnitOfWork.SaveChangesAsync();

            return user;
        }

        public async Task CreateUsersFromEmployeesAsync(int tenantId, List<long> employeeInternalIds)
        {
            var tenant = await GetActiveTenantAsync(tenantId);
            if (tenant == null)
            {
                throw new UserFriendlyException(L("UnknownTenantId{0}", tenantId));
            }

            await _userManager.InitializeOptionsAsync(tenant.Id);

            var employeesByIds = await _employeeRepository.GetAllListAsync(c => employeeInternalIds.Contains(c.Id));
            var employeesInternalIdsUsersExists = _userManager.Users.Where(u => u.EmployeeId.HasValue && employeeInternalIds.Contains(u.EmployeeId.Value))
                            .Select( u2 => u2.EmployeeId.Value).ToList();

            var employeesToRegister = employeesByIds.Where(er => !employeesInternalIdsUsersExists.Contains(er.Id)).ToList();
            if (employeesToRegister.Count > 0)
            {
                var employeeInternalIdsToRegister = employeesToRegister.Select(e => e.Id).ToList();

                // Step 1: Create user accounts
                foreach (var employeeItem in employeesToRegister)
                {
                    var densoUser = User.CreateDensoUser(tenant.Id, employeeItem.EmailAddress, employeeItem.Name, employeeItem.Surnames, employeeItem.Id);

                    foreach (var defaultRole in await _roleManager.Roles.Where(r => r.IsDefault).ToListAsync())
                    {
                        densoUser.Roles.Add(new UserRole(tenant.Id, densoUser.Id, defaultRole.Id));
                    }
                    // TODO:
                    // - Check position or level employee and add correct role, missing create roles                

                    var userResult = await _userManager.CreateAsync(densoUser, passwordBaseUser);
                    CheckErrors(userResult);

                    await CurrentUnitOfWork.SaveChangesAsync();
                }

                // Step 2: Add departments/plants relationships to the users created
                var usersCreated = _userManager.Users.Where(u => u.EmployeeId.HasValue && employeeInternalIdsToRegister.Contains(u.EmployeeId.Value)).ToList();
                foreach (var userItem in usersCreated)
                {
                    var employeeInfo = employeesToRegister.First(e => e.Id == userItem.EmployeeId);

                    var deptoUser = _departmentUserRepository.FirstOrDefault(du => du.DepartmentId == employeeInfo.DepartmentId && du.UserId == userItem.Id);
                    if (deptoUser == null)
                    {
                        await _departmentUserRepository.InsertAsync(new DepartmentUser { DepartmentId = employeeInfo.DepartmentId.Value, UserId = userItem.Id, IsSupervisor = employeeInfo.Supervisor });
                    }

                    var plantUser = _plantUserRepository.FirstOrDefault(du => du.PlantId == employeeInfo.PlantId && du.UserId == userItem.Id);
                    if (plantUser == null)
                    {
                        await _plantUserRepository.InsertAsync(new PlantUser { PlantId = employeeInfo.PlantId.Value, UserId = userItem.Id, IsSupervisor = employeeInfo.Supervisor });
                    }
                }

                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }

        private void CheckForTenant()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                throw new InvalidOperationException("Can not register host users!");
            }
        }

        private async Task<Tenant> GetActiveTenantAsync()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return await GetActiveTenantAsync(AbpSession.TenantId.Value);
        }

        private async Task<Tenant> GetActiveTenantAsync(int tenantId)
        {
            var tenant = await _tenantManager.FindByIdAsync(tenantId);
            if (tenant == null)
            {
                throw new UserFriendlyException(L("UnknownTenantId{0}", tenantId));
            }

            if (!tenant.IsActive)
            {
                throw new UserFriendlyException(L("TenantIdIsNotActive{0}", tenantId));
            }

            return tenant;
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
