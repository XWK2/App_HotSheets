using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Denso.HotSheet.Organization;

namespace Denso.HotSheet.Catalogs.Dto
{
    [AutoMapFrom(typeof(Department))]
    public class DepartmentDto : EntityDto<long?>
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public int TotalUsers { get; set; }
        public string FullName { get; set; }
    }

    [AutoMapFrom(typeof(Department))]
    public class BaseDepartmentDto : EntityDto<long?>
    {
        public string Name { get; set; }
    }
}
