using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs.Dto
{
    [AutoMapFrom(typeof(PaidBy))]
    public class PaidByDto : EntityDto<long?>
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public List<PaidByPaymentTermDto> PaymentTerms { get; set; } = new List<PaidByPaymentTermDto>();
        
        public List<PaidByHotSheetTermDto> HotSheetTerms { get; set; } = new List<PaidByHotSheetTermDto>();
    }
}
