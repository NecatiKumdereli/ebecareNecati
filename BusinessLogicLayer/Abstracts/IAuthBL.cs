using Core.ResultType;
using DataTransferObject.ModuleRole;
using DataTransferObject.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Abstracts
{
    public interface IAuthBL
    {
        Task<Result<UserModel>> Login(UserLoginDTO userLoginDTO);

        Result<UserRegisterDTO> Register(UserRegisterDTO userRegisterDTO);

        void ClearRedis(string redisKey);

        string GenerateUserMenu(List<ModuleRoleDTO> modules);
    }
}
