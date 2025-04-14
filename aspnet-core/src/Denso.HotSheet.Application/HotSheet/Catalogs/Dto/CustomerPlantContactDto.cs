using Abp.Application.Services.Dto;

namespace Denso.HotSheet.Catalogs.Dto
{
    public class CustomerPlantContactDto : EntityDto<long?>
    {
        public long CustomerPlantId { get; set; }        
        public string ContactName { get; set; }        
        public string PhoneNumber { get; set; }        
        public string DepartmentOrSection { get; set; }        
        public string NetNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
