using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoDocumentStatuses")]
    public class DocumentStatus : CreationAuditedEntity<int>
    {
        [StringLength(30)]
        public string Name { get; set; }
    }
}
