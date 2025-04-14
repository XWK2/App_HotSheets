using Abp.Domain.Entities.Auditing;
using Denso.HotSheet.Authorization.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoStaff")]
    public class Staff : AuditedEntity<long>
    {
        public long? UserId { get; set; } // Responsable
        public User User { get; set; }

        public int Type { get; set; }

        public bool IsActive { get; set; }

        public IList<StaffUser> CopyTo { get; set; }
    }

    [Table("DensoStaffUsers")]
    public class StaffUser : CreationAuditedEntity<long>
    {
        public long StaffId { get; set; }

        public long? UserId { get; set; }
        public User User { get; set; }
    }
}

