using Abp.Domain.Entities;
using Denso.HotSheet.Catalogs.Dto;
using System;
using System.Collections.Generic;

namespace Denso.HotSheet.Sheets.Dto
{
    public class HotSheetShipProductDto : Entity<long?>
    {
        public long HotSheetShiptId { get; set; }        

        public long? PartNumberId { get; set; }
        public PartNumberDto PartNumber { get; set; }

        public long? PartNumberInternalId { get; set; }
        public PartNumberInternalDto PartNumberInternal { get; set; }

        public string PartNumberSelected { get; set; }
        public string PartNumberText { get; set; }

        public int? UnitMeasureId { get; set; }
        public UnitMeasureDto UnitMeasure { get; set; }

        public string Description { get; set; }
        public string DescriptionSpanish { get; set; }

        public long? ProductCodeSATId { get; set; }
        public string ProductSATCode { get; set; }

        public decimal Quantity { get; set; }        
        public decimal UnitPrice { get; set; }        
        public decimal Total { get; set; }

        public string Model { get; set; }        
        public string Serial { get; set; }        
        public string Maker { get; set; }        
        public string TechInfo { get; set; }        
        public string PoNumber { get; set; }

        public int? OriginCountryId { get; set; }
        public string OriginCountryName { get; set; }

        public List<FileDto> Files { get; set; }
        public List<Object> FilesToUpload { get; set; }
        public List<long> FilesToDelete { get; set; }
    }
}
