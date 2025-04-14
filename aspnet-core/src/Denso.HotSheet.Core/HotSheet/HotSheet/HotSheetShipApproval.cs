using Abp.Domain.Entities.Auditing;
using Denso.HotSheet.Authorization.Users;
using Denso.HotSheet.Catalogs;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.HotSheet
{
    [Table("DensoHotSheetShipApprovals")]
    public class HotSheetShipApproval : AuditedEntity<long>
    {
        [ForeignKey("HotSheetShiptId")]
        public long? HotSheetShiptId { get; set; }
        public HotSheetsShip HotSheet { get; set; }
        
        public long? IEStaffId { get; set; }
        [ForeignKey("IEStaffId")]
        public Staff IEStaff { get; set; }

        public bool? IEStaffIsApproved { get; set; }
        public DateTime? IEStaffApprovalDate { get; set; }

        public long? ManagerApprovalId { get; set; }
        [ForeignKey("ManagerApprovalId")]
        public User ManagerApproval { get; set; }

        public bool? ManagerIsApproved { get; set; }
        public DateTime? ManagerApprovalDate { get; set; }
        
        public long? AccountingApprovalId { get; set; }
        [ForeignKey("AccountingApprovalId")]
        public Staff AccountingStaff { get; set; }

        public bool? AccountingIsApproved { get; set; }
        public DateTime? AccountingApprovalDate { get; set; }
    }
}
