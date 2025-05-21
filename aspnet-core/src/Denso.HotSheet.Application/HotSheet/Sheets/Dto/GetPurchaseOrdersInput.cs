using System;

namespace Denso.HotSheet.Sheets.Dto
{
    public class GetPurchaseOrdersInput
    {
        public long? UserId { get; set; }
        public string PlannerCode { get; set; }        
        public string SupplierCode { get; set; }
        public string PartNumber { get; set; }        
        public long? StatusId { get; set; }                        
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int StatusHS { get; set; }

    }
}
