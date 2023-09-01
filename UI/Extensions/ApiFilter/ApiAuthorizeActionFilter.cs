using Core.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MSC.Extentions.ApiFilter

{
    public class ApiAuthorizeActionFilter : IAuthorizationFilter
    {
        private readonly IRedisCacheManager _redisCacheManager;



        public ApiAuthorizeActionFilter(IRedisCacheManager redisCacheManager)
        {
            _redisCacheManager = redisCacheManager;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            try
            {
                if (context?.HttpContext.User == null || !context.HttpContext.User.Identity.IsAuthenticated
                || context.HttpContext.User.Claims == null || string.IsNullOrEmpty(context.HttpContext.User.Claims.FirstOrDefault(i => i.Type == "AuthID")?.Value))
                {
                    var message = "";
                    context.Result = new UnauthorizedObjectResult(message);
                    return;
                }

                var auth_id_key = context?.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "AuthID");

                var user_permissions_str = _redisCacheManager.GetValue(auth_id_key.Value);
                if (string.IsNullOrEmpty(user_permissions_str))
                {
                    var message = "";
                    context.Result = new UnauthorizedObjectResult(message);
                    return;
                }
                var role_permissions = user_permissions_str.Split(",").ToList();

                //controller = context?.ActionDescriptor.RouteValues["controller"];
                //action = context?.ActionDescriptor.RouteValues["action"];
                var requested_permission = context.ActionDescriptor.DisplayName.Split(" ")[0];

                if (!role_permissions.Any(i => i == requested_permission))
                {
                    var message = "";
                    context.Result = new UnauthorizedObjectResult(message);
                    return;
                }
            }
            catch (Exception ex)
            {
                var message = "";
                context.Result = new UnauthorizedObjectResult(message);
                return;
            }

        }
    }
}