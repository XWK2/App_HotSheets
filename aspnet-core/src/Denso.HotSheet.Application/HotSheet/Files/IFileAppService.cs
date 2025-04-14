using Abp.Application.Services;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Denso.HotSheet.Files
{
    public interface IFileAppService : IApplicationService
    {
        Task Upload(IFormFile file, string entityType, long entityId);
    }
}
