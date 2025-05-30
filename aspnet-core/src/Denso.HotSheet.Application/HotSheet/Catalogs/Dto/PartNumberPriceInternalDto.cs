﻿using Abp.Application.Services.Dto;
using System;

namespace Denso.HotSheet.Catalogs.Dto
{
    public class PartNumberPriceInternalDto : EntityDto<long?>
    {
        public long? CustomerId { get; set; }
        public CustomerDto Customer { get; set; }

        public long? PartNumberInternalId { get; set; }
        public PartNumberDto PartNumber { get; set; }
        
        public decimal UnitPrice { get; set; }
        public string Currency { get; set; }
        public DateTime PublishDate { get; set; }

        public bool IsActive { get; set; }
    }
}
