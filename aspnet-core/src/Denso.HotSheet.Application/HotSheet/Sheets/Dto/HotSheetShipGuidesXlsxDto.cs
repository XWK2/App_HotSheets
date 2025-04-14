using Denso.HotSheet.Catalogs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denso.HotSheet.Sheets.Dto
{
    public class HotSheetShipGuidesXlsxDto
    {
        public string TrackingNumber { get; set; }        
        public string GuideReference { get; set; }        
        public string GuideStatusDetail { get; set; }
        public string GuideStatus { get; set; }
        public decimal GuideCost { get; set; }
        public string GuideCurrency { get; set; }

    }
}
