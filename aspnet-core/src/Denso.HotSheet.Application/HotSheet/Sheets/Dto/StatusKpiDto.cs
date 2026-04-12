using System;
using System.Collections.Generic;

namespace Denso.HotSheet.Sheets.Dto
{
    public class StatusKpiDto
    {
        public string Area { get; set; } // WH / I/E
        public int Abierto { get; set; }
        public int Cerrado { get; set; }
    }
}
