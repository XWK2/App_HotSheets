using Denso.HotSheet.BackgroundJobs.Enums;
using System;
using System.Collections.Generic;

namespace Denso.HotSheet.BackgroundJobs.Args
{
    public class SendHotSheetEmailArgs
    {
        public long HotSheetShiptId { get; set; }

        public string Folio { get; set; }
        public DateTime CreationDate { get; set; }

        public string CreatorUserName { get; set; }
        public string CreatorFullName { get; set; }
        public string CreatorEmailAddress { get; set; }

        public int DocumentTypeId { get; set; }
        public string CustomerName { get; set; }

        public HotSheetNotificationType NotificationType { get; set; }

        public List<HotSheetEmailItem> UsersToNotify { get; set; }

        public string RejectedBy { get; set; }
        public string ReasonRejection { get; set; }
    }

    public class HotSheetEmailItem
    {
        public long? UserId { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
    }
}
