using Abp.Domain.Repositories;
using Denso.HotSheet.Configuration;
using Denso.HotSheet.Sheets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Denso.HotSheet.Controllers
{
    public class FileController : HotSheetControllerBase
    {
        private readonly IRepository<Catalogs.File, long> _fileRepository;
        private readonly IHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public FileController(
            IRepository<Catalogs.File, long> fileRepository,
            IHostEnvironment env,
            IAppConfigurationAccessor appConfigurationAccessor
        )
        {
            _fileRepository = fileRepository;
            _appConfiguration = appConfigurationAccessor.Configuration;
        }

        public async Task<ActionResult> GetDocumentBy(long docId)
        {
            var item = await _fileRepository.GetAll()
                .Where(c => c.Id == docId)
                .FirstOrDefaultAsync();

            if (item != null)
            {
                string fileName = item.Guid + item.Extension;

                string sEntityType = item.EntityType;

                string uploadFilesPath = _appConfiguration["App:UploadFilesPath"];
                var filePath = Path.Combine(uploadFilesPath, sEntityType, item.EntityId.ToString(), fileName);

                if (System.IO.File.Exists(filePath))
                {
                    string contentType = GetMimeTypeForFileExtension(filePath);
                    var fileBytes = System.IO.File.ReadAllBytes(filePath);

                    return File(fileBytes, contentType, item.Name);
                }
            }

            return null;
        }

        private string GetMimeTypeForFileExtension(string filePath)
        {
            const string DefaultContentType = "application/octet-stream";

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(filePath, out string contentType))
            {
                contentType = DefaultContentType;
            }

            return contentType;
        }
    }
}
