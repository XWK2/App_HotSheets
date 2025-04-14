using Abp.Application.Services.Dto;

namespace Denso.HotSheet.Catalogs.Dto
{
    public class HelpInfoDto : EntityDto<long?>
    {
        public long HelpInfoFieldId { get; set; }
        public HelpInfoFieldDto HelpInfoField { get; set; }

        public string HelpTextEnglish { get; set; }
        public string HelpTextSpanish { get; set; }

        public bool IsActive { get; set; }
    }

    public class HelpInfoFieldDto : EntityDto<long?>
    {
        public string FieldName { get; set; }

        public bool IsActive { get; set; }
    }
}
