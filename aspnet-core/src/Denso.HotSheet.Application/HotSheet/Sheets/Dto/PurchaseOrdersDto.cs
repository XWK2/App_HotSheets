using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Denso.HotSheet.Catalogs.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Sheets.Dto
{
    public class PurchaseOrdersDto : Entity<long?>
    {        
        public string PlannerCode { get; set; }
        public string PlannerName { get; set; }
        public string PurchaseOrder {  get; set; }
        public int? Line { get; set; }
        public string PartNumber { get; set; }
        public string PartDescription { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }        
        public int? Qty { get; set; }
        public DateTime? RequiredDate { get; set; }
        public long? StatusId { get; set; }
        public string Ticket { get; set; }
    }
}
