using Denso.HotSheet.Catalogs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denso.HotSheet.Catalogs.Dto
{
    public class PartNumberInternalXlsxDto
    {
        public string PartNumber { get; set; }
        public string DescriptionInglish { get; set; }
        public string DescriptionSpanish { get; set; }

        public int? UnitMeasureId { get; set; }       

        public long? ProductCodeSATId { get; set; }

        public decimal? Weight { get; set; }

        public string OriginCountryId { get; set; }        
        public string Fraction { get; set; }             
    }
}
