using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Denso.HotSheet.Configuration
{
    [Table("AbpSettings")]
    public class SettingsParameters : AuditedEntity<int>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [StringLength(512)]
        public string Name { get; set; }


        [StringLength(512)]
        public string Value { get; set; }
    }
}

