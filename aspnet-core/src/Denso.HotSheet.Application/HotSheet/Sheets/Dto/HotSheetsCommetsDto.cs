using Abp.Domain.Entities;
using Denso.HotSheet.Users.Dto;
using System;

namespace Denso.HotSheet.Sheets.Dto
{
    public class HotSheetsCommetsDto : Entity<long?>
    {
        public long HotSheetId { get; set; }

        public long DepartmentId { get; set; }

        public string Department { get; set; }
                                        
        public string Comments { get; set; }        
        public long CreatorUserId { get; set; }
        public DateTime? CreationTime { get; set; }
        public string CreatorFullName { get; set; }

        
    }
}
