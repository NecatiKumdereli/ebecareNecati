using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UI.ApiControllers
{
    [Route("api/test")]
    [ApiController]
    public class ApiTestController : ControllerBase
    {

        [HttpGet]
        public IActionResult Deneme(string data)
        {
            return Ok(data);
        }
    }
}
