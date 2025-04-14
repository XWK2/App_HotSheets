using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denso.HotSheet.HotSheet.DBServices.Dto
{
    public class DBServicesLogsDto
    {
        public int IDLog { get; set; }
        public DateTime DtTimeStamp { get; set; }
		public int IDWebServices { get; set; }
        public string NameWebService { get; set; }
        public string KeyWebService { get; set; }
        public int IDProcess { get; set; }
        public string DescriptionProcess { get; set; }
        public string KeyProcess { get; set; }        
        public string Detail { get; set; }
               

        public static List<DBServicesLogsDto> GetList(DataSet ds)
        {
            return (from dr in ds.Tables[0].AsEnumerable()
                    select new DBServicesLogsDto
                    {
                        IDLog = (int)dr["IDLog"],
                        DtTimeStamp = (DateTime)dr["DtTimeStamp"],
                        IDWebServices = (int)dr["IDWebServices"],
                        NameWebService = (string)dr["NameWebService"],
                        KeyWebService = (string)dr["KeyWebService"],
                        IDProcess = (int)dr["IDProcess"],
                        DescriptionProcess = (string)dr["DescriptionProcess"],
                        KeyProcess = (string)dr["KeyProcess"],
                        Detail = (string)dr["Detail"]        
                    }).ToList();
        }
    }
}
