using Core.DataAccess.Repositories;
using DataAccessLayer.EntityFramework.Abstracts;
using DataAccessLayer.EntityFramework.Context;
using DataTransferObject.Module;
using DataTransferObject.ModuleRole;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework.Concretes
{
    public class ModuleRoleRepository : EfRepositoryBase<ModuleRoles, BaseDbContext>, IModuleRoleRepository
    {
        public ModuleRoleRepository(BaseDbContext context) : base(context)
        {
        }

        public async Task<List<ModuleRoleDTO>> GetModuleListForMenu(int roleId)
        {
            List<ModuleRoleDTO> parentModules = await (from permission in Context.ModuleRoles.Where(s => s.RolId == roleId)
                         join module in Context.Modules.Where(s => s.Menu == 1 && s.ParentId == 0)
                         on permission.ModuleId equals module.Id
                         select new ModuleRoleDTO
                         {
                             Id = permission.Id,
                             ModuleId = permission.ModuleId,
                             RolId = permission.RolId,
                             ModuleDTO = new ModuleDTO()
                             {
                                 Id = module.Id,
                                 Action = module.Action,
                                 Controller = module.Controller,
                                 Address = module.Address,
                                 Name = module.Name,
                                 Icon = module.Icon,
                                 Menu = module.Menu,
                                 ParentId = module.ParentId,
                             }

                         }).ToListAsync();
            foreach(var item in parentModules)
            {
                var subModuleList = (from module in Context.Modules.Where(s => s.ParentId == item.ModuleDTO.Id & s.Menu == 1)
                                                select new ModuleDTO
                                                {
                                                    Id = item.Id,
                                                    Action = module.Action,
                                                    Controller = module.Controller,
                                                    Address = module.Address,
                                                    Icon = module.Icon != null ? module.Icon : "",
                                                    Menu = module.Menu,
                                                    Name = module.Name,
                                                    ParentId = module.ParentId,
              
                                                });
                item.ModuleDTO.SubModules = await subModuleList.ToListAsync();
            }

            return parentModules;
        }
        

        public async Task<List<ModuleRoleDTO>> GetAuthorizedModules(Expression<Func<ModuleRoles, bool>> predicate = null)
        {
            List<ModuleRoleDTO> autorizedModuleList = await (from permission in Context.ModuleRoles.Where(predicate)
                                                       join module in Context.Modules on
                                                       permission.ModuleId equals module.Id
                                                       select new ModuleRoleDTO()
                                                       {
                                                           Id = permission.Id,
                                                           ModuleId = permission.ModuleId,
                                                           RolId = permission.RolId,
                                                           ModuleDTO = new ModuleDTO()
                                                           {
                                                               Id = module.Id,
                                                               Action = module.Action,
                                                               Controller = module.Controller,
                                                               Address = module.Address,
                                                               Name = module.Name,
                                                               Icon = module.Icon != null ? module.Icon : "",
                                                               ParentId = module.ParentId,
                                                               Menu = module.Menu
                                                           }
                                                       }
                                                       ).ToListAsync();
            return autorizedModuleList;
        }
    }
}
