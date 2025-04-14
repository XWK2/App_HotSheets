namespace Denso.HotSheet.Catalogs.Dto
{
    public class DepartmentByUserDto
    {
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public long UserId { get; set; }
        public bool IsSupervisor { get; set; }
        public string FullName { get; set; }
    }
}
