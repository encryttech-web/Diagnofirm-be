using Npgsql;
using NpgsqlTypes;
using System.Data;
using DiagnofirmAdmin.Handler;
using DiagnofirmAdmin.Model;

namespace DiagnofirmAdmin.Control
{
    public class UsermasterControl
    {
        private readonly DALHandler dalhandler;
        public UsermasterControl()
        {
            dalhandler = new();
        }

        public DataSet getuser(userModel userModel)
        {
            try
            {
                string query = @"select logreg.get_user(@username,@startdate,@enddate,@ref1,@ref2);";
                NpgsqlParameter[] NpgsqlParameters = new NpgsqlParameter[5];
                NpgsqlParameters[0] = new NpgsqlParameter("@username", NpgsqlDbType.Text)
                {
                    Value = userModel.userid
                };
                NpgsqlParameters[1] = new NpgsqlParameter("@startdate", NpgsqlDbType.Text)
                {
                    Value = userModel.startdate
                };
                NpgsqlParameters[2] = new NpgsqlParameter("@enddate", NpgsqlDbType.Text)
                {
                    Value = userModel.enddate
                };
                NpgsqlParameters[3] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };
                NpgsqlParameters[4] = new NpgsqlParameter("@ref2", NpgsqlDbType.Refcursor)
                {
                    Value = "ref2"
                };

                return dalhandler.getConnectionObject().ExecuteMultipleSelectQuery(query, NpgsqlParameters);
            }
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        public DataSet getuserbyId(getuserModel getusermodel)
        {
            try
            {
                string query = @"select logreg.get_userbyid(@userid,@sgid,@ref1,@ref2);";
                NpgsqlParameter[] NpgsqlParameters = new NpgsqlParameter[4];
                NpgsqlParameters[0] = new NpgsqlParameter("@userid", NpgsqlDbType.Text)
                {
                    Value = getusermodel.userid
                };
                NpgsqlParameters[1] = new NpgsqlParameter("@sgid", NpgsqlDbType.Text)
                {
                    Value = getusermodel.sgid
                };
                NpgsqlParameters[2] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };
                NpgsqlParameters[3] = new NpgsqlParameter("@ref2", NpgsqlDbType.Refcursor)
                {
                    Value = "ref2"
                };

                return dalhandler.getConnectionObject().ExecuteMultipleSelectQuery(query, NpgsqlParameters);
            }
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        public DataSet adduser(adduserModel addusermodel)
        {
            try
            {
                string query = @"select logreg.add_user(@sgid,@firstname,@lastname,@username,@emailid,@phno,@userrole,@userdepartment,@userprocess,@userproduct,@userplant,@userpwd,@createdby,@status,@ref1);";
                NpgsqlParameter[] NpgsqlParameters = new NpgsqlParameter[15];
                NpgsqlParameters[0] = new NpgsqlParameter("@sgid", NpgsqlDbType.Text)
                {
                    Value = addusermodel.sgid
                };
                NpgsqlParameters[1] = new NpgsqlParameter("@firstname", NpgsqlDbType.Text)
                {
                    Value = addusermodel.firstname
                };
                NpgsqlParameters[2] = new NpgsqlParameter("@lastname", NpgsqlDbType.Text)
                {
                    Value = addusermodel.lastname
                };
                NpgsqlParameters[3] = new NpgsqlParameter("@username", NpgsqlDbType.Text)
                {
                    Value = addusermodel.username
                };
                NpgsqlParameters[4] = new NpgsqlParameter("@emailid", NpgsqlDbType.Text)
                {
                    Value = addusermodel.emailid
                };
                NpgsqlParameters[5] = new NpgsqlParameter("@phno", NpgsqlDbType.Text)
                {
                    Value = addusermodel.phoneno
                };
                NpgsqlParameters[6] = new NpgsqlParameter("@userrole", NpgsqlDbType.Text)
                {
                    Value = addusermodel.userrole
                };
                NpgsqlParameters[7] = new NpgsqlParameter("@userdepartment", NpgsqlDbType.Text)
                {
                    Value = addusermodel.userdepartment
                };
                NpgsqlParameters[8] = new NpgsqlParameter("@userprocess", NpgsqlDbType.Text)
                {
                    Value = addusermodel.userprocess
                };
                NpgsqlParameters[9] = new NpgsqlParameter("@userproduct", NpgsqlDbType.Text)
                {
                    Value = addusermodel.userproduct
                };
                NpgsqlParameters[10] = new NpgsqlParameter("@userplant", NpgsqlDbType.Text)
                {
                    Value = addusermodel.userplant
                };
                NpgsqlParameters[11] = new NpgsqlParameter("@userpwd", NpgsqlDbType.Text)
                {
                    Value = addusermodel.password
                };
                NpgsqlParameters[12] = new NpgsqlParameter("@createdby", NpgsqlDbType.Text)
                {
                    Value = addusermodel.userid
                };
                NpgsqlParameters[13] = new NpgsqlParameter("@status", NpgsqlDbType.Text)
                {
                    Value = addusermodel.status
                };
                NpgsqlParameters[14] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject().ExecuteMultipleSelectQuery(query, NpgsqlParameters);
            }
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        public DataSet updateuser(updateuserModel updateusermodel)
        {
            try
            {
                string query = @"select logreg.upd_user(@umid,@sgid,@firstname,@lastname,@username,@emailid,@phno,@userrole,@userdepartment,@userprocess,@userproduct,@userplant,@userpwd,@createdby,@status,@ref1);";
                NpgsqlParameter[] NpgsqlParameters = new NpgsqlParameter[16];
                NpgsqlParameters[0] = new NpgsqlParameter("@umid", NpgsqlDbType.Text)
                {
                    Value = updateusermodel.usermasterid
                };
                NpgsqlParameters[1] = new NpgsqlParameter("@sgid", NpgsqlDbType.Text)
                {
                    Value = updateusermodel.sgid
                };
                NpgsqlParameters[2] = new NpgsqlParameter("@firstname", NpgsqlDbType.Text)
                {
                    Value = updateusermodel.firstname
                };
                NpgsqlParameters[3] = new NpgsqlParameter("@lastname", NpgsqlDbType.Text)
                {
                    Value = updateusermodel.lastname
                };
                NpgsqlParameters[4] = new NpgsqlParameter("@username", NpgsqlDbType.Text)
                {
                    Value = updateusermodel.username
                };
                NpgsqlParameters[5] = new NpgsqlParameter("@emailid", NpgsqlDbType.Text)
                {
                    Value = updateusermodel.emailid
                };
                NpgsqlParameters[6] = new NpgsqlParameter("@phno", NpgsqlDbType.Text)
                {
                    Value = updateusermodel.phoneno
                };
                NpgsqlParameters[7] = new NpgsqlParameter("@userrole", NpgsqlDbType.Text)
                {
                    Value = updateusermodel.userrole
                };
                NpgsqlParameters[8] = new NpgsqlParameter("@userdepartment", NpgsqlDbType.Text)
                {
                    Value = updateusermodel.userdepartment
                };
                NpgsqlParameters[9] = new NpgsqlParameter("@userprocess", NpgsqlDbType.Text)
                {
                    Value = updateusermodel.userprocess
                };
                NpgsqlParameters[10] = new NpgsqlParameter("@userproduct", NpgsqlDbType.Text)
                {
                    Value = updateusermodel.userproduct
                };
                NpgsqlParameters[11] = new NpgsqlParameter("@userplant", NpgsqlDbType.Text)
                {
                    Value = updateusermodel.userplant
                };
                NpgsqlParameters[12] = new NpgsqlParameter("@userpwd", NpgsqlDbType.Text)
                {
                    Value = updateusermodel.password
                };
                NpgsqlParameters[13] = new NpgsqlParameter("@createdby", NpgsqlDbType.Text)
                {
                    Value = updateusermodel.userid
                };
                NpgsqlParameters[14] = new NpgsqlParameter("@status", NpgsqlDbType.Text)
                {
                    Value = updateusermodel.status
                };
                NpgsqlParameters[15] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject().ExecuteMultipleSelectQuery(query, NpgsqlParameters);
            }
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        public DataSet deluser(deluserModel deluserModel)
        {
            try
            {
                string query = @"select logreg.del_user(@delid,@username,@ref1);";
                NpgsqlParameter[] NpgsqlParameters = new NpgsqlParameter[3];
                NpgsqlParameters[0] = new NpgsqlParameter("@delid", NpgsqlDbType.Text)
                {
                    Value = deluserModel.delid
                };
                NpgsqlParameters[1] = new NpgsqlParameter("@username", NpgsqlDbType.Text)
                {
                    Value = deluserModel.username
                };
                NpgsqlParameters[2] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };
               

                return dalhandler.getConnectionObject().ExecuteMultipleSelectQuery(query, NpgsqlParameters);
            }
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

    }
}
