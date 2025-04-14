using Abp.Application.Services.Dto;

namespace Denso.HotSheet.Catalogs.Dto
{
    public class ServiceDto : EntityDto<long?>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsNational { get; set; }
        public bool IsInternational { get; set; }
        public bool ShowHigestCostWarning { get; set; }
        
        public bool Ground { get; set; }
        public bool Air { get; set; }
        public bool Sea { get; set; }
        public bool IsActive { get; set; }

        public string FullName { get; set; }
    }
}
