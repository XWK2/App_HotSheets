using Abp.Domain.Entities.Auditing;
using Denso.HotSheet.Organization;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.HotSheet
{
    [Table("DensoHotSheetsComments")]
    public class HotSheetsComments : AuditedEntity<long>
    {
        [ForeignKey("HotSheetId")]
        public long? HotSheetId { get; set; }
        public HotSheets HotSheet { get; set; }

        [ForeignKey("DepartmentId")]
        public long? DepartmentId { get; set; }
        public Department Department { get; set; }
        public string Comments { get; set; }

        
    }
}
