using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Denso.HotSheet.Catalogs.Dto
{
    [AutoMapFrom(typeof(UnitMeasure))]
    public class UnitMeasureDto : EntityDto<int?>
    {
        public string Name { get; set; }        
        public string DensoCode { get; set; }
        public string SatCode { get; set; }
        public string SegroveCode { get; set; }
        public bool IsActive { get; set; }
        public string FullName { get; set; }
    }
}
