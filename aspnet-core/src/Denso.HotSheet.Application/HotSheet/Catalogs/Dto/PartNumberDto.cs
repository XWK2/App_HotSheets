using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace Denso.HotSheet.Catalogs.Dto
{
    [AutoMapFrom(typeof(PartNumber))]
    public class PartNumberDto : EntityDto<long?>
    {
        public string Number { get; set; }
        public string Description { get; set; }
        public string DescriptionSpanish { get; set; }
        
        public int UnitMeasureId { get; set; }
        public UnitMeasureDto UnitMeasure { get; set; }

        public long ProductCodeSATId { get; set; }
        public ProductCodeSATDto ProductCodeSAT { get; set; }

        public int? OriginCountryId { get; set; }
        public string OriginCountry { get; set; }

        public string Fraction { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationTime { get; set; }

        public string FullNumber { get; set; }

        public decimal? Weight { get; set; }
    }

    [AutoMapFrom(typeof(PartNumber))]
    public class BasePartNumberDto : EntityDto<long?>
    {
        public string Description { get; set; }
    }
}
