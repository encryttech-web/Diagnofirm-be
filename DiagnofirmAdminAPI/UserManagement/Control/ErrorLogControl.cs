using ORIONAPI.Model;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;


namespace ORIONAPI.Control
{
    public class ErrorLogControl
    {
        private readonly DBConnection conn;
        public ErrorLogControl()
        {
            conn = new DBConnection();
        }   

        public DataSet InsertErrorLogDetails(ErrorLogModel ErrorDetails)
        {

            string SGID         = ErrorDetails.SGID;
            string APIName      = ErrorDetails.APIName;
            string MethodName   = ErrorDetails.MethodName;
            string ActionName   = ErrorDetails.ActionName;
            string ErrorMessage = ErrorDetails.ErrorMessage;
            string query = @"select * from fn_insertErrorLogDetails(@ref1,@SGID,@APIName,@MethodName,@ActionName,@ErrorMessage);";
            NpgsqlParameter[] NpgsqlParameters = new NpgsqlParameter[6];
            NpgsqlParameters[0] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor);
            NpgsqlParameters[0].Value = Convert.ToString("ref1");
            NpgsqlParameters[1] = new NpgsqlParameter("@SGID", NpgsqlDbType.Varchar);
            NpgsqlParameters[1].Value = Convert.ToString(SGID);
            NpgsqlParameters[2] = new NpgsqlParameter("@APIName", NpgsqlDbType.Varchar);
            NpgsqlParameters[2].Value = Convert.ToString(APIName);
            NpgsqlParameters[3] = new NpgsqlParameter("@MethodName", NpgsqlDbType.Varchar);
            NpgsqlParameters[3].Value = Convert.ToString(MethodName);
            NpgsqlParameters[4] = new NpgsqlParameter("@ActionName", NpgsqlDbType.Varchar);
            NpgsqlParameters[4].Value = Convert.ToString(ActionName);
            NpgsqlParameters[5] = new NpgsqlParameter("@ErrorMessage", NpgsqlDbType.Varchar);
            NpgsqlParameters[5].Value = Convert.ToString(ErrorMessage.Length >= 800 ? ErrorMessage.Substring(0,790) : ErrorMessage);

            return conn.ExecuteMultipleSelectQuery(query, NpgsqlParameters);
        }

        public DataSet InsertAPIErrorLogDetails(string SGID, string APIName, string MethodName, string ActionName, string ErrorMessage)
        {
            string query = @"select * from fn_insertErrorLogDetails(@ref1,@SGID,@APIName,@MethodName,@ActionName,@ErrorMessage);";
            NpgsqlParameter[] NpgsqlParameters = new NpgsqlParameter[6];
            NpgsqlParameters[0] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor);
            NpgsqlParameters[0].Value = Convert.ToString("ref1");
            NpgsqlParameters[1] = new NpgsqlParameter("@SGID", NpgsqlDbType.Varchar);
            NpgsqlParameters[1].Value = Convert.ToString(SGID);
            NpgsqlParameters[2] = new NpgsqlParameter("@APIName", NpgsqlDbType.Varchar);
            NpgsqlParameters[2].Value = Convert.ToString(APIName);
            NpgsqlParameters[3] = new NpgsqlParameter("@MethodName", NpgsqlDbType.Varchar);
            NpgsqlParameters[3].Value = Convert.ToString(MethodName);
            NpgsqlParameters[4] = new NpgsqlParameter("@ActionName", NpgsqlDbType.Varchar);
            NpgsqlParameters[4].Value = Convert.ToString(ActionName);
            NpgsqlParameters[5] = new NpgsqlParameter("@ErrorMessage", NpgsqlDbType.Varchar);
            NpgsqlParameters[5].Value = Convert.ToString(ErrorMessage.Length >= 800 ? ErrorMessage.Substring(0, 790) : ErrorMessage);

            return conn.ExecuteMultipleSelectQuery(query, NpgsqlParameters);
        }
    }
}
