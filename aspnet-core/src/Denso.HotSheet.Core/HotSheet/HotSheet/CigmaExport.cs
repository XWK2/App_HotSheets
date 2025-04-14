using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.HotSheet
{
    [Table("DensoCigmaExports")]
    public class CigmaExport : CreationAuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public long HotSheetShiptId { get; set; }
        public HotSheetsShip HotSheet { get; set; }

        public bool ExportedToCigma { get; set; }
        public DateTime ExportedDate { get; set; }

        public bool DetailExported { get; set; }
        public bool ErrorOnExport { get; set; }
        
        public string ErrorMessage { get; set; }
    }
}
