using Abp.Application.Services.Dto;
using Denso.HotSheet.Catalogs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denso.HotSheet.HotSheet.Catalogs.Dto
{
    public class PartNumberResXlsxDto
    {
        public DateTime ProcessDate { get; set; }
        public int Counter { get; set; }
        public int CounterDel { get; set; }
        public int CounterUpd { get; set; }
        public int CounterIns { get; set; }      
    }
}
