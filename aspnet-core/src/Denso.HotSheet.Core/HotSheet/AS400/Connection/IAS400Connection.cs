using Denso.HotSheet.AS400.Dto;
using System.Collections.Generic;
using System.Data;

namespace Denso.HotSheet.AS400.Connection
{
    public interface IAS400Connection
    {
        void ExecuteWithNoReturn(string cmdText);
        void ExecuteWithNoReturn(string cmdText, List<AS400ParameterDto> parameters);

        object ExecuteWithReturnScalar(string cmdText);
        object ExecuteWithReturnScalar(string cmdText, List<AS400ParameterDto> parameters);

        DataSet ExecuteWithReturnDataSet(string cmdText);
        DataSet ExecuteWithReturnDataSet(string cmdText, List<AS400ParameterDto> parameters);
    }
}
