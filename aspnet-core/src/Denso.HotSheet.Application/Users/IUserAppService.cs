using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Denso.HotSheet.Roles.Dto;
using Denso.HotSheet.Sheets.Dto;
using Denso.HotSheet.Users.Dto;

namespace Denso.HotSheet.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task DeActivate(EntityDto<long> user);
        Task Activate(EntityDto<long> user);
        Task<ListResultDto<RoleDto>> GetRoles();
        Task ChangeLanguage(ChangeUserLanguageDto input);

        Task<bool> ChangePassword(ChangePasswordDto input);

        Task<List<UserByCurrentUserDto>> GetUsersByCurrentUser();
    }
}
