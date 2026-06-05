using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Serilog.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace DiagnofirmAdmin.Middleware
{
    /// <summary>
    /// This is additional Request Response property in serilog - elastic search
    /// </summary>
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// This will invoke on every Request and Response and add additional property in serilog-elastic search
        /// Better add additional property based on the application requirements
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            var SGID = "";
            var PlantID = "";
            context.Request.Headers.TryGetValue("SITECODE", out StringValues plantId);
            context.Request.Headers.TryGetValue("TCODE", out StringValues tCode);
            context.Request.Headers.TryGetValue("LANG", out StringValues lang);
            context.Request.Headers.TryGetValue("Authorization", out StringValues token_id);
            if (token_id.ToString() != "")
            {
                try
                {
                    var jwtEncodedString = token_id.ToString().Replace("bearer", "", System.StringComparison.OrdinalIgnoreCase);
                    var tokenID = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
                    SGID = tokenID.Claims.First(c => c.Type == "SGID").Value;
                    PlantID = tokenID.Claims.First(c => c.Type == "PlantCode").Value;
                }
                catch
                {
                    //Token is not availale or not valid
                }
            }

            using (LogContext.PushProperty("lang", lang.ToString()))
            using (LogContext.PushProperty("tCode", tCode.ToString()))
            using (LogContext.PushProperty("PlantId", plantId.ToString() == "" ? PlantID : plantId.ToString()))
            {
                using (LogContext.PushProperty("SGID", SGID.ToString()))
                {
                    await _next.Invoke(context);
                }
            }
        }
    }
}
