using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Denso.HotSheet.Configuration.Dto
{
    [AutoMapFrom(typeof(SettingsParameters))]
    public class SettingsParametersDto : EntityDto<int?>
    {
        public int? TenantId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    [AutoMapFrom(typeof(SettingsParameters))]
    public class BaseSettingsParametersDto : EntityDto<int?>
    {
        public string Name { get; set; }
    }
}

