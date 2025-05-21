using Denso.HotSheet.Catalogs.Dto;
using Denso.HotSheet.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Denso.HotSheet.Sheets.Dto
{
    public class PurchaseOrdersItemDto
    {
        public long? PurchaseOrderId { get; set; }
        public string PlannerCode { get; set; }
        public string PlannerName { get; set; }
        public string PurchaseOrder { get; set; }
        public int? Line { get; set; }
        public string PartNumber { get; set; }
        public string PartDescription { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public int? Qty { get; set; }
        public DateTime? RequiredDate { get; set; }
        public long? StatusId { get; set; }
        public string StatusPO { get; set; }
        public string Ticket { get; set; }

        public DateTime? CreationDate { get; set; }


    }
}
