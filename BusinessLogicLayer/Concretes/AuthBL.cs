using AutoMapper;
using BusinessLogicLayer.Abstracts;
using Core.Redis;
using Core.ResultType;
using DataAccessLayer.EntityFramework.Abstracts;
using DataTransferObject.Module;
using DataTransferObject.ModuleRole;
using DataTransferObject.Role;
using DataTransferObject.User;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using Utility.Security.Hashing;
using Utility.Security.Jwt;

namespace BusinessLogicLayer.Concretes
{
    public class AuthBL : IAuthBL
    {
        private readonly IUserRepository _userRepository;
        private readonly IModuleRoleBL _moduleRoleBL;
        private readonly IRoleBL _roleBL;
        private readonly ITokenHelper _tokenHelper;
        private readonly IRedisCacheManager _redisCacheManager;
        private readonly IMapper _mapper;

        public AuthBL(
            IUserRepository userRepository, IMapper mapper,
            IRoleBL roleBL, IModuleRoleBL moduleRoleBL,
            ITokenHelper tokenHelper, IRedisCacheManager redisCacheManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _roleBL = roleBL;
            _moduleRoleBL = moduleRoleBL;
            _tokenHelper = tokenHelper;
            _redisCacheManager = redisCacheManager;
        }

        public async Task<Result<UserModel>> Login(UserLoginDTO userLoginDTO)
        {
            Result<UserModel> result;
            UserModel userModel;
            User user = await _userRepository.GetAsync(u => u.UserName == userLoginDTO.UserName);
            if (user != null)
            {
                if (!HashingHelper.VerifyPasswordHash(userLoginDTO.Password, user.PasswordHash, user.PasswordSalt))
                {
                    result = new Result<UserModel>(false, "Hatalı şifre girdiniz lütfen tekrar deneyiniz.");
                    return result;
                }
                Result<RoleDTO> roleResult = _roleBL.GetById(user.RolId);
                if (!roleResult.IsSuccess)
                {
                    result = new Result<UserModel>(false, roleResult.Message);
                    return result;
                }
                var moduleListForMenu = await _moduleRoleBL.GetModuleListForMenu(user.RolId);
                var authorizedModuleList = await _moduleRoleBL.GetAuthorizedModuleList(user.RolId);
                if (!authorizedModuleList.IsSuccess)
                {
                    result = new Result<UserModel>(false, authorizedModuleList.Message);
                    return result;
                }
                //izinler redise yazılacak.
                string permission_str = string.Join(",", authorizedModuleList.Data.Select(s => s.ModuleDTO.Address));

                string auth_id = user.UserName + "." + user.Id;

                var claims = new { FullName = user.Name + " " + user.Surname, UserName = user.UserName, RoleName = roleResult.Data.Name, AuthID = auth_id };

                var token = _tokenHelper.CreateToken(claims);
                if (string.IsNullOrEmpty(token.Token))
                {
                    result = new Result<UserModel>(false, "Token boş değer içermektedir.");
                    return result;
                }
                TimeSpan diff = token.Expiration - DateTime.UtcNow;

                _redisCacheManager.SetValue(auth_id, permission_str, (int)diff.TotalMinutes);
                //modelin içi doldurulacak
                userModel = _mapper.Map<User, UserModel>(user);
                userModel.AuthList = moduleListForMenu.Data;
                userModel.roleDTO = roleResult.Data;
                result = new Result<UserModel>(true, userModel, "Giriş Başarılı");
                return result;
            }
            else
            {
                result = new Result<UserModel>(false, "Bu kullanıcı adı ile kayıtlı bir kullanıcı bulunamadı");
                return result;
            }
        }

        public Result<UserRegisterDTO> Register(UserRegisterDTO userRegisterDTO)
        {
            Result<UserRegisterDTO> result;
            byte[] passwordHash;
            byte[] passwordSalt;

            try
            {
                if (!PasswordCheck(userRegisterDTO.Password, userRegisterDTO.PasswordRepeat))
                {
                    result = new Result<UserRegisterDTO>(false, "Şifreler uyuşmuyor");
                    return result;
                }

                HashingHelper.CreatePasswordHash(userRegisterDTO.Password, out passwordHash, out passwordSalt);
                User user = _mapper.Map<User>(userRegisterDTO);
                //throw new Exception("Aga hataya düştük");
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                _userRepository.Add(user);

                result = new Result<UserRegisterDTO>(true, userRegisterDTO, "Kayıt başarılı");
            }
            catch (Exception ex)
            {
                result = new Result<UserRegisterDTO>(false, ex.Message);
            }
            return result;
        }

        public void ClearRedis(string redisKey)
        {
            _redisCacheManager.Clear(redisKey);
        }

        private bool PasswordCheck(string password, string passwordRepeat)
        {
            if (!password.Equals(passwordRepeat))
            {
                return false;
            }
            return true;
        }

        public string GenerateUserMenu(List<ModuleRoleDTO> modules)
        {
            string menuTxt = string.Empty;

            if (modules != null)
            {
                var firstModule = modules.FirstOrDefault();
                menuTxt += "<li class=\"menu active\">" +
                            "<a href=\"#page" + firstModule.ModuleDTO.Id + "\" data-bs-toggle=\"collapse\" aria-expanded=\"true\" class=\"dropdown-toggle\">\r\n" +
                            "<div class=\"\">\r\n" +
                            "<i class='" + firstModule.ModuleDTO.Icon + "'></i>\r\n" +
                            "<span>" + firstModule.ModuleDTO.Name + "</span>\r\n" +
                            "</div>\r\n<div>\r\n" +
                            "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"feather feather-chevron-right\">" +
                            "<polyline points=\"9 18 15 12 9 6\"></polyline></svg>\r\n</div>\r\n</a>";
                foreach (var subModule in firstModule.ModuleDTO.SubModules)
                {
                    if (subModule.ParentId == firstModule.ModuleDTO.Id && subModule.Menu == 1)
                    {
                        menuTxt += "<li>\r\n<a href=\"" + subModule.Address + "\">" + subModule.Name + "</a>\r\n</li>";
                    }
                }
                menuTxt += "</li><hr/>";
                modules.Remove(firstModule);
                foreach (var item in modules)
                {
                    if (item.ModuleDTO.ParentId == 0 && item.ModuleDTO.Menu == 1)
                    {
                        menuTxt += "<li class=\"menu\">" +
                            "<a href=\"#page" + item.ModuleDTO.Id + "\" data-bs-toggle=\"collapse\" aria-expanded=\"false\" class=\"dropdown-toggle\">\r\n" +
                            "<div class=\"\">\r\n" +
                            "<i class='" + item.ModuleDTO.Icon + "'></i>\r\n" +
                            "<span>" + item.ModuleDTO.Name + "</span>\r\n" +
                            "</div>\r\n<div>\r\n" +
                            "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"feather feather-chevron-right\">" +
                            "<polyline points=\"9 18 15 12 9 6\"></polyline></svg>\r\n</div>\r\n</a>" +


                        "<ul class=\"collapse submenu list-unstyled\" id=\"page" + item.ModuleDTO.Id + "\" data-bs-parent=\"#accordionExample\">";
                        foreach (ModuleDTO altItem in item.ModuleDTO.SubModules)
                        {
                            if (altItem.ParentId == item.ModuleDTO.Id && altItem.Menu == 1)
                            {
                                menuTxt += "<li>\r\n<a href=\"" + altItem.Address + "\">" + altItem.Name + "</a>\r\n</li>";
                            }
                        }
                        menuTxt += "</ul>" +

                            "</li><hr/>";
                    }
                }
            }
            return menuTxt;
        }
    }
}
