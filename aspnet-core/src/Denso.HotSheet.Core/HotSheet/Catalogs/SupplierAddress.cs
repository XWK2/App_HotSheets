using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoSupplierAddresses")]
    public class SupplierAddress : AuditedEntity<long>
    {
        public long? SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string AddressLine1 { get; set; }

        [StringLength(100)]
        public string AddressLine2 { get; set; }

        [StringLength(100)]
        public string AddressLine3 { get; set; }

        [StringLength(100)]
        public string AddressLine4 { get; set; }

        public bool IsActive { get; set; }
    }
}
