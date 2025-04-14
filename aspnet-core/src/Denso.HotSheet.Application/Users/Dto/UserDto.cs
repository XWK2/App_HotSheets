using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Denso.HotSheet.Authorization.Users;
using Denso.HotSheet.Organization;

namespace Denso.HotSheet.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserDto : EntityDto<long>
    {
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        public bool IsActive { get; set; }

        public string FullName { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public DateTime CreationTime { get; set; }

        public string[] RoleNames { get; set; }

        public long[] DepartmentIds { get; set; }
        public long[] PlantIds { get; set; }

        public List<DepartmentUserDto> Departments { get; set; }
        public List<PlantUserDto> Plants { get; set; }

        public long? EmployeeId { get; set; }
        public long? DensoEmployeeId { get; set; }

        public string DensoFullName { get; set; }
    }

    [AutoMapTo(typeof(DepartmentUser))]
    public class DepartmentUserDto : EntityDto<long>
    {
        public long DepartmentId { get; set; }
        public bool IsSupervisor { get; set; }
        public string DepartmentName { get; set; }
    }

    [AutoMapTo(typeof(PlantUser))]
    public class PlantUserDto : EntityDto<long>
    {
        public long PlantId { get; set; }
        public bool IsSupervisor { get; set; }
        public string PlantName { get; set; }
    }
}
