using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoCustomers")]
    [Index("DensoCustomerId", "Payment", IsUnique = true)]
    public class Customer : AuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        /// <summary>
        /// Denso has duplicated Customer Ids
        /// </summary>
        public long? DensoCustomerId { get; set; }

        [StringLength(300)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Type { get; set; }

        [StringLength(20)]
        public string RFC { get; set; }

        [StringLength(300)]
        public string AddressLine1 { get; set; }

        [StringLength(100)]
        public string AddressLine2 { get; set; }

        [StringLength(100)]
        public string AddressLine3 { get; set; }

        [StringLength(100)]
        public string AddressLine4 { get; set; }

        public bool Payment { get; set; }

        public bool IsActive { get; set; }

        [StringLength(300)]
        public string Contact { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string FedexCta { get; set; }

        [ForeignKey("CustomerId")]
        public virtual IList<CustomerPlant> Plants { get; set; } = new List<CustomerPlant>();

        [MaxLength(200)]
        public string State { get; set; }

        [MaxLength(20)]
        public string Country { get; set; }

        [MaxLength(20)]
        public string ZipCode { get; set; }

        [MaxLength(50)]
        public string TaxId { get; set; }

        public long CustomerIdBilling { get; set; }        
    }
}
