using AutoMapper;
using BusinessLogicLayer.Abstracts;
using Core.ResultType;
using DataAccessLayer.EntityFramework.Abstracts;
using DataTransferObject.ModuleRole;
using Entity;

namespace BusinessLogicLayer.Concretes
{
    public class ModuleRoleBL : IModuleRoleBL
    {
        private readonly IModuleRoleRepository _moduleRoleRepository;
        private readonly IMapper _mapper;

        public ModuleRoleBL(IModuleRoleRepository moduleRoleRepository, IMapper mapper)
        {
            _moduleRoleRepository = moduleRoleRepository;
            _mapper = mapper;
        }


        public async Task<Result<List<ModuleRoleDTO>>> GetModuleListForMenu(int roleId)
        {
            Result<List<ModuleRoleDTO>> result;
            List<ModuleRoleDTO> moduleList;
            moduleList = await _moduleRoleRepository.GetModuleListForMenu(roleId);
            if (moduleList.Count > 0)
            {
                result = new Result<List<ModuleRoleDTO>>(true, moduleList, "Modül listesi başarıyla getirildi");
            }
            else
            {
                moduleList = new List<ModuleRoleDTO>();
                result = new Result<List<ModuleRoleDTO>>(false, moduleList, "Kullanıcının yetkili olduğu modül bulunamadı");
            }
            return result;
        }

        public async Task<Result<List<ModuleRoleDTO>>> GetAuthorizedModuleList(int roleId)
        {
            Result<List<ModuleRoleDTO>> result;
            List<ModuleRoleDTO> authorizedModuleList = await _moduleRoleRepository.GetAuthorizedModules(w => w.RolId == roleId);
            if (authorizedModuleList.Count > 0)
            {
                result = new Result<List<ModuleRoleDTO>>(true, authorizedModuleList, "Modül listesi başarıyla getirildi");
            }
            else
            {
                result = new Result<List<ModuleRoleDTO>>(false, "Kullanıcının yetkili olduğu modül bulunamadı");
            }
            return result;
        }

        public void DeleteRange(List<ModuleRoles> modules)
        {
             _moduleRoleRepository.DeleteRange(modules);
        }

        public void AddRange(List<ModuleRoleDTO> modules)
        {
            List<ModuleRoles> modulesList = _mapper.Map<List<ModuleRoles>>(modules);
            _moduleRoleRepository.AddRange(modulesList);
        }
    }
}
