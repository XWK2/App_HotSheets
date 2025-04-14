using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoHotSheetReasons")]
    public class HotSheetReason : AuditedEntity<int>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public bool BNoticeRMARequired { get; set; }
        public bool PictureTechnicalInfoMakerModelSerialNumber { get; set; }
        public bool AttachPurchaseOrder { get; set; }
        public bool TechnicalInfoPicture { get; set; }
        public bool AccountingApprovalRequired { get; set; }
        public bool ExcludeTermOfPayment { get; set; }

        public bool Remittence { get; set; }
        public bool NoPayment { get; set; }

        public bool IsActive { get; set; }
    }
}
