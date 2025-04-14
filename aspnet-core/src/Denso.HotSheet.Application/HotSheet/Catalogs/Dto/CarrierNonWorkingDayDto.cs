using Abp.Application.Services.Dto;
using System;

namespace Denso.HotSheet.Catalogs.Dto
{
    public class CarrierNonWorkingDayDto : EntityDto<long?>
    {
        public long CarrierId { get; set; }
        public string CarrierName { get; set; }
        public DateTime NonWorkingDay { get; set; }
        public bool IsActive { get; set; }
    }
}
