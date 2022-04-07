using System.Reflection;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ServiceHost
{
    public class PageFilter:IPageFilter
    {

        private readonly IAuthHelper _authHelper;

        public PageFilter(IAuthHelper authHelper)
        {
            _authHelper = authHelper;
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {

            var needPermissions = (NeedsPermissionAttribute)context.HandlerMethod?.MethodInfo.GetCustomAttribute
                (typeof(NeedsPermissionAttribute))!;

            if(needPermissions==null)
                return;

            var userPermissions = _authHelper.GetPermissions();
            if(userPermissions != null && userPermissions.All(x=>x!=needPermissions.Permission))
                context.HttpContext.Response.Redirect("/Account");


            //var needsPermissions = (NeedsPermissionAttribute)context.HandlerMethod?.MethodInfo.GetCustomAttribute(typeof(NeedsPermissionAttribute))!;

            //var permissions = _authHelper.GetPermissions();
            //if(permissions != null && permissions.All(x=>x!=needsPermissions.Permission))
            //     context.HttpContext.Response.Redirect("/Account");
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
        }
    }
}
