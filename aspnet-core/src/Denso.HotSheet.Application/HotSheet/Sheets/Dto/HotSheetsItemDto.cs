using Denso.HotSheet.Catalogs.Dto;
using Denso.HotSheet.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Denso.HotSheet.Sheets.Dto
{
    public class HotSheetsItemDto 
    {
        public long? HotSheetId { get; set; }
        public string PlannerCode { get; set; }
        public string PlannerName { get; set; }
        public string SupplierCode { get; set; }
        public string SupplerName { get; set; }
        public string PartNumber { get; set; }
        public string PartDescription { get; set; }
        public int InTransitQty { get; set; }
        public int DiaStockIn { get; set; }
        public string DiaLocation { get; set; }
        public int CIGMAReceived { get; set; }
        public long? TransportModeId { get; set; }
        public string TransportModeName { get; set; }
        public int DeliveryOrder { get; set; }
        public string TrafficContainerFX { get; set; }
        public string UnitNumber { get; set; }
        public DateTime? EtaDNMX { get; set; }
        public long? StatusId { get; set; }
        public string StatusHotSheetName { get; set; }
        public DateTime? RealShortageDate { get; set; }
        public long? ShortageShiftId { get; set; }

        public string ShortageShiftName { get; set; }
        public string Shortage { get; set; }

        public string ShortageVal { get; set; }
        public string PCComments { get; set; }


    }
}
