using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using DiagnofirmAdmin.Controllers;

namespace DiagnofirmAdmin.Filters
{
    class AuthorizationTokenFilterAttribute : Attribute, IAuthorizationFilter
    {
        void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext context)
        {
            if (context != null)
            {
                AuthorizationControl authorizationControl = new AuthorizationControl(context.HttpContext.Request,"");
                string sgId = authorizationControl.GetUserSGID();
                string site = authorizationControl.GetSiteCode();
                if (!string.IsNullOrWhiteSpace(sgId) && !string.IsNullOrWhiteSpace(site))
                {
                    context.HttpContext.Items.Add("SGID", sgId);
                    context.HttpContext.Items.Add("SITE", site);
                }
                else
                {
                    context.Result = new BadRequestResult();
                }
            }

        }
    }
}
