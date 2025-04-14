using Abp.Application.Services.Dto;

namespace Denso.HotSheet.Catalogs.Dto
{
    public class CountryDto : EntityDto<int?>
    {
        public string Name { get; set; }
        public string NameSpanish { get; set; }
        public string DensoCode { get; set; }        
        public string SatCode { get; set; }        
        public string SegroveCode { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
    }
}
