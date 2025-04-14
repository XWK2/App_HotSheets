using Abp.Application.Services.Dto;
using Denso.HotSheet.Users.Dto;

namespace Denso.HotSheet.Catalogs.Dto
{
    public class StaffDto : EntityDto<long?>
    {
        public long UserId { get; set; }
        public UserBasicDto User { get; set; }

        public int Type { get; set; }
        public bool IsActive { get; set; }
    }
}
