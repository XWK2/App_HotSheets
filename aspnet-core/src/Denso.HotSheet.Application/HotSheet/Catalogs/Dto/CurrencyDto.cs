using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Denso.HotSheet.Catalogs.Dto
{
    [AutoMapFrom(typeof(Currency))]
    public class CurrencyDto : EntityDto<int?>
    {
        public string Code { get; set; }        
        public string DensoCode { get; set; }        
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public string FullName { get; set; }
    }   
}
