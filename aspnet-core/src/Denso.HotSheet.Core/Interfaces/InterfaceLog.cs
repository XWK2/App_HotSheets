using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Interfaces
{
    [Table("DensoInterfaceLogs")]
    public class InterfaceLog : CreationAuditedEntity<long>
    {
        public long? InterfaceId { get; set; }
        public Interface Interface { get; set; }

        public int RowsInserted { get; set; }
        public int RowsUpdated { get; set; }
        public int RowsDeleted { get; set; }
        public int TotalRows { get; set; }

        public DateTime ExecutionDate { get; set; }
        public string Error { get; set; }
    }
}
