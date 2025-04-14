namespace Denso.HotSheet.Sheets.Dto
{
    public class GetTrackingScrapSalesInput
    {
        public long? PlantId { get; set; }
        public long? CustomerId { get; set; }
        public long? PaymentStatusId { get; set; }
        public string Manifest { get; set; }
        public string Folio { get; set; }
        public string Invoice { get; set; }
    }
}