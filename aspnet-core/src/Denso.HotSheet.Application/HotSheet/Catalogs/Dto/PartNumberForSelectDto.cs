namespace Denso.HotSheet.Catalogs.Dto
{
    public class PartNumberForSelectDto
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string DescriptionSpanish { get; set; }        
        public int? OriginCountryId { get; set; }
        public string FullNumber { get; set; }
        public bool IsInternal { get; set; }
        public string NumberValue { get; set; }

        public decimal? UnitPriceInternal { get; set; }
        public int? UnitMeasureId { get; set; }
        public long? ProductCodeSATId { get; set; }
        public string ProductSATCode { get; set; }
        public long? CustomerId { get; set; }
    }
}
