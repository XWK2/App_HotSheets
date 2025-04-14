using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Organization
{
    [Table("DensoPlants")]
    public class Plant : AuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(300)]
        public string AddressLine1 { get; set; }

        [StringLength(200)]
        public string AddressLine2 { get; set; }

        [StringLength(100)]
        public string AddressLine3 { get; set; }

        [StringLength(100)]
        public string AddressLine4 { get; set; }

        [StringLength(20)]
        public string RFC { get; set; }

        [StringLength(5)]
        public string Sufix { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("PlantId")]
        public IList<PlantUser> Users { get; set; } = new List<PlantUser>();
    }
}
