using Microsoft.AspNetCore.Http;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using DiagnofirmAdmin.Handler;
using DiagnofirmAdmin.Model;

namespace DiagnofirmAdmin.Controllers
{
    internal class AuthorizationControl
    {
        private readonly string authToken;
        private readonly string siteCode;
        private readonly string tCode;
        private readonly string controllerAndMethodName;
        private readonly DALHandler dalhandler;
        public AuthorizationControl(HttpRequest httpRequest, string controllerName)
        {
            authToken = httpRequest.Headers["Authorization"];
            siteCode = httpRequest.Headers["SITECODE"];
            tCode = httpRequest.Headers["TCODE"];
            controllerAndMethodName = httpRequest.Headers["CONTROLLERNAME"];
            if (string.IsNullOrWhiteSpace(controllerAndMethodName))
            {
                controllerAndMethodName = controllerName;
            }
            dalhandler = new();
        }
        internal string GetUserSGID()
        {
            var jwtEncodedString = authToken.Substring(7);
            var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
            return token.Claims.First(c => c.Type == "SGID").Value.ToUpper();
        }

        internal string GetSiteCode()
        {
            return siteCode;
        }

        internal string GetTransactionCode()
        {
            return tCode;
        }
        public string GetControllerAndMethodName()
        {
            return controllerAndMethodName;
        }

        internal string GetUserName()
        {
            var jwtEncodedString = authToken.Substring(7);
            var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
            return token.Claims.First(c => c.Type == "Username").Value;
        }

        internal string GetMailId()
        {
            var jwtEncodedString = authToken.Substring(7);
            var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
            return token.Claims.First(c => c.Type == "MailID").Value;
        }

        public bool IsDeploymentInProgress(string plant)
        {
            DataSet ds = GetDeploymentDetails(plant);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return Convert.ToString(ds.Tables[0].Rows[0][1]) == "Y";
            return false;
        }

        public DataSet GetDeploymentDetails(string plant)
        {
            try
            {
                string query = @"SELECT public.fn_get_deploymentprocess(@plant,@ref1);";
                NpgsqlParameter[] NpgsqlParameters = new NpgsqlParameter[2];
                NpgsqlParameters[0] = new NpgsqlParameter("@plant", NpgsqlDbType.Varchar)
                {
                    Value = plant
                };

                NpgsqlParameters[1] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };
                return dalhandler.getConnectionObject().ExecuteMultipleSelectQuery(query, NpgsqlParameters);
            }
            catch
            {
                throw;
            }
        }

        public DataSet GetVersionNumberInformation(string plant)
        {
            try
            {
                string query = @"SELECT public.fn_get_deploymentprocess(@plant,@ref1);";
                NpgsqlParameter[] NpgsqlParameters = new NpgsqlParameter[2];
                NpgsqlParameters[0] = new NpgsqlParameter("@plant", NpgsqlDbType.Varchar)
                {
                    Value = plant
                };

                NpgsqlParameters[1] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };
                return dalhandler.getConnectionObject().ExecuteMultipleSelectQuery(query, NpgsqlParameters);
            }
            catch
            {
                throw;
            }
        }

        public DataSet GetPrinterAddOnAlertEmailDetails(string plant, string language)
        {
            try
            {
                string query = @"SELECT public.fn_getprinteraddonemaildetails(@plantcode, @langu, @ref1, @ref2);";
                NpgsqlParameter[] NpgsqlParameters = new NpgsqlParameter[4];
                
                NpgsqlParameters[0] = new NpgsqlParameter("@plantcode", NpgsqlDbType.Varchar)
                {
                    Value = plant
                };

                NpgsqlParameters[1] = new NpgsqlParameter("@langu", NpgsqlDbType.Varchar)
                {
                    Value = language
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
            catch
            {
                throw;
            }
        }

        public DataSet CreateSessionLogCapture(SessionLogCaptureModel sessionLogCaptureModel)
        {
            try
            {
                string query = @"SELECT public.fn_session_logcapture(@plant, @sgid, @device_type, @ref1);";
                NpgsqlParameter[] NpgsqlParameters = new NpgsqlParameter[4];

                NpgsqlParameters[0] = new NpgsqlParameter("@plant", NpgsqlDbType.Varchar)
                {
                    Value = sessionLogCaptureModel.PlantCode
                };
                NpgsqlParameters[1] = new NpgsqlParameter("@sgid", NpgsqlDbType.Varchar)
                {
                    Value = sessionLogCaptureModel.UserId
                };
                NpgsqlParameters[2] = new NpgsqlParameter("@device_type", NpgsqlDbType.Varchar)
                {
                    Value = sessionLogCaptureModel.DeviceType
                };

                NpgsqlParameters[3] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };
                return dalhandler.getConnectionObject().ExecuteMultipleSelectQuery(query, NpgsqlParameters);
            }
            catch
            {
                throw;
            }
        }

        public object GetDeploymentProcessObject()
        {
            return new { status = "Error", response = "", message = "DIP" };
        }
    }

}


