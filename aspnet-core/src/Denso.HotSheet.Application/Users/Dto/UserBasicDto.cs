using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Denso.HotSheet.Authorization.Users;

namespace Denso.HotSheet.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserBasicDto : EntityDto<long>
    {
        public string UserName { get; set; }  
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
        public string FullName { get; set; }

        public long? EmployeeId { get; set; }
        public string DensoFullName { get; set; }
    }
}
