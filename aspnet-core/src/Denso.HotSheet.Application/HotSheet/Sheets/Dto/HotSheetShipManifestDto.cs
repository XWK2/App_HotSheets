using Abp.Domain.Entities;
using System;

namespace Denso.HotSheet.Sheets.Dto
{
    public class HotSheetShipManifestDto : Entity<long?>
    {
        public long HotSheetShiptId { get; set; }

        public string Manifest { get; set; }
        public DateTime HotSheetDate { get; set; }

        public DateTime ReportDateStart { get; set; }

        public DateTime ReportDateEnd { get; set; }
        public string Comments { get; set; }

        public string Update { get; set; }
    }
}
