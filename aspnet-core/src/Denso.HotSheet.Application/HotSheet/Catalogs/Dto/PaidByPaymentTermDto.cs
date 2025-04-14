using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Denso.HotSheet.Catalogs.Dto
{
    [AutoMapFrom(typeof(PaidByPaymentTerm))]
    public class PaidByPaymentTermDto : EntityDto<int?>
    {
        public long PaidById { get; set; }
        public int PaymentTermId { get; set; }
    }
}
