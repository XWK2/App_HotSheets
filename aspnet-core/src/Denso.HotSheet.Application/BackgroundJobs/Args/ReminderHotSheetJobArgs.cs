using Denso.HotSheet.BackgroundJobs.Enums;
using System;
using System.Collections.Generic;

namespace Denso.HotSheet.BackgroundJobs.Args
{
    public class ReminderHotSheetJobArgs
    {
        public HotSheetNotificationType NotificationType { get; set; }

        public long? UserId { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }

        public List<HotSheetReminderItem> HotSheets { get; set; }
    }

    public class HotSheetReminderItem
    {
        public long HotSheetShiptId { get; set; }
        public string Folio { get; set; }
        public DateTime CreationDate { get; set; }

        public long CreatorUserId { get; set; }
        public string CreatorFullName { get; set; }

        public int DocumentTypeId { get; set; }
        public string CustomerName { get; set; }
    }
}
