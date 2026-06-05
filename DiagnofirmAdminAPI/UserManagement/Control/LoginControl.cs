using DiagnofirmAdmin.Handler;
using DiagnofirmAdmin.Model;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace DiagnofirmAdmin.Control
{
    public class LoginControl
    {

        private readonly DALHandler dalhandler;
        public LoginControl()
        {
            dalhandler = new();
        }

        public DataSet Validateuser(LoginValidationModel loginModel)
        {
            try
            {
                string query = @"select logreg.get_validateuser(@sgid,@ref1);";
                NpgsqlParameter[] NpgsqlParameters = new NpgsqlParameter[2];
                NpgsqlParameters[0] = new NpgsqlParameter("@sgid", NpgsqlDbType.Text)
                {
                    Value = loginModel.SGID
                };
                NpgsqlParameters[1] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "Userdata"
                };

                return dalhandler.getConnectionObject().ExecuteMultipleSelectQuery(query, NpgsqlParameters);
            }
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        public DataSet ValidateLDAP(LoginLDAPValidationModel loginModel)
        {
            try
            {
                string query = @"select logreg.get_validateuser(@sgid,@ref1);";
                NpgsqlParameter[] NpgsqlParameters = new NpgsqlParameter[2];
                NpgsqlParameters[0] = new NpgsqlParameter("@sgid", NpgsqlDbType.Text)
                {
                    Value = loginModel.SGID
                };
                NpgsqlParameters[1] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "Userdata"
                };
                return dalhandler.getConnectionObject().ExecuteMultipleSelectQuery(query, NpgsqlParameters);
            }
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        public DataSet ValidateSgid(LoginModel LoginModel)
        {
            try
            {
                string query = @"SELECT logreg.get_validatesgid(@sgid,@ref1);";
                NpgsqlParameter[] NpgsqlParameters = new NpgsqlParameter[2];
                NpgsqlParameters[0] = new NpgsqlParameter("@sgid", NpgsqlDbType.Varchar)
                {
                    Value = LoginModel.sgid
                };
                NpgsqlParameters[1] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "userdata"
                };

                return dalhandler.getConnectionObject().ExecuteMultipleSelectQuery(query, NpgsqlParameters);
            }
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        public DataSet Login(LoginUserModel LoginUserModel)
        {
            try
            {
                string query = @"select diafrm.user_login(@p_username,@p_password,@ref1);";
                NpgsqlParameter[] NpgsqlParameters = new NpgsqlParameter[3];
                NpgsqlParameters[0] = new NpgsqlParameter("@p_username", NpgsqlDbType.Text)
                {
                    Value = LoginUserModel.username
                };
                NpgsqlParameters[1] = new NpgsqlParameter("@p_password", NpgsqlDbType.Text)
                {
                    Value = LoginUserModel.Password
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
