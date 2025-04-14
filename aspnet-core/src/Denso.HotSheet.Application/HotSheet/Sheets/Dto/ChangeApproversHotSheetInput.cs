namespace Denso.HotSheet.Sheets.Dto
{
    public class ChangeApproversHotSheetInput
    {
        public long HotSheetShiptId { get; set; }
        public long? ManagerApprovalId { get; set; }
        public long? AccountingApprovalId { get; set; }
        public long? IEStaffId { get; set; }
        public string Comments { get; set; }
    }
}
