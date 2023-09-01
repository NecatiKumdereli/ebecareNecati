using Core.ResultType;
using DataTransferObject.ModuleRole;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Abstracts
{
    public interface IModuleRoleBL
    {
        Task<Result<List<ModuleRoleDTO>>> GetModuleListForMenu(int roleId);
        Task<Result<List<ModuleRoleDTO>>> GetAuthorizedModuleList(int roleId);

        void DeleteRange(List<ModuleRoles> modules);
        void AddRange(List<ModuleRoleDTO> modules);
    }
}
