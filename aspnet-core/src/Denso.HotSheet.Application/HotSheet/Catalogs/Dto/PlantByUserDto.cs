namespace Denso.HotSheet.Catalogs.Dto
{
    public class PlantByUserDto
    {
        public long PlantId { get; set; }
        public string PlantName { get; set; }
        public long UserId { get; set; }
        public bool IsSupervisor { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string RFC { get; set; }
    }
}
