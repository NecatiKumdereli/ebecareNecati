using AutoMapper;
using BusinessLogicLayer.Abstracts;
using Core.ResultType;
using DataAccessLayer.EntityFramework.Abstracts;
using DataTransferObject.User;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Concretes
{
    public class UserBL : IUserBL
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserBL(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public Result<UserModel> GetById(int id)
        {
            Result<UserModel> result;

            User user = _userRepository.GetAsync(w => w.Id == id).GetAwaiter().GetResult();
            if (user == null)
            {
                result = new Result<UserModel>(false, "Kullanıcı bulunamadı");
                return result;
            }
            UserModel model = _mapper.Map<UserModel>(user);
            result = new Result<UserModel>(true, model, "İşlem başarılı");
            return result;
        }
    }
}
