using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.HotSheet
{
    [Table("DensoHotSheetShipManifests")]
    public class HotSheetShipManifest : AuditedEntity<long>
    {
        [ForeignKey("HotSheetShiptId")]
        public long? HotSheetShiptId { get; set; }
        public HotSheetsShip HotSheet { get; set; }

        public string Manifest { get; set; }
        public DateTime HotSheetDate { get; set; }

        public DateTime ReportDateStart { get; set; }

        public DateTime ReportDateEnd { get; set; }
        public string Comments { get; set; }

        
    }
}
