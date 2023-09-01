using Core.DataAccess.Repositories;
using DataTransferObject.ModuleRole;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework.Abstracts
{
    public interface IModuleRoleRepository : IAsyncRepository<ModuleRoles>, IRepository<ModuleRoles>
    {
        Task<List<ModuleRoleDTO>> GetModuleListForMenu(int roleId);
        Task<List<ModuleRoleDTO>> GetAuthorizedModules(Expression<Func<ModuleRoles, bool>> predicate = null);
    }
}
