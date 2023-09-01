using System.Text.Json;

namespace UI.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (BadHttpRequestException ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(JsonSerializer.Serialize(new
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = ex.Message,
                    Title = ":( Bir hata meydana geldi."
                }));
                //httpContext.Response.Redirect("/Error/Error");
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsJsonAsync(JsonSerializer.Serialize(new
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = ex.Message,
                    Title = ":( Bir hata meydana geldi."
                }));
            }
        }
    }
}
