using ORIONAPI.Model;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;

namespace ORIONAPI.Control
{

    public class UserRightsControl
    {
        public readonly DBConnection conn;
        public UserRightsControl()
        {
            conn = new DBConnection();
        }

        //public DataTable UpdateUserRules(UserRights user)
        //{
        //    string query = @"select * from fn_updateUserRules(@ref1,@_userid,@_createdby,@_rights);";

        //    var NpgsqlParameters = new NpgsqlParameter[4];
        //    NpgsqlParameters[0] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
        //    {
        //        Value = Convert.ToString("ref1")
        //    };
        //    NpgsqlParameters[1] = new NpgsqlParameter("@_userid", NpgsqlDbType.Varchar)
        //    {
        //        Value = Convert.ToString(user.UserID)
        //    };
        //    NpgsqlParameters[2] = new NpgsqlParameter("@_createdby", NpgsqlDbType.Varchar)
        //    {
        //        Value = Convert.ToString(user.CreatedBy)
        //    };
        //    NpgsqlParameters[3] = new NpgsqlParameter("@_rights", NpgsqlDbType.Text)
        //    {
        //        Value = Convert.ToString(user.StrRights)
        //    };
        //    return conn.ExecuteSingleSelectQuery(query, NpgsqlParameters);
        //}

        //public DataTable GetUser()
        //{
        //    string query = @"select * from fn_lamalogintable(@first_tbl);";
        //    NpgsqlParameter[] NpgsqlParameters = new NpgsqlParameter[1];
        //    NpgsqlParameters[0] = new NpgsqlParameter("@first_tbl", NpgsqlDbType.Refcursor)
        //    {
        //        Value = Convert.ToString("first_tbl"),
        //        Direction = ParameterDirection.InputOutput,
        //        NpgsqlDbType = NpgsqlDbType.Refcursor
        //    };
        //    return conn.ExecuteSingleSelectQuery(query, NpgsqlParameters);
        //}
        //public DataSet UserTransaction(UserRights user)
        //{
        //    string query = @"select * from fn_lama_user_trns_details(@userid,@first_tbl,@second_tbl,@third_tbl,@fourth_tbl,@fifth_tbl);";
        //    NpgsqlParameter[] NpgsqlParameters = new NpgsqlParameter[6];
        //    NpgsqlParameters[0] = new NpgsqlParameter("@userid", NpgsqlDbType.Varchar)
        //    {
        //        Value = Convert.ToString(user.UserID)
        //    };
        //    NpgsqlParameters[1] = new NpgsqlParameter("@first_tbl", NpgsqlDbType.Refcursor)
        //    {
        //        Value = Convert.ToString("group"),
        //        Direction = ParameterDirection.InputOutput,
        //        NpgsqlDbType = NpgsqlDbType.Refcursor
        //    };
        //    NpgsqlParameters[2] = new NpgsqlParameter("@second_tbl", NpgsqlDbType.Refcursor)
        //    {
        //        Value = Convert.ToString("transaction"),
        //        Direction = ParameterDirection.InputOutput,
        //        NpgsqlDbType = NpgsqlDbType.Refcursor
        //    };
        //    NpgsqlParameters[3] = new NpgsqlParameter("@third_tbl", NpgsqlDbType.Refcursor)
        //    {
        //        Value = Convert.ToString("details"),
        //        Direction = ParameterDirection.InputOutput,
        //        NpgsqlDbType = NpgsqlDbType.Refcursor
        //    };

        //    NpgsqlParameters[4] = new NpgsqlParameter("@fourth_tbl", NpgsqlDbType.Refcursor)
        //    {
        //        Value = Convert.ToString("rights"),
        //        Direction = ParameterDirection.InputOutput,
        //        NpgsqlDbType = NpgsqlDbType.Refcursor
        //    };
        //    NpgsqlParameters[5] = new NpgsqlParameter("@fifth_tbl", NpgsqlDbType.Refcursor)
        //    {
        //        Value = Convert.ToString("rightshdr"),
        //        Direction = ParameterDirection.InputOutput,
        //        NpgsqlDbType = NpgsqlDbType.Refcursor
        //    };
        //    return conn.ExecuteMultipleSelectQuery(query, NpgsqlParameters);
        //}
    }
}
