using Microsoft.AspNetCore.Http;
using Novell.Directory.Ldap;
using System;
using System.Data;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DiagnofirmAdmin.Control;
using DiagnofirmAdmin.Model;

namespace DiagnofirmAdmin.Middleware
{
    public class SwaggerBasicAuthMiddleware
    {
        private readonly RequestDelegate next;
        public SwaggerBasicAuthMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                string authHeader = context.Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic "))
                {
                    // Get the credentials from request header
                    var header = AuthenticationHeaderValue.Parse(authHeader);
                    var inBytes = Convert.FromBase64String(header.Parameter);
                    var credentials = Encoding.UTF8.GetString(inBytes).Split(':');
                    var username = credentials[0];
                    var password = credentials[1];
                    // validate credentials
                    //if (ValidateSwaggerInput(username, password))
                    //{
                    //    await next.Invoke(context).ConfigureAwait(false);
                    //    return;
                    //}
                }
                context.Response.Headers["WWW-Authenticate"] = "Basic";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
            {
                await next.Invoke(context).ConfigureAwait(false);
            }
        }

        //private bool ValidateSwaggerInput(string sgid, string password)
        //{
        //    LoginControl loginControl = new();
        //    LoginModelLDAP loginModelLDAP = new();
        //    loginModelLDAP.SGID = sgid;
        //    DataSet ds = loginControl.ValidateSwashBuckle(loginModelLDAP);
        //    if (ds == null)
        //    {
        //        return false;
        //    }
        //    else if (ds.Tables.Count > 0)
        //    {
        //        if (ds.Tables[0].Rows[0]["status"].ToString() == "success")
        //        {
        //            string domain = ds.Tables[0].Rows[0]["domainname"].ToString();
        //            bool isValid = false;
        //            using (var connection = new LdapConnection { SecureSocketLayer = false })
        //            {
        //                connection.Connect(domain, LdapConnection.DefaultPort);
        //                string userDn = $"{sgid}@{domain}";
        //                connection.Bind(userDn, password);

        //                if (connection.Bound)
        //                {
        //                    isValid = true;
        //                }
        //            }
        //            if (isValid)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}
