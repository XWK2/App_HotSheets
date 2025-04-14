using System.Data.OleDb;
using System.Data;
using Denso.HotSheet.AS400.Dto;
using System.Collections.Generic;

namespace Denso.HotSheet.AS400.Connection
{
    public class BaseOleDb : IAS400Connection
    {
        private string _connectionString;

        public BaseOleDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void ExecuteWithNoReturn(string cmdText)
        {
            DataSet ds = new DataSet("Result");
            OleDbConnection oConnection = new OleDbConnection(_connectionString);
            OleDbCommand cmd = new OleDbCommand(cmdText, oConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public void ExecuteWithNoReturn(string cmdText, List<AS400ParameterDto> parameters)
        {
            DataSet ds = new DataSet("Result");
            OleDbConnection oConnection = new OleDbConnection(_connectionString);
            OleDbCommand cmd = new OleDbCommand(cmdText, oConnection);
            cmd.CommandType = CommandType.Text;

            foreach (AS400ParameterDto parameter in parameters)
            {
                cmd.Parameters.Add(new OleDbParameter(parameter.Name, parameter.Value));
            }

            // ===> Following lines for tests to get full insert query <=====
            /*
            string commandStringTest = cmd.CommandText;
            foreach (OleDbParameter parameter in cmd.Parameters)
            {
                string valueParam = parameter.Value.ToString();
                if (parameter.DbType == DbType.String)
                {
                    valueParam = "'" + parameter.Value.ToString() + "'";
                }

                commandStringTest = commandStringTest.Replace(parameter.ParameterName.ToString(), valueParam);
            }
            */

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();            
        }

        public object ExecuteWithReturnScalar(string cmdText)
        {
            DataSet ds = new DataSet("Result");
            OleDbConnection oConnection = new OleDbConnection(_connectionString);
            OleDbCommand cmd = new OleDbCommand(cmdText, oConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Connection.Open();
            object obj = cmd.ExecuteScalar();
            cmd.Connection.Close();
            return obj;
        }

        public object ExecuteWithReturnScalar(string cmdText, List<AS400ParameterDto> parameters)
        {
            DataSet ds = new DataSet("Result");
            OleDbConnection oConnection = new OleDbConnection(_connectionString);
            OleDbCommand cmd = new OleDbCommand(cmdText, oConnection);
            cmd.CommandType = CommandType.Text;

            foreach (AS400ParameterDto parameter in parameters)
            {
                cmd.Parameters.Add(new OleDbParameter(parameter.Name, parameter.Value));
            }
            cmd.Connection.Open();
            object obj = cmd.ExecuteScalar();
            cmd.Connection.Close();
            return obj;
        }

        public DataSet ExecuteWithReturnDataSet(string cmdText)
        {
            DataSet ds = new DataSet("Result");
            OleDbConnection oConnection = new OleDbConnection(_connectionString);
            OleDbCommand cmd = new OleDbCommand(cmdText, oConnection);
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
            adp.Fill(ds);
            cmd.Connection.Close();
            return ds;

        }

        public DataSet ExecuteWithReturnDataSet(string cmdText, List<AS400ParameterDto> parameters)
        {
            DataSet ds = new DataSet("Result");
            OleDbConnection oConnection = new OleDbConnection(_connectionString);
            OleDbCommand cmd = new OleDbCommand(cmdText, oConnection);
            cmd.CommandType = CommandType.Text;

            foreach (AS400ParameterDto parameter in parameters)
            {
                cmd.Parameters.Add(new OleDbParameter(parameter.Name, parameter.Value));
            }

            OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
            adp.Fill(ds);
            cmd.Connection.Close();
            return ds;
        }        
    }
}
