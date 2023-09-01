using BusinessLogicLayer.Abstracts;
using Core.ResultType;
using DataTransferObject.User;
using Microsoft.AspNetCore.Mvc;
using Utility.Security.Encryption;

namespace UI.Controllers
{
    public class BaseController : Controller
    {

        [NonAction]
        public int GetUserId()
        {
            string cookieValueFromContext = Request.Cookies["vid"];
            var idEncryption = EncryptionHelper.Decrypt(cookieValueFromContext);
            string myId = idEncryption.Split("-")[0];
            int id = int.Parse(myId);
            return id;
        }
    }
}
