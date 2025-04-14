using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Email
{
    [Table("DensoEmailTemplates")]
    public class EmailTemplate : AuditedEntity
    {
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsActive { get; set; }
    }
}
