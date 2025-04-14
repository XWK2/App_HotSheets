using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denso.HotSheet.HotSheet.DBServices.Connection
{
    public class BaseDBService : IDBServicesConnection
    {
        private string _connectionString;

        public BaseDBService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void TableExecuteWithNoReturn(string StoredProcedure, string Parameters, DataTable TableGral)
        {
            DataSet ds = new DataSet("Result");

            SqlConnection oConnection = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(StoredProcedure, oConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(Parameters, TableGral);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public void ExecuteWithNoReturn(string StoredProcedure)
        {
            var oConnection = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(StoredProcedure, oConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public void ExecuteWithNoReturn(string StoredProcedure, SqlParameter[] Parametros)
        {
            var oConnection = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(StoredProcedure, oConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            foreach (SqlParameter Parametro in Parametros)
            {
                cmd.Parameters.Add(Parametro);
            }
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public object ExecuteWithReturnScalar(string StoredProcedure)
        {
            var oConnection = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(StoredProcedure, oConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            var result = cmd.ExecuteScalar();
            cmd.Connection.Close();
            return result;
        }

        public object ExecuteWithReturnScalar(string StoredProcedure, SqlParameter[] Parametros)
        {
            var oConnection = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(StoredProcedure, oConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            foreach (SqlParameter Parametro in Parametros)
            {
                cmd.Parameters.Add(Parametro);
            }
            cmd.Connection.Open();
            var result = cmd.ExecuteScalar();
            cmd.Connection.Close();
            return result;
        }

        public DataSet ExecuteWithReturnDataSet(string StoredProcedure)
        {
            var ds = new DataSet("Result");
            var oConnection = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(StoredProcedure, oConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection.Open();
            using (var adp = new SqlDataAdapter(cmd))
            {
                //adp.SelectCommand.CommandTimeout = 180;
                adp.Fill(ds);
            }

            cmd.Connection.Close();
            return ds;
        }

        public DataSet ExecuteWithReturnDataSet(string StoredProcedure, SqlParameter[] Parametros)
        {
            var ds = new DataSet("Result");
            var oConnection = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(StoredProcedure, oConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            foreach (SqlParameter Parametro in Parametros)
            {
                cmd.Parameters.Add(Parametro);
            }

            var adp = new SqlDataAdapter(cmd);
            adp.Fill(ds);
            cmd.Connection.Close();
            return ds;
        }

        public void TableExecuteWithNoReturn(string StoredProcedure, string Parametros, DataTable TableGral, int IdEmpleado)
        {
            var oConnection = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(StoredProcedure, oConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(Parametros, TableGral);
            if (IdEmpleado != 0)
            {
                cmd.Parameters.AddWithValue("@iIdEmpleado", IdEmpleado);
            }
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public DataTable ADataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
