using Denso.HotSheet.Organization;
using System.Collections.Generic;

namespace Denso.HotSheet.Catalogs.Dto
{
    public class EmployeeOptionsDto
    {
        public List<EmployeeType> Types { get; set; }
        public List<EmployeeLevel> Levels { get; set; }        
        public List<EmployeePosition> Positions { get; set; }
        public List<DepartmentDto> Departments { get; set; }
        public List<PlantDto> Plants { get; set; }

    }
}
