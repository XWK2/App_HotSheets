using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Denso.HotSheet.Authorization.Users;
using Denso.HotSheet.Catalogs.Dto;
using System.Collections.Generic;

namespace Denso.HotSheet.Sessions.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserLoginInfoDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public List<BasePlantDto> Plants { get; set; } = new List<BasePlantDto>();
        public List<BaseDepartmentDto> Departments { get; set; } = new List<BaseDepartmentDto>();

        public bool IsAdmin { get; set; }
        public bool IsImpoExpo { get; set; }
    }
}
