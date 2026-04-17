using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denso.HotSheet.HotSheet.Sheets.Dto
{
    public class RegistroDto
    {
        public long Id { get; set; } // 🔥 IMPORTANTE
        public string Folio { get; set; }
        public string Parte { get; set; }
        public string Proveedor { get; set; }
    }
}
