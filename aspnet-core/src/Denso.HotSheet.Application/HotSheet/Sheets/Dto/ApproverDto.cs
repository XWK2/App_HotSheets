namespace Denso.HotSheet.Sheets.Dto
{
    public class ApproverDto
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string ApproverName { get; set; }
        public string DensoFullName { get; set; }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
