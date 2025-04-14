using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Denso.HotSheet.Organization;

namespace Denso.HotSheet.Catalogs.Dto
{
    public class PlantDto : EntityDto<long?>
    {
        public string Name { get; set; }        
        public string AddressLine1 { get; set; }       
        public string AddressLine2 { get; set; }        
        public string AddressLine3 { get; set; }        
        public string AddressLine4 { get; set; }
        public string RFC { get; set; }
        public string Sufix { get; set; }

        public bool IsActive { get; set; }
        public int TotalUsers { get; set; }

        public string FullName { get; set; }
    }

    [AutoMapFrom(typeof(Plant))]
    public class BasePlantDto : EntityDto<long?>
    {
        public string Name { get; set; }        
    }
}
