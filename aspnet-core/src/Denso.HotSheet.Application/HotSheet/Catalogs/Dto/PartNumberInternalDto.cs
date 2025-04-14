using Abp.Application.Services.Dto;

namespace Denso.HotSheet.Catalogs.Dto
{
    public class PartNumberInternalDto : EntityDto<long?>
    {
        public string Number { get; set; }
        public string Description { get; set; }       
        public string DescriptionSpanish { get; set; }

        public int? UnitMeasureId { get; set; }
        public UnitMeasureDto UnitMeasure { get; set; }

        public long? ProductCodeSATId { get; set; }
        public ProductCodeSATDto ProductCodeSAT { get; set; }

        public int? OriginCountryId { get; set; }
        public string OriginCountry { get; set; }        
        public string Fraction { get; set; }
        public bool IsActive { get; set; }
        public string FullNumber { get; set; }

        public decimal? Weight { get; set; }

        public decimal? Price { get; set; }
    }
}
