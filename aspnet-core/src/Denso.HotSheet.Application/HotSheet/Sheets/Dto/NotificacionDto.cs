using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denso.HotSheet.HotSheet.Sheets.Dto
{
    public class NotificacionDto
    {
        public string Planner { get; set; }
        public List<string> Correos { get; set; }
        public List<RegistroDto> Registros { get; set; }
    }
}
