using Core.ResultType;
using DataTransferObject.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Abstracts
{
    public interface IUserBL
    {
        Result<UserModel> GetById(int id);
    }
}
