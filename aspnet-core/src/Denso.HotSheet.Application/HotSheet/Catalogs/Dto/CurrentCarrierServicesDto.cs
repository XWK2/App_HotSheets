namespace Denso.HotSheet.HotSheet.Catalogs.Dto
{
    public class CurrentCarrierServicesDto 
    {
        public long CarrierId { get; set; }

        public long ServiceId { get; set; }

        public string ServiceName { get; set; }        

        public bool IsActive { get; set; }

        public bool Ground { get; set; }
        public bool Air { get; set; }
        public bool Sea { get; set; }
    }     

}
