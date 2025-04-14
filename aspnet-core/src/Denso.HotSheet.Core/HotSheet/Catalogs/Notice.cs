using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoNotices")]
    public class Notice : AuditedEntity<long>
    {
        [StringLength(500)]
        public string Message { get; set; }

        [Column(TypeName = "date")]
        public DateTime NoticeDay { get; set; }

        public int AnticipationDays { get; set; }

        public bool IsActive { get; set; }
    }
}
