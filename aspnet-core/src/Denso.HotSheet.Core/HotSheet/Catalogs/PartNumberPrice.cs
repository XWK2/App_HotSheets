using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoPartNumberPrices")]
    public class PartNumberPrice : AuditedEntity<long>
    {
        public long? CustomerId { get; set; }
        public Customer Customer { get; set; }

        public long? PartNumberId { get; set; }
        //public PartNumber PartNumber { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal UnitPrice { get; set; }

        [StringLength(10)]
        public string Currency { get; set; }

        [Column(TypeName = "date")]
        public DateTime PublishDate { get; set; }

        public bool IsActive { get; set; }
    }
}
