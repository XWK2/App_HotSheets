using System.Collections.Generic;

namespace Denso.HotSheet.Interfaces.Dto
{
    public class CreateUsersFromEmployeesInput
    {
        public int TenantId { get; set; }
        public List<long> EmployeeInternalIds { get; set; }
    }
}
