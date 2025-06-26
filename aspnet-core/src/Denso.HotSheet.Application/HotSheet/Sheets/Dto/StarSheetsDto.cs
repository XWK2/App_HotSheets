using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Denso.HotSheet.Catalogs.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Sheets.Dto
{
    public class StarSheetsDto : Entity<long?>
    {
        //public long Id { get; set; }
        public string PlannerCode { get; set; }
        public string PlannerName { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string PartNumber { get; set; }
        public string PartDescription { get; set; }

        public decimal DateSearch { get; set; }
        public decimal OrderQty { get; set; }
        public string DeliveryOrder { get; set; }
        public decimal DelQty { get; set; }
        public string ASN { get; set; }
        public decimal InTransitQty { get; set; }

        public decimal ADate { get; set; }
        public decimal RCVD { get; set; }
        public decimal DueDate { get; set; }
        public decimal ReceivedQty { get; set; }
        public string Container { get; set; }
        public decimal Stock1 { get; set; }
        public decimal Stock2 { get; set; }

        //public int? InTransitQty { get; set; }
        //public int? DiaStockIn { get; set; }
        //public string DiaLocation { get; set; }
        //public int? CIGMAReceived { get; set; }
        //public long? TransportModeId { get; set; }
        //public TransportModeDto TransportMode { get; set; }
        //public int? DeliveryOrder { get; set; }
        //public string TrafficContainerFX { get; set; }
        //public string UnitNumber { get; set; }
        //public DateTime? EtaDNMX { get; set; }
        //public long? StatusId { get; set; }
        //public StatusHotSheetDto StatusHotSheet { get; set; }
        //public DateTime? RealShortageDate { get; set; }
        //public long? ShortageShiftId { get; set; }

        //public ShortageShiftDto ShortageShift { get; set; }
        //public string Shortage { get; set; }
        //public int ASN { get; set; }
        public string PCComments { get; set; }

        ////Campos nuevos inicio
        //public decimal StockShortageDate { get; set; }
        //public string ShortagePriority { get; set; }
        //public decimal OnHandTotalQty { get; set; }
        //public decimal RequirementTotalQty { get; set; }
        //public decimal OverDefProdQtyFromParentPartNo { get; set; }
        //public decimal OverDefRecQty { get; set; }
        //public decimal ShortageNoticeDateFrom { get; set; }
        //public decimal Stock2 { get; set; }
        ////Campos nuevos fin

    }
}
