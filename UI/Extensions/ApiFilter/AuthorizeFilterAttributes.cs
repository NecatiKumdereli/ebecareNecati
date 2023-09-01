using Microsoft.AspNetCore.Mvc;


namespace MSC.Extentions.ApiFilter
{
    public class AuthorizeFilterAttribute : TypeFilterAttribute
    {
        public AuthorizeFilterAttribute(): base(typeof(ApiAuthorizeActionFilter))
        {
            Arguments = new object[] { };
        }
    }
}