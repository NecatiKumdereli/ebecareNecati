using Core.Redis;
using Core.ResultType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MSC.Extentions;
using System;
using Utility.Security.Encryption;

namespace MSC.Extentions.Filters
{
    public class AuthorizeActionFilter : IAuthorizationFilter
    {
        private IRedisCacheManager _redisCacheManager;
        // ILogger<AuthorizeActionFilter> _logger;
        public string controller;
        public string action;
        private bool loginAccess = true;

        //private ILocService _loc;

        public AuthorizeActionFilter(IRedisCacheManager redisCacheManager, string controller = "", string action = "", bool loginAccess = true)
        {
            _redisCacheManager = redisCacheManager;
            // _logger = logger;
            this.controller = controller;
            this.action = action;
            this.loginAccess = loginAccess;
            // _loc = loc;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var querystring = context.HttpContext.Request.QueryString.ToString();
            try
            {

                this.action = context.ActionDescriptor.RouteValues["action"];
                this.controller = context.ActionDescriptor.RouteValues["controller"];
                if (this.controller != "" && this.action != "")
                {
                    if (action.ToLower() == "yonetim_paneli" || (controller == "Auth" && (action == "Login" || action == "Logout")))
                    {
                        this.loginAccess = false;
                    }
                    if (loginAccess)
                    {
                        if (context?.HttpContext.Request.Cookies.Keys == null)
                        {
                            //context.Result = new RedirectResult("/yonetim_paneli?returnURL=" + controller + "/" + action + "?" + querystring);
                            //context.HttpContext.Response.Redirect("/yonetim_paneli?returnURL=" + controller + "/" + action + "?" + querystring);
                            context.Result = new RedirectResult("/auth/login?returnURL=" + controller + "/" + action + "?" + querystring);
                            context.HttpContext.Response.Redirect("/auth/login?returnURL=" + controller + "/" + action + "?" + querystring);
                            // _logger.LogError(new UnauthorizedAccessException(""), "Giriş süresi doldu.");
                            return;
                        }
                        //Cookiden authid adında bir token tutulacak  //rol-id  şifrelenmiş hali olacak

                        var auth_id_key = context?.HttpContext.Request.Cookies["AuthId"];
                        var decauthid = EncryptionHelper.Decrypt(auth_id_key);
                        if (DateTime.Parse(decauthid.Split('-')[1]).Date < DateTime.Now.Date)
                        {
                            context.Result = new RedirectResult("/auth/login?returnURL=" + controller + "/" + action + "?" + querystring);
                            context.HttpContext.Response.Redirect("/auth/login?returnURL="  + controller + "/" + action + "?" + querystring);
                        }
                        //redisden çekilecek olan veri cookide utuluyor(UI için)
                        var user_permissions_str = _redisCacheManager.GetValue(decauthid.Split('-')[0]);
                        if (string.IsNullOrEmpty(user_permissions_str))
                        {
                            var message = "";
                            context.Result = new RedirectResult("/Auth/AccessError/");
                            context.HttpContext.Response.Redirect("/Auth/AccessError/");
                            // _logger.LogError(new UnauthorizedAccessException(""), "Write Log Message here");
                            return;
                        }
                        var role_permissions = user_permissions_str.Split(",").ToList();

                        var requested_permission = $"/{controller}/{action}";

                        if (!role_permissions.Any(i => i == requested_permission))
                        {
                            var message = "";
                            context.Result = new RedirectResult("/Auth/AccessError/");
                            context.HttpContext.Response.Redirect("/Auth/AccessError/");

                            // _logger.LogError(new UnauthorizedAccessException(""), "Write Log Message here");
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var message = "";
                //context.Result = new RedirectResult("/yonetim_paneli?returnURL=" + controller + "/" + action + "?" + querystring);
                //context.HttpContext.Response.Redirect("/yonetim_paneli?returnURL=" + controller + "/" + action + "?" + querystring);
                context.Result = new RedirectResult("/auth/login?returnURL=" + controller + "/" + action + "?" + querystring);
                context.HttpContext.Response.Redirect("/auth/login?returnURL=" + controller + "/" + action + "?" + querystring);
                //_logger.LogError(ex, "Write Log Message here");
                return;
            }

        }
    }
}