using Denso.HotSheet.HotSheet.DBServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denso.HotSheet.HotSheet.DBServices
{
    
    public interface IDBServicesManager
    {

        Task GetLogsTest();

        Task<List<DBServicesLogsDto>> GetLogsInterfaces(string DateStart, string DateEnd);

    }
}
