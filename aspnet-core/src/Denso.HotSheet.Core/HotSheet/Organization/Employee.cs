using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Organization
{
    [Table("DensoEmployees")]
    [Index("DensoEmployeeId", "Credential", IsUnique = true)]
    public class Employee : AuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        /// <summary>
        /// Denso has duplicated Employee Ids
        /// </summary>
        public long DensoEmployeeId { get; set; }

        [StringLength(100)]
        public string Credential { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Surnames { get; set; }
        
        [StringLength(30)]
        public string Rfc { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        [StringLength(20)]
        public string Nss { get; set; }

        [StringLength(20)]
        public string Curp { get; set; }

        public long? DepartmentId { get; set; }
        public Department Department { get; set; }

        [ForeignKey("EmployeeType")]
        public int? TypeId { get; set; }
        public EmployeeType EmployeeType { get; set; }

        [ForeignKey("EmployeeLevel")]
        public int? LevelId { get; set; }
        public EmployeeLevel EmployeeLevel { get; set; }

        public long? PlantId { get; set; }
        public Plant Plant { get; set; }

        [Column(TypeName = "date")]
        public DateTime EntryDate { get; set; }

        [ForeignKey("EmployeePosition")]
        public int? PositionId { get; set; }
        public EmployeePosition EmployeePosition { get; set; }

        public bool Extras { get; set; }
        public bool NotRequiredAHE { get; set; }
        public bool Supervisor { get; set; }
        public bool Subsidy { get; set; }
        public bool PositionLevel { get; set; }

        [StringLength(300)]
        public string AddressLine1 { get; set; }

        [StringLength(200)]
        public string AddressLine2 { get; set; }

        [StringLength(100)]
        public string AddressLine3 { get; set; }

        [StringLength(100)]
        public string AddressLine4 { get; set; }

        [StringLength(200)]
        public string EmailAddress { get; set; }

        public bool IsActive { get; set; }
    }
}
