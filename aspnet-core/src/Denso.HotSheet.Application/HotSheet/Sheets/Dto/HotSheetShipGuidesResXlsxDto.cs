using Abp.Application.Services.Dto;
using Denso.HotSheet.Catalogs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denso.HotSheet.Sheets.Dto
{
    public class HotSheetShipGuidesResXlsxDto
    {
        public DateTime ProcessDate { get; set; }
        public int Counter { get; set; }        
        public int CounterUpd { get; set; }
        
    }
}
