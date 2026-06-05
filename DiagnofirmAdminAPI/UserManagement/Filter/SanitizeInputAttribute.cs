using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Reflection;
using System.Web;

namespace DiagnofirmAdmin.Filter
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class SanitizeInputAttribute : ActionFilterAttribute
    {
        // Check the Model Before Action Exceutes
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments != null && context.ActionArguments.Count == 1)
            {
                var requestParam = context.ActionArguments.First();
                var properties = requestParam.Value.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
               .Where(x => x.CanRead && x.CanWrite && x.PropertyType == typeof(string) && x.GetGetMethod(true).IsPublic && x.GetSetMethod(true).IsPublic);
                foreach (var propertyInfo in properties)
                {
                    propertyInfo.SetValue(requestParam.Value, HttpUtility.HtmlEncode(propertyInfo.GetValue(requestParam.Value) as string));
                }
            }
        }
    }
}
