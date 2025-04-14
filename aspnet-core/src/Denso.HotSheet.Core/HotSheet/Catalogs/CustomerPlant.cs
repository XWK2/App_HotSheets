using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoCustomerPlants")]
    public class CustomerPlant : AuditedEntity<long>
    {
        public long CustomerId { get; set; }

        public int ShipToNumber { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string AddressLine1 { get; set; }

        [MaxLength(100)]
        public string AddressLine2 { get; set; }

        [MaxLength(100)]
        public string AddressLine3 { get; set; }

        [MaxLength(100)]
        public string AddressLine4 { get; set; }

        [MaxLength(20)]
        public string RFC { get; set; }

        [MaxLength(200)]
        public string State { get; set; }

        [MaxLength(20)]
        public string Country { get; set; }

        [MaxLength(20)]
        public string ZipCode { get; set; }

        [MaxLength(50)]
        public string TaxId { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("CustomerPlantId")]
        public virtual IList<CustomerPlantContact> Contacts { get; set; } = new List<CustomerPlantContact>();
    }
}
