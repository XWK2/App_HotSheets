using Abp.Authorization;
using Abp.Domain.Repositories;
using Denso.HotSheet.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Denso.HotSheet.Files
{
    [AbpAuthorize]
    public class FileAppService : HotSheetAppServiceBase, IFileAppService
    {
        private readonly IRepository<Catalogs.File, long> _fileRepository;        
        private readonly IConfigurationRoot _appConfiguration;

        public FileAppService(
            IRepository<Catalogs.File, long> fileRepository,
            IAppConfigurationAccessor appConfigurationAccessor
        )
        {
            _fileRepository = fileRepository;
            _appConfiguration = appConfigurationAccessor.Configuration;
        }

        [HttpPost]
        public async Task Upload(IFormFile file, string entityType, long entityId)
        {
            if (file != null)
            {
                string uploadFilesPath = _appConfiguration["App:UploadFilesPath"];
                var entityTypeFolder = Path.Combine(uploadFilesPath, entityType);
                if (!Directory.Exists(entityTypeFolder))
                {
                    Directory.CreateDirectory(entityTypeFolder);
                }

                var entityFullFolder = Path.Combine(entityTypeFolder, entityId.ToString());
                if (!Directory.Exists(entityFullFolder))
                {
                    Directory.CreateDirectory(entityFullFolder);
                }

                Guid guidFile = Guid.NewGuid();

                string fileName = file.FileName;
                string guidFileName = guidFile.ToString() + Path.GetExtension(fileName);

                var filePath = Path.Combine(entityFullFolder, guidFileName);

                var fs = new FileStream(filePath, FileMode.Create);

                await file.CopyToAsync(fs);

                fs.Dispose();


                await _fileRepository.InsertAsync(new Catalogs.File
                {
                    EntityType = entityType,
                    EntityId = entityId,
                    Guid = guidFile,
                    Name = fileName,
                    Length = file.Length,
                    Extension = Path.GetExtension(file.FileName),                    
                    //ContentType = ""
                }); ;
            }
        }
    }
}
