namespace Denso.HotSheet.Catalogs.Dto
{
    public class ApproverStaffDto
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string ApproverName { get; set; }
        public string DensoFullName { get; set; }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public long StaffId { get; set; }
        public int Type { get; set; }

        public int PlantId { get; set; }
        public string PlantName { get; set; }
    }
}
