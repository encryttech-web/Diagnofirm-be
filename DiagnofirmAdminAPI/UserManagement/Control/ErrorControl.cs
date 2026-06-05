using ORIONAPI.Model;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;

namespace ORIONAPI.Control
{
    public class ErrorControl
    {
        protected DBConnection conn;
        public ErrorControl()
        {
            conn = new DBConnection();
        }

        public DataSet ErrorDetails(Error error)
        {
            string ErrorCode = error.ErrorCode == null ? "" : error.ErrorCode, lang = error.Lang == null ? "" : error.Lang;
            string query = @"select * from fn_geterrordetails(@error_code, @lang, @second_tbl);";
            NpgsqlParameter[] NpgsqlParameters = new NpgsqlParameter[3];
            NpgsqlParameters[0] = new NpgsqlParameter("@error_code", NpgsqlDbType.Varchar);
            NpgsqlParameters[0].Value = Convert.ToString(ErrorCode);
            NpgsqlParameters[1] = new NpgsqlParameter("@lang", NpgsqlDbType.Varchar);
            NpgsqlParameters[1].Value = Convert.ToString(lang);
            NpgsqlParameters[2] = new NpgsqlParameter("@second_tbl", NpgsqlDbType.Refcursor);
            NpgsqlParameters[2].Value = Convert.ToString("second_tbl");
            NpgsqlParameters[2].Direction = ParameterDirection.InputOutput;
            NpgsqlParameters[2].NpgsqlDbType = NpgsqlDbType.Refcursor;
            return conn.ExecuteMultipleSelectQuery(query, NpgsqlParameters);
        }

        public DataSet ErrorAlertDetails(Error error) 
        {
            string ErrorCode = error.ErrorCode == null ? "" : error.ErrorCode, lang = error.Lang == null ? "" : error.Lang;
            string query = @"select * from stocktransfer_alert(@error_code, @lang, @_message);";
            NpgsqlParameter[] NpgsqlParameters = new NpgsqlParameter[3];
            NpgsqlParameters[0] = new NpgsqlParameter("@error_code", NpgsqlDbType.Varchar);
            NpgsqlParameters[0].Value = Convert.ToString(ErrorCode);
            NpgsqlParameters[1] = new NpgsqlParameter("@lang", NpgsqlDbType.Varchar);
            NpgsqlParameters[1].Value = Convert.ToString(lang);
            NpgsqlParameters[2] = new NpgsqlParameter("@_message", NpgsqlDbType.Refcursor);
            NpgsqlParameters[2].Value = Convert.ToString("_message");
            NpgsqlParameters[2].Direction = ParameterDirection.InputOutput;
            NpgsqlParameters[2].NpgsqlDbType = NpgsqlDbType.Refcursor;
            return conn.ExecuteMultipleSelectQuery(query, NpgsqlParameters);
        }

    }
}
