using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoCustomerPlantContacts")]
    public class CustomerPlantContact : AuditedEntity<long>
    {
        public long CustomerPlantId { get; set; }

        [MaxLength(300)]
        public string ContactName { get; set; }

        [MaxLength(30)]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        public string DepartmentOrSection { get; set; }

        [MaxLength(100)]
        public string NetNumber { get; set; }

        public bool IsActive { get; set; }
    }
}
