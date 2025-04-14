using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoDocumentTypes")]
    public class DocumentType : CreationAuditedEntity<int>
    {
        [StringLength(200)]
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
