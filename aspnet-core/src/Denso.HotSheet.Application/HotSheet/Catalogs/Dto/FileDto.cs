using System;

namespace Denso.HotSheet.Catalogs.Dto
{
    public class FileDto
    {
        public long Id { get; set; }
        public string EntityType { get; set; }
        public long EntityId { get; set; }
        public Guid Guid { get; set; }

        public string Name { get; set; }
        public string Extension { get; set; }
        public long Length { get; set; }
        public string ContentType { get; set; }

        public DateTime CreationTime { get; set; }

        public string Url { get; set; }
        public string UploadedBy { get; set; }
    }
}
