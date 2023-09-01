using BusinessLogicLayer.Abstracts;
using BusinessLogicLayer.FluentValidationRules.User;
using DataTransferObject.User;
using Microsoft.AspNetCore.Mvc;
using UI.Extensions;
using UI.Filters;
using Utility;
using Utility.Security.Encryption;

namespace UI.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthBL _authBL;
        private readonly IWebHostEnvironment _environment;
        public AuthController(IAuthBL authBL, IWebHostEnvironment environment)
        {
            _authBL = authBL;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        //[Route("yonetim_paneli")]
        public IActionResult Login()
        {
            //throw new Exception("Hata meyadan geldi");
            if (CheckUserLogin())
            {
                return RedirectToAction("Index", "Home");
            }
            UserLoginDTO userLoginDTO = new UserLoginDTO();
            return View(userLoginDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {

            var loginResult = await _authBL.Login(userLoginDTO);

            if (loginResult.IsSuccess)
            {
                ManageCookies(loginResult.Data);
                //string menu = _authBL.GenerateUserMenu(loginResult.Data.AuthList);
                Functions.WriteMenu(loginResult.Data.AuthList, loginResult.Data.Id, _environment);
                Functions.WriteUserInformation(loginResult.Data, loginResult.Data.roleDTO, _environment);
                //string filePath = _environment.WebRootPath + "/user/menu";
                //Functions.WriteToFile(filePath, loginResult.Data.Id + ".txt", menu);
            }
            else
            {
                ViewData["message"] = loginResult.Message;
                return View();
            }
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            
            try
            {
                bool state = false;
                string authIdFromContext = Request.Cookies["AuthId"];
                string decryptAuthId = EncryptionHelper.Decrypt(authIdFromContext);
                string redisKey = decryptAuthId.Split("-")[0];
                _authBL.ClearRedis(redisKey);
                ClearCookies();
                string user = HttpContext.Request.Cookies["vusername"];
                if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(user.Trim()) && !string.IsNullOrEmpty(HttpContext.Request.Cookies["vid"].Trim()))
                    state = true;
                if (state)
                    return RedirectToAction("Login", "Auth");
                else
                {
                    ViewData["message"] = "Çıkış işlemi yapılamadı";
                    return Redirect("/Home/Index");
                }
            }
            catch (Exception ex)
            {
                return Redirect("/Auth/login");
            }
        }


        [HttpGet]
        public IActionResult Register()
        {
            UserRegisterDTO registerDTO = new UserRegisterDTO();
            return View(registerDTO);
        }

        [HttpPost]
        [ValidationFilter(typeof(UserRegisterRules))]
        public IActionResult Register([FromBody] UserRegisterDTO userRegisterDTO)
        {
            //throw new Exception("Biz burdaayız");
            var res = _authBL.Register(userRegisterDTO);
            
            if (!res.IsSuccess)
            {
                return Ok(res);
            }
            return RedirectToAction("Login", "Auth");
        }


        [HttpGet]
        public IActionResult AccessError()
        {
            return View();
        }

        [NonAction]
        public void ManageCookies(UserModel userModel)
        {
            //CookieOptions userNameCookie = new CookieOptions();
            //userNameCookie.IsEssential = true;
            //userNameCookie.Expires = DateTime.Now.AddDays(1);
            //Response.Cookies.Append("vusername", userModel.Username, userNameCookie);

            Functions.CookieCreator("vusername", userModel.Username, 24, Response);
            string authId = userModel.Username + "." + userModel.Id + "-" + DateTime.Now.ToString("d");
            string encrypted_auth_id = EncryptionHelper.Encrypt(authId);
            Functions.CookieCreator("AuthId", encrypted_auth_id, 24, Response);
            int adminId = userModel.Id;
            var cryption = adminId.ToString() + "-" + DateTime.Now.ToString("d");
            var idEncryption = EncryptionHelper.Encrypt(cryption);
            Functions.CookieCreator("vid", idEncryption, 24, Response);

            //CookieOptions authIdCookie = new CookieOptions();
            //authIdCookie.IsEssential = true;
            //authIdCookie.Expires = DateTime.Now.AddDays(1);
            
            //Response.Cookies.Append("AuthId", encrypted_auth_id, authIdCookie);


            //CookieOptions vid = new CookieOptions();
            //vid.IsEssential = true;
            //vid.Expires = DateTime.Now.AddDays(1);
            //int adminId = userModel.Id;
            //var cryption = adminId.ToString() + "-" + DateTime.Now.ToString("d");
            //var idEncryption = EncryptionHelper.Encrypt(cryption);
            //Response.Cookies.Append("vid", idEncryption, vid);
        }

        [NonAction]
        public void ClearCookies()
        {
            //CookieOptions usernameCookie = new CookieOptions();
            //usernameCookie.IsEssential = true;
            //usernameCookie.Expires = DateTime.Now.AddHours(-1);
            //Response.Cookies.Append("vusername", "", usernameCookie);
            Functions.CookieCreator("vusername", "", -24, Response);
            Functions.CookieCreator("vid", "", -24, Response);
            Functions.CookieCreator("AuthId", "", -24, Response);

            //CookieOptions vid = new CookieOptions();
            //vid.IsEssential = true;
            //vid.Expires = DateTime.Now.AddHours(-1);
            //Response.Cookies.Append("vid", "", vid);

            //CookieOptions authid = new CookieOptions();
            //authid.IsEssential = true;
            //authid.Expires = DateTime.Now.AddDays(-1);
            //Response.Cookies.Append("AuthId", "", authid);
        }

        [NonAction]
        public bool CheckUserLogin()
        {
            string authIdFromContext = Request.Cookies["AuthId"];
            if (authIdFromContext != null)
            {
                return true;
            }
            return false;
        }
    }
}
