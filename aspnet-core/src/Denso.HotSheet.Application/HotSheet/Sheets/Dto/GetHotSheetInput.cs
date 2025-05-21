using System;

namespace Denso.HotSheet.Sheets.Dto
{
    public class GetHotSheetInput
    {
        public long? UserId { get; set; }
        public string PlannerCode { get; set; }        
        public string SupplierCode { get; set; }
        public string PartNumber { get; set; }
        public long? TransportModeId { get; set; }
        public long? StatusId { get; set; }                
        public long? ShortageShiftId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int StatusHS { get; set; }

    }
}
