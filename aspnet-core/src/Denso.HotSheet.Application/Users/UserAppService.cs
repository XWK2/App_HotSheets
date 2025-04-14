using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Dapper.Repositories;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using Denso.HotSheet.Authorization;
using Denso.HotSheet.Authorization.Roles;
using Denso.HotSheet.Authorization.Users;
using Denso.HotSheet.Organization;
using Denso.HotSheet.Roles.Dto;
using Denso.HotSheet.Sheets.Dto;
using Denso.HotSheet.Users.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Denso.HotSheet.Users
{
    [AbpAuthorize]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAbpSession _abpSession;
        private readonly LogInManager _logInManager;
        private readonly IRepository<Department, long> _departmentRepository;
        private readonly IRepository<Plant, long> _plantRepository;
        private readonly IRepository<DepartmentUser, long> _departmentUserRepository;
        private readonly IRepository<PlantUser, long> _plantUserRepository;
        private readonly IRepository<Employee, long> _employeeRepository;

        private readonly IDapperRepository<User, long> _userDapperRepository;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IPasswordHasher<User> passwordHasher,
            IAbpSession abpSession,
            LogInManager logInManager,
            IRepository<Department, long> departmentRepository,
            IRepository<Plant, long> plantRepository,
            IRepository<DepartmentUser, long> departmentUserRepository,
            IRepository<PlantUser, long> plantUserRepository,
            IDapperRepository<User, long> userDapperRepository,
            IRepository<Employee, long> employeeRepository)
            : base(repository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _abpSession = abpSession;
            _logInManager = logInManager;
            _departmentRepository = departmentRepository;
            _plantRepository = plantRepository;
            _departmentUserRepository = departmentUserRepository;
            _plantUserRepository = plantUserRepository;
            _userDapperRepository = userDapperRepository;
            _employeeRepository = employeeRepository;
        }

        [AbpAuthorize(PermissionNames.Pages_Administration_Users)]
        public override async Task<PagedResultDto<UserDto>> GetAllAsync(PagedUserResultRequestDto input)
        {
            var departments = await _departmentRepository.GetAllListAsync();
            var plants = await _plantRepository.GetAllListAsync();

            var departmentUsers = await _departmentUserRepository.GetAllListAsync();
            var plantUsers = await _plantUserRepository.GetAllListAsync();

            var usersPagedResult = await base.GetAllAsync(input);

            var employeeIds = usersPagedResult.Items.Where(i => i.EmployeeId.HasValue).Select(s => s.EmployeeId).ToList();
            var employees = await _employeeRepository.GetAllListAsync(e => employeeIds.Contains(e.Id));

            foreach (var userItem in usersPagedResult.Items)
            {
                SetDensoInfoByUser(userItem, departmentUsers, plantUsers, departments, plants);

                userItem.DensoFullName = userItem.FullName;
                if (userItem.EmployeeId.HasValue)
                {
                    var employeeInfo = employees.FirstOrDefault(e => e.Id == userItem.EmployeeId.Value);
                    userItem.DensoEmployeeId = employeeInfo?.DensoEmployeeId;
                    userItem.DensoFullName = employeeInfo?.DensoEmployeeId + " - " + userItem.FullName;
                }
            }

            return usersPagedResult;
        }

        [AbpAuthorize]
        public override async Task<UserDto> GetAsync(EntityDto<long> input)
        {
            var departments = await _userDapperRepository.QueryAsync<Department>("SELECT * FROM DensoDepartments");
            var plants = await _userDapperRepository.QueryAsync<Plant>("SELECT * FROM DensoPlants");
            var departmentUsers = await _userDapperRepository.QueryAsync<DepartmentUser>("SELECT * FROM DensoDepartmentUsers");
            var plantUsers = await _userDapperRepository.QueryAsync<PlantUser>("SELECT * FROM DensoPlantUsers");

            var userDtoInfo = await base.GetAsync(input);
            
            SetDensoInfoByUser(userDtoInfo, departmentUsers.ToList(), plantUsers.ToList(), departments.ToList(), plants.ToList());

            return userDtoInfo;
        }

        private void SetDensoInfoByUser(UserDto userItem, List<DepartmentUser> departmentUsers, List<PlantUser> plantUsers,
            List<Department> departments, List<Plant> plants)
        {
            userItem.DepartmentIds = departmentUsers.Where(c => c.UserId == userItem.Id).Select(s => s.DepartmentId).ToArray();
            userItem.PlantIds = plantUsers.Where(c => c.UserId == userItem.Id).Select(s => s.PlantId).ToArray();

            userItem.Departments = departmentUsers.Where(c => c.UserId == userItem.Id)
                            .Select(s => new DepartmentUserDto
                            {
                                Id = s.Id,
                                DepartmentId = s.DepartmentId,
                                IsSupervisor = s.IsSupervisor,
                                DepartmentName = departments.FirstOrDefault(d => d.Id == s.DepartmentId)?.Name
                            }).ToList();

            userItem.Plants = plantUsers.Where(c => c.UserId == userItem.Id)
                            .Select(s => new PlantUserDto
                            {
                                Id = s.Id,
                                PlantId = s.PlantId,
                                IsSupervisor = s.IsSupervisor,
                                PlantName = plants.FirstOrDefault(d => d.Id == s.PlantId)?.Name
                            }).ToList();
        }

        [AbpAuthorize(PermissionNames.Pages_Administration_Users)]
        public override async Task<UserDto> CreateAsync(CreateUserDto input)
        {
            CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.IsEmailConfirmed = true;

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            user.Departments = null;
            user.Plants = null;

            CheckErrors(await _userManager.CreateAsync(user, input.Password));

            await UpdateUserRelationships(user, input.DepartmentIds, input.PlantIds, input.Departments, input.Plants);

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }

        [AbpAuthorize(PermissionNames.Pages_Administration_Users)]
        public override async Task<UserDto> UpdateAsync(UserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            user.Departments = null;
            user.Plants = null;

            CheckErrors(await _userManager.UpdateAsync(user));

            await UpdateUserRelationships(user, input.DepartmentIds, input.PlantIds, input.Departments, input.Plants);

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            return await GetAsync(input);
        }

        private async Task UpdateUserRelationships(User user, long[] departmentIds, long[] plantIds,
            List<DepartmentUserDto> departments, List<PlantUserDto> plants)
        {
            // Update Departments
            if (departmentIds != null)
            {
                var deptosUser = await _departmentUserRepository.GetAllListAsync(c => c.UserId == user.Id);
                var deptosUserToDelete = deptosUser.Where(c => !departmentIds.Contains(c.DepartmentId)).ToList();

                if (departments != null)
                {
                    departments.ForEach(async deptoItem =>
                    {
                        var deptoUserExists = deptosUser.Where(c => c.DepartmentId == deptoItem.DepartmentId).FirstOrDefault();
                        if (deptoUserExists != null)
                        {
                            deptoUserExists.IsSupervisor = deptoItem.IsSupervisor;
                            await _departmentUserRepository.UpdateAsync(deptoUserExists);
                        }
                        else
                        {
                            await _departmentUserRepository.InsertAsync(new DepartmentUser
                            {
                                DepartmentId = deptoItem.DepartmentId,
                                UserId = user.Id,
                                IsSupervisor = deptoItem.IsSupervisor
                            });
                        }
                    });
                }

                deptosUserToDelete.ForEach(async deptoItem =>
                {
                    await _departmentUserRepository.DeleteAsync(deptoItem.Id);
                });
            }

            // Update Plants
            if (plantIds != null)
            {
                var plantsUser = await _plantUserRepository.GetAllListAsync(c => c.UserId == user.Id);
                var plantsUserToDelete = plantsUser.Where(c => !plantIds.Contains(c.PlantId)).ToList();

                if (plants != null)
                {
                    plants.ForEach(async plantItem =>
                    {
                        var plantUserExists = plantsUser.Where(c => c.PlantId == plantItem.PlantId).FirstOrDefault();
                        if (plantUserExists != null)
                        {
                            plantUserExists.IsSupervisor = plantItem.IsSupervisor;
                            await _plantUserRepository.UpdateAsync(plantUserExists);
                        }
                        else
                        {
                            await _plantUserRepository.InsertAsync(new PlantUser
                            {
                                PlantId = plantItem.PlantId,
                                UserId = user.Id,
                                IsSupervisor = plantItem.IsSupervisor
                            });
                        }
                    });
                }

                plantsUserToDelete.ForEach(async plantItem =>
                {
                    await _plantUserRepository.DeleteAsync(plantItem.Id);
                });
            }
        }

        [AbpAuthorize(PermissionNames.Pages_Administration_Users)]
        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        [AbpAuthorize(PermissionNames.Pages_Administration_Users_Activation)]
        public async Task Activate(EntityDto<long> user)
        {
            await Repository.UpdateAsync(user.Id, async (entity) =>
            {
                entity.IsActive = true;
            });
        }

        [AbpAuthorize(PermissionNames.Pages_Administration_Users_Activation)]
        public async Task DeActivate(EntityDto<long> user)
        {
            await Repository.UpdateAsync(user.Id, async (entity) =>
            {
                entity.IsActive = false;
            });
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected override UserDto MapToEntityDto(User user)
        {
            var roleIds = user.Roles.Select(x => x.RoleId).ToArray();

            var roles = _roleManager.Roles.Where(r => roleIds.Contains(r.Id)).Select(r => r.NormalizedName);

            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();

            return userDto;
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedUserResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.Keyword) || x.Name.Contains(input.Keyword) || x.EmailAddress.Contains(input.Keyword))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            return user;
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedUserResultRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<bool> ChangePassword(ChangePasswordDto input)
        {
            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }
            
            if (await _userManager.CheckPasswordAsync(user, input.CurrentPassword))
            {
                CheckErrors(await _userManager.ChangePasswordAsync(user, input.NewPassword));
            }
            else
            {
                CheckErrors(IdentityResult.Failed(new IdentityError
                {
                    Description = "Incorrect password."
                }));
            }

            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attempting to reset password.");
            }
            
            var currentUser = await _userManager.GetUserByIdAsync(_abpSession.GetUserId());
            var loginAsync = await _logInManager.LoginAsync(currentUser.UserName, input.AdminPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Admin Password' did not match the one on record.  Please try again.");
            }
            
            if (currentUser.IsDeleted || !currentUser.IsActive)
            {
                return false;
            }
            
            var roles = await _userManager.GetRolesAsync(currentUser);
            if (!roles.Contains(StaticRoleNames.Tenants.Admin))
            {
                throw new UserFriendlyException("Only administrators may reset passwords.");
            }

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            if (user != null)
            {
                user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            return true;
        }

        public async Task<List<UserByCurrentUserDto>> GetUsersByCurrentUser()
        {
            string sqlQuery = "EXEC GetUsersByCurrentUser @UserId";
            var sqlParams = new
            {
                UserId = AbpSession.UserId
            };
            var itemsDapper = await _userDapperRepository.QueryAsync<UserByCurrentUserDto>(sqlQuery, sqlParams);
            return itemsDapper.ToList();
        }
    }
}

