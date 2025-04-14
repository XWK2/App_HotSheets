using System.Data.Odbc;
using System.Data;
using Denso.HotSheet.AS400.Dto;
using System.Collections.Generic;

namespace Denso.HotSheet.AS400.Connection
{
    public class BaseODBC : IAS400Connection
    {
        private string _connectionString;

        public BaseODBC(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void ExecuteWithNoReturn(string cmdText)
        {
            DataSet ds = new DataSet("Result");
            OdbcConnection oConnection = new OdbcConnection(_connectionString);
            OdbcCommand cmd = new OdbcCommand(cmdText, oConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public void ExecuteWithNoReturn(string cmdText, List<AS400ParameterDto> parameters)
        {
            DataSet ds = new DataSet("Result");
            OdbcConnection oConnection = new OdbcConnection(_connectionString);
            OdbcCommand cmd = new OdbcCommand(cmdText, oConnection);
            cmd.CommandType = CommandType.Text;
            foreach (AS400ParameterDto parameter in parameters)
            {
                cmd.Parameters.Add(new OdbcParameter(parameter.Name, parameter.Value));
            }
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public object ExecuteWithReturnScalar(string cmdText)
        {
            DataSet ds = new DataSet("Result");
            OdbcConnection oConnection = new OdbcConnection(_connectionString);
            OdbcCommand cmd = new OdbcCommand(cmdText, oConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Connection.Open();
            object obj = cmd.ExecuteScalar();
            cmd.Connection.Close();
            return obj;
        }

        public object ExecuteWithReturnScalar(string cmdText, List<AS400ParameterDto> parameters)
        {
            DataSet ds = new DataSet("Result");
            OdbcConnection oConnection = new OdbcConnection(_connectionString);
            OdbcCommand cmd = new OdbcCommand(cmdText, oConnection);
            cmd.CommandType = CommandType.Text;
            foreach (AS400ParameterDto parameter in parameters)
            {
                cmd.Parameters.Add(new OdbcParameter(parameter.Name, parameter.Value));
            }
            cmd.Connection.Open();
            object obj = cmd.ExecuteScalar();
            cmd.Connection.Close();
            return obj;            
        }

        public DataSet ExecuteWithReturnDataSet(string cmdText)
        {
            DataSet ds = new DataSet("Result");
            OdbcConnection oConnection = new OdbcConnection(_connectionString);
            OdbcCommand cmd = new OdbcCommand(cmdText, oConnection);
            cmd.CommandType = CommandType.Text;
            OdbcDataAdapter adp = new OdbcDataAdapter(cmd);
            adp.Fill(ds);
            cmd.Connection.Close();
            return ds;
        }

        public DataSet ExecuteWithReturnDataSet(string cmdText, List<AS400ParameterDto> parameters)
        {
            DataSet ds = new DataSet("Result");
            OdbcConnection oConnection = new OdbcConnection(_connectionString);
            OdbcCommand cmd = new OdbcCommand(cmdText, oConnection);
            cmd.CommandType = CommandType.Text;
            foreach (AS400ParameterDto parameter in parameters)
            {
                cmd.Parameters.Add(new OdbcParameter(parameter.Name, parameter.Value));
            }
            OdbcDataAdapter adp = new OdbcDataAdapter(cmd);
            adp.Fill(ds);
            cmd.Connection.Close();
            return ds;
        }
    }
}
