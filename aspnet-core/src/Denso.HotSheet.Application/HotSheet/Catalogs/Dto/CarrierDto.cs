using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace Denso.HotSheet.Catalogs.Dto
{
    public class CarrierDto : EntityDto<long?>
    {
        public string Name { get; set; }

        public int DocumentTypeId { get; set; }
        public DocumentTypeDto DocumentType { get; set; }

        public int? DivisorNumber { get; set; }

        public bool IsActive { get; set; }

        public List<CarrierServiceDto> Services { get; set; } = new List<CarrierServiceDto>();

        public string FullName { get; set; }

        public List<CarrierNonWorkingDayDto> NonWorkingDays { get; set; } = new List<CarrierNonWorkingDayDto>();
    }
}
