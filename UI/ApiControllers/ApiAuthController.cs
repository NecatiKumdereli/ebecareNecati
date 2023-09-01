using BusinessLogicLayer.Abstracts;
using DataTransferObject.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UI.ApiControllers
{
    [Route("api/auth")]
    [ApiController]
    public class ApiAuthController : ControllerBase
    {
        private readonly IAuthBL _authBL;

        public ApiAuthController(IAuthBL authBL)
        {
            _authBL = authBL;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO loginDTO)
        {
            var res = await _authBL.Login(loginDTO);
            return Ok(res);
        }
    }
}
