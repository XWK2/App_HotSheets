using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Denso.HotSheet.Catalogs.Dto
{
    [AutoMapFrom(typeof(DocumentType))]
    public class DocumentTypeDto : EntityDto<int?>
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public string FullName { get; set; }
    }
}
