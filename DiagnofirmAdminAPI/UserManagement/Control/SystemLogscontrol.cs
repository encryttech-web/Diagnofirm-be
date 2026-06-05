using ORIONAPI.Model;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;

namespace ORIONAPI.Control
{
    public class SystemLogscontrol
    {
        private readonly DBConnection conn;
        public SystemLogscontrol()
        {
            conn = new DBConnection();
        }

        public DataTable GetSystemLogUserList(SystemLogDeatils systemLog)
        {
            string UserName = systemLog.Username;

            string query = @"select * from  fn_get_systemlog_userlist(@RefVal,@UserName)";
            NpgsqlParameter[] NpgsqlParameters = new NpgsqlParameter[2];

            NpgsqlParameters[0] = new NpgsqlParameter("@RefVal", NpgsqlDbType.Refcursor);
            NpgsqlParameters[0].Value = Convert.ToString("RefVal");

            NpgsqlParameters[1] = new NpgsqlParameter("@UserName", NpgsqlDbType.Text);
            NpgsqlParameters[1].Value = Convert.ToString(UserName);

            return conn.ExecuteSingleSelectQuery(query, NpgsqlParameters);
        }

        public DataTable GetUserSystemLog(SystemLogDeatils systemLog)
        {
            string SGID = systemLog.SGid;
            string FromDate = systemLog.Fromdate;
            string ToDate = systemLog.Todate;

            string query = @"select * from  fn_get_user_systemlog(@RefVal,@SGID,@FromDate,@ToDate)";
            NpgsqlParameter[] NpgsqlParameters = new NpgsqlParameter[4];

            NpgsqlParameters[0] = new NpgsqlParameter("@RefVal", NpgsqlDbType.Refcursor);
            NpgsqlParameters[0].Value = Convert.ToString("RefVal");

            NpgsqlParameters[1] = new NpgsqlParameter("@SGID", NpgsqlDbType.Text);
            NpgsqlParameters[1].Value = Convert.ToString(SGID);

            NpgsqlParameters[2] = new NpgsqlParameter("@FromDate", NpgsqlDbType.Text);
            NpgsqlParameters[2].Value = Convert.ToString(FromDate);

            NpgsqlParameters[3] = new NpgsqlParameter("@ToDate", NpgsqlDbType.Text);
            NpgsqlParameters[3].Value = Convert.ToString(ToDate);

            return conn.ExecuteSingleSelectQuery(query, NpgsqlParameters);
        }
    }
}
