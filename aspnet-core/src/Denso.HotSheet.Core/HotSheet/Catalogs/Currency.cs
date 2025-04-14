using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoCurrencies")]
    public class Currency : CreationAuditedEntity<int>
    {
        [StringLength(20)]
        public string Code { get; set; }

        [StringLength(20)]
        public string DensoCode { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
