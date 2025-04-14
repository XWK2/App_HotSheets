using System;
using System.Collections.Generic;

namespace Denso.HotSheet.Sheets.Dto
{
    public class TrackingScrapSales
    {
        public List<TrackingScrapSalesItem> Items { get; set; }
    }

    public class TrackingScrapSalesItem
    {
        public long HotSheetShiptId { get; set; }        
        public string PlantName { get; set; }

        public string CustomerName { get; set; }

        public string PedimentoImpo { get; set; }


        public DateTime? CreationDate { get; set; }

        public string Manifest { get; set; }
        public DateTime? HotSheetDate { get; set; }
        public DateTime? ReportDateStart { get; set; }
        public DateTime? ReportDateEnd { get; set; }

        public string Folio { get; set; }
        public string Comments { get; set; }

        public DateTime? ManagerApprovalDate { get; set; }
        public DateTime? IEStaffApprovalDate { get; set; }
        public DateTime? InvoiceEmissionDate { get; set; }
        public string ProformaInvoice { get; set; }

        public decimal Total { get; set; }

        public string Currency { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
    }
}
