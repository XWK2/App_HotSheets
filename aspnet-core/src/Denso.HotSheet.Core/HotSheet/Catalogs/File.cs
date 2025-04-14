using Abp.Domain.Entities.Auditing;
using Denso.HotSheet.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoFiles")]
    public class File : AuditedEntity<long>
    {
        [StringLength(200)]
        public string EntityType { get; set; }
        public long EntityId { get; set; }

        public Guid Guid { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Extension { get; set; }
        public long Length { get; set; }

        [StringLength(300)]
        public string ContentType { get; set; }

        //public User? CreatorUser { get; set; }
    }
}
