using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.HotSheet
{
    [Table("DensoHotSheetShipHistory")]
    public class HotSheetShipHistory : CreationAuditedEntity<long>
    {
        public long HotSheetShiptId { get; set; }

        [StringLength(100)]
        public string HistoryType { get; set; }

        public string Comments { get; set; }

        public long? UserIdNotified { get; set; }
    }
}
