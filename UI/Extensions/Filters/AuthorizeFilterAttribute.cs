using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MSC.Extentions.Filters
{
    public class AuthorizeFilterAttribute : TypeFilterAttribute
    {
        public AuthorizeFilterAttribute(): base(typeof(AuthorizeActionFilter))
        {
            Arguments = new object[] { };
        }
    }
}