using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Denso.HotSheet.Authorization.Roles;
using Denso.HotSheet.Authorization.Users;
using Denso.HotSheet.Catalogs.Dto;
using Denso.HotSheet.Organization;
using Denso.HotSheet.Sessions.Dto;
using Microsoft.EntityFrameworkCore;

namespace Denso.HotSheet.Sessions
{
    public class SessionAppService : HotSheetAppServiceBase, ISessionAppService
    {
        private readonly IRepository<Department, long> _departmentRepository;
        private readonly IRepository<Plant, long> _plantRepository;
        private readonly IRepository<DepartmentUser, long> _departmentUserRepository;
        private readonly IRepository<PlantUser, long> _plantUserRepository;
        private readonly UserManager _userManager;

        public SessionAppService(
            IRepository<Department, long> departmentRepository,
            IRepository<Plant, long> plantRepository,
            IRepository<DepartmentUser, long> departmentUserRepository,
            IRepository<PlantUser, long> plantUserRepository,
            UserManager userManager
            )
        {
            _departmentRepository = departmentRepository;
            _plantRepository = plantRepository;
            _departmentUserRepository = departmentUserRepository;
            _plantUserRepository = plantUserRepository;
            _userManager = userManager;
        }

        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                Application = new ApplicationInfoDto
                {
                    Version = AppVersionHelper.Version,
                    ReleaseDate = AppVersionHelper.ReleaseDate,
                    Features = new Dictionary<string, bool>()
                }
            };

            if (AbpSession.TenantId.HasValue)
            {
                output.Tenant = ObjectMapper.Map<TenantLoginInfoDto>(await GetCurrentTenantAsync());
            }

            if (AbpSession.UserId.HasValue)
            {
                var currentUser = await GetCurrentUserAsync();
                output.User = ObjectMapper.Map<UserLoginInfoDto>(currentUser);

                var departmentIdsByUser = _departmentUserRepository.GetAll()
                        .Where(du => du.UserId == AbpSession.UserId)
                        .Select(d => d.DepartmentId)
                        .ToList();

                if (departmentIdsByUser.Count > 0)
                {
                    var departments = _departmentRepository.GetAll();
                    var departmentsByUser = departments.Where(d => departmentIdsByUser.Contains(d.Id)).ToList();
                    output.User.Departments.AddRange(ObjectMapper.Map<List<BaseDepartmentDto>>(departmentsByUser));
                }

                var plantIdsByUser = _plantUserRepository.GetAll()
                        .Where(du => du.UserId == AbpSession.UserId)
                        .Select(d => d.PlantId)
                        .ToList();

                if (plantIdsByUser.Count > 0)
                {
                    var plants = _plantRepository.GetAll();
                    var plantsByUser = plants.Where(d => plantIdsByUser.Contains(d.Id)).ToList();
                    output.User.Plants.AddRange(ObjectMapper.Map<List<BasePlantDto>>(plantsByUser));
                }

                var rolesByUser = await _userManager.GetRolesAsync(currentUser);
                output.User.IsAdmin = rolesByUser.Any() && rolesByUser.Contains(StaticRoleNames.Tenants.Admin);
                output.User.IsPC = rolesByUser.Any() && rolesByUser.Contains(StaticRoleNames.Tenants.PC);
                output.User.IsImpoExpo = rolesByUser.Any() && rolesByUser.Contains(StaticRoleNames.Tenants.StaffImpoExpo);
            }

            return output;
        }
    }
}
