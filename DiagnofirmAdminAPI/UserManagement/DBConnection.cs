using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Data;

namespace DiagnofirmAdmin
{
    public class DBConnection
    {
        readonly private static string connstr = Startup.StaticConfig.GetConnectionString("LoginConnection");
        readonly EncryptDecrypt _Protect = new EncryptDecrypt();

        public DataTable ExecuteSingleSelectQuery(string _query, NpgsqlParameter[] sqlParameter)
        {
            using var npg_connection = new NpgsqlConnection(_Protect.SGDecryption(connstr));
            var npg_command = new NpgsqlCommand(_query, npg_connection);
            npg_command.CommandTimeout = 120;
            try
            {
                npg_connection.Open();
                npg_connection.Notice += Conn_Notice;
                var dt = new DataTable();
                using NpgsqlTransaction tran = npg_connection.BeginTransaction();
                npg_command.CommandType = CommandType.Text;
                foreach (NpgsqlParameter parm in sqlParameter)
                {
                    npg_command.Parameters.AddWithValue(parm.ParameterName, parm.NpgsqlDbType, parm.NpgsqlValue ?? DBNull.Value);
                }
                npg_command.ExecuteNonQuery();
                foreach (NpgsqlParameter parm in sqlParameter)
                {
                    if (parm.NpgsqlDbType == NpgsqlTypes.NpgsqlDbType.Refcursor && (parm.Value.ToString() != "null" || parm.Value.ToString() != "NULL" || parm.Value.ToString() != ""))
                    {
                        string parm_val = string.Format("FETCH ALL IN \"{0}\"", parm.Value.ToString());
                        var npg_adapter = new NpgsqlDataAdapter(parm_val.Trim().ToString(), npg_connection);
                        npg_adapter.Fill(dt);
                    }
                }
                tran.Commit();
                return dt;
            }
            catch (NpgsqlException ex)
            {
                Serilog.Log.Logger.Error($"Error while processing the db { ex.ErrorCode } { ex.Data } Message: { ex.Message } { (ex.InnerException != null ? ex.InnerException.Message : "") } State: { ex.SqlState }  Stack: { ex.StackTrace } ");
                throw new DataException("", ex);
            }
            finally
            {
                npg_connection.Close();
            }
        }
        public DataSet ExecuteMultipleSelectQuery(string _query, NpgsqlParameter[] sqlParameter)
        {
            using var npg_connection = new NpgsqlConnection(_Protect.SGDecryption(connstr));
            var npg_command = new NpgsqlCommand(_query, npg_connection);
            npg_command.CommandTimeout = 120;
            var i = 0;
            try
            {
                var ds = new DataSet();
                npg_connection.Open();
                npg_connection.Notice += Conn_Notice;
                using NpgsqlTransaction tran = npg_connection.BeginTransaction();
                npg_command.CommandType = CommandType.Text;
                foreach (NpgsqlParameter parm in sqlParameter)
                {
                    npg_command.Parameters.AddWithValue(parm.ParameterName, parm.NpgsqlDbType, parm.NpgsqlValue ?? DBNull.Value);
                }
                npg_command.ExecuteNonQuery();
                foreach (NpgsqlParameter parm in sqlParameter)
                {
                    if (parm.NpgsqlDbType == NpgsqlTypes.NpgsqlDbType.Refcursor && (parm.Value.ToString() != "null" || parm.Value.ToString() != "NULL" || parm.Value.ToString() != ""))
                    {
                        string parm_val = string.Format("FETCH ALL IN \"{0}\"", parm.Value.ToString());
                        var npg_adapter = new NpgsqlDataAdapter(parm_val.Trim().ToString(), npg_connection);
                        ds.Tables.Add(parm.Value.ToString());
                        npg_adapter.Fill(ds.Tables[i]);
                        i++;
                    }
                }
                tran.Commit();
                return ds;
            }
            catch (NpgsqlException ex)
            {
                Serilog.Log.Logger.Error($"Error while processing the db { ex.ErrorCode } { ex.Data } Message: { ex.Message } { (ex.InnerException != null ? ex.InnerException.Message : "") } State: { ex.SqlState }  Stack: { ex.StackTrace } ");
                throw new DataException("", ex);
            }
            finally
            {
                npg_connection.Close();
            }
        }
        public bool ExecuteInsertQuery(string _query, NpgsqlParameter[] NpgsqlParameter)
        {
            using var npg_connection = new NpgsqlConnection(_Protect.SGDecryption(connstr));
            var npg_command = new NpgsqlCommand(_query, npg_connection);
            npg_command.CommandTimeout = 120;
            try
            {
                npg_connection.Open();
                npg_connection.Notice += Conn_Notice;
                npg_command.CommandText = _query;
                foreach (NpgsqlParameter parm in NpgsqlParameter)
                {
                    npg_command.Parameters.AddWithValue(parm.ParameterName, parm.NpgsqlDbType, parm.NpgsqlValue ?? DBNull.Value);
                }
                var i = npg_command.ExecuteNonQuery();

                return i == -1;
            }
            catch (NpgsqlException ex)
            {
                Serilog.Log.Logger.Error($"Error while processing the db { ex.ErrorCode } { ex.Data } Message: { ex.Message } { (ex.InnerException != null ? ex.InnerException.Message : "") } State: { ex.SqlState }  Stack: { ex.StackTrace } ");
                throw new DataException("", ex);
            }
            finally
            {
                npg_connection.Close();
            }
        }

        private void Conn_Notice(object sender, NpgsqlNoticeEventArgs e)
        {
            Serilog.Log.Logger.Information($"DB Raise Notice { Newtonsoft.Json.JsonConvert.SerializeObject(e.Notice) }");
        }

    }
}

