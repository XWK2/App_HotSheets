using System;
using System.Collections.Generic;

namespace Denso.HotSheet.Sheets.Dto
{
    public class DashboardKpiDto
    {
        public List<SupplierKpiDto> WH { get; set; }
        public List<SupplierKpiDto> IE { get; set; }
        public List<StatusKpiDto> Status { get; set; }
    }
}
