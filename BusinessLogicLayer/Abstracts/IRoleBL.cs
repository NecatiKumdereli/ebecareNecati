using Core.ResultType;
using DataTransferObject.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Abstracts
{
    public interface IRoleBL
    {
        //Doldurulacak

        Result<RoleDTO> Add(RoleDTO model);
        Result<RoleDTO> Update(RoleDTO model);
        Result<RoleDTO> Delete(int id);
        Result<RoleDTO> GetById(int id);
        Result<List<RoleDTO>> GetAll();
        void AuthorizeRole(int[] moduleIdList, int roleId, int userId);
    }
}
