using System;

namespace Denso.HotSheet.Sheets.Dto
{
    public class HotSheetHistoryDto
    {
        public long HotSheetShiptId { get; set; }        
        public string HistoryType { get; set; }
        public string Comments { get; set; }

        public DateTime? CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public string CreatorUserName { get; set; }
    }
}
