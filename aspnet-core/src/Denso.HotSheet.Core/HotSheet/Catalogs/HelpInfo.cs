using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoHelpInfo")]
    public class HelpInfo : AuditedEntity<long>
    {
        public long HelpInfoFieldId { get; set; }
        public HelpInfoField HelpInfoField { get; set; }

        public string HelpTextEnglish { get; set; }
        public string HelpTextSpanish { get; set; }

        public bool IsActive { get; set; }
    }
}
