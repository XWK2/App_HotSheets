using Denso.HotSheet.HotSheet.Enums;

namespace Denso.HotSheet.Sheets.Dto
{
    public class ApproveHotSheetShipInput
    {
        public long HotSheetShiptId { get; set; }
        public string Comments { get; set; }                
        public HotSheetApprovalType ApprovalType { get; set; }

        public HotSheetStatus StatusToUpdate { get; set; }
    }

    public enum HotSheetApprovalType
    {
        Manager = 1,        
        ImpoExpoStaff = 2,
        AccountingStaff = 3
    }
}
