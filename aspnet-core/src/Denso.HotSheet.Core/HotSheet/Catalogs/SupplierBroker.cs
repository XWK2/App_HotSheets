using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoSupplierBrokers")]
    public class SupplierBroker : AuditedEntity<long>
    {
        public long? SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(100)]
        public string Address3 { get; set; }

        [StringLength(100)]
        public string Address4 { get; set; }

        [StringLength(100)]
        public string Phone { get; set; }

        [StringLength(20)]
        public string Zip { get; set; }
    }
}