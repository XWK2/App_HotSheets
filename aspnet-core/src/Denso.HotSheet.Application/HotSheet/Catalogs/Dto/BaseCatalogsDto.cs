using System.Collections.Generic;

namespace Denso.HotSheet.Catalogs.Dto
{
    public class BaseCatalogsDto
    {
        public List<CarrierDto> Carriers { get; set; }
        public List<DocumentTypeDto> DocumentTypes { get; set; }
        public List<PaymentTermDto> PaymentTerms { get; set; }
        public List<HotSheetReasonDto> HotSheetReasons { get; set; }
        public List<ServiceDto> Services { get; set; }
        public List<DocumentStatusDto> Status { get; set; }
    }
}
