using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace Denso.HotSheet.Catalogs.Dto
{
    public class PaymentTermDto : EntityDto<int?>
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        public bool AlwaysDnmx { get; set; }
        public bool AccountingApprovalRequired { get; set; }
        public bool ExcludeOnSamples { get; set; }

        // Warning 1 => parameters for Carries-Company Names
        public List<int> Warning1CompanyIds { get; set; }
        public string Warning1Message { get; set; }

        // Warning 2 => parameters Big Amounts
        public List<int> Warning2CompanyIds { get; set; }
        public string Warning2Message { get; set; }
        public decimal Warning2Amount { get; set; }

        public string POWarning { get; set; }

        public bool IsActive { get; set; }

        public List<PaymentTermCarrierDto> Carriers { get; set; } = new List<PaymentTermCarrierDto>();
    }
}
