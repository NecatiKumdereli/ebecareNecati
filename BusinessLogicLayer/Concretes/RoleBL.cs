using AutoMapper;
using BusinessLogicLayer.Abstracts;
using Core.ResultType;
using DataAccessLayer.EntityFramework.Abstracts;
using DataTransferObject.ModuleRole;
using DataTransferObject.Role;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Concretes
{
    public class RoleBL : IRoleBL
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IModuleRoleBL _moduleRoleBL;
        private readonly IMapper _mapper;

        public RoleBL(IRoleRepository roleRepository, IMapper mapper, IModuleRoleBL moduleRoleBL)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _moduleRoleBL = moduleRoleBL;
        }

        public Result<RoleDTO> Add(RoleDTO model)
        {
            Result<RoleDTO> result;
            try
            {
                Role role =  _mapper.Map<Role>(model);
                _roleRepository.Add(role);
                result = new Result<RoleDTO>(true, model, "Kayıt başarılı");
            }
            catch(Exception ex)
            {
                result = new Result<RoleDTO>(false, model, ex.Message); 
            }
            return result;
        }

        public Result<RoleDTO> Delete(int id)
        {
            Result<RoleDTO> result;
            Role role = _roleRepository.Get(w => w.Id == id);
            var authList = _moduleRoleBL.GetAuthorizedModuleList(id).GetAwaiter().GetResult();
            List<ModuleRoles> moduleRoles = _mapper.Map<List<ModuleRoleDTO>, List<ModuleRoles>>(authList.Data);
            _moduleRoleBL.DeleteRange(moduleRoles);
            _roleRepository.Delete(role);
            RoleDTO model = _mapper.Map<RoleDTO>(role);
            result = new Result<RoleDTO>(true, model, "İşlem başarılı");
            return result;
        }

        public Result<List<RoleDTO>> GetAll()
        {
            Result<List<RoleDTO>> result;
            List<RoleDTO> roleList = new List<RoleDTO>();
            List<Role> roles = _roleRepository.GetAsList().ToList();
            if(roles != null && roles.Count <= 0)
            {
                result = new Result<List<RoleDTO>>(false, "Sistemde kayıtlı rol bulunamadı");
                return result;
            }
            roleList = _mapper.Map<List<RoleDTO>>(roles);
            result = new Result<List<RoleDTO>>(true, roleList, "İşlem başarılı");
            return result;
        }

        public Result<RoleDTO> GetById(int id)
        {
            Result<RoleDTO> result;

            try
            {
                Role role = _roleRepository.Get(r => r.Id == id);
                if(role == null)
                {
                    result = new Result<RoleDTO>(false, "Rol bulunamadı");
                }
                RoleDTO roleDTO = _mapper.Map<RoleDTO>(role);

                result = new Result<RoleDTO>(true, roleDTO, "İşlem başarılı");
            }
            catch(Exception ex)
            {
                result = new Result<RoleDTO>(false, ex.Message);
            }
            return result;
        }

        public Result<RoleDTO> Update(RoleDTO model)
        {
            Result<RoleDTO> result;

            Role role = _mapper.Map<Role>(model);

            _roleRepository.Update(role);

            result = new Result<RoleDTO>(true, model, "İşlem başarılı");
            return result;
        }

        public void AuthorizeRole(int[] moduleIdList, int roleId, int userId)
        {
            var authModuleList = _moduleRoleBL.GetAuthorizedModuleList(roleId).GetAwaiter().GetResult();
            List<ModuleRoles> deletedList = _mapper.Map<List<ModuleRoles>>(authModuleList.Data);
            _moduleRoleBL.DeleteRange(deletedList);
            List<ModuleRoleDTO> addedModuleList = new List<ModuleRoleDTO>();
            foreach (int moduleid in moduleIdList)
            {
                ModuleRoleDTO moduleRoleDTO = new ModuleRoleDTO();
                moduleRoleDTO.ModuleId = moduleid;
                moduleRoleDTO.RolId = roleId;
                moduleRoleDTO.UserId = userId;
                addedModuleList.Add(moduleRoleDTO);
            }
            _moduleRoleBL.AddRange(addedModuleList);
        }
    }
}
