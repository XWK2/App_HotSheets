using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Authorization.Users;
using Abp.Extensions;
using Denso.HotSheet.Organization;

namespace Denso.HotSheet.Authorization.Users
{
    public class User : AbpUser<User>
    {
        public const string DefaultPassword = "123qwe";

        public virtual Guid? ProfilePictureId { get; set; }

        public long? EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();

            return user;
        }

        [ForeignKey("UserId")]
        public virtual IList<DepartmentUser> Departments { get; set; } = new List<DepartmentUser>();

        [ForeignKey("UserId")]
        public virtual IList<PlantUser> Plants { get; set; } = new List<PlantUser>();

        public static User CreateDensoUser(int tenantId, string emailAsUserName, string name, string surname, long employeeId)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = emailAsUserName,
                Name = name,
                Surname = surname,
                EmailAddress = emailAsUserName,
                Roles = new List<UserRole>(),
                EmployeeId = employeeId,
                IsEmailConfirmed = true,
                IsActive = true
            };

            user.SetNormalizedNames();

            return user;
        }
    }
}
