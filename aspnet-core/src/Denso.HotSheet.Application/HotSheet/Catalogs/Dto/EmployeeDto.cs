using Abp.Application.Services.Dto;
using System;

namespace Denso.HotSheet.Catalogs.Dto
{
    public class EmployeeDto : EntityDto<long?>
    {
        public long DensoEmployeeId { get; set; }        
        public string Credential { get; set; }        
        public string Name { get; set; }
        public string Surnames { get; set; }
        public string Rfc { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Nss { get; set; }
        public string Curp { get; set; }

        public long? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        
        public int? TypeId { get; set; }
        public string EmployeeType { get; set; }
        
        public int? LevelId { get; set; }
        public string EmployeeLevel { get; set; }

        public long? PlantId { get; set; }
        public string PlantName { get; set; }
        
        public DateTime EntryDate { get; set; }
        
        public int? PositionId { get; set; }
        public string EmployeePosition { get; set; }

        public bool Extras { get; set; }
        public bool NotRequiredAHE { get; set; }
        public bool Supervisor { get; set; }
        public bool Subsidy { get; set; }
        public bool PositionLevel { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }        
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }

        public string FullName { get; set; }
    }
}
