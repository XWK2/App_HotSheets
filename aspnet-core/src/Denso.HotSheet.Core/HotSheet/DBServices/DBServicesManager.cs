using Abp.Dependency;
using Denso.HotSheet.HotSheet.DBServices.Connection;
using Denso.HotSheet.HotSheet.DBServices.Dto;
using Denso.HotSheet.HotSheet.Logger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Denso.HotSheet.HotSheet.DBServices
{
    public class DBServicesManager : IDBServicesManager, ITransientDependency
    {
        private readonly IHotSheetHistoryManager _shippingHistoryManager;
        private readonly IDBServicesConnection _dbServicesConnection;

        public DBServicesManager(
            IHotSheetHistoryManager shippingHistoryManager,
            IDBServicesConnection dbServicesConnection)
        {
            _shippingHistoryManager = shippingHistoryManager;
            _dbServicesConnection = dbServicesConnection;
        }

        public async Task GetLogsTest() {
            DateTime DateStart = DateTime.Now;
            DateTime DateEnd = DateTime.Now;

            await GetLogsInterfaces(DateStart.AddMonths(-2).ToShortDateString(), DateEnd.ToShortDateString());
        }

        public async Task<List<DBServicesLogsDto>> GetLogsInterfaces(string DateStart, string DateEnd) 
        {                 
            List<string> errorMessages = new List<string>();
            List<DBServicesLogsDto> listLogs = new List<DBServicesLogsDto>();

            if (DateStart == "")
            {
                DateStart = DateTime.Now.AddMonths(-2).ToShortDateString();
            }

            if (DateEnd == "")
            {
                DateEnd = DateTime.Now.ToShortDateString();
            }

            try
            {                
                DataSet ds;
                var Pars = new SqlParameter[2];
                Pars[0] = new SqlParameter("@DateStart", DateStart);
                Pars[1] = new SqlParameter("@DateEnd", DateEnd);

                ds = _dbServicesConnection.ExecuteWithReturnDataSet("spLogServicesInterface_s", Pars);                
                listLogs = DBServicesLogsDto.GetList(ds);
            }
            catch (Exception exc)
            {
                errorMessages.Add(exc.Message);
            }

            return listLogs;
        }

        
    }
}
