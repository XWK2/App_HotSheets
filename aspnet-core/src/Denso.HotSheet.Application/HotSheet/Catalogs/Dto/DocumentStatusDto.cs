using Abp.Application.Services.Dto;

namespace Denso.HotSheet.Catalogs.Dto
{
    public class DocumentStatusDto : EntityDto<long?>
    {
        public string Name { get; set; }
    }
}
