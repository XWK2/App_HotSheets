using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoHelpInfoFields")]
    public class HelpInfoField : AuditedEntity<long>
    {
        [StringLength(100)]
        public string FieldName { get; set; }

        public bool IsActive { get; set; }
    }
}
