using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoSuppliers")]
    public class Supplier : AuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

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

        [StringLength(20)]
        public string Rfc { get; set; }

        [StringLength(100)]
        public string Contact { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Zip { get; set; }

        [StringLength(100)]
        public string FedexCta { get; set; }

        public bool IsActive { get; set; }

        public List<SupplierAddress> Addresses { get; set; } = new List<SupplierAddress>();
    }
}
