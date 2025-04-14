using Denso.HotSheet.AS400.Dto;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Denso.HotSheet.HotSheet.DBServices.Connection
{
    public interface IDBServicesConnection
    {

        void TableExecuteWithNoReturn(string StoredProcedure, string Parameters, DataTable TableGral);

        void ExecuteWithNoReturn(string StoredProcedure);

        void ExecuteWithNoReturn(string StoredProcedure, SqlParameter[] Parametros);

        object ExecuteWithReturnScalar(string StoredProcedure);

        object ExecuteWithReturnScalar(string StoredProcedure, SqlParameter[] Parametros);

        DataSet ExecuteWithReturnDataSet(string StoredProcedure);

        DataSet ExecuteWithReturnDataSet(string StoredProcedure, SqlParameter[] Parametros);

        void TableExecuteWithNoReturn(string StoredProcedure, string Parametros, DataTable TableGral, int IdEmpleado);

    }
}
