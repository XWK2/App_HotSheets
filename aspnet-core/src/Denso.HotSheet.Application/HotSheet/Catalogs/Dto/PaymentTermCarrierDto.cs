using Abp.Application.Services.Dto;

namespace Denso.HotSheet.Catalogs.Dto
{
    public class PaymentTermCarrierDto : EntityDto<int?>
    {
        public int PaymentTermId { get; set; }

        public long CarrierId { get; set; }
        public int WarningType { get; set; }
    }
}
