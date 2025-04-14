using System;
using System.Collections.Generic;

namespace Denso.HotSheet.Sheets.Dto
{
    public class DashboardData
    {
        public List<DashboardItemData> Items { get; set; }
    }

    public class DashboardItemData
    {
        public long HotSheetShiptId { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int PlantId { get; set; }
        public string PlantName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? SentForApprovalDate { get; set; }
        public int DaysLateApproval { get; set; }

        public int CarrierId { get; set; }
        public string CarrierName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public long CreatorUserId { get; set; }
        public string CreatorUser { get; set; }


       // Flags
        public bool PendingApprovalByIE { get; set; }
        public bool PendingForApproval { get; set; }
        public bool PendingForProformaInvoice { get; set; }
        public bool PendingForPayment { get; set; }

        public string IEStaffName { get; set; }
        public int HotSheetReasonId { get; set; }
        public string HotSheetReason { get; set; }

        public int DocumentTypeId { get; set; }
        public string DocumentType { get; set; }
        public int PaymentTermId { get; set; }
        public string PaymentTerm { get; set; }

        public int ExportedCigmaStatus { get; set; }
    }
}
