using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Organization
{
    [Table("DensoPlantUsers")]
    public class PlantUser : AuditedEntity<long>
    {
        public long PlantId { get; set; }
        public long UserId { get; set; }
        public bool IsSupervisor { get; set; }
    }
}
