using Abp.Application.Services.Dto;
using System;

namespace Denso.HotSheet.Catalogs.Dto
{
    public class NoticeDto : EntityDto<long?>
    {
        public string Message { get; set; }        
        public DateTime NoticeDay { get; set; }
        public int AnticipationDays { get; set; }
        public bool IsActive { get; set; }
    }
}
