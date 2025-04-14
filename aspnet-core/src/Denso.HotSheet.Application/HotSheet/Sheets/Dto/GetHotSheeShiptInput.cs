using System;

namespace Denso.HotSheet.Sheets.Dto
{
    public class GetHotSheeShiptInput
    {
        public bool? IsTemplate { get; set; }


        public long? PlantId { get; set; }        
        public long? DepartmentId { get; set; }
        public long? CarrierId { get; set; }
        public long? ServiceId { get; set; }
        public long? AuthorId { get; set; }
        public long? CustomerId { get; set; }
        public long? HotSheetTermId { get; set; }
        public int? StatusId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
